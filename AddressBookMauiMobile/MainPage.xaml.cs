using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel pageModel)
        {
            InitializeComponent();
            BindingContext = pageModel;
        }
    }

}
