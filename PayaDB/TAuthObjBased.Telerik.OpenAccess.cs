

using System;
using System.Collections.Generic;

namespace PayaDB 
{
    // Generated by Telerik OpenAccess
    // Used template: c:\program files\telerik\openaccess orm\sdk\IDEIntegrations\templates\PCClassGeneration\cs\templates\classgen\class\partialdesignerdefault.vm
    [Telerik.OpenAccess.Persistent(IdentityField="authID")]
    public partial class TAuthObjBased 
    {
        private int authID; // pk 

        private string authKey;

        private int moduleDefID;

        private TModuleDef tModuleDef;


        

        
    }
}

#region main class file contents
/*


using System;
using System.Collections.Generic;

namespace PayaDB 
{
    //Generated by Telerik OpenAccess
    public partial class TAuthObjBased 
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TAuthObjBased() 
        {
        }
    
        [Telerik.OpenAccess.FieldAlias("authID")]
        public int AuthID
        {
            get { return authID; }
            set { this.authID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("authKey")]
        public string AuthKey
        {
            get { return authKey; }
            set { this.authKey = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tModuleDef")]
        public TModuleDef TModuleDef
        {
            get { return tModuleDef; }
            set { this.tModuleDef = value; }
        }
 

    }
}
*/
#endregion //main class file contents
