using AddressBookMauiMobile.EventArguments;
using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AddressBookMauiMobile.PageModels;

public partial class GetContactsPageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<IContact>? _contactList;

    private readonly IContactService _contactService;
    private readonly UpdateContactPageModel _updateContact;
    private readonly AddContactPageModel _addContactPageModel;

    public GetContactsPageModel(IContactService contactService, UpdateContactPageModel updateContact, AddContactPageModel addContactPageModel)
    {
        _contactService = contactService;
        _updateContact = updateContact;
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());

        addContactPageModel.ContactAdded += OnContactAdded!;
        _addContactPageModel = addContactPageModel;
    }

    /// <summary>
    /// Event handler for the ContactAdded event.
    /// Adds the newly added contact to the COntactList and triggers an update notification.
    /// </summary>
    /// <param name="sender">The object to raise the event</param>
    /// <param name="e">Event argument that holds information about the added contact</param>
    private void OnContactAdded(object sender, ContactAddedEventArgs e)
    {
        ContactList?.Add(e.AddedContact);
        OnPropertyChanged(nameof(ContactList));
    }

    /// <summary>
    /// Updates the ContactList with the provided collection of contacts.
    /// Clears the existing list then adds each contact from the collection.
    /// </summary>
    /// <param name="contacts">A collection of contacts to update the ContactList</param>
    /// <returns>A Task for the operation</returns>
    public async Task UpdateContactList(IEnumerable<IContact> contacts)
    {
        try
        {
            if (ContactList is not null)
            {
                ContactList?.Clear();
                if (contacts is not null)
                {
                    foreach (var contact in contacts)
                    {
                        ContactList?.Add(contact);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred {ex.Message}", "OK");
        }
    }

    /// <summary>
    /// Navigates to UpdateContactPage for the contact with the specified email.
    /// </summary>
    /// <param name="email">Email of the contact to update</param>
    /// <returns>Null if not found, else a task representing the operation</returns>
    [RelayCommand]
    public async Task NavigateToUpdate(string email)
    {
        try
        {
            await _updateContact.ContactToUpdate(email);
            await UpdateContactList(_contactService.GetContacts());
            await Shell.Current.GoToAsync("UpdateContactPage");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred {ex.Message}", "OK");
        }

    }

    /// <summary>
    /// Removes the contact with the specified email.
    /// </summary>
    /// <param name="email">The email of the contact to remove</param>
    /// <returns>True if contact was successfully removed, it not removed: false</returns>
    [RelayCommand]
    public async Task RemoveContact(string email)
    {
        try
        {
            _contactService.RemoveContact(email);
            await Shell.Current.DisplayAlert("Removed", "Contact successfully removed.", "OK");
            await UpdateContactList(_contactService.GetContacts());
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"An error occurred {ex.Message}", "OK");
        }
    }
}
