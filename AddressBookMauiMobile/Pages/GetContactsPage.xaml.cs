using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile.Pages;

public partial class GetContactsPage : ContentPage
{
	public GetContactsPage(GetContactsPageModel pageModel)
	{
		InitializeComponent();
		BindingContext = pageModel;
	}
}