namespace CryptoMarket.Core.CrossCuttingConcerns.Serilog.ConfigurationModels
{
    public class FileConfiguration
    {
        public string FolderPath { get; set; }

        public FileConfiguration()
        {
            FolderPath = string.Empty;
        }

        public FileConfiguration(string folderPath)
        {
            FolderPath = folderPath;
        }
    }
}
