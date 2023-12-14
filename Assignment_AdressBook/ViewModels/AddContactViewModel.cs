using Assignment_AdressBook.EventArguments;
using Assignment_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class AddContactViewModel : ObservableObject, IAddContactViewModel
{
    /// <summary>
    /// EventHandler signaling added contacts, triggers a UI render.
    /// </summary>
    public event EventHandler<ContactAddedEventArgs> ContactAdded;

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

    /// <summary>
    /// Invokes the ContactAdded event to signal to all subscribers that a new contact were added to the list,this triggers a render/update in the UI.
    /// </summary>
    /// <param name="contact"></param>
    private void OnContactAdded(IContact contact)
    {
        ContactAdded?.Invoke(this, new ContactAddedEventArgs(contact));
    }

    /// <summary>
    /// Saves the new contact in NewContact when calling AddContact(ClassLibrary).
    /// Using OnContactAdded to trigger the list rendering.
    /// </summary>
    [RelayCommand]
    public void AddContact()
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

    /// <summary>
    /// Navigation method that allows the user to return to the main page.
    /// Essential for managing user navigation.
    /// </summary>
    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
