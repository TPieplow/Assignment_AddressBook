using AddressBookMauiMobile.PageModels;
using AddressBookMauiMobile.Pages;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ClassLibrary_AdressBook.Services;
using Contact = ClassLibrary_AdressBook.Models.Contact;

namespace AddressBookMauiMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IJsonReader, JsonReader>();
            builder.Services.AddSingleton<IJsonWriter, JsonWriter>();
            builder.Services.AddSingleton<IContact, Contact>();
            builder.Services.AddSingleton<IContactService, ContactService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageModel>();
            builder.Services.AddSingleton<AddContactPage>();
            builder.Services.AddSingleton<AddContactPageModel>();
            builder.Services.AddSingleton<GetContactsPage>();
            builder.Services.AddSingleton<GetContactsPageModel>();
            builder.Services.AddSingleton<UpdateContactPage>();
            builder.Services.AddSingleton<UpdateContactPageModel>();


            return builder.Build();
        }
    }
}
