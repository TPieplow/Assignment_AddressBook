using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
