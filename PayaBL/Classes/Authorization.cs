using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class ObjRole :

    /// <summary>
    /// می توان به هر شی از هر ماژول مجوز کاری خاص را داد 
    /// مثلا مشاهده این صفحه فقط برای کاربران شناخته شده مقدور است 
    /// </summary>
    public class ObjRole
    {
        #region Fields  :
        #endregion

        #region Properties :

        #region Base:

        int ObjRoleID { set; get; }

        public int AuthID { set; get; }

        public int ObjID { set; get; }

        public int RoleID { set; get; }

        public int ModuleID { set; get; }

        #endregion

        #region Related :

        public TAuthObjBased AuthObjBased { set; get; }

        public TModule Module1 { set; get; }

        public TRole Role { set; get; }

        #endregion

        #endregion

        #region Constrauctors :

        public ObjRole(int objRoleID, int authID, int objID, int roleID, int moduleID)
        {
            ObjRoleID = objRoleID;
            AuthID = authID;
            ObjID = objID;
            RoleID = roleID;
            ModuleID = moduleID;
        }

        #endregion

        #region Methods :

        #region Instance Methods :

        #endregion

        #region Static Methods :

        public static bool Update(int objRoleID, int authID, int objID, int roleID, int moduleID)
        {
            return TObjRole.Update(objRoleID, authID, objID, roleID, moduleID);
        }

        public static bool Delete(int objRoleID)
        {
            return TObjRole.Delete(objRoleID);
        }

        public static bool Delete(int objID, int roleId, int authId)
        {
            return TObjRole.Delete(objID, roleId, authId);
        }

        public static int Add(int authID, int objID, int roleID, int moduleID)
        {
            return TObjRole.Add(authID, objID, roleID, moduleID);
        }

        public static List<ObjRole> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TObjRole.GetAll());
        }

        public static ObjRole GetSingleByID(int objRoleId)
        {
            return GetObjectFromDbObject(TObjRole.GetSingleByID(objRoleId));
        }

        private static ObjRole GetObjectFromDbObject(TObjRole objRole)
        {
            return new ObjRole(objRole.ObjRoleID, objRole.AuthID, objRole.ObjID, objRole.RoleID, objRole.ModuleID);
        }

        public static List<ObjRole> GetCollectionObjectFromDbCollectionObject(IEnumerable<TObjRole> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<Role> GetobjRoles(int objId, int authId)
        {
            return Classes.Role.GetCollectionObjectFromDbCollectionObject(TRole.GetobjRoles(objId, authId));
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class ObjUser:

    /// <summary>
    /// کدام کاربر بر روی کدام شی جه دسترسی ای دارد 
    /// مثلا محمد می تواند فلان خبر را پاک کند 
    /// </summary>
    public class ObjUser
    {
        #region Fields :

        #endregion

        #region Properties :

        #region Base:

        public int ObjUserID { get; set; }

        public int AuthID { get; set; }

        public int ModuleID { get; set; }

        public int ObjID { get; set; }

        public int UserID { get; set; }

        #endregion

        #region Related :

        public TAuthObjBased AuthObjBased { get; set; }

        public TModule Module1 { get; set; }

        public TUser User { get; set; }

        #endregion

        #endregion

        #region Constrauctors :

        public ObjUser(int objUserID, int authID, int moduleID, int objID, int userID)
        {
            ObjUserID = objUserID;
            AuthID = authID;
            ModuleID = moduleID;
            ObjID = objID;
            UserID = userID;
        }

        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return ObjUser.Add(AuthID, ModuleID, ObjID, UserID);
        }

        public bool Update()
        {
            return ObjUser.Update(ObjUserID, AuthID, ModuleID, ObjID, UserID);
        }

        public bool Delete()
        {
            return ObjUser.Delete(ObjUserID);
        }

        #endregion

        #region Static Methods :

        public static bool Update(int objUserID, int authID, int moduleID, int objID, int userID)
        {
            return TObjUser.Update(objUserID, authID, moduleID, objID, userID);
        }

        public static bool Delete(int objUserID)
        {
            return TObjUser.Delete(objUserID);
        }

        public static int Add(int authID, int moduleID, int objID, int userID)
        {
            return TObjUser.Add(authID, moduleID, objID, userID);
        }

        public static List<ObjUser> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TObjUser.GetAll());
        }

        public static ObjUser GetSingleByID(int objUserID)
        {
            return GetObjectFromDbObject(TObjUser.GetSingleByID(objUserID));
        }

        private static ObjUser GetObjectFromDbObject(TObjUser objUser)
        {
            return new ObjUser(objUser.ObjUserID, objUser.AuthID, objUser.ModuleID, objUser.ObjUserID, objUser.UserID);
        }

        private static List<ObjUser> GetCollectionObjectFromDbCollectionObject(IEnumerable<TObjUser> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static bool Delete(int objId, int userId, int authId)
        {
            return TObjUser.Delete(objId, userId, authId);
        }

        public static List<PortalUser> GetobjRoles(int objId, int authID)
        {
            return PortalUser.GetCollectionObjectFromDbCollectionObject(TObjUser.GetobjRoles(objId, authID));
        }

        #endregion

        #endregion


       
    }

    #endregion
}
