using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PayaBL.Classes;
using PayaDB;

namespace PayaBL.Common
{
    public class ForgetPass
    {
        // Fields
        private PortalUser _user;

       


        // Methods
        public ForgetPass()
        {
        }

        public ForgetPass(int id, int userId, Guid reqGuid, DateTime dateSent)
        {
            Id = id;
            UserId = userId;
            ReqGuid = reqGuid;
            DateSent = dateSent;
        }

        public int AddNewRequest()
        {
            return AddNewRequest(UserId, DateSent);
        }

        public static int AddNewRequest(int userid, DateTime datesent)
        {
            return TForgetPass.AddNewRequest(userid, datesent);
        }

        public bool DeleteRequest()
        {
            return DeleteRequest(Id);
        }

        public static bool DeleteRequest(int id)
        {
            return TForgetPass.DeleteRequest(id);
        }

        public static List<ForgetPass> GetForgetPasswordCollectionFromDataReader(List<TForgetPass> tForgetPasses)
        {
            var lst = new List<ForgetPass>();
            if (tForgetPasses != null)
            {
                lst.AddRange(tForgetPasses.Select(GetSingleObjectFromDb));
            }
            return lst;
        }

        public static ForgetPass GetSingleObjectFromDb(TForgetPass tForgetPass)
        {
            return tForgetPass==null?null: new ForgetPass(tForgetPass.Id,tForgetPass.UserID,tForgetPass.ReqGUID,tForgetPass.DateSent);
        }

        public static ForgetPass GetSingleRequest(Guid reqid)
        {
            return GetSingleObjectFromDb(TForgetPass.GetSingleRequest(reqid));
            
        }

        public static ForgetPass GetSingleRequestById(int id)
        {
            return  GetSingleObjectFromDb(TForgetPass.GetSingleRequestById(id));
            
        }

        public static ForgetPass GetSingleRequestByUserId(int userId)
        {
            var t = TForgetPass.GetSingleRequestByUserId(userId);
            return t == null ? null : GetSingleObjectFromDb(t);
            
        }

        // Properties
        public DateTime DateSent { get; set; }

        public int Id { get; set; }

        public Guid ReqGuid { get; set; }

        public PortalUser ReqUser
        {
            get
            {
                return (_user ?? (_user = PortalUser.GetSingleByID(UserId)));
            }
        }

        public int UserId { get; set; }
    }

 

}
