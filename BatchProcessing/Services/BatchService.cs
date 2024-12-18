using AutoMapper;
using BatchProcessing.Models;
using Hangfire;
using IPDetailsCache.Services;
using System.Collections.Concurrent;

namespace BatchProcessing.Services
{
    public class BatchService : IBatchService
    {
        private const int _chunkSize = 10;

        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        private ConcurrentDictionary<Guid, BatchRecord> _batches = new();

        public BatchService(ICacheService cacheService,
            IMapper mapper)
        {
            _cacheService = cacheService;
            _mapper = mapper;
        }

        public Guid StartBatchProcessing(BatchRequest batchRequest)
        {
            ArgumentNullException.ThrowIfNull(batchRequest);

            var batchId = new Guid("8f85278c-c0e4-484a-be09-049b4d96a945"); //Guid.NewGuid();

            var chunks = batchRequest.IpAddresses
                   .Where(ip => _cacheService.GetIpDetails(ip) == null)
                   .Select((ip, index) => new { ip, Group = index / _chunkSize })
                   .GroupBy(x => x.Group)
                   .Select(group => group.Select(x => x.ip).ToList())
                   .ToList();

            var jobIds = new List<string>();
            foreach (var chunk in chunks)
            {
                var jobId = BackgroundJob.Enqueue<IJobProcessor>(a => a.GetData(chunk));
                jobIds.Add(jobId);
            }

            var record = new BatchRecord
            {
                BatchId = batchId,
                IpAddresses = batchRequest.IpAddresses,
                JobIds = jobIds
            };
            _batches[batchId] = record;
            return batchId;
        }

        public BatchRecord? GetBatch(Guid batchId)
        {
            if (_batches.TryGetValue(batchId, out var record))
            {
                GetBatchStatus(record);
                GetBatchData(record);
                return record;
            }

            return null;
        }

        private void GetBatchStatus(BatchRecord record)
        {
            var monitoringApi = JobStorage.Current.GetMonitoringApi();
            var jobStatuses = record.JobIds
                .Select(jobId => monitoringApi.JobDetails(jobId))
                .Select(jobData => jobData?.History.LastOrDefault()?.StateName ?? "Unknown")
                .ToList();

            if (jobStatuses.Contains("Failed"))
            {
                record.Status = "Failed";
            }
            else if (jobStatuses.All(status => status == "Succeeded"))
            {
                record.Status = "Completed";
            }
            else if (jobStatuses.Any(status => status == "Processing" || status == "Enqueued"))
            {
                record.Status = "In Progress";
            }
        }

        private void GetBatchData(BatchRecord record)
        {
            record.ProcessedIpDetails.Clear();
            record.ProcessedIpDetails.AddRange(
                record.IpAddresses
                    .Select(ip => _cacheService.GetIpDetails(ip))
                    .Where(ipDetails => ipDetails != null)
                    .Select(ipDetails => _mapper.Map<IpDetails>(ipDetails))
            );
        }
    }
}
