using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBookMauiMobile.PageModels;

public partial class UpdateContactPageModel : ObservableObject
{
    [ObservableProperty]
    private IContact? _contact;
    private readonly IContactService _contactService;
    private readonly IJsonReader _reader;

    public UpdateContactPageModel(IContactService contactService, IJsonReader reader)
    {
        _contactService = contactService;
        _reader = reader;
        _reader.LoadFromFile();
    }

    [RelayCommand]
    public async Task ContactToUpdate(string email)
    {
        try
        {
            if (email is not null)
            {
                Contact = _contactService.GetContact(email);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Couldnt update contact.", $"An error occurred: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    public async Task SaveAndUpdate()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Contact?.Email))
            {
                var resultUpdate = _contactService!.UpdateContact(Contact);
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
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Couldnt update contact.", $"An error occurred: {ex.Message}", "OK");
        }
    }
}
