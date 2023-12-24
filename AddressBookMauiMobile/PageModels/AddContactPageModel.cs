using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = ClassLibrary_AdressBook.Models.Contact;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using System.Collections.Specialized;
using AddressBookMauiMobile.EventArguments;


namespace AddressBookMauiMobile.PageModels;

public partial class AddContactPageModel : ObservableObject
{

    [ObservableProperty]
    private IContact newContact = new Contact();
    private readonly IContactService _contactService;
    [ObservableProperty]
    private ObservableCollection<IContact> _contactList;
    private readonly ContactListEventHandler _contactListEventHandler;
    public event EventHandler<ContactAddedEventArgs> ContactAdded;

    private readonly IJsonReader _reader;
    public AddContactPageModel(IContactService contactService, ObservableCollection<IContact> contactList, IJsonReader reader, ContactListEventHandler contactListEventHandler)
    {
        _contactService = contactService;
        _contactList = contactList;
        _reader = reader;
        _reader.LoadFromFile();

        //_contactListEventHandler = contactListEventHandler;
        //_contactListEventHandler.CollectionChanged += ContactList_CollectionChanged!;
    }

    //private void ContactList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    //{
    //    OnPropertyChanged(nameof(ContactList));
    //}

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
                    //_contactListEventHandler.CollectionChanged += ContactList_CollectionChanged!;
                    await Shell.Current.DisplayAlert("Added", "Contact added successfully", "OK");
                    NewContact = new Contact();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Not added", "Couldnt add contact", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to add contact", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
