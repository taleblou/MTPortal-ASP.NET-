using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class Portal:

    /// <summary>
    /// 
    /// </summary>
    public class Portal
    {
        #region enum

        public enum SettingsName
        {
            DefaultCulture,
            DefaultStyle,
            EmailAddress,
            LoginAction,
            LoginAttempts,
            Monitoring,
            ShowRegisterCaptcha
        }

        #endregion

        #region Fields :

        #endregion

        #region Properties :

        #region Base:

        public int PortalID { set; get; }

        public string PortalAlias { set; get; }

        public string PortalName { set; get; }

        public string PortalPath { set; get; }

        #endregion

        #region Related :

        public List<PortalSetting> Settings { set; get; }

        #endregion

        #endregion

        #region Constrauctors :
        public Portal()
        {
        }

        public Portal(int portalID, string portalAlias, string portalName, string portalPath)
        {
            PortalID = portalID;
            PortalAlias = portalAlias;
            PortalName = portalName;
            PortalPath = portalPath;
            Settings = PortalSetting.GetAll(portalID);
        }
        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return Add(PortalAlias, PortalName, PortalPath);
        }

        public bool Update()
        {
            return Update(PortalID, PortalAlias, PortalName, PortalPath);
        }

        public bool Delete()
        {
            return Delete(PortalID);
        }
        #endregion

        #region Static Methods :

        public static int Add(string portalAlias, string portalName, string portalPath)
        {
            return TPortal.Add(portalAlias, portalName, portalPath);
        }

        public static bool Update(int portalID, string portalAlias, string portalName, string portalPath)
        {
            return TPortal.Update(portalID, portalAlias, portalName, portalPath);
        }

        public static bool Delete(int portalId)
        {
            return TPortal.Delete(portalId);
        }

        public static List<Portal> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TPortal.GetAll());
        }

        public static Portal GetSingleByID(int portalId)
        {
            return GetObjectFromDbObject(TPortal.GetSingleByID(portalId));
        }

        public static Portal GetObjectFromDbObject(TPortal portal)
        {
            return new Portal(portal.PortalID, portal.PortalAlias, portal.PortalName, portal.PortalPath);
        }

        public static List<Portal> GetCollectionObjectFromDbCollectionObject(IEnumerable<TPortal> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        ////////////////////////////////////////////

        public static Dictionary<string, string> GetPortalSettings(int portalId)
        {
            var li = PortalSetting.GetAll(portalId);
            return li.ToDictionary(portalSetting => portalSetting.SettingName, portalSetting => portalSetting.SettingValue);
        }

        public static Portal GetSingleByPortalAlias(string portalAlias)
        {
            return GetObjectFromDbObject(TPortal.GetSingleByPortalAlias(portalAlias));
        }

        /////////////////
        //توابع اضافه شده
        /////////////

        public static bool UpdatePortalSettings(int portalId, string settingName, string settingValue)
        {
            return TPortalSetting.UpdatePortalSettings(portalId, settingName, settingValue);
        }

        public static DataTable AllPortalStyles()
        {
            DataTable dt = new DataTable("PortalStyles");
            dt.Columns.Add("Style"); 
            XDocument doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/UI/Styles/Styles.xml"));
            var langs = Language.GetAll();
            foreach (Language language in langs)
            {
                dt.Columns.Add(language.Culture);
            }
            //dt.Columns.Add("En");
            //dt.Columns.Add("Fa");
            try
            {
                var t =doc.Descendants("Style").ToList();
                foreach (var xElement in t)
                {
                    int elementAttCount =  xElement.Attributes().ToList().Count;
                    var dc = new object[elementAttCount];
                    var li = new List<DataColumn>();

                   // foreach (var att in xElement.Attributes())
                    {
                        for (int i = 0; i < elementAttCount; i++)
                        {
                            dc[i] = xElement.Attributes().ToList()[i].Value;
                        }
                    
                        
                      // li.Add(new DataColumn(element.Name.ToString()){DefaultValue=element.Value});
                    }
                    dt.Rows.Add(dc);

                }
                //var query = from element in doc.Descendants("Style")
                //            select
                //                new
                //                    {
                //                        folder = element.Attribute("folder"),
                //                        En = element.Attribute("en-US"),
                //                        Fa = element.Attribute("fa-IR")
                //                    };
                //foreach (var item in query)
                //{
                //    dt.Rows.Add(new object[] { item.folder.Value, item.En.Value, item.Fa.Value });
                //}
            }
            catch
            {
            }
            return dt;
        }

 

 


        public static bool Delete(string portalAlias)
        {
            return TPortal.Delete(portalAlias);
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class PortalLanguage:

    /// <summary>
    /// 
    /// </summary>
    public class PortalLanguage
    {
        #region Fields :
        #endregion.

        #region Properties :

        #region Base:

        public int ID { set; get; }

        public bool Enabled { set; get; }

        public int HomeTabID { set; get; }

        public int LanguageID { set; get; }

        public int PortalID { set; get; }

        #endregion

        #region Related :

        private TLanguage _language { set; get; }
    
        private Language Language
        {
            
            get { return Language.GetObjectFromDbObject(_language); }
        }

        private TPortal TPortal { set; get; }
        public Portal Portal { get { return Portal.GetObjectFromDbObject(TPortal); } }

        #endregion

        #endregion

        #region Constrauctors :
        public PortalLanguage()
        {
        }

        public PortalLanguage(int id, bool enabled, int homeTabIDI, int languageID, int portalID)
        {
            ID = id;
            Enabled = enabled;
            HomeTabID = homeTabIDI;
            LanguageID = languageID;
            PortalID = portalID;
        }
        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return PortalLanguage.Add(Enabled, HomeTabID, LanguageID, PortalID);
        }

        public bool Update()
        {
            return PortalLanguage.Update(ID, Enabled, HomeTabID, LanguageID, PortalID);
        }

        public bool Delete()
        {
            return PortalLanguage.Delete(ID);
        }
        #endregion

        #region Static Methods :

        public static int Add(bool enabled, int homeTabIDI, int languageID, int portalID)
        {
            return TPortalLanguage.Add(enabled, homeTabIDI, languageID, portalID);
        }

        public static bool Update(int id, bool enabled, int homeTabIDI, int languageID, int portalID)
        {
            return TPortalLanguage.Update(id, enabled, homeTabIDI, languageID, portalID);
        }

        public static bool Delete(int id)
        {
            return TPortalLanguage.Delete(id);
        }

        public static List<PortalLanguage> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TPortalLanguage.GetAll());
        }

        public static PortalLanguage GetSingleByID(int portalId)
        {
            return GetObjectFromDbObject(TPortalLanguage.GetSingleByID(portalId));
        }

        public static PortalLanguage GetObjectFromDbObject(TPortalLanguage portalLanguage)
        {
            return new PortalLanguage(portalLanguage.Id, portalLanguage.Enabled, portalLanguage.HomeTabID, portalLanguage.LanguageID, portalLanguage.PortalID);
        }

        public static List<PortalLanguage> GetCollectionObjectFromDbCollectionObject(IEnumerable<TPortalLanguage> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        /// <summary>
        /// دوباره باید باز نویسی شود 
        /// با توجه به یک کالچر یک زبان پورتال بر می گرداند
        /// </summary>
        /// <param name="currentCulture"></param>
        /// <param name="portalId"></param>
        /// <returns></returns>
        public static PortalLanguage GetLanguagePortalByCulture(string currentCulture, int portalId)
        {
            return GetObjectFromDbObject(TPortalLanguage.GetLanguagePortalByCulture(currentCulture, portalId));
        }

        public static List<PortalLanguage> GetAllEnabledLanguagePortal()
        {
            return GetCollectionObjectFromDbCollectionObject(TPortalLanguage.GetAllEnabledLanguagePortal());
        }

        #endregion

        #endregion

    }

    #endregion

    #region Class PortalSetting:

    /// <summary>
    /// 
    /// </summary>
    public class PortalSetting
    {
        #region Fields :

        private const bool IsCms = false;
        public Portal portal;

        #endregion

        #region Properties :

        #region Base:
        //==============================
        public static string DefaultCulture
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.Settings.Single(o => o.SettingName == "DefaultCulture").SettingValue;
            }
        }

        public static string DefaultPortal
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultPortal"];
            }
        }

        public static string DefaultStyle
        {
            get
            {
                var ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.Settings.Single(o => o.SettingName == "DefaultStyle").SettingValue;
            }
        }

        public static string EmailAddress
        {
            get
            {
                var ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.Settings.Single(o => o.SettingName == "EmailAddress").SettingValue;
            }
        }

        public static string FullPortalPath
        {
            get
            {
                return (PortalPath + ((LocalPortalPath == "") ? "" : ("/" + LocalPortalPath)));
            }
        }

        public static bool IsCMS
        {
            get
            {
                return false;
            }
        }

        public static string LocalPortalPath
        {
            get
            {
                var ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.PortalPath;
            }
        }

        //public static string LogConnectionString
        //{
        //    get
        //    {
        //        if (SqlDataHelper.GetConnectionString("LogConnectionString") != null)
        //        {
        //            return SqlDataHelper.GetConnectionString("LogConnectionString");
        //        }
        //        return SqlDataHelper.GetConnectionString("MSSQLConnectionString");
        //    }
        //}

        public static string LogEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["LogEmailAddress"];
            }
        }

        public static bool LogException
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["LogErrors"]);
            }
        }

        public static LoginAction LoginAction
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ((ps.portal.Settings.Find(o => o.SettingName == "LoginAction").ToString() == "1") ? LoginAction.ShowCaptcha : LoginAction.LockUser);
            }
        }

        public static int LoginAttempts
        {
            get
            {
                var ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return int.Parse(ps.portal.Settings.Find(o => o.SettingName == "LoginAttempts").SettingValue);
            }
        }

        public static bool Monitoring
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return (ps.portal.Settings.Find(o => o.SettingName == "Monitoring").ToString() == "on");
            }
        }

        //public static string MonitoringConnectionString
        //{
        //    get
        //    {
        //        if (SqlDataHelper.GetConnectionString("MonitoringConnectionString") != null)
        //        {
        //            return SqlDataHelper.GetConnectionString("MonitoringConnectionString");
        //        }
        //        return SqlDataHelper.GetConnectionString("MSSQLConnectionString");
        //    }
        //}

        public static string PortalAlias
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.PortalAlias;
            }
        }

        public static int PortalId
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.PortalID;
            }
        }

        public static string PortalName
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.PortalName;
            }
        }

        public static string PortalPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PortalPath"];
            }
        }

        public static string PortalVersion
        {
            get
            {
                string str = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                str = str.Remove(str.LastIndexOf("."));
                return ("Paya Portal - V " + str);
            }
        }

        public static List<PortalSetting> Settings
        {
            get
            {
                var ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return ps.portal.Settings;
            }
        }

        public static bool ShowRegisterCaptcha
        {
            get
            {
                PortalSetting ps = (PortalSetting)HttpContext.Current.Items["PortalSettings"];
                return (ps.portal.Settings.Find(o => o.SettingName == "ShowRegisterCaptcha").ToString() == "on");
            }
        }

        public static bool SingleUserBase
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["SingleUserBase"]);
            }
        }

        public static bool UserMustChangePasswordOnFirstLogin
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["UserMustChangePasswordOnFirstLogin"]);
            }
        }

        public static bool WhiteSpaceFilter
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["WhiteSpaceFilter"]);
            }
        }


        //=============================

        public int PortalID { set; get; }

        public string SettingName { set; get; }

        public string SettingValue { set; get; }


        #endregion

        #region Related :

        public TPortal TPortal { set; get; }

        #endregion

        #endregion

        #region Constrauctors :

        public PortalSetting()
        {
        }

        public PortalSetting(int portalID, string settingName, string settingValue)
        {
            PortalID = portalID;
            SettingName = settingName;
            SettingValue = settingValue;
        }

        public PortalSetting(string portalAlias)
        {
            portal = Portal.GetSingleByPortalAlias(portalAlias);
        }


        #endregion

        #region Methods :

        #region Instance Methods :
        public int Add()
        {
            return Add(SettingName, SettingValue);
        }

        public bool Update()
        {
            return Update(PortalID, SettingName, SettingValue);
        }

        public bool Delete()
        {
            return Portal.Delete(PortalID);
        }
        #endregion

        #region Static Methods :


        public static int Add(string settingName, string settingValue)
        {
            return TPortalSetting.Add(settingName, settingValue);
        }

        public static bool Update(int portalID, string settingName, string settingValue)
        {
            return TPortalSetting.Update(portalID, settingName, settingValue);
        }

        public static bool Delete(int id)
        {
            return TPortalSetting.Delete(id);
        }

        public static List<PortalSetting> GetAll(int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TPortalSetting.GetAll(portalId));
        }

        public static PortalSetting GetSingleById(int portalId, string SettingName)
        {
            return GetObjectFromDbObject(TPortalSetting.GetSingleByID(portalId, SettingName));
        }

        private static PortalSetting GetObjectFromDbObject(TPortalSetting portalSetting)
        {
            return new PortalSetting(portalSetting.PortalID, portalSetting.SettingName, portalSetting.SettingValue);
        }

        private static List<PortalSetting> GetCollectionObjectFromDbCollectionObject(IEnumerable<TPortalSetting> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }


        //////////////////////////////////

        public static bool UpdatePortalSettings(int portalId, string settingName, string settingValue)
        {
            return TPortalSetting.UpdatePortalSettings(portalId, settingName, settingValue);
        }

        #endregion

        #endregion

    }

    #endregion
}
