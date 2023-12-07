namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IJsonWriter
    {
        void SaveToFile(string fileName, string textChoice, List<IContact> contacts);
    }
}