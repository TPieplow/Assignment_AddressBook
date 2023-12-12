using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class GetContactsViewModel : ObservableObject
{

    private IContactService _contactService;
    [ObservableProperty]
    private IEnumerable<IContact>? _contactList;

    private readonly IServiceProvider _serviceProvider;
    public GetContactsViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        ContactList = _contactService.GetContacts();
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    public void RemoveContact(string email)
    {
        if (email != null)
        {
            _contactService.RemoveContact(email, "whateverfilename");
            MessageBox.Show("Contact removed");
            BackToMenu();
        }
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
