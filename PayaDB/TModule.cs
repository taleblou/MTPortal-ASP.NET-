using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TModule
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModule()
        {
        }

        #region Properties:
        [Telerik.OpenAccess.FieldAlias("moduleID")]
        public int ModuleID
        {
            get { return moduleID; }
            set { this.moduleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("cacheTime")]
        public int? CacheTime
        {
            get { return cacheTime; }
            set { this.cacheTime = value; }
        }

        [Telerik.OpenAccess.FieldAlias("container")]
        public string Container
        {
            get { return container; }
            set { this.container = value; }
        }

        [Telerik.OpenAccess.FieldAlias("lastUpdate")]
        public DateTime? LastUpdate
        {
            get { return lastUpdate; }
            set { this.lastUpdate = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleOrder")]
        public int? ModuleOrder
        {
            get { return moduleOrder; }
            set { this.moduleOrder = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleTitle")]
        public string ModuleTitle
        {
            get { return moduleTitle; }
            set { this.moduleTitle = value; }
        }

        [Telerik.OpenAccess.FieldAlias("paneName")]
        public string PaneName
        {
            get { return paneName; }
            set { this.paneName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("showinAllTab")]
        public bool ShowinAllTab
        {
            get { return showinAllTab; }
            set { this.showinAllTab = value; }
        }

        [Telerik.OpenAccess.FieldAlias("skinHTML")]
        public string SkinHTML
        {
            get { return skinHTML; }
            set { this.skinHTML = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tabID")]
        public int TabID
        {
            get { return tabID; }
            set { this.tabID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("updatePeriod")]
        public int UpdatePeriod
        {
            get { return updatePeriod; }
            set { this.updatePeriod = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tTab")]
        public TTab TTab
        {
            get { return tTab; }
            set { this.tTab = value; }
        }

        #endregion

        #region Method

        public static int Add(int? cachetime, string container, DateTime? lastUpdate, int moduleDefID,
              int? moduleOrder, string moduleTitle, string paneName, bool showinAllTab, string skinHTML,
              int tabID, int updatePeriod)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TModule
                {
                    CacheTime = cachetime,
                    Container = container,
                    LastUpdate = lastUpdate,
                    ModuleDefID = moduleDefID,
                    ModuleOrder = moduleOrder,
                    ModuleTitle = moduleTitle,
                    PaneName = paneName,
                    ShowinAllTab = showinAllTab,
                    SkinHTML = skinHTML,
                    TabID = tabID,
                    UpdatePeriod = updatePeriod
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

        public static bool Update(int moduleiD, int? cachetime, string container, DateTime? lastUpdate, int moduleDefID,
                      int? moduleOrder, string moduleTitle, string paneName, bool showinAllTab, string skinHTML,
                      int tabID, int updatePeriod)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleiD);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.CacheTime = cachetime;
                    o.Container = container;
                    o.LastUpdate = lastUpdate;
                    o.ModuleDefID = moduleDefID;
                    o.ModuleOrder = moduleOrder;
                    o.ModuleTitle = moduleTitle;
                    o.PaneName = paneName;
                    o.ShowinAllTab = showinAllTab;
                    o.SkinHTML = skinHTML;
                    o.TabID = tabID;
                    o.UpdatePeriod = updatePeriod;
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

        public static bool Delete(int moduleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
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

        public static List<TModule> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModule>().ToList();
        }

        public static TModule GetSingleByID(int moduleId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModule>().SingleOrDefault(o => o.ModuleID == moduleId);
        }

        public static List<TModule> GetModulesOfTab(int TabID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModule>().Where(o => o.TabID == TabID).ToList();
        }

        public static List<TModule> GetModulesShowInAllTabs(int hometabid)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModule>().Where(o => o.TabID == hometabid && o.ShowinAllTab).ToList();
        }

        public static DateTime GetLastUpdateDate()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var update = scope.Extent<TModule>().OrderByDescending(o => o.lastUpdate).First().lastUpdate;
            if (update != null)
                return (DateTime)update;
            return new DateTime();
        }

        public static bool UpdateModulePane(int moduleId, string panName)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.PaneName = panName;
                    o.LastUpdate = DateTime.Now;
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

        public static bool UpdateModuleOrder(int moduleId, int order)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.ModuleOrder = order;
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

        public static List<TModule> GetTabModules(int tabId, string panName)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModule>().Where(o => o.tabID == tabId && o.paneName == panName).ToList();
        }

        public static int AddModule(int tabId, int moduleOrder, int moduleDefId, string moduleTitle, string paneName, bool showInAllTabs)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TModule
                {
                    TabID = tabId,
                    ModuleOrder = moduleOrder,
                    ModuleDefID = moduleDefId,
                    ModuleTitle = moduleTitle,
                    PaneName = paneName,
                    cacheTime = 0,
                    container = "LayoutControl.ascx",
                    skinHTML = null,
                    LastUpdate = DateTime.Now,
                    updatePeriod = 0,
                    ShowinAllTab = showInAllTabs,

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

        public static bool UpdateModuleInfo(int moduleId, string moduletitle, int tabid, string container,
                                            string skinhtml, int updatePeriod, int cacheTime, bool showInAllPage)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.Container = container;
                    o.ModuleTitle = moduletitle;
                    o.CacheTime = cacheTime;
                    o.ShowinAllTab = showInAllPage;
                    o.SkinHTML = skinhtml;
                    o.TabID = tabid;
                    o.lastUpdate = DateTime.Now;
                    o.UpdatePeriod = updatePeriod;
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

        public static List<TModule> GetAuthKeyRoleBased(int AuthId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            TModuleRole tmoduleRole = scope.Extent<TModuleRole>().SingleOrDefault(oo => oo.AuthID == AuthId);
            return GetAll().Where(o => tmoduleRole != null && o.ModuleID == tmoduleRole.ModuleID).ToList();

        }

        public static bool UpdateModuleTitle(int moduleId, string title)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.ModuleTitle = title;
                    o.LastUpdate = DateTime.Now;
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
        public static bool UpdateModuleLastUpdateDate(int moduleId)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModule>().Single(emp => emp.ModuleID == moduleId);
                if (o != null)
                {
                    scope.Transaction.Begin();

                    o.LastUpdate = DateTime.Now;
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
        #endregion


    }
}
