using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TTab
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TTab()
        {
        }

        #region Properties :

        [Telerik.OpenAccess.FieldAlias("tabID")]
        public int TabID
        {
            get { return tabID; }
            set { this.tabID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("description")]
        public string Description
        {
            get { return description; }
            set { this.description = value; }
        }

        [Telerik.OpenAccess.FieldAlias("isreserved")]
        public sbyte Isreserved
        {
            get { return isreserved; }
            set { this.isreserved = value; }
        }

        [Telerik.OpenAccess.FieldAlias("keywords")]
        public string Keywords
        {
            get { return keywords; }
            set { this.keywords = value; }
        }

        [Telerik.OpenAccess.FieldAlias("languageID")]
        public int LanguageID
        {
            get { return languageID; }
            set { this.languageID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("parentID")]
        public int? ParentID
        {
            get { return parentID; }
            set { this.parentID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("portalID")]
        public int? PortalID
        {
            get { return portalID; }
            set { this.portalID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("roles")]
        public string Roles
        {
            get { return roles; }
            set { this.roles = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settings")]
        public string Settings
        {
            get { return settings; }
            set { this.settings = value; }
        }

        [Telerik.OpenAccess.FieldAlias("showFooter")]
        public bool ShowFooter
        {
            get { return showFooter; }
            set { this.showFooter = value; }
        }

        [Telerik.OpenAccess.FieldAlias("showHorizontal")]
        public bool ShowHorizontal
        {
            get { return showHorizontal; }
            set { this.showHorizontal = value; }
        }

        [Telerik.OpenAccess.FieldAlias("showVertical")]
        public bool ShowVertical
        {
            get { return showVertical; }
            set { this.showVertical = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tabName")]
        public string TabName
        {
            get { return tabName; }
            set { this.tabName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tabOrder")]
        public int? TabOrder
        {
            get { return tabOrder; }
            set { this.tabOrder = value; }
        }

        [Telerik.OpenAccess.FieldAlias("target")]
        public sbyte Target
        {
            get { return target; }
            set { this.target = value; }
        }

        [Telerik.OpenAccess.FieldAlias("url")]
        public string Url
        {
            get { return url; }
            set { this.url = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tLanguage")]
        public TLanguage TLanguage
        {
            get { return tLanguage; }
            set { this.tLanguage = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tPortal")]
        public TPortal TPortal
        {
            get { return tPortal; }
            set { this.tPortal = value; }
        }

        #endregion

        #region Method

        public static int Add(string description, sbyte isreserved, string keywords, int languageID,
                              int? parentID, int portalID, string roles, string settings, bool showFooter,
                              bool showHorizontal,
                              bool showVertical, string tabName, int? tabOrder, sbyte target, string url)
        {
            IObjectScope scopeProvider = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scopeProvider.Transaction.Begin();
                var tab = new TTab
                              {
                                  Description = description,
                                  Isreserved = isreserved,
                                  Keywords = keywords,
                                  LanguageID = languageID,
                                  ParentID = parentID,
                                  PortalID = int.Parse(portalID.ToString()),
                                  Roles = roles,
                                  Settings = settings,
                                  ShowFooter = showFooter,
                                  ShowHorizontal = showHorizontal,
                                  ShowVertical = showVertical,
                                  TabName = tabName,
                                  TabOrder = tabOrder,
                                  Target = target,
                                  Url = url

                              };
                scopeProvider.Add(tab);
                scopeProvider.Transaction.Commit();

                return int.Parse(scopeProvider.GetObjectId(tab).ToString());
            }
            catch (Exception)
            {
                if (scopeProvider.Transaction.IsActive)
                    scopeProvider.Transaction.Rollback();
                return 0;
            }
        }

        public static bool Delete(int tabId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTab>().Single(emp => emp.TabID == tabId);
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

        public static bool Update(int tabId, string description, sbyte isreserved, string keywords, int languageID,
                                  int? parentID, int portalID, string roles, string settings, bool showFooter,
                                  bool showHorizontal,
                                  bool showVertical, string tabName, int? tabOrder, sbyte target, string url)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTab>().Single(emp => emp.TabID == tabId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.Description = description;
                    o.Isreserved = isreserved;
                    o.Keywords = keywords;
                    o.LanguageID = languageID;
                    o.ParentID = parentID;
                    o.PortalID = portalID;
                    o.Roles = roles;
                    o.Settings = settings;
                    o.ShowFooter = showFooter;
                    o.ShowHorizontal = showHorizontal;
                    o.ShowVertical = showVertical;
                    o.TabName = tabName;
                    o.TabOrder = tabOrder;
                    o.Target = target;
                    o.Url = url;
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

        public static IEnumerable<TTab> GetAll(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.PortalID == portalId).ToList();
        }

        public static TTab GetSingleByID(int id)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().SingleOrDefault(o => o.TabID == id);

        }

        public static List<TTab> GetReservedTabs()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.Isreserved == 1).ToList();

        }


        ///////////////////////////////////

        public static List<TTab> GetChildTabs(int tabId, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.PortalID == portalId && o.ParentID == tabId).ToList();
        }

        public static List<TTab> GetChildTabs(int tabId, int portalId, int nastedLevel)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.PortalID == portalId && o.ParentID == tabId).ToList();
        }

        public static List<TTab> GetParentTabs(int parentId, int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.PortalID == portalId && o.TabID == parentId).ToList();
        }

        public static List<TTab> GetParentTabs(int parentId, int portalId, int nastedLevel)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TTab>().Where(o => o.PortalID == portalId && o.TabID == parentId).ToList();
        }

        //public static bool TabHasModule(int tabId)
        //{

        //}

        public static int GetTabOrder(int tabId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var singleOrDefault = scope.Extent<TTab>().SingleOrDefault(o => o.TabID == tabId);
            if (singleOrDefault != null)
            {
                var i = singleOrDefault.TabOrder;
                if (i != null)
                {
                    return i.Value;
                }
            }
            return -1;
        }

        public static object GetTabSettings(int tabId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                return scope.Extent<TTab>().SingleOrDefault(o => o.TabID == tabId).settings;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public static List<TTab> GetViewableTabs(int langId, int portalId)
        //{
        //}

        //public static bool TabHasModule(int tabId)
        //{
        //}

        public static bool UpdateTabName(int tabId, string tabName)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTab>().Single(emp => emp.TabID == tabId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.TabName = tabName;
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

        private static bool updateOrder(int tabId, int newOrder)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTab>().Single(emp => emp.TabID == tabId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.TabOrder = newOrder;
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

        public static bool UpdateTabOrder(int tabId1, int tabId2)
        {

            var o1 = GetSingleByID(tabId1);
            var t = o1.tabOrder;
            return updateOrder(tabId1, (int) GetSingleByID(tabId2).TabOrder) && updateOrder(tabId2, (int) t);

        }

        //public string GetSetting(string settingName, string defValue)
        //{
        //}
        public static List<TTab> GetTabsTree(int portalId, int langId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var t = scope.Extent<TTab>().Where(o => o.PortalID == portalId && o.LanguageID == langId && o.isreserved!=5).ToList();
            t.AddRange(scope.Extent<TTab>().Where(o => o.isreserved == 5).ToList());
            return t;
        }

        public static int AddTab(string tabName, int parentId, string url, bool showVertical, bool showHorizontal,
                         bool showFooter, sbyte isreserved, sbyte target, int languageID, int portalId,
                         string keywords, string description)
        {
            IObjectScope scopeProvider = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scopeProvider.Transaction.Begin();
                var tab = new TTab
                {
                    Description = description,
                    Isreserved = isreserved,
                    Keywords = keywords,
                    LanguageID = languageID,
                    ParentID = parentId,
                    PortalID = portalId,
                    ShowFooter = showFooter,
                    ShowHorizontal = showHorizontal,
                    ShowVertical = showVertical,
                    TabName = tabName,
                    Target = target,
                    Url = url

                };
                scopeProvider.Add(tab);
                scopeProvider.Transaction.Commit();

                return int.Parse(scopeProvider.GetObjectId(tab).ToString());
            }
            catch (Exception)
            {
                if (scopeProvider.Transaction.IsActive)
                    scopeProvider.Transaction.Rollback();
                return 0;
            }
        }

        public static int AddTab(string tabName, int? parentId, string url, bool showVertical, bool showHorizontal,
                                 bool showFooter, sbyte isreserved, sbyte target, int languageID, int portalId,
                                 string keywords, string description, string settings,int rolId)
        {
            IObjectScope scopeProvider = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scopeProvider.Transaction.Begin();
                var tab = new TTab
                {
                    Description = description,
                    Isreserved = isreserved,
                    Keywords = keywords,
                    LanguageID = languageID,
                    ParentID = parentId,
                    PortalID = portalId,
                    ShowFooter = showFooter,
                    ShowHorizontal = showHorizontal,
                    ShowVertical = showVertical,
                    TabName = tabName,
                    Target = target,
                    Settings = settings,
                    Url = url,
                    Roles=rolId.ToString()

                };
                scopeProvider.Add(tab);
                scopeProvider.Transaction.Commit();

                return int.Parse(scopeProvider.GetObjectId(tab).ToString());
            }
            catch (Exception)
            {
                if (scopeProvider.Transaction.IsActive)
                    scopeProvider.Transaction.Rollback();
                return 0;
            }
        }

        public static bool Update(int tabId, string tabName, int? parentId, string url, bool showVertical, bool showHorizontal,
                                  bool showFooter, byte target, string keywords, string description, string settings)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TTab>().Single(emp => emp.TabID == tabId);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.Description = description;
                    o.Keywords = keywords;
                    o.ParentID = parentId;
                    o.Settings = settings;
                    o.ShowFooter = showFooter;
                    o.ShowHorizontal = showHorizontal;
                    o.ShowVertical = showVertical;
                    o.TabName = tabName;
                    o.Target = (sbyte)target;
                    o.Url = url;
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
