

using System;
using System.Collections.Generic;

namespace PayaDB 
{
    // Generated by Telerik OpenAccess
    // Used template: c:\program files\telerik\openaccess orm\sdk\IDEIntegrations\templates\PCClassGeneration\cs\templates\classgen\class\partialdesignerdefault.vm
    [Telerik.OpenAccess.Persistent(IdentityField="moduleDefRoleID")]
    public partial class TModuleDefRoleDefault 
    {
        private int moduleDefRoleID; // pk 

        private int authID;

        private int moduleDefId;

        private int roleID;

        private TAuthRoleBased tAuthRoleBased;

        private TModuleDef tModuleDef;

        private TRole tRole;


        

        
    }
}

#region main class file contents
/*


using System;
using System.Collections.Generic;

namespace PayaDB 
{
    //Generated by Telerik OpenAccess
    public partial class TModuleDefRoleDefault 
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModuleDefRoleDefault() 
        {
        }
    
        [Telerik.OpenAccess.FieldAlias("moduleDefRoleID")]
        public int ModuleDefRoleID
        {
            get { return moduleDefRoleID; }
            set { this.moduleDefRoleID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("authID")]
        public int AuthID
        {
            get { return authID; }
            set { this.authID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("moduleDefId")]
        public int ModuleDefId
        {
            get { return moduleDefId; }
            set { this.moduleDefId = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("roleID")]
        public int RoleID
        {
            get { return roleID; }
            set { this.roleID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tAuthRoleBased")]
        public TAuthRoleBased TAuthRoleBased
        {
            get { return tAuthRoleBased; }
            set { this.tAuthRoleBased = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tModuleDef")]
        public TModuleDef TModuleDef
        {
            get { return tModuleDef; }
            set { this.tModuleDef = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tRole")]
        public TRole TRole
        {
            get { return tRole; }
            set { this.tRole = value; }
        }
 

    }
}
*/
#endregion //main class file contents
