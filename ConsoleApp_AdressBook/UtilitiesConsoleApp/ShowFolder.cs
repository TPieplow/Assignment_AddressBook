namespace ConsoleApp_AdressBook.UtilitiesConsoleApp;

public class ShowFolder
{
    public void AvailableFiles(string folderPath)
    {
        Console.WriteLine("### AVAILABLE FILES ###");

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
