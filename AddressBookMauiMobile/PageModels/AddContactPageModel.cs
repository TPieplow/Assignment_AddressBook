using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contact = ClassLibrary_AdressBook.Models.Contact;


namespace AddressBookMauiMobile.PageModels;

public partial class AddContactPageModel : ObservableObject
{

    [ObservableProperty]
    private IContact newContact = new Contact();
    private readonly IContactService _contactService;
    public AddContactPageModel(IContactService contactService)
    {
        _contactService = contactService;
    }

    [RelayCommand]
    private async Task AddContact()
    {
        try
        {
            if (NewContact != null)
            {
                if (!string.IsNullOrWhiteSpace(NewContact.Email))
                {
                    bool contactAdded = _contactService!.AddContact(NewContact);
                    if (contactAdded)
                    {
                        await Shell.Current.DisplayAlert("Added", "Contact added successfully", "OK");
                        NewContact = new Contact();
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Unable to add contact", "Contact already exist", "OK");
                }
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Unable to add contact", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
