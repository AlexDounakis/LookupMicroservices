namespace BatchProcessing.Models
{
    public class BatchStatusResponse
    {
        public string BatchId { get; set; }
        public string Status { get; set; }
        public IEnumerable<IpDetails?> IpDetails { get; set; }

    }
}
