using System;
using System.Collections.Generic;

namespace PayaDB
{
    // Generated by Telerik OpenAccess
    // Used template: c:\program files (x86)\telerik\openaccess orm\sdk\IDEIntegrations\templates\PCClassGeneration\cs\templates\classgen\class\partialuserdefault.vm
    // NOTE: Field declarations and 'Object ID' class implementation are added to the 'designer' file.
    //       Changes made to the 'designer' file will be overwritten by the wizard.  	
    public partial class TLogException
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TLogException()
        {
        }

        [Telerik.OpenAccess.FieldAlias("id")]
        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        [Telerik.OpenAccess.FieldAlias("createdOn")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { this.createdOn = value; }
        }

        [Telerik.OpenAccess.FieldAlias("form")]
        public string Form
        {
            get { return form; }
            set { this.form = value; }
        }

        [Telerik.OpenAccess.FieldAlias("iPAddress")]
        public string IPAddress
        {
            get { return iPAddress; }
            set { this.iPAddress = value; }
        }

        [Telerik.OpenAccess.FieldAlias("message")]
        public string Message
        {
            get { return message; }
            set { this.message = value; }
        }

        [Telerik.OpenAccess.FieldAlias("pageUrl")]
        public string PageUrl
        {
            get { return pageUrl; }
            set { this.pageUrl = value; }
        }

        [Telerik.OpenAccess.FieldAlias("queryString")]
        public string QueryString
        {
            get { return queryString; }
            set { this.queryString = value; }
        }

        [Telerik.OpenAccess.FieldAlias("refere")]
        public string Refere
        {
            get { return refere; }
            set { this.refere = value; }
        }

        [Telerik.OpenAccess.FieldAlias("source")]
        public string Source
        {
            get { return source; }
            set { this.source = value; }
        }

        [Telerik.OpenAccess.FieldAlias("stackTrace")]
        public string StackTrace
        {
            get { return stackTrace; }
            set { this.stackTrace = value; }
        }

        [Telerik.OpenAccess.FieldAlias("targetSite")]
        public string TargetSite
        {
            get { return targetSite; }
            set { this.targetSite = value; }
        }

        [Telerik.OpenAccess.FieldAlias("userID")]
        public int? UserID
        {
            get { return userID; }
            set { this.userID = value; }
        }


    }
}
