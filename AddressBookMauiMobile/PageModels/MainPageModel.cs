using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBookMauiMobile.PageModels;

public partial class MainPageModel : ObservableObject
{
    [RelayCommand]
    private static async Task NavigateToAddContact()
    {
        await Shell.Current.GoToAsync("AddContactPage");
    }

    [RelayCommand]
    private static async Task NavigateToGetContacts()
    {
        await Shell.Current.GoToAsync("GetContactsPage");
    }
}
