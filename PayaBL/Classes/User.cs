using System;
using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class User:

    /// <summary>
    /// مشخصات کاربران در این کلاس قرار دارد 
    /// </summary>
    public class PortalUser
    {
        #region Fields :
        #endregion

        #region Properties :

        #region Base:

        public int UserID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public bool IsLocked { get; set; }

        public bool IsSuperUser { get; set; }

        public string LastName { get; set; }

        public int PortalID { get; set; }

        public string UserName { get; set; }

        public string UserPass { get; set; }

        public string UserStyle { get; set; }

        #endregion

        #region Related :

        public IList<TPortal> UserInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctors :

        public PortalUser()
        {
        }

        public PortalUser(int userID, string email, string firstName, bool isLocked, bool isSuperUser, string lastName,
                    int portalID, string userName, string userPass, string userStyle)
        {
            UserID = userID;
            Email = email;
            FirstName = firstName;
            IsLocked = isLocked;
            IsSuperUser = isSuperUser;
            LastName = lastName;
            PortalID = portalID;
            UserName = userName;
            UserPass = userPass;
            UserStyle = userStyle;
        }

        #endregion

        #region Methods :

        #region Instance Methods :

        public int Add()
        {
            return Add(Email, FirstName, IsLocked, IsSuperUser, LastName, PortalID, UserName, UserPass, UserStyle);
        }

        public bool Update()
        {
            return Update(UserID, Email, FirstName, IsLocked, IsSuperUser, LastName, PortalID, UserName,
                               UserStyle);
        }

        public bool UpdateUserPassword()
        {
            return TUser.UpdateUserPassword(UserID, UserPass);
        }

        public bool Delete()
        {
            return Delete(UserID);
        }

        #endregion

        #region Static Methods :

        public static bool Update(int userID, string email, string firstName, bool isLocked, bool isSuperUser,
                                  string lastName, int portalID, string userName, string userStyle)
        {
            return TUser.Update(userID, email, firstName, isLocked, isSuperUser,
                                lastName, portalID, userName, userStyle);
        }

        public static bool Delete(int userID)
        {
            return TUser.Delete(userID);
        }


        public static int Add(string email, string firstName, bool isLocked, bool isSuperUser,
                                  string lastName, int portalID, string userName, string userPass, string userStyle)
        {
            return TUser.Add(email, firstName, isLocked, isSuperUser,
                             lastName, portalID, userName, userPass, userStyle);
        }

        public static int Add(string userName, string firstName, string lastName, string email, string password,
                              int portalId, string userStyle, bool isSuperUser)
        {
            return TUser.Add(userName, firstName, lastName, email, password,
                             portalId, userStyle, isSuperUser);
        }

        public static List<PortalUser> GetAll(int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TUser.GetAll(portalId));
        }

        public static List<PortalUser> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TUser.GetAll());
        }

        public static PortalUser GetSingleByID(int id, int portalId)
        {
            return GetObjectFromDbObject(TUser.GetSingleByID(id, portalId));
        }

        public static PortalUser GetSingleByID(int userId)
        {
            return GetObjectFromDbObject(TUser.GetSingleByID(userId));
        }

        public static PortalUser GetObjectFromDbObject(TUser user)
        {
            try
            {
                return new PortalUser(user.UserID, user.Email, user.FirstName, user.IsLocked, user.IsSuperUser, user.LastName,
                                            (int)user.PortalID, user.UserName, user.UserPass, user.UserStyle);
            }
            catch
            {

                return null;
            }

        }

        public static List<PortalUser> GetCollectionObjectFromDbCollectionObject(IEnumerable<TUser> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        /////////////

        public static PortalUser GetSingleByEmail(string email, int portalId)
        {
            return GetObjectFromDbObject(TUser.GetSingleByEmail(email, portalId));
        }

        public static bool UpdateLockUser(int userId, bool isLocked)
        {
            return TUser.UpdateLockUser(userId, isLocked);
        }

        public static bool UpdateUserPassword(int userId, string userPass)
        {
            return TUser.UpdateUserPassword(userId, userPass);
        }

        /// <summary>
        /// حالت کاربر در دیتا بیس چک می شود  
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public static PortalUser Login(string userName, string password, int portalId)
        {
            portalId = PortalSetting.SingleUserBase ? 0 : portalId;
            var user = new PortalUser();
            int i = TUser.Login(userName, password, portalId);
            switch (i)
            {
                case -3:
                    user.UserID = -3;
                    return user;

                case -2:
                    user.UserID = -2;
                    return user;

                case -1:
                    user.UserID = -1;
                    return user;
                case 0:
                    user.UserID = 0;
                    return user;

            }
            return GetSingleByID(i);

        }

        public static List<PortalUser> GetobjUsers(int ObjectId, int AuthId)
        {
            return GetCollectionObjectFromDbCollectionObject(TUser.GetobjUsers(ObjectId, AuthId));
        }

        public static bool Update(int id, string email, string firstName, string lastName, bool isSuperUser)
        {
            return TUser.Update(id, email, firstName, lastName, isSuperUser);
        }

        public static bool UpdateUserStyle(int userId, string userSyle)
        {
            return TUser.UpdateUserStyle(userId, userSyle);
        }

        public static bool UpdateUserName(int userId, string userName)
        {
            return TUser.UpdateUserName(userId, userName);
        }

        ///////////////////
        // ادامه توابع
        ////////////

        public static PortalUser GetSingleByUserName(string userName, int portalId)
        {
            return GetObjectFromDbObject(TUser.GetSingleByUserName(userName, portalId));
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class UserRole:

    /// <summary>
    /// مشخصات کاربران در این کلاس قرار دارد 
    /// </summary>
    public class UserRole
    {
        #region Fields :
        #endregion

        #region Properties :

        #region Base:

        public int UserRoleID { get; set; }

        public int RoleID { get; set; }

        public int UserID { get; set; }

        #endregion

        #region Related :

        public IList<TPortal> UserInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctors :

        public UserRole()
        {
        }

        public UserRole(int userRoleID, int roleID, int userID)
        {
            UserRoleID = userRoleID;
            RoleID = roleID;
            UserID = userID;
        }

        #endregion

        #region Methods :

        #region Instance Methods :

        public int Add()
        {
            return Add(RoleID, UserID);
        }

        public bool Update()
        {
            return Update(UserID, RoleID, UserID);
        }

        public bool Delete()
        {
            return Delete(UserID);
        }

        #endregion

        #region Static Methods :

        public static bool Update(int userRoleID, int roleID, int userID)
        {
            return TUserRole.Update(userRoleID, roleID, userID);
        }

        public static bool Delete(int userRoleID)
        {
            return TUserRole.Delete(userRoleID);
        }

        public static bool Delete(int userID, int rolID)
        {
            return TUserRole.Delete(userID, rolID);
        }

        public static int Add(int roleID, int userID)
        {
            return TUserRole.Add(roleID, userID);
        }

        public static List<UserRole> GetAll(int userRoleID)
        {
            return GetCollectionObjectFromDbCollectionObject(TUserRole.GetAll(userRoleID));
        }

        public static List<UserRole> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TUserRole.GetAll());
        }

        public static UserRole GetSingleByID(int userID)
        {
            return GetObjectFromDbObject(TUserRole.GetSingleByID(userID));
        }

        private static UserRole GetObjectFromDbObject(TUserRole userRole)
        {
            return new UserRole(userRole.UserRoleID, userRole.RoleID, userRole.UserID);
        }

        private static List<UserRole> GetCollectionObjectFromDbCollectionObject(IEnumerable<TUserRole> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        //////////////////////////////////////

        public static List<PortalUser> GetAllUserInRole(int roleId, int portalId)
        {
            return PortalUser.GetCollectionObjectFromDbCollectionObject(TUser.GetAllUserInRole(roleId, portalId));
        }

        public static List<Role> GetUserRoleByUserId(int userId)
        {
            return Role.GetCollectionObjectFromDbCollectionObject(TRole.GetUserRoleByUserId(userId));
        }

        public static string GetUserRolesToString(int userId)
        {
            return GetUserRoleByUserId(userId).Aggregate("", (current, userRole) => (current + userRole.RoleID + ";"));
        }

        public static List<PortalUser> GetAllUserInRole(int roleId, int portalId, string UserName)
        {
            return GetAllUserInRole(roleId, portalId).Where(o => o.UserName == UserName).ToList();
        }

        ///////////////////
        // ادامه توابع
        ////////////

        public static List<PortalUser> GetRoleMembers(int roleId, string userName, string fName, string lName, int othroleId, int searchType, int portalId)
        {
            return PortalUser.GetCollectionObjectFromDbCollectionObject(TRole.GetRoleMembers(roleId, userName, fName, lName, othroleId, searchType, portalId));
        }


        #endregion

        #endregion

    }

    #endregion
}
