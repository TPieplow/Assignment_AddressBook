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

    private void OnContactAdded(object sender, ContactAddedEventArgs e)
    {
        if (ContactList is not null)
        {
            ContactList.Add(e.AddedContact);
            OnPropertyChanged(nameof(ContactList));
        }
    }

    public void UpdateContactList(IEnumerable<IContact> contacts)
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

    [RelayCommand]
    public async Task NavigateToUpdate(string email)
    {
        await _updateContact.ContactToUpdate(email);
        UpdateContactList(_contactService.GetContacts());

        await Shell.Current.GoToAsync("UpdateContactPage");
    }

    [RelayCommand]
    public async Task RemoveContact(string email)
    {
         _contactService.RemoveContact(email);
        await Shell.Current.DisplayAlert("Removed", "Contact successfully removed.", "OK");
        UpdateContactList(_contactService.GetContacts());
    }
}
