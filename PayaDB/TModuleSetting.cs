using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TModuleSetting
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModuleSetting()
        {
        }

        #region Properties

        [Telerik.OpenAccess.FieldAlias("mSettID")]
        public int MSettID
        {
            get { return mSettID; }
            set { this.mSettID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleID")]
        public int ModuleID
        {
            get { return moduleID; }
            set { this.moduleID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingID")]
        public int SettingID
        {
            get { return settingID; }
            set { this.settingID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingValue")]
        public string SettingValue
        {
            get { return settingValue; }
            set { this.settingValue = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tModule")]
        public TModule TModule
        {
            get { return tModule; }
            set { this.tModule = value; }
        }

        #endregion

        #region Method
        public static int Add(int moduleID, int settingID, string settingValue)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TModuleSetting
                {
                    ModuleID = moduleID,
                    SettingID = settingID,
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

        public static bool Update(int mSettID, int moduleID, int settingID, string settingValue)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModuleSetting>().Single(emp => emp.MSettID == mSettID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.ModuleID = moduleID;
                    o.SettingID = settingID;
                    o.settingValue = settingValue;
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

        public static bool Delete(int mSettId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModuleSetting>().Single(emp => emp.MSettID == mSettId);
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

        public static IEnumerable<TModuleSetting> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleSetting>().ToList();
        }

        public static TModuleSetting GetSingleByID(int mSettId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleSetting>().Single(o => o.MSettID == mSettId);
        }

        public static IEnumerable<TModuleSetting> GetModuleSettingsByModuleId(int moduleID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            var t =TModuleDefSetting.GetAll().Where(o => o.ModuleDefID == TModule.GetSingleByID(moduleID).ModuleDefID).ToList();
            var temp = GetAll().Where(emp => emp.ModuleID== moduleID).ToList();
            if (t.Count>temp.Count)
            {
                foreach (var moduleDefSetting in t)
                {
                    if (temp.SingleOrDefault(o => o.SettingID == moduleDefSetting.SettingID)== null)
                        temp.Add(new TModuleSetting
                                     {
                                         ModuleID = moduleID,
                                         SettingID = moduleDefSetting.SettingID,
                                         SettingValue = moduleDefSetting.DefValue
                                     });
                }
            }
            return temp;
        }

        public static bool Delete(int modulId, int settingId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModuleSetting>().Single(emp => emp.MSettID == modulId&&emp.SettingID==settingId);
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

        #endregion



       
    }
}
