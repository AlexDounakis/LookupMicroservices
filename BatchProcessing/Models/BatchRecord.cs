namespace BatchProcessing.Models
{
    public class BatchRecord
    {
        public required Guid BatchId { get; set; }
        public required string[] IpAddresses { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<string> JobIds { get; set; } = new();
        public List<IpDetails> ProcessedIpDetails { get; set; } = new();
    }
}
