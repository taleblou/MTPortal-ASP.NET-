using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TAuthObjBased
    {
        #region properties:
        //The 'no-args' constructor required by OpenAccess. 
        public TAuthObjBased()
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

        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tModuleDef")]
        public TModuleDef TModuleDef
        {
            get { return tModuleDef; }
            set { this.tModuleDef = value; }
        }
        #endregion

        #region Methods:

        public static bool Update(int authID, string authKey, int moduleDefID)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                TAuthObjBased o = scope.Extent<TAuthObjBased>().Single(emp => emp.AuthID == authID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.AuthKey = authKey;
                    o.ModuleDefID = moduleDefID;
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
                var o = scope.Extent<TAuthObjBased>().Single(emp => emp.AuthID == authID);
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

        public static int Add(string authKey, int moduleDefID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TAuthObjBased()
                {
                    AuthKey = authKey,
                    ModuleDefID = moduleDefID
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

        public static List<TAuthObjBased> GetModuleAuthObjBased(int modulDefID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return  scope.Extent<TAuthObjBased>().Where(o => o.ModuleDefID==modulDefID).ToList();
            
        }

        public static string GetAuthKeyObjBased(int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var singleOrDefault = scope.Extent<TAuthObjBased>().SingleOrDefault(o => o.AuthID == authId);
            if (singleOrDefault != null)
                return singleOrDefault.AuthKey;
            return null;
        }

        ///////////////////////////////////

        //public static List<TAuthObjBased> GetModuleAuthObjBased(int moduledefid, int objId)
        //{
        //    var scope = PayaScopeProvider1.GetNewObjectScope();
        //    return scope.Extent<TAuthObjBased>().Where(o => o.ModuleDefID == moduledefid && o.);
        //}

        #endregion



       
    }
}
