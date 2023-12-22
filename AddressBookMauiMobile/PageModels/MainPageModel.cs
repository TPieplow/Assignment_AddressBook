using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AddressBookMauiMobile.PageModels;

public partial class MainPageModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToAddContact()
    {
        await Shell.Current.GoToAsync("AddContactPage");
    }
    [RelayCommand]
    private async Task NavigateToGetContacts()
    {
        await Shell.Current.GoToAsync("GetContactsPage");
    }
}
