
namespace ConsoleFileManager
{
    public class DirectoryProperties
    {
        public string DirectoryName { get; set; }
        public string DirectoryPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void GetDirectoryProperties(string dirPath)
        {
            var directoryInfo = new DirectoryInfo(dirPath);
            DirectoryName = directoryInfo.Name;
            DirectoryPath = directoryInfo.FullName;
            CreatedDate = directoryInfo.CreationTime;
            ModifiedDate = directoryInfo.LastWriteTime;

            Console.WriteLine($"Directory: {DirectoryName}, Created: {CreatedDate}, Modified: {ModifiedDate}");
        }
    }
}