using Core.Service;

namespace Service.Model.FileSystem;

public class PublicDirModel : DirectoryModel
{
    public PublicDirModel(string root) : base(root, Path.Join("public", CurrentDateDirName()))
    {
    }
}