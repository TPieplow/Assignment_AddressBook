using System.Diagnostics;
using ClassLibrary_AdressBook.Interfaces;

namespace ClassLibrary_AdressBook.FolderHandler;

public class DeleteFile : IDeleteFile
{
    public bool Delete(string fileName)
    {
        string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";
        string fileToDelete = Path.Combine(folderPath, fileName);
        try
        {
            if (File.Exists(fileToDelete))
            {
                File.Delete(fileToDelete);
                Debug.WriteLine($"File {fileName} deleted successfully");
                return true;
            }
            else
            {
                Debug.WriteLine($"File {fileName} does not exist");
                return false;

            }
        }
        catch (IOException ioExp)
        {
            Debug.WriteLine(ioExp.Message);
            return false;
        }
    }
}
