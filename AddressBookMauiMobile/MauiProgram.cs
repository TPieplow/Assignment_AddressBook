using AddressBookMauiMobile.EventArguments;
using AddressBookMauiMobile.PageModels;
using AddressBookMauiMobile.Pages;
using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.JsonHandling;
using ClassLibrary_AdressBook.Services;
using System.Collections.ObjectModel;
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
            builder.Services.AddSingleton<ObservableCollection<IContact>>();
            builder.Services.AddSingleton<UpdateContactPageModel>();
            builder.Services.AddSingleton<AddContactPageModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageModel>();
            builder.Services.AddSingleton<AddContactPage>();
            builder.Services.AddSingleton<GetContactsPage>();
            builder.Services.AddSingleton<UpdateContactPage>();

            builder.Services.AddScoped<GetContactsPageModel>();


            return builder.Build();
        }
    }
}
