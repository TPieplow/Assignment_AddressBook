using ConsoleApp_AdressBook.ServiceConsoleApp;
using System.Text.RegularExpressions;

namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

public class Helpers
{
    private static Regex EmailRegex => new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static string GetValidInput(string prompt)
    {
        string input;
        do
        {
            Console.Write($"{prompt}: ");
            input = Console.ReadLine()!;
            if (string.IsNullOrEmpty(input))
            {
                if (prompt == "Email")
                {
                    GetValidEmail(prompt);
                }
                else
                {
                    DisplayMessage.Message("Invalid input. Please try again.");
                }
            }
        } while (string.IsNullOrEmpty(input));
        return input;
    }

    public static string GetValidEmail(string prompt)
    {
        string email;
        do
        {
            Console.Write($"{prompt}");
            email = Console.ReadLine()!;
            if (!EmailRegex.IsMatch(email))
            {
                DisplayMessage.Message("Invalid e-mail format, please try again...");
            }
        } while (!EmailRegex.IsMatch(email));
        return email;
    }
}
