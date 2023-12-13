using Assignment_AdressBook.ViewModels;
using Assignment_AdressBook.Views;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Assignment_AdressBook;

public partial class App : Application
{
    private IHost? _host;
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IContactService, ContactService>();
                services.AddSingleton<IContact, Contact>();
                services.AddSingleton<List<IContact>>();
                services.AddSingleton<IJsonWriter, JsonWriter>();
                services.AddSingleton<IJsonReader, JsonReader>();
                services.AddSingleton<CreateFileViewModel>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MenuViewModel>();
                services.AddSingleton<AddContactView>();
                services.AddSingleton<AddContactViewModel>();
                services.AddSingleton<GetContactViewModel>();
                services.AddSingleton<GetContactsViewModel>();
                services.AddSingleton<GetContactsView>();
                services.AddSingleton<UpdateContactViewModel>();
                services.AddSingleton<UpdateContactView>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host!.Start();

        var mainWindow = _host!.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        var contactService = _host!.Services.GetRequiredService<IContactService>();
        contactService.LoadContactsAtStart();
    }
}
