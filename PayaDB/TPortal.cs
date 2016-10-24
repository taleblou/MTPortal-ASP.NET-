using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TPortal
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TPortal()
        {
        }

        #region Properties :

        [Telerik.OpenAccess.FieldAlias("portalID")]
        public int PortalID
        {
            get { return portalID; }
            set { this.portalID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalAlias")]
        public string PortalAlias
        {
            get { return portalAlias; }
            set { this.portalAlias = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalName")]
        public string PortalName
        {
            get { return portalName; }
            set { this.portalName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalPath")]
        public string PortalPath
        {
            get { return portalPath; }
            set { this.portalPath = value; }
        }

        #endregion

        #region Method

        public static int Add(string portalAlias, string portalName, string portalPath)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TPortal
                            {
                                PortalAlias = portalAlias,
                                PortalName = portalName,
                                PortalPath = portalPath
                            };
                scope.Add(o);
                scope.Transaction.Commit();

                return TPortal.GetSingleByPortalAlias(portalAlias).PortalID;
            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return 0;
            }

        }

        public static bool Update(int portalID, string portalAlias, string portalName, string portalPath)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TPortal>().Single(emp => emp.PortalID == portalID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.PortalAlias = portalAlias;
                    o.PortalName = portalName;
                    o.PortalPath = portalPath;
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

        public static bool Delete(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var t = scope.Extent<TModuleDefInPortal>().Where(emp => emp.PortalID == portalId).ToList();
                if (t.Count > 0)
                    foreach (var moduleDefInPortal in t)
                    {
                        TModuleDefInPortal.Delete(moduleDefInPortal.ModuleDefID, moduleDefInPortal.PortalID);
                    }

                var t2 = scope.Extent<TTab>().Where(emp => emp.PortalID == portalId).ToList();
                if (t2.Count > 0)
                    foreach (var tab in t2)
                    {
                        TTab.Delete(tab.TabID);
                    }
                var t3 = scope.Extent<TUser>().Where(emp => emp.PortalID == portalId).ToList();
                if (t3.Count > 0)
                    foreach (var user in t3)
                    {
                        TUser.Delete(user.UserID);
                    }
                //try
                //{
                //    var o = scope.Extent<TPortalLanguage>().Single(emp => emp.PortalID == portalId);
                //    scope.Transaction.Begin();
                //    scope.Remove(o);
                //    scope.Transaction.Commit();
                //    flag = true;
                //}
                //catch (Exception)
                //{
                //    if (scope.Transaction.IsActive)
                //        scope.Transaction.Rollback();
                //    flag = false;
                //}

                //try
                //{
                //    var o = scope.Extent<TPortalSetting>().Single(emp => emp.PortalID == portalId);
                //    scope.Transaction.Begin();
                //    scope.Remove(o);
                //    scope.Transaction.Commit();
                //    flag = true;
                //}
                //catch (Exception)
                //{
                //    if (scope.Transaction.IsActive)
                //        scope.Transaction.Rollback();
                //    flag = false;
                //}


                var o = scope.Extent<TPortal>().Single(emp => emp.PortalID == portalId);
                scope.Transaction.Begin();
                scope.Remove(o);
                scope.Transaction.Commit();

            }
            catch (Exception)
            {
                if (scope.Transaction.IsActive)
                    scope.Transaction.Rollback();
                return false;
            }
            return true;
        }

        public static List<TPortal> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TPortal>().ToList();
        }

        public static TPortal GetSingleByID(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TPortal>().SingleOrDefault(o => o.PortalID == portalId);
        }

        public static TPortal GetSingleByPortalAlias(string portalAlias)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var t = scope.Extent<TPortal>().Single(o => o.PortalAlias == portalAlias);
            return t;
        }

        //////////////////////////

        #endregion


        public static bool Delete(string portalAlias)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TPortal>().Single(emp => emp.PortalAlias == portalAlias);
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
    }
}
