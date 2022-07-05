using System.IO.Compression;

namespace Core.Service;

public class DirectoryModel
{
    public string Root { get; }
    public string Dir { get; }

    public string FullDirPath => Path.Join(Root, Dir);

    public DirectoryModel(string root, string dir)
    {
        Dir = dir;
        Root = root;
    }

    public virtual FileModel CreateFile(string fileName)
    {
        return new FileModel(Root, Dir, fileName);
    }

    public FileModel CreateFileGuid(string extension)
    {
        return new FileModel(Root, Dir, $"{Guid.NewGuid().ToString()}{extension}");
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
        var model = new DirectoryModel(Root, Path.Join(Dir, dir));
        model.CreateDirectory();
        return model;
    }
    
    private void CreateDirectory()
    {
        if (!Directory.Exists(Path.Join(Root, Dir)))
        {
            Directory.CreateDirectory(Path.Join(Root, Dir));
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