using ClassLibrary_AdressBook.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Assignment_AdressBook.ViewModels;

public partial class LoadFileViewModel : ObservableObject
{
    private IJsonReader _reader;
    [ObservableProperty]
    private string[] _loadedFiles;
    [ObservableProperty]
    private string _selectedFile;
    private readonly IServiceProvider _serviceProvider;


    public LoadFileViewModel(IJsonReader reader, IServiceProvider serviceProvider)
    {
        _reader = reader;
        LoadedFiles = Directory.EnumerateFiles(@"C:\EC\csharp\Assignment_AdressBook\Contact_Files").ToArray();
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    public void LoadSelectedFile()
    {

            string fileName = Path.GetFileName(_selectedFile);
            _reader.LoadFromFile(fileName);
            MessageBox.Show($"File '{fileName}' successfully loaded.");

    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
    

