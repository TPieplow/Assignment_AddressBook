
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ClassLibrary_AdressBook.Services;
using ConsoleApp_AdressBook.Interfaces;
using ConsoleApp_AdressBook.ServiceConsoleApp;
using ConsoleApp_AdressBook.ServicesConsoleApp;
using ConsoleApp_AdressBook.UtilitiesConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



public class Program
{
    public static void Main()
    {
        var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
        {

            services.AddSingleton<IContactService, ContactService>();
            services.AddSingleton<IContactServiceConsoleApp, ContactServiceConsoleApp>();
            services.AddSingleton<ShowFolder>();
            services.AddSingleton<IJsonReader, JsonReader>();
            services.AddSingleton<IJsonWriter, JsonWriter>();
            services.AddSingleton<MenuConsole>();

        }).Build();

        Console.Clear();
        builder.Start();
        var menuService = builder.Services.GetRequiredService<MenuConsole>();
        menuService.Menu();
    }
}
