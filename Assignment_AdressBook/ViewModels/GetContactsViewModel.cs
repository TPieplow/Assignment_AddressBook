using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class GetContactsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<IContact>? _contactList;
    private IContactService _contactService;
    private UpdateContactViewModel _updateContactViewModel;

    private readonly IServiceProvider _serviceProvider;
    public GetContactsViewModel(IContactService contactService, IServiceProvider serviceProvider, UpdateContactViewModel updateContactViewModel)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        _updateContactViewModel = updateContactViewModel;
        if (_serviceProvider.GetRequiredService<AddContactViewModel>() is AddContactViewModel addContactViewModel)
        {
            addContactViewModel.ContactAdded += AddContactViewModel_ContactAdded!;
        }
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
    }

    private void AddContactViewModel_ContactAdded(object sender, ContactAddedEventArgs e)
    {
        ContactList!.Add(e.AddedContact);
    }

    [RelayCommand]
    public void NavigateToEditContact(string email)
    {
        _updateContactViewModel.EditContact(email);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateContactViewModel>();
    }

    [RelayCommand]
    public void RemoveContact(string email)
    {
        if (email != null)
        {
            _contactService.RemoveContact(email);
            ContactList?.Remove(ContactList.FirstOrDefault(c => c.Email == email));
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
