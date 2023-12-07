namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

public class ShowFolder
{
    public void AvailableFiles(string folderPath)
    {
        Console.WriteLine("### LOAD A FILE ###");

        //string folderPath = @"C:\EC\csharp\Assignment_AdressBook\Contact_Files";
        string[] files = Directory.GetFiles(folderPath);

        foreach (string file in files)
        {
            if (File.Exists(file))
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
    }
}
