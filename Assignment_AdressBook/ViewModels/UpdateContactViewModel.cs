using Assignment_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class UpdateContactViewModel : ObservableObject, IUpdateContactViewModel
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

    [RelayCommand]
    public void SaveAndUpdate(IContact contact)
    {
        try
        {
            bool emailExist = _contactService!.EmailExists(contact.Email);
            if (!emailExist)
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
            }
            else
            {
                MessageBox.Show("Contact with the same email already exists.");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: '{ex.Message}' ");
        }
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
