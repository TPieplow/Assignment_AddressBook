using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBookMauiMobile.PageModels;

public partial class UpdateContactPageModel : ObservableObject
{
    [ObservableProperty]
    private IContact _contact;
    private readonly IContactService _contactService;

    public UpdateContactPageModel(IContactService contactService, IContact contact)
    {
        _contactService = contactService;
        _contact = contact;
    }

    [RelayCommand]
    public void ContactToUpdate(string email)
    {
        Contact = _contactService.GetContact(email);
    }

    [RelayCommand]
    private async Task SaveAndUpdate(IContact contact)
    {
        try
        {
            var resultUpdate = _contactService!.UpdateContact(contact);
            if (resultUpdate)
            {
                await Shell.Current.DisplayAlert("Updated", "Contact successfully updated.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Couldnt update", "Contact already exist", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Couldnt update contact.", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
