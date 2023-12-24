using AddressBookMauiMobile.EventArguments;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AddressBookMauiMobile.PageModels;

public partial class GetContactsPageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<IContact>? _contactList;

    private readonly IContactService _contactService;
    private readonly UpdateContactPageModel _updateContact;
    private readonly MainPageModel _mainPageModel;
    private readonly ContactListEventHandler _contactListEventHandler;
    private readonly IJsonReader _reader;
    private readonly AddContactPageModel _addContactPageModel;

    public GetContactsPageModel(IContactService contactService, UpdateContactPageModel updateContact, MainPageModel mainPageModel, IJsonReader reader, ContactListEventHandler contactListEventHandler, AddContactPageModel addContactPageModel)
    {
        _contactService = contactService;
        _updateContact = updateContact;
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
        _mainPageModel = mainPageModel;
        _reader = reader;
        _reader.LoadFromFile();
        LoadContactsAtStart();
        UpdateContactList(_contactService.GetContacts());

        _contactListEventHandler = contactListEventHandler;
        _contactListEventHandler.CollectionChanged += ContactList_CollectionChanged!;

        addContactPageModel.ContactAdded += OnContactAdded;
        _addContactPageModel = addContactPageModel;
    }

    private void OnContactAdded(object sender, ContactAddedEventArgs e)
    {
        if (ContactList is not null)
        {
            ContactList.Add((IContact)e.AddedContact);
            OnPropertyChanged(nameof(ContactList));
        }
    }

    private void ContactList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(ContactList));
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
