
namespace ConsoleFileManager
{
    public class FileProperties
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void GetFileProperties(string filePath)
        {
            // Fetch and store the file properties.
            var fileInfo = new FileInfo(filePath);
            FileName = fileInfo.Name;
            FilePath = fileInfo.FullName;
            FileSize = fileInfo.Length;
            CreatedDate = fileInfo.CreationTime;
            ModifiedDate = fileInfo.LastWriteTime;

            // Log the information regarding the file to the user.
            Console.WriteLine($"File: {FileName}, Size: {FileSize}, Created: {CreatedDate}, Modified: {ModifiedDate}");

        }
    }
}