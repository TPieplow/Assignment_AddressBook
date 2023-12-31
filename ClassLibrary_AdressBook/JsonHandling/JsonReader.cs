﻿using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.JsonHandling;

public class JsonReader : IJsonReader
{

    public List<IContact> LoadFromFile()
    {
        List<IContact>? loadedContacts = null;
        try
        {
            string fileName = "TheFile.json";
            string filePath = Path.Combine(@"C:\EC\csharp\Assignment_AdressBook\Contact_Files", fileName);

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonContent = reader.ReadToEnd();
                    loadedContacts = JsonConvert.DeserializeObject<List<IContact>>(jsonContent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                }
            }
            else
            {
                Debug.WriteLine($"File not found: {filePath}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return loadedContacts ?? new List<IContact>();
    }
}


