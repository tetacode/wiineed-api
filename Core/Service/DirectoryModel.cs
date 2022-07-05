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

    public virtual FileModel CreateFileModel(string fileName)
    {
        return new FileModel(Dir, fileName);
    }

    public void CreateZip(FileModel file, bool deleteAfterZip = false)
    {
        ZipFile.CreateFromDirectory(FullDirPath, file.FullFilePath);
        
        if(deleteAfterZip)
            Directory.Delete(FullDirPath, deleteAfterZip);
    }

    public DirectoryModel CreateDirectoryModel(params string[] dirs)
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

    public void Delete()
    {
        Directory.Delete(FullDirPath, true);
    }
}