using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    #region Class Language:

    /// <summary>
    /// 
    /// </summary>
    public class Language
    {
        #region enum

        public enum CalendarType
        {
            PersianCalendar,
            HijriCalendar,
            GregorianCalendar
        }

        #endregion

        #region Fields :

        #endregion

        #region Properties :

        #region Base:

        public int LanguageID { set; get; }

        public string CalendarLanguage { set; get; }

        public string Culture { set; get; }

        public bool Direction { set; get; }

        public bool Enabled { set; get; }

        public string HomeTabName { set; get; }

        public string LanguageName { set; get; }

        #endregion

        #region Related :
        #endregion

        #endregion

        #region Constrauctors :

        public Language()
        {
        }

        public Language(int languageID, string calendarLanguage, string culture, bool direction, bool enabled,
                        string homeTabName, string languageName)
        {
            LanguageID = languageID;
            CalendarLanguage = calendarLanguage;
            Culture = culture;
            Direction = direction;
            Enabled = enabled;
            HomeTabName = homeTabName;
            LanguageName = languageName;
        }
        #endregion

        #region Methods :

        #region Instance Methods :

        public int Add()
        {
            return Language.Add(CalendarLanguage, Culture, Direction, Enabled, HomeTabName, LanguageName);
        }

        public bool Update()
        {
            return Language.Update(LanguageID, CalendarLanguage, Culture, Direction, Enabled, HomeTabName, LanguageName);
        }

        public bool Delete()
        {
            return Language.Delete(LanguageID);
        }
        #endregion

        #region Static Methods :

        public static int Add(string calendarLanguage, string culture, bool direction, bool enabled,
                        string homeTabName, string languageName)
        {
            return TLanguage.Add(calendarLanguage, culture, direction, enabled,
                                 homeTabName, languageName);
        }

        public static bool Update(int languageID, string calendarLanguage, string culture, bool direction, bool enabled,
                        string homeTabName, string languageName)
        {
            return TLanguage.Update(languageID, calendarLanguage, culture, direction, enabled,
                                    homeTabName, languageName);
        }

        public static bool Delete(int languageId)
        {
            return TLanguage.Delete(languageId);
        }

        public static List<Language> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TLanguage.GetAll());
        }

        public static Language GetSingleByID(int languageId)
        {
            return GetObjectFromDbObject(TLanguage.GetSingleByID(languageId));
        }

        public static Language GetObjectFromDbObject(TLanguage Language)
        {
            return new Language(Language.LanguageID, Language.CalendarLanguage, Language.Culture, Language.Direction,
                                Language.Enabled, Language.HomeTabName, Language.LanguageName);
        }

        public static List<Language> GetCollectionObjectFromDbCollectionObject(IEnumerable<TLanguage> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        ////////////////////////////
        public static Language GetSingleLangaugeByCultureName(string cultureName)
        {
            return GetObjectFromDbObject(TLanguage.GetSingleLangaugeByCultureName(cultureName));
        }

        #endregion

        #endregion

    }

    #endregion
}
