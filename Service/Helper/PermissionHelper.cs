namespace Service.Helper;

public class PermissionHelper
{
    // public static bool HasPermission(User user, Permissions permissionId)
    // {
    //     if (!user.PermissionIdList.Contains((int)permissionId) && !user.Role.PermissionIdList.Contains((int)permissionId))
    //     {
    //         return false;
    //     }
    //
    //     return true;
    // }
    //
    // public static bool HasPermission(User user, List<int> permissions)
    // {
    //     if (!user.PermissionIdList.Any(x => permissions.Contains(x)) && user.Role.PermissionIdList.Any(x => permissions.Contains(x)))
    //     {
    //         return false;
    //     }
    //
    //     return true;
    // }
    //
    // public static void ThrowHasNotPermission(User user, Permissions permissionId)
    // {
    //     if (user.PermissionIdList.Contains((int)permissionId) || user.Role.PermissionIdList.Contains((int)permissionId))
    //     {
    //         return;
    //     }
    //     
    //     throw new ServiceNotAllowedException("Not Allowed", null);
    // }
    //
    // public static void ThrowHasNotPermission(User user, List<Permissions> permissions)
    // {
    //     var list = permissions.Select(x => (int)x);
    //     if (!user.PermissionIdList.Any(x => list.Contains(x)) && !user.Role.PermissionIdList.Any(x => list.Contains(x)))
    //     {
    //         throw new ServiceNotAllowedException("Not Allowed", null);
    //     }
    // }
    //
    // public static void ThrowHasNotPermission()
    // {
    //     throw new ServiceNotAllowedException("Not Allowed", null);
    // }
}