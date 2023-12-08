﻿using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ClassLibrary_AdressBook.JsonHandling;

public class JsonReader : IJsonReader
{

    public List<IContact> LoadFromFile(string fileName)
    {
        List<IContact> loadedContacts = new List<IContact>();

        string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";
        string filePath = Path.Combine(folderPath, fileName);

        try
        {
            Thread.Sleep(1000);

            if (File.Exists(filePath))
            {
                Console.WriteLine("File exists. Reading contents...");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonContent = reader.ReadToEnd();
                    Console.WriteLine($"JSON content: {jsonContent}");
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

        return loadedContacts;
    }
}

