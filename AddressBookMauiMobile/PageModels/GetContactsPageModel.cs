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

    public GetContactsPageModel(IContactService contactService, UpdateContactPageModel updateContact, MainPageModel mainPageModel)
    {
        _contactService = contactService;
        _updateContact = updateContact;
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
        _mainPageModel = mainPageModel;
        LoadContactsAtStart();
    }

    private void LoadContactsAtStart()
    {
        try
        {
            ObservableCollection<IContact> loadedContacts = _mainPageModel.ContactList;
            ContactList = new ObservableCollection<IContact>(loadedContacts);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
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
    }
}
