using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using PayaBL.Common.PortalCach;
using PayaDB;

namespace PayaBL.Classes
{

    #region Class Tab:

    /// <summary>
    /// مشخصات یک صفحه را نگهداری می کند 
    /// </summary>
    public class Tab : IComparable
    {
        #region enums

        public enum ReservedType
        {
            NotReserved,
            IContentsGroupLastPage,
            GameGroupLastPage,
            GalleryGroupReservedType,
            ClipGroupReservedType,
            admintab
        }

        public enum TargetTypes
        {
            Self,
            Blank,
            Empty
        }

        #endregion

        #region Fields :

        #endregion

        #region Properties :

        #region Base:

        public string Template
        {
            set {  }
            get { return GetTabTemplate(TabID); }

        }

        

        public string Theme
        {
            set { }
            get { return GetTabTheme(TabID); }
        }

       

        public int TabID { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public int LanguageID { get; set; }

        public int? ParentID { get; set; }

        public int PortalID { get; set; }

        public string Roles
        {
            get { return TabRole.GetTabRolesToString(TabID); }
            set { }
        }

        public string Settings { get; set; }

        public bool ShowFooter { get; set; }

        public bool ShowHorizontal { get; set; }

        public bool ShowVertical { get; set; }

        public string TabName { get; set; }

        public int? TabOrder { get; set; }

        public string Url { get; set; }

        public ReservedType IsReserved { get; set; }

        //public int NestLevel { get; set; }

        public string ShowTitle { get; set; }

        public Tab ParentTab
        {
            get
            {
                if (ParentID != null) return GetSingleByID((int)ParentID);
                return null;
            }
        }

        public sbyte Target { get; set; }

        public bool ShowHeader { get; set; }

        public bool ShowMenu { get; set; }


        #endregion

        #region Related :

        private TLanguage tLanguage { get; set; }

        public Language Language
        {
            get { return Language.GetObjectFromDbObject(tLanguage); }

        }

        public List<Module> Modules
        {
            get { return Module.GetModulesOfTab(TabID); }
        }

        //public IList<TPortal> TabInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctors :

        public Tab()
        {
        }

        public Tab(int tabID, string description, ReservedType isreserved, string keywords, int languageID,
                   int? parentID,
                   int portalID, string roles, string settings, bool showFooter, bool showHorizontal, bool showVertical,
                   string tabName, int? tabOrder, sbyte target, string url, TLanguage tlanguage /*, TTab tTab1*/)
        {

            TabID = tabID;
            Description = description;
            IsReserved = isreserved;
            Keywords = keywords;
            LanguageID = languageID;
            ParentID = parentID;
            PortalID = portalID;
            Roles = roles;
            Settings = settings;
            ShowFooter = showFooter;
            ShowHorizontal = showHorizontal;
            ShowHorizontal = ShowHorizontal;
            ShowVertical = showVertical;
            TabName = tabName;
            TabOrder = tabOrder;
            Target = target;
            Url = url;
            tLanguage = tlanguage;
            Template = GetSetting("Template", "Default");
            ShowHeader = Convert.ToBoolean(int.Parse(GetSetting("SH", "1")));
            ShowMenu = Convert.ToBoolean(int.Parse(GetSetting("SM", "1")));
            ShowTitle = GetSetting("ST", TabName);
            Theme = GetSetting("Theme", "Default");

            //TTab1 = tTab1;
        }


        #endregion

        #region Methods :

        #region Instance Methods :

        public int Add()
        {
            return Add(TabName, PortalID, Url, ShowVertical, ShowHorizontal,
                           ShowFooter,
                           (byte)IsReserved, Target, LanguageID, (int)ParentID, Keywords,
                           Description, Roles, Settings, TabOrder);
        }

        public bool Update()
        {
            return Update(TabID, Description, (sbyte)IsReserved, Keywords, LanguageID,
                              ParentID, PortalID, Roles, Settings, ShowFooter, ShowHorizontal,
                              ShowVertical, TabName, TabOrder, Target, Url);
        }

        public bool Delete()
        {
            return Delete(TabID);
        }


        public List<Tab> GetChildTabs( int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetChildTabs(TabID, portalId));
        }

        public  List<Tab> GetParentTabs(int portalId)
        {
            if (ParentID==null)
            {
                return null;
            }
            return GetCollectionObjectFromDbCollectionObject(TTab.GetParentTabs((int)ParentID, portalId));
        }

        

        #endregion

        #region Static Methods :

        public static int AddTab(string tabName, int parentId, string url, bool showVertical, bool showHorizontal,
                                 bool showFooter,
                                 byte isreserved, sbyte target, int languageID, int portalId, string keywords,
                                 string description)
        {
            return TTab.AddTab(tabName, parentId, url, showVertical, showHorizontal, showFooter,
                               (sbyte)isreserved, target, languageID, portalId, keywords, description);
        }

        public static int AddTab(string tabName, int? parentId, string url, bool showVertical, bool showHorizontal,
                                 bool showFooter, byte isreserved, sbyte target, int languageID, int portalId,
                                 string keywords,
                                 string description, string settings,int rolId)
        {
            return TTab.AddTab(tabName, parentId, url, showVertical, showHorizontal, showFooter,
                               (sbyte)isreserved, target, languageID, portalId, keywords, description, settings,rolId);
        }

        public static int Add(string tabName, int portalID, string url, bool showVertical, bool showHorizontal,
                              bool showFooter,
                              byte isreserved, sbyte target, int languageID, int? parentID, string keywords,
                              string description, string roles, string settings, int? tabOrder)
        {
            return TTab.Add(description, (sbyte)isreserved, keywords, languageID,
                            parentID, portalID, roles, settings, showFooter, showHorizontal,
                            showVertical, tabName, tabOrder, target, url);
        }

        public static bool Delete(int tabId)
        {
            return TTab.Delete(tabId);
        }

        public static bool Update(int tabId, string description, sbyte isreserved, string keywords, int languageID,
                                  int? parentID, int portalID, string roles, string settings, bool showFooter,
                                  bool showHorizontal,
                                  bool showVertical, string tabName, int? tabOrder, sbyte target, string url)
        {
            return TTab.Update(tabId, description, isreserved, keywords, languageID,
                               parentID, portalID, roles, settings, showFooter, showHorizontal,
                               showVertical, tabName, tabOrder, target, url);
        }

        public static List<Tab> GetAll(int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetAll(portalId));
        }

        public static Tab GetSingleByID(int id)
        {
            return GetObjectFromDbObject(TTab.GetSingleByID(id));
        }

        private static Tab GetObjectFromDbObject(TTab tab)
        {
            return new Tab(tab.TabID, tab.Description, (ReservedType)tab.Isreserved, tab.Keywords, tab.LanguageID,
                           tab.ParentID,
                           (int)tab.PortalID, tab.Roles, tab.Settings, tab.ShowFooter, tab.ShowHorizontal, tab.ShowVertical,
                           tab.TabName, tab.TabOrder, tab.Target, tab.Url, tab.TLanguage);
        }

        private static List<Tab> GetCollectionObjectFromDbCollectionObject(IEnumerable<TTab> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<Tab> GetReservedTabs()
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetReservedTabs());
        }

        ///////////////

        public static List<Tab> GetChildTabs(int tabid, int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetChildTabs(tabid, portalId));
        }

        public static List<Tab> GetParentTabs(int parentId, int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetParentTabs(parentId, portalId));
        }

        //public static bool TabHasModule(int tabId)
        //{

        //}

        public static int GetTabOrder(int tabId)
        {
            return TTab.GetTabOrder(tabId);
        }

        //public static List<Tab> GetTabsArray(int portalId, int langId){}

        //public static List<Tab> GetTabsTree(int portalId, int langId){}

        public static string GetTabTheme(int tabId)
        {
            object settings = TTab.GetTabSettings(tabId);
            if (settings == DBNull.Value)
            {
                return "Default";
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml((string)settings);
                return doc.GetElementsByTagName("Theme")[0].InnerText;
            }
            catch
            {
                return "Default";
            }
        }

        public static string SetTabTheme(int tabId, string theme)
        {
            object settings = TTab.GetTabSettings(tabId);
            if (settings == DBNull.Value)
            {
                return "";
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml((string)settings);
                doc.GetElementsByTagName("Theme")[0].InnerXml = theme;
                return doc.InnerXml;
            }
            catch
            {
                return "";
            }
        }

        public static string GetTabTemplate(int tabId)
        {
            object settings = TTab.GetTabSettings(tabId);
            if (settings == DBNull.Value)
            {
                return "Default";
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml((string)settings);
                return doc.GetElementsByTagName("Template")[0].InnerText;
            }
            catch
            {
                return "Default";
            }
        }

        public static string SetTabTemplate(int tabId, string template)
        {
            object settings = TTab.GetTabSettings(tabId);
            if (settings == DBNull.Value)
            {
                return "";
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml((string)settings);
                doc.GetElementsByTagName("Template")[0].InnerText = template;
                
                return doc.InnerXml;
            }
            catch
            {
                return "";
            }
        }

        public static List<Tab> GetTabsArray(int portalId, int langId)
        {
            return GetCollectionObjectFromDbCollectionObject(TTab.GetTabsTree(portalId, langId));
        }

        //public static bool TabHasModule(int tabId){}

        public static bool UpdateTabName(int tabId, string tabName)
        {
            return TTab.UpdateTabName(tabId, tabName);
        }

        public static bool UpdateTabOrder(int tabId1, int tabId2)
        {
            return TTab.UpdateTabOrder(tabId1, tabId2);
        }

        public string GetSetting(string settingName, string defValue)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(Settings);
                return doc.GetElementsByTagName(settingName)[0].InnerText;
            }
            catch
            {
                return defValue;
            }
        }

        public string GetSetting()
        {
            return
                string.Format(
                    "<Settings><SM>{0}</SM><SH>{1}</SH><ST>{2}</ST><Template>{3}</Template><Theme>{4}</Theme></Settings>",
                    new object[]
                        {
                            Convert.ToByte(ShowMenu), Convert.ToByte(ShowHeader), ShowTitle, Template,
                            Theme
                        });


        }

        public static List<Tab> GetTabsTree(int portalId, int langId)
        {
            List<Tab> list = Caching.GetCachedTabs(portalId, langId);
            if (list == null)
            {
                list = GetCollectionObjectFromDbCollectionObject(TTab.GetTabsTree(portalId, langId));
                //    Caching.CacheTabs(list, portalId, langId);
            }
            return list;
        }

        public static bool Update(int tabId, string tabName, int? parentId, string url, bool showVert, bool showHorz,
                                  bool showFoot, byte target, string keywords, string desc, string settings)
        {
            return TTab.Update(tabId, tabName, parentId, url, showVert, showHorz, showFoot, target, keywords, desc,
                               settings);

        }

        //////////
        //توابع اضافه شده
        /////////

        public static bool TabHasModule(int tabId)
        {
            return ((from o in Module.GetModulesOfTab(tabId)
                     where !o.HasNoPermission()
                     select o).Any());
        }

        public static List<Tab> GetViewableTabs(int langId, int portalId)
        {
            return (from tab in GetTabsTree(portalId, langId)
                    let roles = TabRole.GetTabRolesToString(tab.TabID)
                    where Role.IsInRoles(roles)
                    select tab).ToList<Tab>();
        }

        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }
            var tabOrder = ((Tab)value).TabOrder;
            if (tabOrder != null)
            {
                int compareOrder = (int)tabOrder;
                if (this.TabOrder != compareOrder)
                {
                    if (this.TabOrder < compareOrder)
                    {
                        return -1;
                    }
                    if (this.TabOrder > compareOrder)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class TabFlat:

    ///// <summary>
    ///// 
    ///// </summary>
    //public class TabFlat
    //{
    //    #region Fields :
    //    #endregion

    //    #region Properties :

    //    #region Base:
    //    public int ID { set; get; }

    //    public int? NestLevel { set; get; }

    //    public int? TabID { set; get; }

    //    #endregion

    //    #region Related :
    //    public TTab TTab { set; get; }
    //    public IList<TPortal> TabFlatInPortal = new List<TPortal>();
    //    #endregion

    //    #endregion

    //    #region Constrauctors :
    //    public TabFlat()
    //    {
    //    }

    //    public TabFlat(int id, int? nestLevel, int? tabID, TTab tTab)
    //    {
    //        ID = id;
    //        NestLevel = nestLevel;
    //        TabID = tabID;
    //        TTab = tTab;
    //    }
    //    #endregion

    //    #region Methods :

    //    #region Instance Methods :

    //    public int Add()
    //    {
    //        return TabFlat.Add(NestLevel, TabID);
    //    }

    //    public bool Update()
    //    {
    //        return TabFlat.Update(ID, NestLevel, TabID);
    //    }

    //    public bool Delete()
    //    {
    //        return TabFlat.Delete(ID);
    //    }

    //    #endregion

    //    #region Static Methods :

    //    public static int Add(int? nestLevel, int? tabID)
    //    {

    //        var scope = PayaScopeProvider.GetNewObjectScope();
    //        try
    //        {
    //            scope.Transaction.Begin();
    //            var o = new TTabFlat
    //            {
    //                NestLevel = nestLevel,
    //                TabID = tabID
    //            };
    //            scope.Add(o);
    //            scope.Transaction.Commit();

    //            return int.Parse(scope.GetObjectId(o).ToString());
    //        }
    //        catch (Exception)
    //        {
    //            if (scope.Transaction.IsActive)
    //                scope.Transaction.Rollback();
    //            return 0;
    //        }

    //    }

    //    public static bool Update(int id, int? nestLevel, int? tabID)
    //    {
    //        IObjectScope scope = PayaScopeProvider.GetNewObjectScope();
    //        try
    //        {
    //            var o = scope.Extent<TTabFlat>().Single(emp => emp.Id == id);
    //            if (o != null)
    //            {
    //                scope.Transaction.Begin();
    //                o.NestLevel = nestLevel;
    //                o.TabID = tabID;
    //                scope.Transaction.Commit();
    //                return true;
    //            }
    //            return false;
    //        }
    //        catch (Exception)
    //        {
    //            if (scope.Transaction.IsActive)
    //                scope.Transaction.Rollback();
    //            return false;
    //        }
    //    }

    //    public static bool Delete(int id)
    //    {
    //        var scope = PayaScopeProvider.GetNewObjectScope();
    //        try
    //        {
    //            var o = scope.Extent<TTabFlat>().Single(emp => emp.Id == id);
    //            scope.Transaction.Begin();
    //            scope.Remove(o);
    //            scope.Transaction.Commit();
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            if (scope.Transaction.IsActive)
    //                scope.Transaction.Rollback();
    //            return false;
    //        }
    //    }

    //    public static List<TabFlat> GetAll()
    //    {
    //        var scope = PayaScopeProvider.GetNewObjectScope();
    //        return GetCollectionObjectFromDbCollectionObject(scope.Extent<TTabFlat>().ToList());
    //    }

    //    public static TabFlat GetByID(int id)
    //    {
    //        var scope = PayaScopeProvider.GetNewObjectScope();
    //        return GetObjectFromDbObject(scope.Extent<TTabFlat>().SingleOrDefault(o => o.Id == id));

    //    }

    //    private static TabFlat GetObjectFromDbObject(TTabFlat tabFlat)
    //    {
    //        return new TabFlat(tabFlat.Id, tabFlat.NestLevel, tabFlat.TabID, tabFlat.TTab);
    //    }

    //    private static List<TabFlat> GetCollectionObjectFromDbCollectionObject(IEnumerable<TTabFlat> list)
    //    {
    //        return list.Select(GetObjectFromDbObject).ToList();
    //    }

    //    #endregion

    //    #endregion

    //}

    #endregion

    #region TabRole:

    public class TabRole
    {
        #region Field
        #endregion

        #region Properties

        #region Base

        public int TabRoleID { get; set; }

        public int RoleID { get; set; }

        public int TabID { get; set; }

        #endregion

        #region Related

        #endregion

        #endregion

        #region Constrauctor

        public TabRole()
        {
        }

        public TabRole(int tabRoleID, int roleID, int tabID)
        {
            TabRoleID = tabRoleID;
            RoleID = roleID;
            TabID = tabID;
        }

        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return TabRole.Add(RoleID, TabID);
        }

        public bool Update()
        {
            return TabRole.Update(TabRoleID, RoleID, TabID);
        }

        public bool Delete()
        {
            return TabRole.Delete(TabRoleID);
        }

        #endregion

        #region Static Method:

        public static int Add(int roleId, int tabId)
        {
            return TTabRole.Add(roleId, tabId);
        }

        public static bool Update(int tabRoleID, int roleID, int tabID)
        {
            return TTabRole.Update(tabRoleID, roleID, tabID);
        }

        public static bool Delete(int tabRoleId)
        {
            return TTabRole.Delete(tabRoleId);
        }

        public static List<TabRole> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TTabRole.GetAll());
        }

        public static TabRole GetSingleByID(int tabRoleId)
        {
            return GetObjectFromDbObject(TTabRole.GetSingleByID(tabRoleId));
        }

        private static TabRole GetObjectFromDbObject(TTabRole tR)
        {
            return new TabRole(tR.TabRoleID, tR.RoleID, tR.TabID);
        }

        private static List<TabRole> GetCollectionObjectFromDbCollectionObject(IEnumerable<TTabRole> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        /////////////////////////

        public static string GetTabRolesToString(int tabId)
        {
            return Role.GetTabRolesByTabId(tabId).Aggregate("", (current, tabRole) => (current + tabRole.RoleID + ";"));
        }

        public static bool Delete(int tabId, int rolId)
        {
            return TTabRole.Delete(tabId, rolId);
        }

        //////////
        //توابع اضافه شده
        /////////

        public static List<Role> GetTabRolesByTabId(int tabId)
        {
            return Role.GetCollectionObjectFromDbCollectionObject(TRole.GetTabRolesByTabId(tabId));
        }
        

        #endregion

        #endregion

    }

    #endregion
}