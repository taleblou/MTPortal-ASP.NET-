using System;
using System.Collections.Generic;
using System.Linq;
using PayaDB;

namespace PayaBL.Classes
{
    public class BannedIpNetwork
    {
        #region Field

        public const string CachBannedIpNetwork = "BannedIpNetwork";

        #endregion

        #region Properties

        #region Base

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public string EndIpAddress { get; set; }

        public int Id { get; set; }

        public string IpException { get; set; }

        public string StartIpAddress { get; set; }

        public DateTime? UpdatedOn { get; set; }

        #endregion

        #region Related

        #endregion

        #endregion

        #region Constrauctor

        public BannedIpNetwork()
        {
        }

        public BannedIpNetwork(int id, string startIpAddress, string endIpAddress, string ipException, string comment, DateTime createdOn, DateTime? updatedOn)
        {
            Id = id;
            StartIpAddress = startIpAddress;
            EndIpAddress = endIpAddress;
            IpException = ipException;
            Comment = comment;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
        }

        #endregion

        #region Method

        #region Insttance metod:

        public int Add()
        {
            return Add(StartIpAddress, EndIpAddress, IpException, Comment, CreatedOn);
        }

        public bool Delete()
        {
            return Delete(Id);
        }

        public bool Update()
        {
            return Update(Id, StartIpAddress, EndIpAddress, IpException, Comment, (DateTime)UpdatedOn);
        }

        #endregion

        #region Static Method:

        public static int Add(string startIpAddress, string endIpAddress, string ipException, string comment, DateTime createdOn)
        {
            return TBannedIpNetwork.Add(startIpAddress, endIpAddress, ipException, comment, createdOn);
        }

        public static bool Delete(int id)
        {
            return TBannedIpNetwork.Delete(id);
        }

        public static bool Update(int id, string startIpAddress, string endIpAddress, string ipException, string comment, DateTime updatedOn)
        {
            return TBannedIpNetwork.Update(id, startIpAddress, endIpAddress, ipException, comment, updatedOn);
        }

        public static List<BannedIpNetwork> GetAll()
        {
            return GetCollectionObjectFromDbCollectionObject(TBannedIpNetwork.GetAll());
        }

        public static BannedIpNetwork GetSingleByID(int id)
        {
            return GetObjectFromDbObject(TBannedIpNetwork.GetSingleByID(id));
        }

        public static BannedIpNetwork GetObjectFromDbObject(TBannedIpNetwork bannedIpNetwork)
        {
            return new BannedIpNetwork(bannedIpNetwork.Id, bannedIpNetwork.StartIpAddress, bannedIpNetwork.EndIpAddress, bannedIpNetwork.IpException, bannedIpNetwork.Comment, bannedIpNetwork.CreatedOn, bannedIpNetwork.UpdatedOn);
        }

        public static List<BannedIpNetwork> GetCollectionObjectFromDbCollectionObject(IEnumerable<TBannedIpNetwork> list)
        {
            return list.Select(GetObjectFromDbObject).ToList();
        }

        #endregion
        
        #endregion
    }
    
}
