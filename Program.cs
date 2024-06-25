using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ConsoleFileManager
{
    class Program
    {

        /// <summary>
        /// Display the full command list, along with the command usage and description.
        /// </summary>
        public static void DisplayHelpList()
        {
            Console.WriteLine();
            Console.WriteLine("┌───────────────────────────────────────────────┬───────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│ # Command usage \t\t\t\t│ # Description(s) \t\t\t\t\t\t│");
            Console.WriteLine("╞═══════════════════════════════════════════════╪═══════════════════════════════════════════════════════════════╡");
            Console.WriteLine("│ help \t\t\t\t\t\t│ Displays the help and information display. \t\t\t│");
            Console.WriteLine("├───────────────────────────────────────────────┼───────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ clear \t\t\t\t\t│ Clears the content of the console. \t\t\t\t│");
            Console.WriteLine("├───────────────────────────────────────────────┼───────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ exit \t\t\t\t\t\t│ Exits the application. \t\t\t\t\t│");
            Console.WriteLine("├───────────────────────────────────────────────┼───────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ directory <command> \t\t\t\t│ Runs one of the valid directory commands. \t\t\t│");
            Console.WriteLine("│  > set <target_path> \t\t\t\t│ Sets the target directory path.\t\t\t\t│");
            Console.WriteLine("│  > list <target_path> \t\t\t│ Lists each folder in the specified directory. \t\t│");
            Console.WriteLine("│  > create <folder_name> <target_path> \t│ Creates a new folder in the specified directory. \t\t│");
            Console.WriteLine("│  > copy <target_path> <destination_path> \t│ Copies a folder to the specified directory. \t\t\t│");
            Console.WriteLine("│  > move <target_path> <destination_path> \t│ Moves a folder to the specified directory. \t\t\t│");
            Console.WriteLine("│  > delete <folder_name> <target_path> \t│ Deletes a folder in the specified directory. \t\t\t│");
            Console.WriteLine("├───────────────────────────────────────────────┼───────────────────────────────────────────────────────────────┤");
            Console.WriteLine("│ file <command> \t\t\t\t│ Runs one of the valid file commands. \t\t\t\t│");
            Console.WriteLine("│  > list <target_path> \t\t\t│ Lists each file in the specified directory. \t\t\t│");
            Console.WriteLine("│  > create <folder_name> <target_path> \t│ Creates a new file in the specified directory. \t\t│");
            Console.WriteLine("│  > copy <target_path> <destination_path> \t│ Copies a file to the specified directory. \t\t\t│");
            Console.WriteLine("│  > move <target_path> <destination_path> \t│ Moves a file to the specified directory. \t\t\t│");
            Console.WriteLine("│  > delete <folder_name> <target_path> \t│ Deletes a file in the specified directory. \t\t\t│");
            Console.WriteLine("└───────────────────────────────────────────────┴───────────────────────────────────────────────────────────────┘");
            Console.WriteLine();

        }

        /// <summary>
        /// Ensures that the path given is a existing path and that it isn't empty or null.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private static string? GetValidDirectory(string directoryPath)
        {
            if (!string.IsNullOrEmpty(directoryPath) && Directory.Exists(directoryPath))
            {
                return directoryPath;
            }
            else
            {
                return null;
            }
        }

        static void Main()
        {

            // Explicit object declaration(s).
            FileManager fileManager = new FileManager();
            DirectoryManager directoryManager = new DirectoryManager();

            // Set the program state and default the directory to the UserProfile folder.
            bool programState = true;
            string defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToLower();
            string currentDirectory = defaultDirectory;
            char[] illegalCharacters = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

            // Loop the program until the exit command is ran, or the console is closed.
            while (programState)
            {
                // Prompt the user for input.
                Console.Write(currentDirectory + " > ");
                string userInput = Console.ReadLine().ToLower();
                string[] userArgs = userInput.Split(' ');

                // Run the switch loop in a try-catch in-casse of unexpected errors.
                try
                {

                    // Determine which action to take based on the user input.
                    switch (userArgs[0])
                    {
                        case "exit":
                            programState = false;
                            break;

                        case "help":
                            DisplayHelpList();
                            break;

                        case "clear":
                            Console.Clear();
                            break;

                        case "directory":

                            string command = userArgs[1];

                            // There has to be a much better way to do this portion of the code.
                            // Once finished and everything functions as intended, refractor and fix it.
                            if (command == "set")
                            {
                                if (userArgs.Length < 3)
                                {
                                    Console.WriteLine("> Invalid command usage, see 'help' for more information.");
                                    break;
                                }

                                // Fetch the provided directory path including whitespaces.
                                string path = userInput.Substring(14);
                                if (GetValidDirectory(path) == null)
                                {
                                    Console.WriteLine("> The specified directory could not be found.");
                                    break;
                                }

                                // Set the current path to the specified path.
                                currentDirectory = path;
                            }

                            else if (command == "list") 
                            {
                                Console.WriteLine($"\n> Listing folders in '@{currentDirectory}'");
                                directoryManager.ListDirectories(currentDirectory);
                            }

                            else if (command == "create")
                            {
                                // Ensures that there are the valid amount of values in the input.
                                if (userArgs.Length < 3)
                                {
                                    Console.WriteLine("> Invalid command usage, see 'help' for more information.");
                                    break;
                                }

                                // Ensures there are no illegal characters in the provided name.
                                if (userArgs[2].Any(c => illegalCharacters.Contains(c)))
                                {
                                    Console.WriteLine("> Illegal directory characters found. Try a different name.");
                                    break;
                                }

                                // Trim any leading or trailing whitespace from the input.
                                if (userArgs[2].EndsWith(' '))
                                {
                                    userArgs[2] = userArgs[2].Trim();
                                }

                                // Create the folder if the checks have passed.
                                directoryManager.CreateDirectory(Path.Combine(currentDirectory, userArgs[2]));

                            }

                            /* Add at a later time
                            else if (command == "copy")
                            */

                            else if (command == "move") 
                            {
                                // Check arguments count
                                // Store target path and destination path.
                                // 
                            }
                            else if (command == "delete")
                            {
                            }

                            break;

                        case "file":
                            Console.WriteLine("");
                            break;

                        default:
                            Console.WriteLine("Command not recognized. Try again or use 'help' for more information.");
                            break;

                    }
                }

                // Print a error message to the user to notify them of the error.
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}