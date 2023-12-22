using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Services;
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
    private readonly MainPageModel _mainPageModel;
    private readonly IJsonReader _reader;

    public GetContactsPageModel(IContactService contactService, UpdateContactPageModel updateContact, MainPageModel mainPageModel, IJsonReader reader)
    {
        _contactService = contactService;
        _updateContact = updateContact;
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
        _mainPageModel = mainPageModel;
        _reader = reader;
        _reader.LoadFromFile();
        LoadContactsAtStart();
    }

    private void LoadContactsAtStart()
    {
        try
        {
            ObservableCollection<IContact> loadedContacts = _mainPageModel.ContactList;
            UpdateContactList(loadedContacts);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private void UpdateContactList(IEnumerable<IContact> contacts)
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
        _updateContact.ContactToUpdate(email);
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
