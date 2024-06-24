



using System.ComponentModel.Design;
using System.Threading.Channels;

namespace ConsoleFileManager
{

    class Program
    {

        /// <summary>
        /// Generates the prompt string that is displayed every time a user uses a commmand.
        /// </summary>
        /// <param name="currentPath"></param>
        /// <param name="additionalArgs"></param>
        private static void PrintPromptText(string currentPath, params string[] additionalArgs)
        {

            // Create the base string.
            string szBaseString = currentPath + " » ";

            // Iterate through each additional argument and create a formatted string.
            if (additionalArgs.Count() > 0)
            {
                string args = "";
                for (int i = 0; i < additionalArgs.Length; i++)
                {
                    args += additionalArgs[i];
                    if (!(i == additionalArgs.Length - 1))
                        args += " | ";
                    else
                        args += " » ";
                }
                    szBaseString += args;
            }

            // Print the formatted string to the console.
            Console.Write(szBaseString);

        }

        /// <summary>
        /// Prints the message that contains generic information.
        /// </summary>
        private static void DisplayGenericInfo()
        {
            string consoleMessage =
                "\n" +
                "  ┌ File Manager ───────────────────────────────────────────────────────────────────────────────────┐\n" +
                "  │ > Avoid using folders and/or names with spaces in them as it can result in unexpected behavior. │\n" +
                "  │ > Use 'help' for more information and/or usage of command(s).                                   │\n" +
                "  └─────────────────────────────────────────────────────────────────────────────────────────────────┘\n";
            Console.WriteLine(consoleMessage);
        }

        /// <summary>
        /// Displays the help menu.
        /// </summary>
        private static void DisplayHelpMenu()
        {
            Console.WriteLine();
            Console.WriteLine("  ┌ Command information / usage ───────────────────────────────────┐");
            Console.WriteLine("  │ > help (aliases: h) Displays the current menu.                 │");
            Console.WriteLine("  │ > return (aliases: <) Returns the action to the baseline.      │");
            Console.WriteLine("  │ > clear (aliases: c) Clears the console of all previous input. │");
            Console.WriteLine("  │ > directory (aliases: dir) Manage directory related tasks.     │");
            Console.WriteLine("  │ > file (aliases: f) Manage file related tasks.                 │");
            Console.WriteLine("  └────────────────────────────────────────────────────────────────┘");
            Console.WriteLine();
        }

        /// <summary>
        /// Handles the transitions for directory related commands.
        /// </summary>
        /// <param name="currentPath"></param>
        private static void MoveToFileInput(string currentPath)
        {
            // Determine which action to take based on the first argument of the user input.
            while (true)
            {
                PrintPromptText(currentPath, "file");
                string? userInput = Console.ReadLine().ToLower();
                string[] userInputArgs = userInput.Split(' ');

                switch (userInputArgs[0])
                {
                    case "help":
                        DisplayHelpMenu();
                        break;

                    case "clear":
                        Console.Clear();
                        DisplayGenericInfo();
                        break;

                    case "return":
                        Console.WriteLine();
                        return;

                    default:
                        Console.WriteLine($" \"{userInputArgs[0]}\" is not a recognized command. Try again or use 'help' for more information.");
                        Console.WriteLine();
                        break;

                }

            }

        }

        /// <summary>
        /// Handles the transitions for directory related commands.
        /// </summary>
        /// <param name="currentPath"></param>
        private static void MoveToDirectoryInput(string currentPath)
        {
            // Determine which action to take based on the first argument of the user input.
            while (true)
            {
                PrintPromptText(currentPath, "directory");
                string? userInput = Console.ReadLine().ToLower();
                string[] userInputArgs = userInput.Split(' ');

                switch (userInputArgs[0])
                {
                    case "help":
                        DisplayHelpMenu();
                        break;

                    case "clear":
                        Console.Clear();
                        DisplayGenericInfo();
                        break;

                    case "return":
                        Console.WriteLine();
                        return;

                    default:
                        Console.WriteLine($" \"{userInputArgs[0]}\" is not a recognized command. Try again or use 'help' for more information.");
                        Console.WriteLine();
                        break;

                }

            }

        }

        static void Main(string[] args)
        {

            FileManager fileManager = new FileManager();
            DirectoryManager directoryManager = new DirectoryManager();
            Logger logger = new Logger();
            bool programState = true;

            // Set the initial directory to the user profile directory (e.g. C:/users/ben).
            string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Shows the intial generic information screen.
            DisplayGenericInfo();

            // Start the application main loop.
            while (programState)
            {
                // Generate the prompt text and store the user reply.
                PrintPromptText(currentPath);
                string? userInput = Console.ReadLine().ToLower();
                string[] userInputArgs = userInput.Split(' ');

                // Handle potential errors using try-catch.
                try
                {
                    // Determine which action to take based on the first argument of the user input.
                    switch (userInputArgs[0])
                    {
                        case "exit":
                            programState = false;
                            break;

                        case "help":
                            DisplayHelpMenu();
                            break;

                        case "clear":
                            Console.Clear();
                            DisplayGenericInfo();
                            break;

                        case "directory":
                            MoveToDirectoryInput(currentPath);
                            break;

                        case "file":
                            MoveToFileInput(currentPath);
                            break;

                        default:
                            Console.WriteLine($"Command \"{userInputArgs[0]}\" is not a recognized. Try again or use 'help' for more information.");
                            Console.WriteLine();
                            break;
                    }
                }

                // Print the error message when an error occurs.
                catch (Exception exception)
                {
                    logger.LogError(exception.Message);
                }

            }

        }

    }

}