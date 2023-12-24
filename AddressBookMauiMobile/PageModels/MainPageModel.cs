using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace AddressBookMauiMobile.PageModels;

public partial class MainPageModel : ObservableObject
{
    private readonly IJsonReader _reader;
    private ObservableCollection<IContact> _contactList;

    public ObservableCollection<IContact> ContactList
    {
        get => _contactList;
        set => SetProperty(ref _contactList, value);
    }

    public MainPageModel(IJsonReader reader)
    {
        _reader = reader;
        LoadContactsAtStart();
    }

    public void LoadContactsAtStart()
    {
        try
        {
            List<IContact> loadedContacts = _reader.LoadFromFile();
            ContactList = new ObservableCollection<IContact>(loadedContacts);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

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
