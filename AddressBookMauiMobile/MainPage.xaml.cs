using AddressBookMauiMobile.PageModels;

namespace AddressBookMauiMobile
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageModel _pageModel;

        public MainPage(MainPageModel pageModel)
        {
            InitializeComponent();
            BindingContext = _pageModel = pageModel;
        }
    }
}
