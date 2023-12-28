using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile.Pages;

public partial class AddContactPage : ContentPage
{
	public AddContactPage(AddContactPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}

    private void Entry_Focused(object sender, FocusEventArgs e)
    {

    }
}