namespace ConsoleFileManager
{
    public class FileManager
    {
        /// <summary>
        /// Lists all the files in the given directory path.
        /// </summary>
        /// <param name="directoryPath"></param>
        public void ListFiles(string directoryPath)
        {
            foreach (string file in Directory.GetFiles(directoryPath))
            {
                Console.WriteLine(" > " + file);
            }
        }

        /// <summary>
        /// Creates a new file at the given file path.
        /// </summary>
        /// <param name="filePath"></param>
        public void CreateFile(string filePath)
        {
            File.Create(filePath);
            Console.WriteLine($" > File created: {filePath}");
        }

        /// <summary>
        /// Deletes the targeted file of the file path.
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
            Console.WriteLine($" > File deleted: {filePath}");
        }

        /// <summary>
        /// Copies the targeted file of the file path to the specified directory path.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        public void CopyFile(string sourceFilePath, string destinationFilePath)
        {
            File.Copy(sourceFilePath, destinationFilePath);
            Console.WriteLine($" > File copied from {sourceFilePath} to {destinationFilePath}");
        }

        /// <summary>
        /// Moves the targeted file of the file path to the specified directory path.
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationFilePath"></param>
        public void MoveFile(string sourceFilePath, string destinationFilePath)
        {
            File.Move(sourceFilePath, destinationFilePath);
            Console.WriteLine($" > File moved from {sourceFilePath} to {destinationFilePath}");
        }

        /// <summary>
        /// Modifies the name of the targeted file of the file path to the specified directory path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newFileName"></param>
        public void RenameFile(string filePath, string newFileName)
        {
            string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);
            File.Move(filePath, newFilePath);
            Console.WriteLine($" > File renamed to {newFileName}");
        }
    }
}
