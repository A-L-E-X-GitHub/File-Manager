



using System.ComponentModel.Design;
using System.Threading.Channels;

namespace ConsoleFileManager
{

    class Program
    {

        
        public static void PrintHelp()
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

        static void Main(string[] args)
        {

            FileManager fileManager = new FileManager();
            DirectoryManager directoryManager = new DirectoryManager();
            Logger logger = new Logger();

            // Set the initial directory to the user profile directory (e.g. C:/Users/Ben).
            string currentPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string currentAction = "";

            string consoleMessage =
                "\n" +
                "  ┌ File Manager ───────────────────────────────────────────────────────────────────────────────────┐\n" +
                "  │ > Avoid using folders and/or names with spaces in them as it can result in unexpected behavior. │\n" +
                "  │ > Use 'help' for more information and/or usage of commands.                                     │\n" +
                "  │ > Use 'clear' to clear the contents of the console.                                             │\n" +
                "  └─────────────────────────────────────────────────────────────────────────────────────────────────┘\n";

            Console.WriteLine(consoleMessage);
            while (true)
            {

                Console.Write("   " + currentPath + " » " + currentAction);
                string? userInput = Console.ReadLine().ToLower();
                string[] userInputArgs = userInput.Split(' ');

                if (userInputArgs[0] == "exit") break;

                try
                {
                    switch (userInputArgs[0])
                    {
                        case "help":
                            PrintHelp();
                            break;
                        case "clear":
                            Console.Clear();
                            Console.WriteLine(consoleMessage);
                            break;
                        case "return":
                            currentAction = "";
                            break;
                        case "<":
                            currentAction = "";
                            break;
                        case "directory":
                            currentAction = "directory » ";
                            break;
                        case "file":
                            FileInput("   " + currentPath + " » " + currentAction);
                            break;
                        default:
                            Console.WriteLine($" \"{userInputArgs[0]}\" is not a recognized command. Try again or use 'help' for more information.");
                            Console.WriteLine();
                            break;
                    }
                }
                catch (Exception exception)
                {
                    logger.LogError(exception.Message);
                }

            }

        }

        public static void FileInput(string s)
        {
            while (true)
            {
                Console.Write(s + "file » ");
                string? userInput = Console.ReadLine().ToLower();
                string[] userInputArgs = userInput.Split(' ');

                switch (userInputArgs[0])
                {
                    case "move":
                        Console.WriteLine("Move file");
                        break;

                    case "return":
                        return;

                    default:
                        Console.WriteLine($" \"{userInputArgs[0]}\" is not a recognized command. Try again or use 'help' for more information.");
                        Console.WriteLine();
                        break;
                }

            }

        }

    }

}