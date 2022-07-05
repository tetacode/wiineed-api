using Core.Service;

namespace Service.Model.FileSystem.User;

public class UserDirModel : DirectoryModel
{
    public UserDirModel(string root, string userId) : base(root, Path.Join("users", userId))
    {
    }
}