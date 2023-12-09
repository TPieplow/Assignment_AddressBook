using ClassLibrary_AdressBook.Interfaces;
using ConsoleApp_AdressBook.ServiceConsoleApp;
using ConsoleApp_AdressBook.UtilitiesConsoleApp;

namespace ConsoleApp_AdressBook.FolderHandler;

public class DeleteFileConsoleApp
{
    private readonly IDeleteFile deleteFile;
    private readonly ShowFolder showFolder;

    public DeleteFileConsoleApp(IDeleteFile deleteFile, ShowFolder showFolder)
    {
        this.deleteFile = deleteFile;
        this.showFolder = showFolder;
    }

    public void DeleteFile()
    {
        try
        {
            Console.Clear();
            string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";

            if (!string.IsNullOrEmpty(folderPath))
            {
                showFolder.AvailableFiles(folderPath);
            }

            Console.WriteLine("### DELETE FILE ###");
            Console.Write("Choose a file to delete: ");
            string fileName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(fileName))
            {
                bool fileDeleted = deleteFile.Delete(fileName);
                if (fileDeleted)
                {
                    DisplayMessage.Message("File removed successfully.");
                }
                else
                {
                    DisplayMessage.Message("File does not exist or couldnt not be removed.");
                }
            }
            else
            {
                DisplayMessage.Message($"File: '{fileName}' does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
