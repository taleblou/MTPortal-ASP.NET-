using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TObjRole
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TObjRole()
        {
        }

        #region Properties :

        [Telerik.OpenAccess.FieldAlias("objRoleID")]
        public int ObjRoleID
        {
            get { return objRoleID; }
            set { this.objRoleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("authID")]
        public int AuthID
        {
            get { return authID; }
            set { this.authID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleID")]
        public int ModuleID
        {
            get { return moduleID; }
            set { this.moduleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("objID")]
        public int ObjID
        {
            get { return objID; }
            set { this.objID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("roleID")]
        public int RoleID
        {
            get { return roleID; }
            set { this.roleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tAuthObjBased")]
        public TAuthObjBased TAuthObjBased
        {
            get { return tAuthObjBased; }
            set { this.tAuthObjBased = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tModule")]
        public TModule TModule
        {
            get { return tModule; }
            set { this.tModule = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tRole")]
        public TRole TRole
        {
            get { return tRole; }
            set { this.tRole = value; }
        }

        #endregion

        #region Method

        public static bool Update(int objRoleID, int authID, int objID, int roleID, int moduleID)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TObjRole>().Single(emp => emp.ObjRoleID == objRoleID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.AuthID = authID;
                    o.ObjID = objID;
                    o.RoleID = roleID;
                    o.moduleID = moduleID;
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

        public static bool Delete(int objRoleID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TObjRole>().Single(emp => emp.ObjRoleID == objRoleID);
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

        public static int Add(int authID, int objID, int roleID, int moduleID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TObjRole
                {
                    AuthID = authID,
                    ObjID = objID,
                    RoleID = roleID,
                    ModuleID=moduleID

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

        public static List<TObjRole> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TObjRole>().ToList();
        }

        public static TObjRole GetSingleByID(int objRoleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TObjRole>().SingleOrDefault(o => o.ObjRoleID == objRoleId);

        }

        public static bool Delete(int objID, int roleId, int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o =
                    scope.Extent<TObjRole>().Single(
                        emp => emp.ObjID == objID && emp.RoleID == roleId && emp.AuthID == authId);
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
