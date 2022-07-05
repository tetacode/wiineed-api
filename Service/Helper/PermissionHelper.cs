using Core.Service.Exception;
using Data.Entity;
using Data.StaticRepository;

namespace Service.Helper;

public class PermissionHelper
{
    public static bool HasPermission(User user, PermissionEnum permissionId)
    {
        if (!user.PermissionIdList.Contains((int)permissionId) 
            && user.Roles.Select(x => x.PermissionIdList).Any(x => x.Contains((int) permissionId)))
        {
            return false;
        }
    
        return true;
    }
    
    public static bool HasPermission(User user, List<int> permissions)
    {
        if (!user.PermissionIdList.Any(x => permissions.Contains(x)) 
            && !user.Roles.Select(x => x.PermissionIdList).Any(x => x.Any(y => permissions.Contains(y))))
        {
            return false;
        }
    
        return true;
    }
    
    public static void ThrowHasNotPermission(User user, PermissionEnum permissionId)
    {
        if (!user.PermissionIdList.Contains((int)permissionId) 
            && user.Roles.Select(x => x.PermissionIdList).Any(x => x.Contains((int) permissionId)))
        {
            return;
        }
        
        throw new ServiceNotAllowedException("Not Allowed", null);
    }
    
    public static void ThrowHasNotPermission(User user, List<int> permissions)
    {
        if (!user.PermissionIdList.Any(x => permissions.Contains(x)) 
            && !user.Roles.Select(x => x.PermissionIdList).Any(x => x.Any(y => permissions.Contains(y))))
        {
            throw new ServiceNotAllowedException("Not Allowed", null);
        }
    }
}