namespace Service.Helper;

public class FileHelper
{
    public static string CreateDateDir(string baseDir)
    {
        var dateFolder = GetCurrentDateFolderName();
        var dir = Path.Join(baseDir, dateFolder);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        return dateFolder;
    }

    public static string GetCurrentDateFolderName()
    {
        return DateTime.Now.ToString("dd-MM-yyyy");
    }
}