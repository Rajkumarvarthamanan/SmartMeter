namespace SmartMeterAPI.Helpers
{
    public class SaveFile : ISaveFile
    {
        public string SaveFileToTempLLocation(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "")
                           + Path.GetExtension(file.FileName);
            var path = Path.Combine(Path.GetTempPath(), fileName);
            using (FileStream stream = File.Create(path))
            {
                file.CopyToAsync(stream);
            }
            return path;
        }
    }
}
