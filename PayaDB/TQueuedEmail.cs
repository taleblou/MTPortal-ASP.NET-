using System;
using System.Collections.Generic;

namespace PayaDB
{
    // Generated by Telerik OpenAccess
    // Used template: c:\program files (x86)\telerik\openaccess orm\sdk\IDEIntegrations\templates\PCClassGeneration\cs\templates\classgen\class\partialuserdefault.vm
    // NOTE: Field declarations and 'Object ID' class implementation are added to the 'designer' file.
    //       Changes made to the 'designer' file will be overwritten by the wizard.  	
    public partial class TQueuedEmail
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TQueuedEmail()
        {
        }

        [Telerik.OpenAccess.FieldAlias("queuedEmailID")]
        public int QueuedEmailID
        {
            get { return queuedEmailID; }
            set { this.queuedEmailID = value; }
        }

        [Telerik.OpenAccess.FieldAlias("bcc")]
        public string Bcc
        {
            get { return bcc; }
            set { this.bcc = value; }
        }

        [Telerik.OpenAccess.FieldAlias("body")]
        public string Body
        {
            get { return body; }
            set { this.body = value; }
        }

        [Telerik.OpenAccess.FieldAlias("cc")]
        public string Cc
        {
            get { return cc; }
            set { this.cc = value; }
        }

        [Telerik.OpenAccess.FieldAlias("createdOn")]
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set { this.createdOn = value; }
        }

        [Telerik.OpenAccess.FieldAlias("from")]
        public string From
        {
            get { return from; }
            set { this.from = value; }
        }

        [Telerik.OpenAccess.FieldAlias("fromName")]
        public string FromName
        {
            get { return fromName; }
            set { this.fromName = value; }
        }

        [Telerik.OpenAccess.FieldAlias("priority")]
        public int Priority
        {
            get { return priority; }
            set { this.priority = value; }
        }

        [Telerik.OpenAccess.FieldAlias("sendTries")]
        public int SendTries
        {
            get { return sendTries; }
            set { this.sendTries = value; }
        }

        [Telerik.OpenAccess.FieldAlias("sentOn")]
        public DateTime? SentOn
        {
            get { return sentOn; }
            set { this.sentOn = value; }
        }

        [Telerik.OpenAccess.FieldAlias("subject")]
        public string Subject
        {
            get { return subject; }
            set { this.subject = value; }
        }

        [Telerik.OpenAccess.FieldAlias("to1")]
        public string To1
        {
            get { return to1; }
            set { this.to1 = value; }
        }

        [Telerik.OpenAccess.FieldAlias("toName")]
        public string ToName
        {
            get { return toName; }
            set { this.toName = value; }
        }


    }
}
