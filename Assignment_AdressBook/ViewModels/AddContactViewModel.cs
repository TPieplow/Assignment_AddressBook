using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    public event EventHandler<ContactAddedEventArgs> ContactAdded;

    private void OnContactAdded(IContact contact)
    {
        ContactAdded?.Invoke(this, new ContactAddedEventArgs(contact));
    }

    [ObservableProperty]
    private ObservableCollection<IContact> _contacts;
    [ObservableProperty]
    private IContact newContact;
    private IContactService? _contactService;
    private readonly IServiceProvider _serviceProvider;

    public AddContactViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        newContact = new Contact();
        Contacts = new ObservableCollection<IContact>(_contactService.GetContacts());
    }

    [RelayCommand]
    private void AddContact()
    {
        try
        {
            if (NewContact is not null)
            {
                if (!string.IsNullOrWhiteSpace(NewContact?.Email))
                {
                    bool contactAdded = _contactService!.AddContact(NewContact!);
                    if (contactAdded)
                    {
                        Contacts.Add(NewContact);
                        OnContactAdded(NewContact);
                        NewContact = new Contact();
                        MessageBox.Show("Contact added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding contact.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or empty e-mail.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
