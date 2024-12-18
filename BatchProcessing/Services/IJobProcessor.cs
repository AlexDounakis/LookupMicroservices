namespace BatchProcessing.Services
{
    public interface IJobProcessor
    {
        Task GetData(List<string> chunk);
    }
}
