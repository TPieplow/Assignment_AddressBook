using Assignment_AdressBook.ViewModels;
using System.Windows;

namespace Assignment_AdressBook;


public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}