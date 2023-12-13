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
    [ObservableProperty]
    private string[] _loadedFiles;
    [ObservableProperty]
    private string _selectedFile;
    private IJsonReader _reader;
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
        if (!string.IsNullOrEmpty(SelectedFile))
        {
            string fileName = Path.GetFileName(SelectedFile);
            _reader.LoadFromFile(fileName);
            MessageBox.Show($"File '{fileName}' successfully loaded.");
        }
        else
        {
            MessageBox.Show("File not loaded, please select a file before loading.");
        }

    }

    [RelayCommand]
    public void BackToMenu()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainViewModel>();
        mainWindow.CurrentViewModel = _serviceProvider.GetRequiredService<MenuViewModel>();
    }
}
    

