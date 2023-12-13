using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment_AdressBook.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MenuViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    public void NavigateToCreateFile()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateFileViewModel>();
    }

    [RelayCommand]
    public void NavigateToAddContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<AddContactViewModel>();
    }

    [RelayCommand]
    public void NavigateToGetContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<GetContactsViewModel>();
    }

    [RelayCommand]
    public void NavigateToGetContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<GetContactViewModel>();
    }

    [RelayCommand]
    public void NavigateToUpdateContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateContactViewModel>();
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
