using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TUser
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TUser()
        {
        }

        #region Properties :

        [Telerik.OpenAccess.FieldAlias("userID")]
        public int UserID
        {
            get { return userID; }
            set { this.userID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("email")]
        public string Email
        {
            get { return email; }
            set { this.email = value; }
        }

        [Telerik.OpenAccess.FieldAlias("firstName")]
        public string FirstName
        {
            get { return firstName; }
            set { this.firstName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("isLocked")]
        public bool IsLocked
        {
            get { return isLocked; }
            set { this.isLocked = value; }
        }

        [Telerik.OpenAccess.FieldAlias("isSuperUser")]
        public bool IsSuperUser
        {
            get { return isSuperUser; }
            set { this.isSuperUser = value; }
        }

        [Telerik.OpenAccess.FieldAlias("lastName")]
        public string LastName
        {
            get { return lastName; }
            set { this.lastName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalID")]
        public int? PortalID
        {
            get { return portalID; }
            set { this.portalID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("userName")]
        public string UserName
        {
            get { return userName; }
            set { this.userName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("userPass")]
        public string UserPass
        {
            get { return userPass; }
            set { this.userPass = value; }
        }

        [Telerik.OpenAccess.FieldAlias("userStyle")]
        public string UserStyle
        {
            get { return userStyle; }
            set { this.userStyle = value; }
        }

        #endregion

        #region Method

        public static bool Update(int userID, string email, string firstName, bool isLocked, bool isSuperUser,
                                  string lastName, int portalID, string userName, string userStyle)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.UserID = userID;
                    o.Email = email;
                    o.FirstName = firstName;
                    o.IsLocked = isLocked;
                    o.IsSuperUser = isSuperUser;
                    o.LastName = lastName;
                    o.PortalID = portalID;
                    o.UserName = userName;
                    //o.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(userPass, "MD5");
                    o.UserStyle = userStyle;
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static bool Delete(int userID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userID);
                scope.Transaction.Begin();
                scope.Remove(o);
                scope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static int Add(string email, string firstName, bool isLocked, bool isSuperUser,
                                  string lastName, int portalID, string userName, string userPass, string userStyle)
        {
            IObjectScope scopeProvider = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scopeProvider.Transaction.Begin();
                var user = new TUser
                {
                    Email = email,
                    FirstName = firstName,
                    IsSuperUser = isSuperUser,
                    LastName = lastName,
                    UserName = userName,
                    UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(userPass, "MD5"),
                    IsLocked = isLocked,
                    PortalID = portalID,
                    UserStyle = userStyle
                };
                scopeProvider.Add(user);
                scopeProvider.Transaction.Commit();

                return int.Parse(scopeProvider.GetObjectId(user).ToString());
            }
            catch (Exception)
            {
                if (scopeProvider.Transaction.IsActive)
                    scopeProvider.Transaction.Rollback();
                return 0;
            }
        }

        public static IEnumerable<TUser> GetAll(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().Where(o => o.PortalID == portalId).ToList();
        }

        public static IEnumerable<TUser> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().ToList();
        }

        public static TUser GetSingleByID(int id, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().SingleOrDefault(o => o.UserID == id && o.portalID == portalId);
        }

        public static TUser GetSingleByID(int userId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().SingleOrDefault(o => o.UserID == userId);
        }

        /////////////////

        public static TUser GetSingleByEmail(string email, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().SingleOrDefault(o => o.PortalID == portalId && o.Email == email);
        }

        public static bool UpdateLockUser(int userId, bool isLocked)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.IsLocked = isLocked;
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static bool UpdateUserPassword(int userId, string userPass)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(userPass, "MD5");
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static IEnumerable<TUser> GetAllUserInRole(int roleId, int portalId)
        {
            return
                TUserRole.GetAll().Where(o => o.RoleID == roleId).Select(trole => GetSingleByID(trole.UserID)).ToList().
                    Where(ob => ob.PortalID == portalId);
        }

        public static List<TUser> GetobjUsers(int objectId, int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return
                scope.Extent<TObjUser>().Where(o => o.ObjID == objectId && o.AuthID == authId).Select(
                    objUser => GetSingleByID(objUser.UserID)).ToList();
        }

        public static bool Update(int id, string email, string firstName, string lastName, bool isSuperUser)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == id);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.Email = email;
                    o.FirstName = firstName;
                    o.LastName = lastName;
                    o.IsSuperUser = isSuperUser;
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static bool UpdateUserStyle(int userId, string userSyle)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.UserStyle = userSyle;
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static bool UpdateUserName(int userId, string userName)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TUser>().Single(emp => emp.UserID == userId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.UserName = userName;
                    scope.Transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
        }

        public static int Add(string userName, string firstName, string lastName, string email, string password, int portalId, string userStyle, bool isSuperUser)
        {
            IObjectScope scopeProvider = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scopeProvider.Transaction.Begin();
                var user = new TUser
                {
                    Email = email,
                    FirstName = firstName,
                    IsSuperUser = isSuperUser,
                    LastName = lastName,
                    UserName = userName,
                    UserPass = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5"),
                    IsLocked = true,
                    PortalID = portalId,
                    UserStyle = userStyle
                };
                scopeProvider.Add(user);
                scopeProvider.Transaction.Commit();

                return int.Parse(scopeProvider.GetObjectId(user).ToString());
            }
            catch (Exception)
            {
                if (scopeProvider.Transaction.IsActive)
                    scopeProvider.Transaction.Rollback();
                return 0;
            }
        }

        public static int Login(string userName, string password, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var user = scope.Extent<TUser>().SingleOrDefault(o => o.PortalID == portalId && o.UserName == userName);
            if (user != null)
            {
                if (user.userPass != password)
                {
                    return -3;
                }
                if (user.userPass == "-1")
                {
                    return -1;
                }
                if (user.isLocked)
                {
                    return -2;
                }
                return user.UserID;
            }
            return 0;
        }

        public static TUser GetSingleByUserName(string userName, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TUser>().SingleOrDefault(o => o.PortalID == portalId && o.UserName == userName);
        }

        #endregion

    }
}
