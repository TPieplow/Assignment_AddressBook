using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class AddContactViewModel : ObservableObject
{
    [ObservableProperty]
    private IContact newContact;
    private IContactService? _contactService;
    private readonly IServiceProvider _serviceProvider;

    public AddContactViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        newContact = new Contact();
    }

    [RelayCommand]
    private void AddContact()
    {
        try
        {
            if (NewContact is not null)
            {
                if (!string.IsNullOrWhiteSpace(NewContact?.Email))
                {
                    bool contactAdded = _contactService!.AddContact(NewContact!);
                    if (contactAdded)
                    {
                        NewContact = new Contact();
                        MessageBox.Show("Contact added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Error adding contact.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid or empty e-mail.");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An unexpected error occurred: {ex.Message}");
        }
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
