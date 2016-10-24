using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayaDB;
using Telerik.OpenAccess;
using System.Linq;

namespace PayaBL.Classes
{
    public class CustomProfileBl
    {
        public CustomProfileBl()
        {
        }

        public CustomProfileBl(bool hasChangedPassword, int userID, string address, DateTime? birthDate,
                               string certificateNumber, int? failedPasswordAttemptCount,
                               DateTime? failedPasswordAttemptStart, DateTime? lastLockoutDate, DateTime? lastLoginDate,
                               DateTime? lastPasswordChangeDate, string mobile, string nationalNumber, byte[] picture,
                               bool sex, string tel, string zipCode)
        {
            HasChangedPassword = hasChangedPassword;
            UserID = userID;
            Address = address;
            BirthDate = birthDate;
            CertificateNumber = certificateNumber;
            FailedPasswordAttemptCount = failedPasswordAttemptCount;
            FailedPasswordAttemptStart = failedPasswordAttemptStart;
            LastLockoutDate = lastLockoutDate;
            LastLoginDate = lastLoginDate;
            LastPasswordChangeDate = lastPasswordChangeDate;
            Mobile = mobile;
            NationalNumber = nationalNumber;
            Picture = picture;
            Sex = sex;
            Tel = tel;
            ZipCode = zipCode;

        }

        // Methods
        public static int AddProfile(int userId, string nationalNumber, string certificateNumber, DateTime? birthDate,
                                     bool sex, string zipCode, string address, string tel, string mobile)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                var profile2 = new TUserCustomProfile
                                   {
                                       UserID = userId,
                                       NationalNumber = nationalNumber,
                                       CertificateNumber = certificateNumber,
                                       BirthDate = birthDate,
                                       Sex = sex,
                                       ZipCode = zipCode,
                                       Address = address,
                                       Tel = tel,
                                       Mobile = mobile,
                                       HasChangedPassword = PortalSetting.UserMustChangePasswordOnFirstLogin
                                   };
                TUserCustomProfile persistenceCapableObject = profile2;
                newObjectScope.Add(persistenceCapableObject);
                newObjectScope.Transaction.Commit();
                return int.Parse(newObjectScope.GetObjectId(persistenceCapableObject).ToString());
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return 0;
            }
        }

        public static CustomProfileBl GetSingleProfileByUserId(int userId)
        {

            var t =
                PayaScopeProvider1.GetNewObjectScope().Extent<TUserCustomProfile>().SingleOrDefault(
                    o => o.UserID == userId);


            return t != null ? GetObjectFromDbObject(t) : null;
        }
        public static List<CustomProfileBl> GetCollectionObjectFromDbCollectionObject(IEnumerable<TUserCustomProfile> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }
        public static List<CustomProfileBl> GetAll()
        {

            var t =
                PayaScopeProvider1.GetNewObjectScope().Extent<TUserCustomProfile>();

            return t != null ? GetCollectionObjectFromDbCollectionObject(t) : null;
        }

        public static CustomProfileBl GetObjectFromDbObject(TUserCustomProfile profile)
        {
            return new CustomProfileBl(profile.HasChangedPassword, profile.UserID, profile.Address, profile.BirthDate,
                                       profile.CertificateNumber, profile.FailedPasswordAttemptCount,
                                       profile.FailedPasswordAttemptStart, profile.LastLockoutDate,
                                       profile.LastLoginDate, profile.LastPasswordChangeDate, profile.Mobile,
                                       profile.NationalNumber, profile.Picture, profile.Sex, profile.Tel,
                                       profile.ZipCode);
        }

        public static bool UpdateFailedPasswordAttemptCount(int userId, int failedPasswordAttemptCount)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single(newObjectScope.Extent<TUserCustomProfile>(), o => o.UserID == userId).
                    FailedPasswordAttemptCount = failedPasswordAttemptCount;
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdateFailedPasswordAttemptStart(int userId)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single(newObjectScope.Extent<TUserCustomProfile>(), o => o.UserID == userId).
                    FailedPasswordAttemptStart = new DateTime?(DateTime.Now);
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdateLastLockoutDate(int userId)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single<TUserCustomProfile>(newObjectScope.Extent<TUserCustomProfile>(),
                                                     o => o.UserID == userId).LastLockoutDate =
                    new DateTime?(DateTime.Now);
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdateLastLoginDate(int userId)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single<TUserCustomProfile>(newObjectScope.Extent<TUserCustomProfile>(),
                                                     o => o.UserID == userId).LastLoginDate = new DateTime?(DateTime.Now);
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdateLastPasswordChangeDate(int userId)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single<TUserCustomProfile>(newObjectScope.Extent<TUserCustomProfile>(),
                                                     o => o.UserID == userId).LastPasswordChangeDate =
                    new DateTime?(DateTime.Now);
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdatePicture(int userId, byte[] picture)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                Queryable.Single<TUserCustomProfile>(newObjectScope.Extent<TUserCustomProfile>(),
                                                     o => o.UserID == userId).Picture = picture;
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        public static bool UpdateProfile(int userId, string nationalNumber, string certificateNumber,
                                         DateTime? birthDate, bool sex, string zipCode, string address, string tel,
                                         string mobile)
        {
            IObjectScope newObjectScope = PayaScopeProvider1.GetNewObjectScope();
            try
            {
                newObjectScope.Transaction.Begin();
                TUserCustomProfile profile =
                    Queryable.Single<TUserCustomProfile>(newObjectScope.Extent<TUserCustomProfile>(),
                                                         o => o.UserID == userId);
                profile.NationalNumber = nationalNumber;
                profile.CertificateNumber = certificateNumber;
                profile.BirthDate = birthDate;
                profile.Sex = sex;
                profile.ZipCode = zipCode;
                profile.Address = address;
                profile.Tel = tel;
                profile.Mobile = mobile;
                newObjectScope.Transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                if (newObjectScope.Transaction.IsActive)
                {
                    newObjectScope.Transaction.Rollback();
                }
                return false;
            }
        }

        // Properties
        public bool HasChangedPassword
        {
            get
            {
                return (HttpContext.Current.User.Identity.IsAuthenticated &&
                        GetSingleProfileByUserId(int.Parse(HttpContext.Current.User.Identity.Name)).HasChangedPassword);
            }
            set { }
        }

        public int UserID { set; get; }
        public string Address { set; get; }
        public DateTime? BirthDate { set; get; }
        public string CertificateNumber { set; get; }
        public int? FailedPasswordAttemptCount { set; get; }
        public DateTime? FailedPasswordAttemptStart { set; get; }
        public DateTime? LastLockoutDate { set; get; }
        public DateTime? LastLoginDate { set; get; }
        public DateTime? LastPasswordChangeDate { set; get; }
        public string Mobile { set; get; }
        public string NationalNumber { set; get; }
        public byte[] Picture { set; get; }
        public bool Sex { set; get; }
        public string Tel { set; get; }
        public string ZipCode { set; get; }
        public TUser TUser { set; get; }
    }





}