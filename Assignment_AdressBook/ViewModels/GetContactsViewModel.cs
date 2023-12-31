﻿using Assignment_AdressBook.EventArguments;
using Assignment_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class GetContactsViewModel : ObservableObject, IGetContactsViewModel
{
    [ObservableProperty]
    private ObservableCollection<IContact>? _contactList;
    private IContactService _contactService;
    private UpdateContactViewModel _updateContactViewModel;
    public event EventHandler<ContactAddedEventArgs>? ContactAdded;

    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor for GetContacsViewModel
    /// Initializes the view model with everything it needs. 
    /// </summary>
    /// <param name="contactService">Manage the methods regarding contacts</param>
    /// <param name="serviceProvider">Service provider for dependency injection</param>
    /// <param name="updateContactViewModel">Viewmodel taking care of updating contacts</param>
    public GetContactsViewModel(IContactService contactService, IServiceProvider serviceProvider, UpdateContactViewModel updateContactViewModel)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;
        _updateContactViewModel = updateContactViewModel;
        // If available, subscribe to the ContactAdded event in the AddContactViewModel
        if (_serviceProvider.GetRequiredService<AddContactViewModel>() is AddContactViewModel addContactViewModel)
        {
            addContactViewModel.ContactAdded += AddContactViewModel_ContactAdded!;
        }
        ContactList = new ObservableCollection<IContact>(_contactService.GetContacts());
    }

    /// <summary>
    /// Handles ContactAdded event by adding the new contact to the ContactList 
    /// </summary>
    /// <param name="sender">The event sender</param>
    /// <param name="e">ContactAdded event containing the added contact</param>
    private void AddContactViewModel_ContactAdded(object sender, ContactAddedEventArgs e)
    {
        ContactList!.Add(e.AddedContact);
    }


    [RelayCommand]
    public void NavigateToEditContact(string email)
    {
        _updateContactViewModel.EditContact(email);
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<UpdateContactViewModel>();
    }

    [RelayCommand]
    public void RemoveContact(string email)
    {
        if (email != null)
        {
            _contactService.RemoveContact(email);
            ContactList?.Remove(ContactList.FirstOrDefault(c => c.Email == email)!);
            MessageBox.Show("Contact removed");
        }
    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
