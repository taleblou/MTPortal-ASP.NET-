using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;

namespace PayaDB
{

    public partial class TLanguage
    {

        public TLanguage()
        {
        }

        #region Properties:

        [Telerik.OpenAccess.FieldAlias("languageID")]
        public int LanguageID
        {
            get { return languageID; }
            set { this.languageID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("calendarLanguage")]
        public string CalendarLanguage
        {
            get { return calendarLanguage; }
            set { this.calendarLanguage = value; }
        }

        [Telerik.OpenAccess.FieldAlias("culture")]
        public string Culture
        {
            get { return culture; }
            set { this.culture = value; }
        }

        [Telerik.OpenAccess.FieldAlias("direction")]
        public bool Direction
        {
            get { return direction; }
            set { this.direction = value; }
        }

        [Telerik.OpenAccess.FieldAlias("enabled")]
        public bool Enabled
        {
            get { return enabled; }
            set { this.enabled = value; }
        }

        [Telerik.OpenAccess.FieldAlias("homeTabName")]
        public string HomeTabName
        {
            get { return homeTabName; }
            set { this.homeTabName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("languageName")]
        public string LanguageName
        {
            get { return languageName; }
            set { this.languageName = value; }
        }

        #endregion

        #region Method


        public static int Add(string calendarLanguage, string culture, bool direction, bool enabled,
                        string homeTabName, string languageName)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                scope.Transaction.Begin();
                var o = new TLanguage
                {
                    CalendarLanguage = calendarLanguage,
                    Culture = culture,
                    Direction = direction,
                    Enabled = enabled,
                    HomeTabName = homeTabName,
                    LanguageName = languageName
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

        public static bool Update(int languageID, string calendarLanguage, string culture, bool direction, bool enabled,
                        string homeTabName, string languageName)
        {
            IObjectScope scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TLanguage>().Single(emp => emp.LanguageID == languageID);
                if (o != null)
                {
                    scope.Transaction.Begin();
                    o.CalendarLanguage = calendarLanguage;
                    o.Culture = culture;
                    o.Direction = direction;
                    o.Enabled = enabled;
                    o.HomeTabName = homeTabName;
                    o.LanguageName = languageName;
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

        public static bool Delete(int languageId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                var o = scope.Extent<TLanguage>().Single(emp => emp.LanguageID == languageId);
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

        public static List<TLanguage> GetAll()
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TLanguage>().ToList();
        }

        public static TLanguage GetSingleByID(int languageId)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TLanguage>().SingleOrDefault(o => o.LanguageID == languageId);
        }

        ///////////////////////////////////
        public static TLanguage GetSingleLangaugeByCultureName(string cultureName)
        {
            var scope = PayaScopeProvider1.GetNewObjectScope();
            return scope.Extent<TLanguage>().SingleOrDefault(o => o.Culture == cultureName);
        }

        #endregion
    }
}
