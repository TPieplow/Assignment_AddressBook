using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile.Pages;

public partial class AddContactPage : ContentPage
{
	public AddContactPage(AddContactPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}