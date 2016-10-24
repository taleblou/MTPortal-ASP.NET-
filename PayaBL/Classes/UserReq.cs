using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    public class UserReq
    {
        #region Field
        #endregion

        #region Properties

        #region Base

        public int ReqID { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNo { get; set; }

        public int PortalID { get; set; }

        public string UserName { get; set; }

        #endregion

        #region Related

        #endregion

        #endregion

        #region Constrauctor

        public UserReq()
        {
        }

        public UserReq(int reqID, string email, string firstName, string lastName, string phoneNo, int portalID, string userName)
        {
            ReqID = reqID;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNo = phoneNo;
            PortalID = portalID;
            UserName = userName;
        }

        #endregion

        #region Method

        #region Insttance metod:
        public int Add()
        {
            return Add(UserName, FirstName, LastName, Email, PhoneNo, PortalID);
        }

        public bool Delete()
        {
            return Delete(ReqID);
        }

        public bool Update()
        {
            return Update(ReqID, UserName, FirstName, LastName, Email, PhoneNo, PortalID);
        }

        #endregion

        #region Static Method:

        public static int Add(string userName, string firstName, string lastName, string email, string phoneNumber, int portalId)
        {
            return TRegisterReq.Add(userName, firstName, lastName, email, phoneNumber, portalId);
        }

        public static bool Delete(int reqId)
        {
            return TRegisterReq.Delete(reqId);
        }

        public static bool Update(int reqId, string userName, string firstName, string lastName, string email, string phoneNumber, int portalId)
        {
            return TRegisterReq.Update(reqId, userName, firstName, lastName, email, phoneNumber, portalId);
        }

        public static List<UserReq> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TRegisterReq.GetAll());
        }

        public static UserReq GetSingleByID(int reqId)
        {
            return GetObjectFromDbObject(TRegisterReq.GetSingleByID(reqId));
        }

        public static UserReq GetObjectFromDbObject(TRegisterReq userReq)
        {
            return new UserReq(userReq.ReqID, userReq.Email, userReq.FirstName, userReq.LastName,
                               userReq.PhoneNo, userReq.PortalID, userReq.UserName);
        }

        public static List<UserReq> GetCollectionObjectFromDbCollectionObject(IEnumerable<TRegisterReq> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        public static List<UserReq> GetAll(string userName, string firstName, string lastName, string email, int portalId)
        {
            return GetCollectionObjectFromDbCollectionObject(TRegisterReq.GetAllRegisterReqs(userName, firstName, lastName, email, portalId));
        }

        #endregion

        #endregion

    }

}
