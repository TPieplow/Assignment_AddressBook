using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile.Pages;

public partial class UpdateContactPage : ContentPage
{
	public UpdateContactPage(UpdateContactPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}