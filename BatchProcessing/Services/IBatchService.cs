using BatchProcessing.Models;

namespace BatchProcessing.Services
{
    public interface IBatchService
    {
        Guid StartBatchProcessing(BatchRequest batchRequest);
        BatchRecord? GetBatch(Guid batchId);
    }
}
