namespace ConsoleApp_AdressBook.ServiceConsoleApp;

public class DisplayMessage
{
    /// <summary>
    /// Displays a user message and waits for a key press to continue.
    /// </summary>
    /// <param name="message">The message to be displayed to the user</param>
    public static void Message(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
