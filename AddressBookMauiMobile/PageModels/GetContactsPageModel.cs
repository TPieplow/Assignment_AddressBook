using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AddressBookMauiMobile.PageModels;

public partial class GetContactsPageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<IContact>? _contactList;

    private readonly IContactService _contactService;
    private readonly UpdateContactPageModel _updateContact;

    public GetContactsPageModel(IContactService contactService, UpdateContactPageModel updateContact)
    {
        _contactService = contactService;
        _updateContact = updateContact;
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
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
