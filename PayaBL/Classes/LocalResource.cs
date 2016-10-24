using System.Collections.Generic;
using System.Linq;
using PayaBL.Common;
using PayaDB;

namespace PayaBL.Classes
{
    public class LocalResource
    {
        #region Field

        private Language _language;
        public const string CachLocalResource = "AllLocalResource";
        public const string CachLocalResourceLang = "AllLocalResourceLang{0}";

        #endregion

        #region Properties

        #region Base

        public int Id { get; set; }

        public int LanguageId { get; set; }

        public string ResourceName { get; set; }

        public string ResourceValue { get; set; }

        #endregion

        #region Related

        public Language language
        {
            get
            {
                return (_language ?? (_language = Language.GetSingleByID(LanguageId)));
            }
        }

        #endregion

        #endregion

        #region Constrauctor
        public LocalResource()
        {
        }

        public LocalResource(int id, int lanuageId, string resourceName, string resourceValue)
        {
            Id = id;
            LanguageId = lanuageId;
            ResourceName = resourceName;
            ResourceValue = resourceValue;
        }

        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return AddLocaleResource(LanguageId, ResourceName, ResourceValue);
        }

        public bool Delete()
        {
            return DeleteLocaleResource(this.Id);
        }

        public bool Update()
        {
            return UpdateLocaleResource(Id, ResourceValue);
        }

        public bool UpdateWithKey()
        {
            return UpdateLocaleResource(LanguageId, ResourceName, ResourceValue);
        }

        #endregion

        #region Static Method:

        public static int AddLocaleResource(int languageId, string resourceName, string resourceValue)
        {
            return TLocalResource.Add(languageId, resourceName, resourceValue);
        }

        public static bool DeleteLocaleResource(int id)
        {
            return TLocalResource.Delete(id);
        }

        public static IEnumerable<LocalResource> GetLocaleResourceByLanguageId(int languageId)
        {
            return GetCollectionObjectFromDbCollectionObject(TLocalResource.GetLocaleResourceByLanguageId(languageId));
        }

        public static string GetResourceValue(string resourceName, int lanuageId)
        {
            LocalResource obj = Enumerable.SingleOrDefault(GetLocaleResourceByLanguageId(lanuageId), l => (l.ResourceName == resourceName));
            if (obj == null)
            {
                return "";
            }
            return obj.ResourceValue;
        }

        public static string GetResourceValueCurrentCulture(string resourceName)
        {
            return GetResourceValue(resourceName, Language.GetSingleLangaugeByCultureName(PayaTools.CurrentCulture).LanguageID);
        }

        public static List<LocalResource> GetResourceValues(string resourceName)
        {
            return (from l in GetAll()
                    where l.ResourceName == resourceName
                    select l).ToList<LocalResource>();
        }

        public static bool UpdateLocaleResource(int id, string resourceValue)
        {
            return TLocalResource.Update(id, resourceValue);
        }

        public static bool UpdateLocaleResource(int languageId, string resourceKey, string resourceValue)
        {
            return TLocalResource.Update(languageId, resourceKey, resourceValue);
        }

        public static List<LocalResource> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TLocalResource.GetAll());
        }

        public static LocalResource GetSingleByID(int id)
        {
            return GetObjectFromDbObject(TLocalResource.GetSingleByID(id));
        }

        public static LocalResource GetObjectFromDbObject(TLocalResource localResource)
        {
            return new LocalResource(localResource.Id, localResource.LanguageID, localResource.ResourceName, localResource.ResourceValue);
        }

        public static List<LocalResource> GetCollectionObjectFromDbCollectionObject(IEnumerable<TLocalResource> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        #endregion

        #endregion

    }

}
