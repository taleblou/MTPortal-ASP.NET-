using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TObjUser
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TObjUser()
        {
        }

        #region Properties :

        [Telerik.OpenAccess.FieldAlias("objUserID")]
        public int ObjUserID
        {
            get { return objUserID; }
            set { this.objUserID = value; }
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

        [Telerik.OpenAccess.FieldAlias("userID")]
        public int UserID
        {
            get { return userID; }
            set { this.userID = value; }
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

        [Telerik.OpenAccess.FieldAlias("tUser")]
        public TUser TUser
        {
            get { return tUser; }
            set { this.tUser = value; }
        }

        #endregion

        #region Method

        public static bool Update(int objUserID, int authID, int moduleID, int objID, int userID)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TObjUser>().Single(emp => emp.ObjUserID == objUserID);
                if (o != null)
                {
                    scope.Transaction.Begin();

                    o.AuthID = authID;
                    o.ModuleID = moduleID;
                    o.ObjID = objID;
                    o.UserID = userID;

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

        public static bool Delete(int objUserID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TObjUser>().Single(emp => emp.ObjUserID == objUserID);
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

        public static int Add(int authID, int moduleID, int objID, int userID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TObjUser
                {
                    AuthID = authID,
                    ModuleID = moduleID,
                    ObjID = objID,
                    UserID = userID
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

        public static List<TObjUser> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TObjUser>().ToList();
        }

        public static TObjUser GetSingleByID(int objUserID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TObjUser>().SingleOrDefault(o => o.ObjUserID == objUserID);

        }

        public static bool Delete(int objId, int userId, int authId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o =
                    scope.Extent<TObjUser>().Single(
                        emp => emp.ObjID == objId && emp.userID == userId && emp.AuthID == authId);
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

        public static List<TUser> GetobjRoles(int objId, int authID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var t =scope.Extent<TObjUser>().ToList();
            t=t.Where(o=>o.AuthID==authID&&o.objID==objId).ToList();
            var t1=t.Select(emp=>TUser.GetSingleByID(emp.userID)).ToList();
            return t1;
        }

        #endregion


        
    }
}
