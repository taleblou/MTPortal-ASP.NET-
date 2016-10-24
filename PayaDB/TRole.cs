using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TRole
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TRole()
        {
        }

        #region Properties:

        [Telerik.OpenAccess.FieldAlias("roleID")]
        public int RoleID
        {
            get { return roleID; }
            set { this.roleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalID")]
        public int? PortalID
        {
            get { return portalID; }
            set { this.portalID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("roleKey")]
        public string RoleKey
        {
            get { return roleKey; }
            set { this.roleKey = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tPortal")]
        public TPortal TPortal
        {
            get { return tPortal; }
            set { this.tPortal = value; }
        }

        //[Telerik.OpenAccess.FieldAlias("tTabRole")]
        //public IList<TTab> TTabRoles
        //{
        //    get { return tTabRole; }
        //}

        //[Telerik.OpenAccess.FieldAlias("tUserRole")]
        //public IList<TUser> TUserRoles
        //{
        //    get { return tUserRole; }
        //}
        #endregion

        #region Method

        public static int Add(int? portalID, string roleKey)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TRole
                {
                    PortalID = portalID,
                    RoleKey = roleKey
                };
                scope.Add(o);
                scope.Transaction.Commit();

                return int.Parse(scope.GetObjectId(o).ToString());
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return 0;
            }

        }

        public static bool Update(int roleID, int? portalID, string roleKey)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TRole>().Single(emp => emp.RoleID == roleID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.PortalID = portalID;
                    o.RoleKey = roleKey;
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

        public static bool Delete(int roleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TRole>().Single(emp => emp.RoleID == roleId);
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

        public static IEnumerable<TRole> GetAll(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TRole>().Where(o => o.PortalID == portalId).ToList();
        }

        public static TRole GetSingleByID(int roleId,int? portalID)
        {  
            var scope = PayaScopeProvider1.GetNewObjectScope();

            if (portalID == null)
            {
                return scope.Extent<TRole>().SingleOrDefault(o => o.RoleID == roleId);
            }

            return scope.Extent<TRole>().SingleOrDefault(o => o.RoleID == roleId && o.PortalID==portalID);
        }

        /// <summary>
        /// لیستی از رول هایی را می دهد که یک صفحه دارد 
        /// </summary>
        /// <param name="tabId"></param>
        /// <returns></returns>
        public static IEnumerable<TRole> GetTabRolesByTabId(int tabId)
        {
            return
                TTabRole.GetAll().Where(o => o.TabID == tabId).Select(tabRole => GetSingleByID(tabRole.RoleID,null)).ToList();
        }

        public static IEnumerable<TRole> GetobjRoles(int objId, int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            
                var t =scope.Extent<TObjRole>().Where(o => o.ObjID == objId && o.AuthID == authId).ToList();
            var t2=t.Select(
                    objRole => GetSingleByID(objRole.RoleID,null)).ToList();
            return t2;
        }

        public static IEnumerable<TRole> GetUserRoleByUserId(int userId)
        {
            return
                TUserRole.GetAll().Where(o => o.UserID == userId).Select(trol => GetSingleByID(trol.RoleID,null)).ToList();
        }

        public static TRole GetSingleById(int RoleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TRole>().SingleOrDefault(o => o.RoleID == RoleId);
        }

        public static List<TRole> GetRolesOfModule(int moduleID, int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var t = scope.Extent<TModuleRole>().Where(o => o.ModuleID == moduleID && o.AuthID == authId).ToList();
            var p=t.Select(
                    moduleRole => GetSingleByID(moduleRole.RoleID,null)).ToList();
            return p;

        }

        public static List<TRole> GetRolesAllOfModule(int moduleID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
                var t =scope.Extent<TModuleRole>().Where(o => o.ModuleID == moduleID).ToList();
            var t2=t.Select(
                    moduleRole => GetSingleByID(moduleRole.RoleID,null)).ToList();
            return t2;

        }

        /// <summary>
        /// کامل شود
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userName"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="othroleId"></param>
        /// <param name="searchType"></param>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public static List<TUser> GetRoleMembers(int roleId, string userName, string fName, string lName, int othroleId, int searchType, int portalId)
        {
            throw new NotImplementedException();
        }

        public static List<TRole> GetBelongaleRoles(int userId, int portalId)
        {
            throw new NotImplementedException();
        }

        public static List<TRole> GetBelongRolesTabs(int tabId, int searchId)
        {
            throw new NotImplementedException();
        }

        public static List<TRole> GetModuleRoles(int moduleId, int authId)
        {
            throw new NotImplementedException();
        }

        public static List<TRole> SpecifiedObjPermission(int authId, int moduleId)
        {
            throw new NotImplementedException();
        }

        #endregion


        
    }
}
