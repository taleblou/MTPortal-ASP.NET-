using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TTabRole
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TTabRole()
        {
        }
        #region Properties:

        [Telerik.OpenAccess.FieldAlias("tabRoleID")]
        public int TabRoleID
        {
            get { return tabRoleID; }
            set { this.tabRoleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("roleID")]
        public int RoleID
        {
            get { return roleID; }
            set { this.roleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tabID")]
        public int TabID
        {
            get { return tabID; }
            set { this.tabID = value; }
        }

        #endregion

        #region Method
        public static int Add(int roleID, int tabID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TTabRole
                {
                    RoleID = roleID,
                    TabID = tabID
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

        public static bool Update(int tabRoleId, int roleId, int tabId)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTabRole>().Single(emp => emp.RoleID == roleId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.tabRoleID = tabRoleId;
                    o.roleID = roleId;
                    o.tabID = tabId;
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

        public static bool Delete(int tabRoleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTabRole>().Single(emp => emp.RoleID == tabRoleId);
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
        
        public static List<TTabRole> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTabRole>().ToList();
        }

        public static TTabRole GetSingleByID(int tabRoleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTabRole>().SingleOrDefault(o => o.TabRoleID == tabRoleId);

        }



        ////////////////////////////////////////////

        //public static string GetTabRolesToString(int tabId) { }

        
        public static bool Delete(int tabId, int rolId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTabRole>().Single(emp => emp.TabID == tabId && emp.RoleID==rolId);
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

        #endregion

    }
}
