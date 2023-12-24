using System.Collections.Specialized;

namespace AddressBookMauiMobile.EventArguments;

public class ContactListEventHandler
{
    public event EventHandler<NotifyCollectionChangedEventArgs>? CollectionChanged;

    public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(sender, e);
    }
}
