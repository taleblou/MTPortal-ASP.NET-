

using System;
using System.Collections.Generic;

namespace PayaDB 
{
    // Generated by Telerik OpenAccess
    // Used template: c:\program files\telerik\openaccess orm\sdk\IDEIntegrations\templates\PCClassGeneration\cs\templates\classgen\class\partialdesignerdefault.vm
    [Telerik.OpenAccess.Persistent(IdentityField="moduleID")]
    public partial class TModule 
    {
        private int moduleID; // pk 

        private int? cacheTime;

        private string container;

        private DateTime? lastUpdate;

        private int moduleDefID;

        private int? moduleOrder;

        private string moduleTitle;

        private string paneName;

        private bool showinAllTab;

        private string skinHTML;

        private int tabID;

        private int updatePeriod;

        private TTab tTab;


        

        
    }
}

#region main class file contents
/*


using System;
using System.Collections.Generic;

namespace PayaDB 
{
    //Generated by Telerik OpenAccess
    public partial class TModule 
    {
        //The 'no-args' constructor required by OpenAccess. 
        public TModule() 
        {
        }
    
        [Telerik.OpenAccess.FieldAlias("moduleID")]
        public int ModuleID
        {
            get { return moduleID; }
            set { this.moduleID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("cacheTime")]
        public int? CacheTime
        {
            get { return cacheTime; }
            set { this.cacheTime = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("container")]
        public string Container
        {
            get { return container; }
            set { this.container = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("lastUpdate")]
        public DateTime? LastUpdate
        {
            get { return lastUpdate; }
            set { this.lastUpdate = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("moduleDefID")]
        public int? ModuleDefID
        {
            get { return moduleDefID; }
            set { this.moduleDefID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("moduleOrder")]
        public int? ModuleOrder
        {
            get { return moduleOrder; }
            set { this.moduleOrder = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("moduleTitle")]
        public string ModuleTitle
        {
            get { return moduleTitle; }
            set { this.moduleTitle = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("paneName")]
        public string PaneName
        {
            get { return paneName; }
            set { this.paneName = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("showinAllTab")]
        public bool ShowinAllTab
        {
            get { return showinAllTab; }
            set { this.showinAllTab = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("skinHTML")]
        public string SkinHTML
        {
            get { return skinHTML; }
            set { this.skinHTML = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tabID")]
        public int TabID
        {
            get { return tabID; }
            set { this.tabID = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("updatePeriod")]
        public int UpdatePeriod
        {
            get { return updatePeriod; }
            set { this.updatePeriod = value; }
        }
 
        [Telerik.OpenAccess.FieldAlias("tTab")]
        public TTab TTab
        {
            get { return tTab; }
            set { this.tTab = value; }
        }
 

    }
}
*/
#endregion //main class file contents