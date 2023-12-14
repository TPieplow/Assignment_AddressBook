namespace ClassLibrary_AdressBook.Interfaces
{
    public interface IJsonWriter
    {
        /// <summary>
        /// Method saving the current list to system, using JsonConvert
        /// </summary>
        /// <param name="contacts">Takes a list of contacts</param>
        void SaveToFile(List<IContact> contacts);
    }
}