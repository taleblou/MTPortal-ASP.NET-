using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaDB;
using System;

namespace PayaBL.Classes
{
    #region Class ModuleDef:

    /// <summary>
    /// ارائه اطلاعات اولیه از ماژول 
    /// که دربر گیرنده آدرس فیزیکی 
    /// دربرگیرنده حق دسترسی کلی
    /// </summary>
    public class ModuleDef
    {
        #region Field
        #endregion

        #region Properties

        #region Base

        public int ModuleDefId { set; get; }

        public bool? AdminType { set; get; }

        public string DeskTopSRC { set; get; }

        public bool Enabled { set; get; }

        public string ModuleKey { set; get; }

        public bool Updatable { set; get; }

        public string FriendlyName
        {
            get
            {
                return LocalResource.GetResourceValue(ModuleKey,
                                                      PortalLanguage.GetLanguagePortalByCulture(
                                                          PayaTools.CurrentCulture, PortalSetting.PortalId).LanguageID);
            }
        }

        #endregion

        #region Related

        public IList<TPortal> ModuleDefInPortal = new List<TPortal>();

        #endregion


        #endregion

        #region Constrauctor

        public ModuleDef()
        {
        }

        public ModuleDef(int moduleDefId, string moduleKey, string deskTopSource, bool? adminType, bool enabled, bool updateable)
        {
            ModuleDefId = moduleDefId;
            ModuleKey = moduleKey;
            DeskTopSRC = deskTopSource;
            AdminType = adminType;
            Enabled = enabled;
            Updatable = updateable;
        }

        #endregion

        #region MEthod

        #region Insttance metod:

        public bool IsUserControl()
        {
            return IsUserControl(DeskTopSRC);
        }

        #endregion

        #region Static Method:

        public static List<ModuleDef> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TModuleDef.GetAll());
        }

        public static List<ModuleDef> GetAll(int portalID, bool? adminType)
        {
            List<ModuleDef> list;
            if (portalID == 0)
            {
                list = (List<ModuleDef>)Caching.Get(string.Format("AllModuleDef{0}", adminType));
                if (list == null)
                {
                    list = GetCollectionObjectFromDbCollectionObject(TModuleDef.GetAll(portalID, adminType));
                    //Caching.Insert(string.Format("AllModuleDef{0}", adminType), list, new SqlCacheDependency("PortalDBCache", "t_ModuleDef"), DateTime.Now.AddDays(1.0));
                }
            }
            else
            {
                list = (List<ModuleDef>)Caching.Get(string.Format("AllModuleDefInPortal{0}", portalID));
                if (list == null)
                {
                    list = GetCollectionObjectFromDbCollectionObject(TModuleDef.GetAll(portalID, adminType));
               //     Caching.Insert(string.Format("AllModuleDefInPortal{0}", portalID), list, new SqlCacheDependency("PortalDBCache", "t_ModuleDef"), DateTime.Now.AddDays(1.0));
                }
            }
            return (from o in list
                    orderby o.FriendlyName
                    select o).ToList<ModuleDef>();

        }

        public static ModuleDef GetSingleByID(int moduleDefId)
        {
            return GetObjectFromDbObject(TModuleDef.GetSingleByID(moduleDefId));
        }

        private static ModuleDef GetObjectFromDbObject(TModuleDef moduleDef)
        {
            return new ModuleDef(moduleDef.ModuleDefID, moduleDef.ModuleKey, moduleDef.DeskTopSRC, moduleDef.AdminType,
                                 moduleDef.Enabled, moduleDef.Updatable);
        }

        private static List<ModuleDef> GetCollectionObjectFromDbCollectionObject(IEnumerable<TModuleDef> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }


        ////////////////////////////////

        public static bool IsUserControl(string src)
        {
            return src.ToLower().Trim().EndsWith(".ascx");
        }

        public static bool AddModuleDefInPortal(int moduleDefId, int PortalId)
        {
            return TModuleDefInPortal.Add(moduleDefId, PortalId);
        }

        public static bool DeleteModuleDefInPortal(int moduleDefId, int PortalId)
        {
            return TModuleDefInPortal.Delete(moduleDefId, PortalId);
        }

        #endregion

        #endregion



    }

    #endregion

    #region Class Module:

    /// <summary>
    /// ارائه سرویس های لازم برا ی لود و ویرایش ماژول ها
    /// تنظیم مشخصات اصلی هر ماژول
    /// </summary>
    public class Module
    {
        #region Field

        private List<AuthRoleBased> _authRoleBaseds;

        #endregion

        #region Properties

        #region Base

        public int ModuleID { set; get; }

        public int? CacheTime { set; get; }

        public string Container { set; get; }

        public DateTime? LastUpdate { set; get; }

        public int? ModuleDefID { set; get; }

        public int? ModuleOrder { set; get; }

        public string ModuleTitle { set; get; }

        public string PaneName { set; get; }

        public bool ShowinAllTab { set; get; }

        public string SkinHTML { set; get; }

        public int TabID { set; get; }

        public int UpdatePeriod { set; get; }


        #endregion

        #region Related

        public TTab TTab { set; get; }

        public List<ModuleRole> ModuleRoles
        {
            get { return ModuleRole.GetModuleRoles(ModuleID); }
        }

        public IList<TPortal> ModuleDefInPortal = new List<TPortal>();

        public ModuleDef ModuleDef = new ModuleDef();

        public Tab ModuleTab
        {
            get { return Tab.GetSingleByID(TabID); }
        }

        public List<ModuleSetting> ModuleSettings
        {
            get { return ModuleSetting.GetModuleSettingsByModuleId(ModuleID); }
        }

        public List<AuthRoleBased> AuthRoleBaseds
        {
            get { return (_authRoleBaseds ?? (_authRoleBaseds = AuthRoleBased.GetModuleAuthRoleBased(ModuleDefID))); }
        }



        #endregion


        #endregion

        #region Constrauctor

        public Module()
        {
        }

        public Module(int moduleID, int? cachetime, string container, DateTime? lastUpdate, int moduleDefID,
                      int? moduleOrder, string moduleTitle, string paneName, bool showinAllTab, string skinHTML,
                      int tabID, int updatePeriod)
        {
            ModuleID = moduleID;
            CacheTime = cachetime;
            Container = container;
            LastUpdate = lastUpdate;
            ModuleDefID = moduleDefID;
            ModuleOrder = moduleOrder;
            ModuleTitle = moduleTitle;
            PaneName = paneName;
            ShowinAllTab = showinAllTab;
            SkinHTML = skinHTML;
            TabID = tabID;
            UpdatePeriod = updatePeriod;
            ModuleDef = ModuleDef.GetSingleByID(moduleDefID);
        }

        #endregion

        #region MEthod

        #region Insttance metod:

        public string GetSettingValue(int settingid)
        {
            ModuleSetting setting = ModuleSettings.SingleOrDefault(o => o.SettingID == settingid);

            if (setting != null)
            {
                return ((setting.SettingValue == "") ? setting.DefaultValue : setting.SettingValue);
            }
            return "";
        }

        public bool HasObjPermission(int authId, int objId)
        {
            return HasObjPermission((int) ModuleDefID, authId, objId);
        }




        #endregion

        #region Static Method:

        public static int Add(int? cachetime, string container, DateTime? lastUpdate, int moduleDefID,
                              int? moduleOrder, string moduleTitle, string paneName, bool showinAllTab, string skinHTML,
                              int tabID, int updatePeriod)
        {
            return TModule.Add(cachetime, container, lastUpdate, moduleDefID,
                               moduleOrder, moduleTitle, paneName, showinAllTab, skinHTML,
                               tabID, updatePeriod);
        }


        public static bool Update(int moduleiD, int? cachetime, string container, DateTime? lastUpdate, int moduleDefID,
                                  int? moduleOrder, string moduleTitle, string paneName, bool showinAllTab,
                                  string skinHTML,
                                  int tabID, int updatePeriod)
        {
            return TModule.Update(moduleiD, cachetime, container, DateTime.Now, moduleDefID,
                                  moduleOrder, moduleTitle, paneName, showinAllTab, skinHTML,
                                  tabID, updatePeriod);
        }

        public static bool Delete(int moduleId)
        {
            return TModule.Delete(moduleId);
        }

        public static List<Module> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TModule.GetAll());
        }

        public static Module GetSingleByID(int moduleId)
        {
            return GetObjectFromDbObject(TModule.GetSingleByID(moduleId));
        }

        private static Module GetObjectFromDbObject(TModule module)
        {
            return new Module(module.ModuleID, module.CacheTime, module.Container, module.LastUpdate, module.ModuleDefID,
                              module.ModuleOrder, module.ModuleTitle, module.PaneName, module.ShowinAllTab,
                              module.SkinHTML, module.TabID, module.UpdatePeriod);
        }

        private static List<Module> GetCollectionObjectFromDbCollectionObject(IEnumerable<TModule> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<Module> GetModulesOfTab(int TabID)
        {
            return GetCollectionObjectFromDbCollectionObject(TModule.GetModulesOfTab(TabID));
        }

        //

        public bool HasNoPermission()
        {
            var mrol = ModuleRole.GetAll();
            mrol = mrol.Where(o => o.ModuleID == ModuleID).ToList();
            return Enumerable.All(mrol, t => !Role.IsInRoles(t.GetRolesOfModulToString()));
        }

        public bool HasDefinedPermission(int authId)
        {
            var obj = ModuleRoles.Where(o =>
                                                  ((o.ModuleID == ModuleID) &&
                                                   (o.AuthID == authId)));
            var b= obj.Aggregate(string.Empty, (current, moduleRole) => current + moduleRole.GetRolesOfModulToString());
            return ((obj != null) && Role.IsInRoles(b));
        }

        public static List<Module> GetModulesShowInAllTabs(int hometabid)
        {
            return GetCollectionObjectFromDbCollectionObject(TModule.GetModulesShowInAllTabs(hometabid));
        }

        public static DateTime GetLastUpdateDate()
        {
            return TModule.GetLastUpdateDate();
        }

        public static bool UpdateModulePane(int moduleId, string pan)
        {
            return TModule.UpdateModulePane(moduleId, pan);
        }
        public static bool UpdateModuleLastUpdateDate(int moduleId)
        {
            return TModule.UpdateModuleLastUpdateDate(moduleId);
        }

        public static bool UpdateModuleOrder(int moduleId, int order)
        {
            return TModule.UpdateModuleOrder(moduleId, order);
        }

        public static List<Module> GetTabModules(int tabId, string panName)
        {
            return GetCollectionObjectFromDbCollectionObject(TModule.GetTabModules(tabId, panName));
        }

        public static int AddModule(int tabId, int moduleOrder, int moduleDefId, string moduleTitle, string paneName,
                                    bool showInAllTabs)
        {
            return TModule.AddModule(tabId, moduleOrder, moduleDefId, moduleTitle, paneName, showInAllTabs);
        }

        public static bool UpdateModuleInfo(int moduleId, string moduletitle, int tabid, string container,
                                            string skinhtml, int updatePeriod, int cacheTime, bool showInAllPage)
        {
            return TModule.UpdateModuleInfo(moduleId, moduletitle, tabid, container,
                                            skinhtml, updatePeriod, cacheTime, showInAllPage);
        }

        /////////////////////////////
        //توابع اضافه شده
        //////////////////

        public static bool UpdateModuleTitle(int moduleId, string title)
        {
            return TModule.UpdateModuleTitle(moduleId, title);
        }

        public static bool HasObjPermission(int moduleDefId, int authId, int objId)
        {
            AuthObjBased obj = Enumerable.SingleOrDefault(AuthObjBased.GetModuleAuthObjBased(moduleDefId, objId),
                                                          o => (o.AuthID == authId));
            if (obj != null)
            {
                if (Role.IsInRoles(obj.AuthorizedRoles))
                {
                    return true;
                }
                if (
                    Enumerable.SingleOrDefault(obj.AuthorizedUsers,
                                               o => (o.UserID.ToString() == HttpContext.Current.User.Identity.Name)) !=
                    null)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasSpecifiedObjPermission(int authId)
        {
            return HasSpecifiedObjPermission(authId, 0);
        }

        public static bool HasSpecifiedObjPermission(int authId, int moduleId)
        {
            
            List<Role> list = Role.GetCollectionObjectFromDbCollectionObject(TRole.SpecifiedObjPermission(authId, moduleId));
            foreach (var role in list)
            {
                //if (HttpContext.Current.User.Identity.Name == role.["UserID"].ToString())
                //        {
                //            return true;
                //        }
            }
            
                        
                    
                
            
            return false;
        }


        #endregion

        #endregion

    }

    #endregion

    #region Class ModuleDefSetting:

    /// <summary>
    /// تنظیمات مختص به هر ماژول را در خود نگهداری می کند
    /// </summary>
    public class ModuleDefSetting
    {
        #region Field
        #endregion

        #region Properties

        #region Base

        public int SettingID { set; get; }

        public string DefValue { set; get; }

        public int ModuleDefID { set; get; }

        public string SettingName { set; get; }

        public string SettingValues { set; get; }

        #endregion

        #region Related

        public TModuleDef TModuleDef { set; get; }

        public IList<TPortal> ModuleDefInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctor

        public ModuleDefSetting()
        {
        }

        public ModuleDefSetting(int settingID, string defValue, int moduleDefID, string settingName, string settingValues, TModuleDef tModuleDef)
        {
            SettingID = settingID;
            DefValue = defValue;
            ModuleDefID = moduleDefID;
            SettingName = settingName;
            SettingValues = settingValues;
            TModuleDef = tModuleDef;
        }

        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return ModuleDefSetting.Add(DefValue, ModuleDefID, SettingName, SettingValues);
        }

        public bool Update()
        {
            return ModuleDefSetting.Update(SettingID, DefValue, ModuleDefID, SettingName, SettingValues);
        }

        public bool Delete()
        {
            return ModuleDefSetting.Delete(SettingID);
        }

        #endregion

        #region Static Method:

        public static int Add(string defValue, int moduleDefID, string settingName, string settingValues)
        {
            return TModuleDefSetting.Add(defValue, moduleDefID, settingName, settingValues);
        }

        public static bool Update(int settingID, string defValue, int moduleDefID, string settingName, string settingValues)
        {
            return TModuleDefSetting.Update(settingID, defValue, moduleDefID, settingName, settingValues);
        }

        public static bool Delete(int settingId)
        {
            return TModuleDefSetting.Delete(settingId);
        }

        public static List<ModuleDefSetting> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TModuleDefSetting.GetAll());
        }

        public static ModuleDefSetting GetSingleByID(int settingId)
        {
            return GetObjectFromDbObject(TModuleDefSetting.GetSingleByID(settingId));
        }

        private static ModuleDefSetting GetObjectFromDbObject(TModuleDefSetting moduleDefSetting)
        {
            return new ModuleDefSetting(moduleDefSetting.SettingID, moduleDefSetting.SettingValues,
                                        moduleDefSetting.ModuleDefID, moduleDefSetting.SettingName,
                                        moduleDefSetting.SettingValues, moduleDefSetting.TModuleDef);
        }

        private static List<ModuleDefSetting> GetCollectionObjectFromDbCollectionObject(IEnumerable<TModuleDefSetting> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }
        #endregion

        #endregion
    }

    #endregion

    #region Class ModuleSetting:

    /// <summary>
    /// کنترل مقداردهی تنظیمات اختصاصی ماژول ها
    /// تخصیص تنضیمات اختصاصی برای هر ماژول
    /// </summary>
    public class ModuleSetting
    {
        #region Field

        #endregion

        #region Properties

        #region Base

        public int MSettID { set; get; }

        public int ModuleID { set; get; }

        public int SettingID { set; get; }

        public string SettingValue { set; get; }

        public string DefaultValue
        {
            get
            {
                return TModuleDefSetting.GetDefulteSettingByID(SettingID);
            }
        }

        public string PossibleValues
        {
            get { return Setting.SettingValues; }
        }

        public string SettingName { get { return Setting.SettingName; }
        }

        #endregion

        #region Related

        public TModule TModule { set; get; }

        public IList<TPortal> ModuleDefInPortal = new List<TPortal>();

        public ModuleDefSetting Setting
        {
            get { return ModuleDefSetting.GetAll().First(o => o.SettingID == SettingID); }
        }

        #endregion

        #endregion

        #region Constrauctor

        public ModuleSetting()
        {
        }

        public ModuleSetting(int mSettID, int moduleID, int settingID, string settingValue, TModule tModule)
        {
            MSettID = mSettID;
            ModuleID = moduleID;
            SettingID = settingID;
            SettingValue = settingValue;
            TModule = tModule;
        }

        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return Add(ModuleID, SettingID, SettingValue);
        }

        public bool Update()
        {
            return Update(MSettID, ModuleID, SettingID, SettingValue);
        }

        public bool Delete()
        {
            return Delete(MSettID);
        }
        #endregion

        #region Static Method:

        public static int Add(int moduleID, int settingID, string settingValue)
        {
            return TModuleSetting.Add(moduleID, settingID, settingValue);
        }

        public static bool Update(int mSettID, int moduleID, int settingID, string settingValue)
        {
            return TModuleSetting.Update(mSettID, moduleID, settingID, settingValue);
        }

        public static bool Delete(int mSettId)
        {
            return TModuleSetting.Delete(mSettId);
        }

        public static bool Delete(int moduleID, int settingID)
        {
            return TModuleSetting.Delete(moduleID, settingID);
        }

        public static List<ModuleSetting> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TModuleSetting.GetAll());
        }

        public static ModuleSetting GetSingleByID(int mSettId)
        {
            return GetObjectFromDbObject(TModuleSetting.GetSingleByID(mSettId));
        }

        private static ModuleSetting GetObjectFromDbObject(TModuleSetting moduleSetting)
        {
            return new ModuleSetting(moduleSetting.MSettID, moduleSetting.ModuleID, moduleSetting.SettingID,
                                     moduleSetting.SettingValue, moduleSetting.TModule);
        }

        private static List<ModuleSetting> GetCollectionObjectFromDbCollectionObject(IEnumerable<TModuleSetting> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<ModuleSetting> GetModuleSettingsByModuleId(int moduleID)
        {
            return GetCollectionObjectFromDbCollectionObject(TModuleSetting.GetModuleSettingsByModuleId(moduleID));
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class ModuleRole:

    /// <summary>
    /// کنترل مقداردهی نقشهای اختصاصی ماژول ها
    ///  تخصیص نقشهای اختصاصی برای هر ماژول
    /// </summary>
    public class ModuleRole
    {
        #region Field
        #endregion

        #region Properties

        #region Base

        public int ModuleRoleID { set; get; }

        public int AuthID { set; get; }

        public int ModuleID { set; get; }

        public int RoleID { set; get; }

        #endregion

        #region Related

        public TAuthRoleBased AuthRoleBased { set; get; }

        public TModule tModule { set; get; }

        public TRole tRole { set; get; }

        public IList<TPortal> ModuleDefInPortal = new List<TPortal>();

        #endregion

        #endregion

        #region Constrauctor
        public ModuleRole()
        {
        }

        public ModuleRole(int moduleRoleID, int authID, int moduleID, int roleID, TAuthRoleBased tAuthRoleBased, TModule tmodule, TRole trole)
        {
            ModuleRoleID = moduleRoleID;
            AuthID = authID;
            ModuleID = moduleID;
            RoleID = roleID;
            AuthRoleBased = tAuthRoleBased;
            tModule = tmodule;
            tRole = trole;
        }
        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return ModuleRole.Add(AuthID, ModuleID, RoleID);
        }

        public bool Update()
        {
            return ModuleRole.Update(ModuleRoleID, AuthID, ModuleID, RoleID);
        }

        public bool Delete()
        {
            return ModuleRole.Delete(ModuleRoleID);
        }

        public string GetRolesOfModulToString()
        {
            return GetRolesOfModulToString(ModuleID, AuthID);

        }
        #endregion

        #region Static Method:

        public static int Add(int authID, int moduleID, int roleID)
        {

            return TModuleRole.Add(authID, moduleID, roleID);
        }

        public static bool Update(int moduleRoleID, int authID, int moduleID, int roleID)
        {
            return TModuleRole.Update(moduleRoleID, authID, moduleID, roleID);
        }

        public static bool Delete(int moduleId, int roleId, int authId)
        {
            return TModuleRole.Delete(moduleId, roleId, authId);
        }

        public static bool Delete(int moduleRoleId)
        {
            return TModuleRole.Delete(moduleRoleId);
        }

        public static List<ModuleRole> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TModuleRole.GetAll());
        }

        public static ModuleRole GetSingleByID(int moduleRoleId)
        {
            return GetObjectFromDbObject(TModuleRole.GetSingleByID(moduleRoleId));
        }

        private static ModuleRole GetObjectFromDbObject(TModuleRole moduleRole)
        {
            var t = new ModuleRole(moduleRole.ModuleRoleID, moduleRole.AuthID, moduleRole.ModuleID, moduleRole.RoleID, moduleRole.TAuthRoleBased, moduleRole.TModule, moduleRole.TRole);
            return t;
        }

        private static List<ModuleRole> GetCollectionObjectFromDbCollectionObject(IEnumerable<TModuleRole> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static string GetRolesOfModulToString(int moduleId, int authId)
        {
            return
                GetAll().Where(o => o.ModuleID == moduleId && o.AuthID == authId).Aggregate
                    <ModuleRole, string>(null, (current, moduleRole) => current + (moduleRole.RoleID + ";"));

        }

        public static List<ModuleRole> GetModuleRoles(int moduleID)
        {
            return GetAll().Where(o => o.ModuleID == moduleID).ToList();
        }

        ///////////////////////توابع اضافه شده
        ///////////////

        public static List<Role> GetRolesOfModule(int moduleId, int authId)
        {
            return Role.GetCollectionObjectFromDbCollectionObject(TRole.GetModuleRoles(moduleId, authId));
        }


        #endregion

        #endregion

    }

    #endregion

}
