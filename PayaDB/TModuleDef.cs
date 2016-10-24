using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TModuleDef
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModuleDef()
        {
        }

        #region Properties


        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("adminType")]
        public bool? AdminType
        {
            get { return adminType; }
            set { this.adminType = value; }
        }

        [Telerik.OpenAccess.FieldAlias("deskTopSRC")]
        public string DeskTopSRC
        {
            get { return deskTopSRC; }
            set { this.deskTopSRC = value; }
        }

        [Telerik.OpenAccess.FieldAlias("enabled")]
        public bool Enabled
        {
            get { return enabled; }
            set { this.enabled = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleKey")]
        public string ModuleKey
        {
            get { return moduleKey; }
            set { this.moduleKey = value; }
        }

        [Telerik.OpenAccess.FieldAlias("updatable")]
        public bool Updatable
        {
            get { return updatable; }
            set { this.updatable = value; }
        }
        

        #endregion

        #region Method
        //public static int Add(bool? adminType, string deskTopSRC, bool enabled,
        //              string moduleKey, bool updatable)
        //{
        //    var scope = PayaScopeProvider.GetNewObjectScope();
        //    try
        //    {
        //        scope.Transaction.Begin();
        //        var o = new TModuleDef
        //        {
        //            AdminType = adminType,
        //            DeskTopSRC = deskTopSRC,
        //            Enabled = enabled,
        //            ModuleKey = moduleKey,
        //            Updatable = updatable
        //        };
        //        scope.Add(o);
        //        scope.Transaction.Commit();

        //        return int.Parse(scope.GetObjectId(o).ToString());
        //    }
        //    catch (Exception)
        //    {
        //        if (scope.Transaction.IsActive)
        //            scope.Transaction.Rollback();
        //        return 0;
        //    }

        //}

        //public static bool Update(int moduleDefId, bool? adminType, string deskTopSRC, bool enabled,
        //                          string moduleKey, bool updatable)
        //{
        //    IObjectScope scope = PayaScopeProvider.GetNewObjectScope();
        //    try
        //    {
        //        var o = scope.Extent<TModuleDef>().Single(emp => emp.ModuleDefID == moduleDefId);
        //        if (o != null)
        //        {
        //            scope.Transaction.Begin();
        //            o.AdminType = adminType;
        //            o.DeskTopSRC = deskTopSRC;
        //            o.Enabled = enabled;
        //            o.ModuleKey = moduleKey;
        //            o.Updatable = updatable;
        //            scope.Transaction.Commit();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        if (scope.Transaction.IsActive)
        //            scope.Transaction.Rollback();
        //        return false;
        //    }
        //}

        //public static bool Delete(int id)
        //{
        //    var scope = PayaScopeProvider.GetNewObjectScope();
        //    try
        //    {
        //        var o = scope.Extent<TModuleDef>().Single(emp => emp.ModuleDefID == id);
        //        scope.Transaction.Begin();
        //        scope.Remove(o);
        //        scope.Transaction.Commit();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        if (scope.Transaction.IsActive)
        //            scope.Transaction.Rollback();
        //        return false;
        //    }
        //}

        public static List<TModuleDef> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleDef>().ToList();
        }

        public static TModuleDef GetSingleByID(int id)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleDef>().SingleOrDefault(o => o.ModuleDefID == id);
        }
        
        public static List<TModuleDef> GetAll(int portalID, bool? adminType)
        {
            if (portalID == 0)
            {
                var scope = PayaScopeProvider1.GetNewObjectScope();
                var t = scope.Extent<TModuleDef>().ToList();
                if (adminType!=null)
                {
                    t = t.Where(o => o.AdminType == adminType && o.Enabled).ToList();
                }
                else
                {
                    t= t.Where(o =>o.Enabled).ToList();
                }
                
                return t;
            }
            else
            {
                //var scope = PayaScopeProvider1.GetNewObjectScope();
                //var t = scope.Extent<TModuleDef>().ToList();
                //t = t.Where(o => o.AdminType == adminType && o.Enabled).ToList();
                //return t;


                //var scope = PayaScopeProvider1.GetNewObjectScope();

                //return
                //    scope.Extent<TModuleDef>().Where(o => o.AdminType == adminType && o.Enabled).Select(
                //        moduleDef => (TModuleDef) moduleDef.tModuleDefInPortal.Select(o => o.PortalID == portalID)).
                //        ToList();

                // TModuleRole TmoduleR = scope.Extent<TModuleRole>().SingleOrDefault(oo => oo.AuthID == AuthId);
                //return TModuleDef.GetAll().Where(o => o.tModuleDefInPortal != null && o.. == o.ModuleID).ToList();
                //var scope = PayaScopeProvider1.GetNewObjectScope();
                //  return scope.Extent<TModuleDef>().Where(o => o.AdminType == adminType && o.Enabled == true ).Select(tModuleDefInPortal.Where(oo => oo.PortalID == portalID)).ToList();

                var scope = PayaScopeProvider1.GetNewObjectScope();
                var tmoduleDefInPortal = scope.Extent<TModuleDefInPortal>().Where(o => o.PortalID == portalID).ToList();
                if (adminType != null)
                { 
                    return
                    tmoduleDefInPortal.Select(moduleDefInPortal => GetSingleByID(moduleDefInPortal.ModuleDefID)).Where(
                        t => t.AdminType != null && ((bool) t.AdminType && t.Enabled)).ToList();
                }
                return
                    tmoduleDefInPortal.Select(moduleDefInPortal => GetSingleByID(moduleDefInPortal.ModuleDefID)).Where(
                        t => t.Enabled).ToList();

            }
        }

        #endregion
        
    }
}
