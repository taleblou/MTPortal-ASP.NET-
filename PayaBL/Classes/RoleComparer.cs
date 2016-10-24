using System.Collections.Generic;

namespace PayaBL.Classes
{
    public class RoleComparer : IEqualityComparer<Role>
{
    // Methods
    public bool Equals(Role x, Role y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
        {
            return false;
        }
        return (x.RoleID  == y.RoleID);
    }

    public int GetHashCode(Role role)
    {
        int hashRoleKey = role.RoleKey.GetHashCode();
        int hashRoleId = role.RoleID .GetHashCode();
        int hashPortalId = role.PortalID==null ? 0 : role.PortalID.GetHashCode();
        return ((hashRoleKey ^ hashRoleId) ^ hashPortalId);
    }
}


 

}
