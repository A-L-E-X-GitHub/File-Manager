namespace ConsoleFileManager
{
    public class DirectoryManager
    {
        /// <summary>
        /// Lists each folder within the specified directory.
        /// </summary>
        /// <param name="directoryPath"></param>
        public void ListDirectories(string directoryPath)
        {
            foreach (string directory in Directory.GetDirectories(directoryPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                Console.WriteLine(" > " + directoryInfo.Name);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Creates a new folder (directory) in the specified location.
        /// </summary>
        /// <param name="directoryPath"></param>
        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($" > New directory created: {directoryPath}");
        }

        /// <summary>
        /// Deletes the specified folder (directory).
        /// </summary>
        /// <param name="directoryPath"></param>
        public void DeleteDirectory(string directoryPath)
        {
            Directory.Delete(directoryPath, true);
            Console.WriteLine($" > Directory deleted: {directoryPath}");
        }

        /// <summary>
        /// Moves a folder (directory) to another folder (directory) location.
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <param name="destinationDirectoryPath"></param>
        public void MoveDirectory(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            Directory.Move(sourceDirectoryPath, destinationDirectoryPath);
            Console.WriteLine($" > Directory moved from {sourceDirectoryPath} to {destinationDirectoryPath}");
        }

        /// <summary>
        /// Renames the specified folder (directory) to the specified name.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="newDirectoryName"></param>
        public void RenameDirectory(string directoryPath, string newDirectoryName)
        {
            string newDirectoryPath = Path.Combine(Path.GetDirectoryName(directoryPath), newDirectoryName);
            Directory.Move(directoryPath, newDirectoryPath);
            Console.WriteLine($" > Directory renamed to {newDirectoryName}");
        }

        /*
        public void CopyDirectory(string sourceDirectoryPath, string destinationDirectoryPath)
        {
            // Run the method in a recursive manner where it calls itself again for the sub folders.
        }
        */
    }
}