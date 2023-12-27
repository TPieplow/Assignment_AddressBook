using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = ClassLibrary_AdressBook.Models.Contact;
using System.Collections.ObjectModel;
using AddressBookMauiMobile.EventArguments;


namespace AddressBookMauiMobile.PageModels;

public partial class AddContactPageModel : ObservableObject
{
    [ObservableProperty]
    private IContact newContact = new Contact();
    private readonly IContactService _contactService;
    [ObservableProperty]
    private ObservableCollection<IContact> _contactList;

    public event EventHandler<ContactAddedEventArgs>? ContactAdded;

    public AddContactPageModel(IContactService contactService, ObservableCollection<IContact> contactList)
    {
        _contactService = contactService;
        _contactList = contactList;
    }

    /// <summary>
    /// Adds a new contact to the list and triggers an event (contact added)
    /// </summary>
    /// <returns>A task or false if contact couldnt be added.</returns>
    [RelayCommand]
    public async Task AddContact()
    {
        try
        {
            if (NewContact is not null)
            {
                bool contactAdded = _contactService.AddContact(NewContact);
                if (contactAdded)
                {
                    ContactList.Add(NewContact);
                    ContactAdded?.Invoke(this, new ContactAddedEventArgs(NewContact));
                    await Shell.Current.DisplayAlert("Added", "Contact added successfully", "OK");
                    NewContact = new Contact();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not added", "Contact already exists, try with another e-mail.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to add contact", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
