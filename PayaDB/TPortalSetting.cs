using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TPortalSetting
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TPortalSetting()
        {
        }


        #region Properties:
        [Telerik.OpenAccess.FieldAlias("portalID")]
        public int PortalID
        {
            get { return portalID; }
            set { this.portalID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingName")]
        public string SettingName
        {
            get { return settingName; }
            set { this.settingName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingValue")]
        public string SettingValue
        {
            get { return settingValue; }
            set { this.settingValue = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tPortal")]
        public TPortal TPortal
        {
            get { return tPortal; }
            set { this.tPortal = value; }
        }

        #endregion

        #region Method

        public static int Add(string settingName, string settingValue)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TPortalSetting
                {
                    SettingName = settingName,
                    SettingValue = settingValue
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

        public static bool Update(int portalID, string settingName, string settingValue)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TPortalSetting>().Single(emp => emp.PortalID == portalID && emp.SettingName == settingName);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.PortalID = portalID;
                    o.SettingName = settingName;
                    o.SettingValue = settingValue;
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
                var o = scope.Extent<TPortalSetting>().Single(emp => emp.PortalID == portalId);
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

        public static IEnumerable<TPortalSetting> GetAll(int portalId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TPortalSetting>().Where(o => o.PortalID == portalId).ToList();
        }

        public static TPortalSetting GetSingleByID(int portalId, string SettingName)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TPortalSetting>().SingleOrDefault(o => o.PortalID == portalId && o.SettingName == SettingName);
        }
    
        public static bool UpdatePortalSettings(int portalId, string settingName, string settingValue)
        {
            var t = GetAll(portalId).ToList();
            if (t.SingleOrDefault(o=>o.SettingName==settingName)==null)
            {
                var scope = PayaScopeProvider1.GetNewObjectScope();
                try
                {
                    scope.Transaction.Begin();
                    var o = new TPortalSetting
                                {
                                    PortalID = portalId,
                                    SettingName = settingName,
                                    SettingValue = settingValue
                                };
                    scope.Add(o);
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
            else
            {
                return Update(portalId, settingName, settingValue );
            }
        }

        #endregion
    }
}
