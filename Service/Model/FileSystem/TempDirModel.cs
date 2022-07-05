using Core.Service;

namespace Service.Model.FileSystem;

public class TempDirModel : DirectoryModel
{
    public TempDirModel(string root) : base(root, Path.Join("temp", CurrentDateDirName()))
    {
    }

    public DirectoryModel CreateTempDir()
    {
        return new DirectoryModel(Root,Path.Join(Dir, Guid.NewGuid().ToString())).CreateDir();
    }
}