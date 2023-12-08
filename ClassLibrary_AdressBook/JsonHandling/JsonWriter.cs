using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.JsonHandling;

public class JsonWriter : IJsonWriter
{

    public void SaveToFile(string fileName, string textChoice, List<IContact> contacts)
    {
        try
        {
            string filePath = Path.Combine(@"C:\EC\csharp\Assignment_AdressBook\Contact_Files", fileName);

            switch (textChoice)
            {
                case "1":
                    filePath = Path.ChangeExtension(filePath, ".txt");

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (IContact contact in contacts)
                        {
                            string contactInformation = $"{contact.Id}{contact.FirstName}{contact.LastName}{contact.Email}{contact.Phone}{contact.Address}";
                            writer.WriteLine(contactInformation);
                        }
                    }
                    break;

                case "2":
                    filePath = Path.ChangeExtension(filePath, ".json");

                    string jsonData = JsonConvert.SerializeObject(contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    File.WriteAllText(filePath, jsonData);
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
