using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{
    public partial class TModuleDefSetting
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModuleDefSetting()
        {
        }

        #region Properties
        [Telerik.OpenAccess.FieldAlias("settingID")]
        public int SettingID
        {
            get { return settingID; }
            set { this.settingID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("defValue")]
        public string DefValue
        {
            get { return defValue; }
            set { this.defValue = value; }
        }

        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingName")]
        public string SettingName
        {
            get { return settingName; }
            set { this.settingName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("settingValues")]
        public string SettingValues
        {
            get { return settingValues; }
            set { this.settingValues = value; }
        }

        [Telerik.OpenAccess.FieldAlias("tModuleDef")]
        public TModuleDef TModuleDef
        {
            get { return tModuleDef; }
            set { this.tModuleDef = value; }
        }

        #endregion

        #region Method

        public static int Add(string defValue, int moduleDefID, string settingName, string settingValues)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TModuleDefSetting
                {
                    DefValue = defValue,
                    ModuleDefID = moduleDefID,
                    SettingName = settingName,
                    SettingValues = settingValues
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

        public static bool Update(int settingID, string defValue, int moduleDefID, string settingName, string settingValues)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModuleDefSetting>().Single(emp => emp.SettingID == settingID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.SettingID = settingID;
                    o.DefValue = defValue;
                    o.ModuleDefID = moduleDefID;
                    o.SettingName = settingName;
                    o.SettingValues = settingValues;
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

        public static bool Delete(int settingId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TModuleDefSetting>().Single(emp => emp.SettingID == settingId);
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

        public static IEnumerable<TModuleDefSetting> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleDefSetting>().ToList();
        }

        public static TModuleDefSetting GetSingleByID(int settingId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleDefSetting>().SingleOrDefault(o => o.SettingID == settingId);

        }

        public static string GetDefulteSettingByID(int settingID)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TModuleDefSetting>().Single(o => o.SettingID == settingID).DefValue;
        }

        #endregion

    }
}
