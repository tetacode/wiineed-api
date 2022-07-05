using System.IO.Compression;

namespace Core.Service;

public class DirectoryModel
{
    public string Dir { get; }

    public string FullDirPath => Dir;

    public DirectoryModel(string dir)
    {
        Dir = dir;
    }

    public virtual FileModel CreateFile(string fileName)
    {
        return new FileModel(Dir, fileName);
    }

    public FileModel CreateFileGuid(string extension)
    {
        return new FileModel(Dir, $"{Guid.NewGuid().ToString()}{extension}");
    }

    public void CreateZip(FileModel file, bool deleteAfterZip = false)
    {
        ZipFile.CreateFromDirectory(FullDirPath, file.FullFilePath);
        
        if(deleteAfterZip)
            Directory.Delete(FullDirPath, deleteAfterZip);
    }

    public DirectoryModel CreateDir(params string[] dirs)
    {
        var dir = string.Join(Path.DirectorySeparatorChar, dirs);
        var model = new DirectoryModel(Path.Join(Dir, dir));
        model.CreateDirectory();
        return model;
    }
    
    private void CreateDirectory()
    {
        if (!Directory.Exists(Dir))
        {
            Directory.CreateDirectory(Dir);
        }
    }

    public DirectoryModel CreateDateDir()
    {
        return CreateDir(CurrentDateDirName());
    }

    public static string CurrentDateDirName()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    public void Delete()
    {
        Directory.Delete(FullDirPath, true);
    }
}