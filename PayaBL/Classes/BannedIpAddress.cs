using System;
using System.Collections.Generic;
using System.Linq;
using PayaDB;

public class BannedIpAddress
{

    #region Fields :

    public const string CachBannedIpAddress = "BannedIpAddress";

    #endregion

    #region Properties :

    #region Base:

    public string Comment { get; set; }

    public DateTime CreatedOn { get; set; }

    public int Id { get; set; }

    public string IpAddress { get; set; }

    public DateTime? UpdatedOn { get; set; }

    #endregion

    #region Related :

    public IList<TPortal> UserInPortal = new List<TPortal>();

    #endregion

    #endregion

    #region Constrauctors :

    public BannedIpAddress()
    {
    }

    public BannedIpAddress(int id, string ipAddress, string comment, DateTime createdOn, DateTime? updatedOn)
    {
        Id = id;
        IpAddress = ipAddress;
        Comment = comment;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    #endregion

    #region Methods :

    #region Instance Methods :

    public int Add()
    {
        return Add(IpAddress, Comment, CreatedOn);
    }

    public bool Update()
    {
        return Update(Id, IpAddress, Comment, UpdatedOn);
    }

    public bool Delete()
    {
        return Delete(Id);
    }

    #endregion

    #region Static Methods :

    public static int Add(string ipAddress, string comment, DateTime createdOn)
    {
        return TBannedIpAddress.Add(ipAddress, comment, createdOn);
    }

    public static bool Delete(int id)
    {
        return TBannedIpAddress.Delete(id);
    }

    public static List<BannedIpAddress> GetAll()
    {
        return GetCollectionObjectFromDbCollectionObject(TBannedIpAddress.GetAll());
    }

    public static BannedIpAddress GetSingleByID(int id)
    {
        return GetObjectFromDbObject(TBannedIpAddress.GetSingleByID(id));
    }

    public static BannedIpAddress GetObjectFromDbObject(TBannedIpAddress bannedIpAddress)
    {
        return new BannedIpAddress(bannedIpAddress.Id, bannedIpAddress.IpAddress, bannedIpAddress.Comment, bannedIpAddress.CreatedOn, bannedIpAddress.UpdatedOn);
    }

    public static List<BannedIpAddress> GetCollectionObjectFromDbCollectionObject(IEnumerable<TBannedIpAddress> list)
    {
        return list.Select(GetObjectFromDbObject).ToList();
    }

    public static bool Update(int id, string ipAddress, string comment, DateTime? updatedOn)
    {
        return TBannedIpAddress.Update(id, ipAddress, comment, updatedOn);
    }

    #endregion

    #endregion

}

