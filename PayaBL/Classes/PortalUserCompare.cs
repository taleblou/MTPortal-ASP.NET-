using System.Collections.Generic;

namespace PayaBL.Classes
{
    public class PortalUserCompare : IEqualityComparer<PortalUser>

    {
        // Methods
        public bool Equals(PortalUser x, PortalUser y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }
            return (x.UserID == y.UserID);
        }

        public int GetHashCode(PortalUser user)
        {
            int hashUserName = user.UserName.GetHashCode();
            int hashUserId = user.UserID.GetHashCode();
            int hashPortalId = user.PortalID.GetHashCode();
            int hashFirstName = user.FirstName.GetHashCode();
            int hashLastName = user.LastName.GetHashCode();
            int hashEmail = user.Email.GetHashCode();
            int hashPassword = user.UserPass.GetHashCode();
            //int hashUserStyle = user.UserStyle.GetHashCode();
            int hashIsSuperUser = user.IsSuperUser.GetHashCode();
            int hashIsLocked = user.IsLocked.GetHashCode();
            return (((((((((hashUserId ^ hashFirstName) ^ hashLastName) ^ hashEmail) ^ hashUserName) ^ hashPortalId) ^ hashIsLocked) ^ hashIsSuperUser) ^ hashPassword) );
        }

    }
}
