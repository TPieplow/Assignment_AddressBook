using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.JsonHandling;

public class JsonWriter : IJsonWriter
{
    public void SaveToFile(List<IContact> contacts)
    {
        try
        {
            string fileName = "TheFile";
            string filePath = Path.Combine(@"C:\EC\csharp\Assignment_AdressBook\Contact_Files", fileName);
            filePath = Path.ChangeExtension(filePath, ".json");

            string jsonData = JsonConvert.SerializeObject(contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(filePath, jsonData);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
