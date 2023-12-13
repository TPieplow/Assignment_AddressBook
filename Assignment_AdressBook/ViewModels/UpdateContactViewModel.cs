using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class UpdateContactViewModel : ObservableObject
{
    private IContactService _contactService;
    private readonly IServiceProvider _serviceProvider;
    [ObservableProperty]
    private IContact? _contact;

    public UpdateContactViewModel(IContactService contactService, IServiceProvider serviceProvider, IContact contact)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        _contact = contact;
    }


    /// <summary>
    /// Method responsible of calling the right contact to update.
    /// </summary>
    /// <param name="email">Taking email as a parameter to find the right contact to update.</param>
    [RelayCommand]
    public void EditContact(string email)
    {
        try
        {
            if (email is not null)
            {
                Contact = _contactService.GetContact(email);
            }
            else
            {
                MessageBox.Show("Contact doesnt exist, please try again.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating contact: '{ex.Message}', please try again");
        }
    }

    /// <summary>
    /// Method for calling the UpdateContact method. 
    /// </summary>
    /// <param name="contact">Takes a contact to update</param>
    [RelayCommand]
    public void SaveAndUpdate(IContact contact)
    {
        try
        {
            var resultUpdate = _contactService.UpdateContact(contact);
            if (resultUpdate)
            {
                MessageBox.Show("Contact successfully updated.");
            }
            else
            {
                MessageBox.Show("Couldnt update contact, please try again.");
            }
        } catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: '{ex.Message}' ");
        }
    }

    /// <summary>
    /// Navigation method that allows the user to return to the main page.
    /// Essential for managing user navigation.
    /// </summary>
    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
