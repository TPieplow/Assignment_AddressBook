namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IJsonReader
    {
        /// <summary>
        /// Method using StreamReader, loading a predetermined JSON-file into the application
        /// </summary>
        /// <returns>A list of all contacts</returns>
        List<IContact> LoadFromFile();
    }
}