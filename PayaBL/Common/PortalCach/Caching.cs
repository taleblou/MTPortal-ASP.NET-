using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml.Linq;
using PayaBL.Classes;

namespace PayaBL.Common.PortalCach
{
    public class Caching
    {
        // Methods
        public static void CacheLanguages(List<PortalLanguage> languages)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_PortalLanguage");
            HttpContext.Current.Cache.Insert("LanguagesCache", languages, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CacheModules(List<Module> modules)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_Module");
            HttpContext.Current.Cache.Insert("ModulesCache", modules, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CacheModulesAuthRoleBased(List<AuthRoleBased> modules)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_AuthRoleModuleDef");
            HttpContext.Current.Cache.Insert("ModulesRBATCache", modules, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CacheModulesRoles(List<ModuleRole> modulesRoles)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_ModuleRole");
            HttpContext.Current.Cache.Insert("ModulesRolesCache", modulesRoles, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CacheModulesSettings(List<ModuleSetting> modules)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_ModuleSettings");
            HttpContext.Current.Cache.Insert("ModulesSettingsCache", modules, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CachePortals(List<Portal> portals)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_Portals");
            HttpContext.Current.Cache.Insert("PortalsCache", portals, sqlDependency, DateTime.Now.AddDays(1.0),
                                             Cache.NoSlidingExpiration);
        }

        public static void CachePortalsSettings(DataTable portalsSettings)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_PortalSettings");
            HttpContext.Current.Cache.Insert("PortalsSettingsCache", portalsSettings, sqlDependency,
                                             DateTime.Now.AddDays(1.0), Cache.NoSlidingExpiration);
        }

        public static void CacheTabs(List<Tab> tabs, int portalid, int langId)
        {
            SqlCacheDependency sqlDependency = new SqlCacheDependency("PortalDBCache", "t_Tab");
            HttpContext.Current.Cache.Insert(GetTabsCacheName(portalid, langId), tabs, sqlDependency,
                                             DateTime.Now.AddDays(1.0), Cache.NoSlidingExpiration);
        }

        public static void Clear()
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

        public static void DeleteLanguagesCache()
        {
            HttpContext.Current.Cache.Remove("PortalLanguage");
        }

        public static void DeleteModulesAuthRoleBasedCache()
        {
            HttpContext.Current.Cache.Remove("ModulesRBATCache");
        }

        public static void DeleteModulesCache()
        {
            HttpContext.Current.Cache.Remove("ModulesCache");
        }

        public static void DeleteModulesRolesCache()
        {
            HttpContext.Current.Cache.Remove("ModulesRolesCache");
        }

        public static void DeleteModulesSettingsCache()
        {
            HttpContext.Current.Cache.Remove("ModulesSettingsCache");
        }

        public static void DeletePortalsCache()
        {
            HttpContext.Current.Cache.Remove("PortalsCache");
        }

        public static void DeletePortalsSettingdCache()
        {
            HttpContext.Current.Cache.Remove("PortalsSettingsCache");
        }

        public static object Get(string key)
        {
            return HttpContext.Current.Cache[key];
        }

        public static List<PortalLanguage> GetCachedLanguages()
        {
            return (HttpContext.Current.Cache["PortalLanguageCache"] as List<PortalLanguage>);
        }

        public static List<Module> GetCachedModules()
        {
            return (HttpContext.Current.Cache["ModulesCache"] as List<Module>);
        }

        public static List<AuthRoleBased> GetCachedModulesAuthRoleBased()
        {
            return (HttpContext.Current.Cache["ModulesRBATCache"] as List<AuthRoleBased>);
        }

        public static List<ModuleRole> GetCachedModulesRoles()
        {
            return (HttpContext.Current.Cache["ModulesRolesCache"] as List<ModuleRole>);
        }

        public static List<ModuleSetting> GetCachedModulesSettings()
        {
            return (HttpContext.Current.Cache["ModulesSettingsCache"] as List<ModuleSetting>);
        }

        public static List<Portal> GetCachedPortals()
        {
            return (HttpContext.Current.Cache["PortalsCache"] as List<Portal>);
        }

        public static DataTable GetCachedPortalsSettings()
        {
            return (HttpContext.Current.Cache["PortalsSettingsCache"] as DataTable);
        }

        public static XDocument GetCachedSettingsXml()
        {
            string path =
                HttpContext.Current.Server.MapPath(PayaTools.SetStyle(HttpContext.Current.User.Identity.Name, false) +
                                                   "/Setting.xml");
            string key = path.Substring(path.IndexOf("Styles") + 7);
            XDocument xdoc = (XDocument) Get(key);
            if (xdoc == null)
            {
                xdoc = XDocument.Load(path);
                Insert(key, xdoc, new CacheDependency(path), DateTime.Now.AddDays(1.0));
            }
            return xdoc;
        }


        public static XDocument GetCachedStyleXml()
        {
            string path =
                HttpContext.Current.Server.MapPath(PayaTools.SetStyle(HttpContext.Current.User.Identity.Name, false) +
                                                   "/../Styles.xml");
            string key = path.Substring(path.IndexOf("Styles") + 7);
            XDocument xdoc = (XDocument) Get(key);
            if (xdoc == null)
            {
                xdoc = XDocument.Load(path);
                Insert(key, xdoc, new CacheDependency(path), DateTime.Now.AddDays(1.0));
            }
            return xdoc;
        }

        public static List<Tab> GetCachedTabs(int portalid, int langId)
        {
            return (HttpContext.Current.Cache[GetTabsCacheName(portalid, langId)] as List<Tab>);
        }

        public static string GetTabsCacheName(int portalid, int langId)
        {
            return string.Concat(new object[] { "PortalTabsDS_", portalid, "_", langId });
        }

        public static void Insert(string key, object obj, CacheDependency dep, DateTime time)
        {
            if (obj != null)
            {
                HttpContext.Current.Cache.Insert(key, obj, dep, time, Cache.NoSlidingExpiration);
            }
        }

        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                HttpContext.Current.Cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = HttpContext.Current.Cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
                }
            }
        }

    }
}
