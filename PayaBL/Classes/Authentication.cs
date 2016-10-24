using System;
using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class AuthObjBased:

    /// <summary>
    /// کارهایی که اگر یک نقش مجوز آن را داشته باشد می تواند بر روی یک شی از یک ماژول اعمال کند 
    /// مثلا حذف خبر
    /// </summary>
    public class AuthObjBased
    {
        #region Fields :
        #endregion

        #region Properties :

        #region Base:

        public int AuthID { get; set; }

        public string AuthKey { get; set; }

        public int ModuleDefID { get; set; }

        #endregion

        #region Related :

        public List<Role> AuthorizedRoles { get; set; }

        public List<PortalUser> AuthorizedUsers { get; set; }

        public ModuleDef ModuleDef { get; set; }

        #endregion

        #endregion

        #region Constrauctors :
        public AuthObjBased(int authID, string authKey, int moduleDefID, int objId)
        {
            AuthID = authID;
            AuthKey = authKey;
            ModuleDefID = moduleDefID;
            AuthorizedRoles = ObjRole.GetobjRoles(objId, authID);
            AuthorizedUsers = ObjUser.GetobjRoles(objId, authID);
        }

        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return Add(AuthKey, ModuleDefID);
        }

        public bool Update()
        {
            return Update(AuthID, AuthKey, ModuleDefID);
        }

        public bool Delete()
        {
            return Delete(AuthID);
        }
        #endregion

        #region Static Methods :

        public static bool Update(int authID, string authKey, int moduleDefID)
        {
            return TAuthObjBased.Update(authID, authKey, moduleDefID);
        }

        public static bool Delete(int authID)
        {
            return TAuthObjBased.Delete(authID);
        }

        public static int Add(string authKey, int moduleDefID)
        {
            return TAuthObjBased.Add(authKey, moduleDefID);
        }
       

        public static List<AuthObjBased> GetModuleAuthObjBased(int modulDefID,int objID)
        {
            return
                GetCollectionObjectFromDbCollectionObject(
                    TAuthObjBased.GetModuleAuthObjBased(modulDefID),objID);
        }

        public static string GetAuthKeyObjBased(int authId)
        {
            return TAuthObjBased.GetAuthKeyObjBased(authId);
        }



        private static AuthObjBased GetObjectFromDbObject(TAuthObjBased authObjBased,int objID)
        {
            return new AuthObjBased(authObjBased.AuthID, authObjBased.AuthKey, authObjBased.ModuleDefID, objID);
        }

        private static List<AuthObjBased> GetCollectionObjectFromDbCollectionObject(IEnumerable<TAuthObjBased> list,int objID)
        {
            return list.Select(authObjBased => GetObjectFromDbObject(authObjBased, objID)).ToList();
        }


 
        #endregion

        #endregion

    }

    #endregion

    #region Class AuthRoleBased:

    /// <summary>
    /// کارهایی که اجازه ی انجام آن ها را می توانیم به نقش هایی خاص بدهیم
    /// مثلا مدیر اجازه اضافه و حذف صفحه جدید دارد  
    /// </summary>
    public class AuthRoleBased
    {
        #region Fields :

        #endregion

        #region Properties :

        #region Base:

        public int AuthID { get; set; }

        public string AuthKey { get; set; }

        #endregion

        #region Related :

        public IList<TModuleDef> TAuthRoleModuleDef = new List<TModuleDef>();

        #endregion

        #endregion

        #region Constrauctors :

        public AuthRoleBased()
        {
        }

        public AuthRoleBased(int authID, string authKey)
        {
            AuthID = authID;
            AuthKey = authKey;
        }

        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return AuthRoleBased.Add(AuthID, AuthKey);
        }

        public bool Update()
        {
            return AuthRoleBased.Update(AuthID, AuthKey);
        }

        public bool Delete()
        {
            return AuthRoleBased.Delete(AuthID);
        }
        #endregion

        #region Static Methods :

        public static bool Update(int authID, string authKey)
        {
            return TAuthRoleBased.Update(authID, authKey);
        }

        public static bool Delete(int authID)
        {
            return TAuthRoleBased.Delete(authID);
        }

        public static int Add(int authID, string authKey)
        {
            return TAuthRoleBased.Add(authKey);
        }

        public static List<AuthRoleBased> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TAuthRoleBased.GetAll());
        }

        public static AuthRoleBased GetSingleByID(int authId)
        {
            return GetObjectFromDbObject(TAuthRoleBased.GetSingleByID(authId));
        }

        public static AuthRoleBased GetObjectFromDbObject(TAuthRoleBased authRoleBased)
        {
            return new AuthRoleBased(authRoleBased.AuthID, authRoleBased.AuthKey);
        }

        public static List<AuthRoleBased> GetCollectionObjectFromDbCollectionObject(IEnumerable<TAuthRoleBased> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<AuthRoleBased> GetModuleAuthRoleBased(int? ModuleDefID)
        {
            return GetCollectionObjectFromDbCollectionObject(TAuthRoleBased.GetModuleAuthRoleBased(ModuleDefID));
        }

        public static string GetAuthKeyRoleBased(int authId)
        {
            var str = GetObjectFromDbObject(TAuthRoleBased.GetSingleByID(authId));
            //if (str == DBNull.Value)
            //{
            //    return "";
            //}
            return str.AuthKey;
        }

        #endregion

        #endregion

    }

    #endregion
}
