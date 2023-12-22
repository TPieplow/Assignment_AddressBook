using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = ClassLibrary_AdressBook.Models.Contact;
using System.Collections.ObjectModel;


namespace AddressBookMauiMobile.PageModels;

public partial class AddContactPageModel : ObservableObject
{

    [ObservableProperty]
    private IContact newContact = new Contact();
    private readonly IContactService _contactService;
    private readonly ObservableCollection<IContact> _contactList;
    public AddContactPageModel(IContactService contactService, ObservableCollection<IContact> contactList)
    {
        _contactService = contactService;
        _contactList = contactList;
    }

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
                    _contactList.Add(NewContact);
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
