namespace SmartMeterAPI.ServiceLogic
{
    public interface IReadingUploadProcessor
    {
        Task<string> Upload(string path);
    }
}
