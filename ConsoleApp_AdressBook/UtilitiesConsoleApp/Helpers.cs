//using System.Text.RegularExpressions;

//namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

//public class Helpers
//{
//    private static Regex EmailRegex => new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

//    /// <summary>
//    /// Method checking if input is null or empty using a do while loop to "trap" the user if incorrect input.
//    /// If the prompt is email, call GetValidEmail
//    /// </summary>
//    /// <param name="prompt">takes a predetermined promt i.e First name, last name</param>
//    /// <returns></returns>
//    public static string GetValidInput(string prompt)
//    {
//        string input;
//        do
//        {
//            Console.Write($"{prompt}: ");
//            input = Console.ReadLine()!;
//            if (string.IsNullOrEmpty(input))
//            {
//                if (prompt == "Email")
//                {
//                    GetValidEmail(prompt);
//                }
//                else
//                {
//                    DisplayMessage.Message("Invalid input. Please try again.");
//                }
//            }
//        } while (string.IsNullOrEmpty(input));
//        return input;
//    }

//    /// <summary>
//    /// Method using regex to determain if email is valid.
//    /// </summary>
//    /// <param name="prompt">takes a predetermined promt, email</param>
//    /// <returns></returns>
//    public static string GetValidEmail(string prompt)
//    {
//        string email;
//        do
//        {
//            Console.Write($"{prompt}");
//            email = Console.ReadLine()!;
//            if (!EmailRegex.IsMatch(email))
//            {
//                DisplayMessage.Message("Invalid e-mail format, please try again...");
//            }
//        } while (!EmailRegex.IsMatch(email));
//        return email;
//    }
//}
