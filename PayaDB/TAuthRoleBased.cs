using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TAuthRoleBased
    {
        #region Properties:

        public TAuthRoleBased()
        {
        }

        [Telerik.OpenAccess.FieldAlias("authID")]
        public int AuthID
        {
            get { return authID; }
            set { this.authID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("authKey")]
        public string AuthKey
        {
            get { return authKey; }
            set { this.authKey = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tAuthRoleModuleDef")]
        public IList<TModuleDef> TAuthRoleModuleDefs
        {
            get { return tAuthRoleModuleDef; }
        }

        #endregion

        #region Method

        public static bool Update(int authID, string authKey)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TAuthRoleBased>().Single(emp => emp.AuthID == authID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.AuthKey = authKey;
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

        public static bool Delete(int authID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TAuthRoleBased>().Single(emp => emp.AuthID == authID);
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

        public static int Add(string authKey)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TAuthObjBased()
                {
                    AuthKey = authKey
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

        public static List<TAuthRoleBased> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TAuthRoleBased>().ToList();
        }

        public static TAuthRoleBased GetSingleByID(int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TAuthRoleBased>().SingleOrDefault(o => o.AuthID == authId);
        }

        public static List<TAuthRoleBased> GetModuleAuthRoleBased(int? moduleDefID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var temp =
                scope.Extent<TAuthRoleBased>().Where(
                    authRoleBased =>
                    authRoleBased.TAuthRoleModuleDefs.Any(
                        authRoleModuleDef => authRoleModuleDef.ModuleDefID == moduleDefID)).ToList();
            return temp;

        }
        #endregion
      
    }
}
