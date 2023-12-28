using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ClassLibrary_AdressBook.Utilities;

public class Helpers
{
    private static Regex EmailRegex => new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    private const string EmailPrompt = "Email";

        /// <summary>
        /// Method checking if input is null or empty using a do while loop to "trap" the user if incorrect input.
        /// If the prompt is email, call GetValidEmail
        /// </summary>
        /// <param name="prompt">takes a predetermined promt i.e First name, last name</param>
        /// <returns></returns>
    public static string GetValidInput(string prompt)
    {
        string input;
        do
        {
            Console.Write($"{prompt}: ");
            input = Console.ReadLine()!;
            if (string.IsNullOrEmpty(input))
            {
                if (prompt == EmailPrompt)
                {
                    return GetValidEmail(input);
                }
                else
                {
                    Debug.WriteLine("Invalid input. Please try again.");
             
                }
            }
        } while (string.IsNullOrEmpty(input));
        return CapitalizeFirstLetter(input);
    }

    /// <summary>
    /// Method using regex to determin if email is valid.
    /// </summary>
    /// <param name="prompt">takes a predetermined prompt, email</param>
    /// <returns></returns>
    public static string GetValidEmail(string prompt)
    {
        string email;
        do
        {
            Console.Write($"{prompt}");
            email = Console.ReadLine()!;
            if (!EmailRegex.IsMatch(email))
            {
                Debug.WriteLine("Invalid e-mail format, please try again...");
                return string.Empty;
            }
        } while (!EmailRegex.IsMatch(email));
        return CapitalizeFirstLetter(email);
    }

    /// <summary>
    /// Method that capitalizes the first letter on all user input
    /// </summary>
    /// <param name="input">The user input</param>
    /// <returns>Returns the user input capitalized</returns>
    public static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return char.ToUpper(input[0]) + input[1..];
    }
}

