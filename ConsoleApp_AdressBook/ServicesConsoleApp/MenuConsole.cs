using ClassLibrary_AdressBook.Interfaces;
using ConsoleApp_AdressBook.Interfaces;


namespace ConsoleApp_AdressBook.ServicesConsoleApp;

public class MenuConsole
{
    private readonly IContactServiceConsoleApp _contactsConsoleApp;

    public MenuConsole(IContactServiceConsoleApp contactsConsoleApp,
        IEnumerable<IContact> contacts,
        IJsonReader jsonReader,
        IJsonWriter jsonWriter,
        IContactService contactService
    )

    {
        _contactsConsoleApp = contactsConsoleApp;
    }

    public void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("### HANDLE CONTACT MENU ###");
            string[] menu =
            {
                "1. Add Contact",
                "2. Update Contact",
                "3. Show Specific Contact",
                "4. Show All Contacs",
                "5. Delete Contact",
                "0. Exit application"
            };

            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    _contactsConsoleApp.AddContactConsoleApp();
                    break;

                case "2":
                    _contactsConsoleApp.UpdateContact();
                    break;

                case "3":
                    _contactsConsoleApp.ShowContactDetails();
                    break;

                case "4":
                    _contactsConsoleApp.ShowAllContactDetails();
                    break;

                case "5":
                    _contactsConsoleApp.DeleteContact();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
