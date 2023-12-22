using AddressBookMauiMobile.Pages;

namespace AddressBookMauiMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddContactPage), typeof(AddContactPage));
            Routing.RegisterRoute(nameof(GetContactsPage), typeof(GetContactsPage));
            Routing.RegisterRoute(nameof(UpdateContactPage), typeof(UpdateContactPage));
        }
    }
}
