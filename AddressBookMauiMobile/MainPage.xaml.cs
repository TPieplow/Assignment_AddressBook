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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadContacts();
        }

        private void LoadContacts()
        {
            _pageModel.LoadContactsAtStart();
        }
    }
}
