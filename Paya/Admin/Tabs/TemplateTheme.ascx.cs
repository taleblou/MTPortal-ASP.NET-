using System;
using System.IO;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;

namespace Paya.Admin.Tab
{
    public partial class TemplateTheme : TabControl
    {
        // Fields
        protected Button _btnSetStyle;
        protected Panel _pnlTemplate;
        protected Panel _pnlTheme;
        protected RadioButtonList _rblstTemplate;
        protected RadioButtonList _rblstTheme;

        // Methods
        protected void _btnSetStyle_Click(object sender, EventArgs e)
        {
            TabLoad.Settings =PayaBL.Classes.Tab.SetTabTheme(TabLoad.TabID, _rblstTheme.SelectedValue);
            TabLoad.Settings =PayaBL.Classes.Tab.SetTabTemplate(TabLoad.TabID,_rblstTemplate.SelectedValue)  ;
            
            TabLoad.Update();
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            _pnlTemplate.GroupingText = "قالب صفحه \"" + TabLoad.TabName + "\"";
            _pnlTheme.GroupingText = "پوسته صفحه \"" + TabLoad.TabName + "\"";
            SetTemplateTheme(TabLoad.Template, TabLoad.Theme);
        }

        private void SetTemplateTheme(string template, string theme)
    {
        DirectoryInfo directoryInfo;
        string path;
        string[] imagePath;
        ListItem li;
        if (this._rblstTemplate.Items.Count == 0)
        {
            string templatePath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Templates";
            directoryInfo = new DirectoryInfo(Server.MapPath(templatePath));
            foreach (DirectoryInfo info in directoryInfo.GetDirectories())
            {
                if (info.Attributes != FileAttributes.Hidden)
                {
                    imagePath = Directory.GetFiles(Server.MapPath(templatePath + "/" + info.Name + "/"), "thumbnail.*");
                    if ((imagePath.Length > 0) && (imagePath[0] != ""))
                    {
                        path = PortalSetting.PortalPath + templatePath.Remove(0, 1) + "/" + info.Name + "/" + imagePath[0].Substring(imagePath[0].LastIndexOf(@"\"));
                    }
                    else
                    {
                        path = PortalSetting.PortalPath + "/images/NoPic.jpg";
                    }
                     li = new ListItem
                    {
                        Value = info.Name,
                        Text = info.Name + "<img align=\"right\" width='100' height='100' src='" + path + "'/><br/>"
                    };
                    
                    _rblstTemplate.Items.Add(li);
                }
            }
        }
        _rblstTemplate.SelectedValue = template;
        if (_rblstTheme.Items.Count == 0)
        {
            string themePath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Themes";
            directoryInfo = new DirectoryInfo(Server.MapPath(themePath));
            foreach (DirectoryInfo info in directoryInfo.GetDirectories())
            {
                if (info.Attributes != FileAttributes.Hidden)
                {
                    imagePath = Directory.GetFiles(Server.MapPath(themePath + "/" + info.Name + "/"), "thumbnail.*");
                    if ((imagePath.Length > 0) && (imagePath[0] != ""))
                    {
                        path = PortalSetting.PortalPath + themePath.Remove(0, 1) + "/" + info.Name + "/" + imagePath[0].Substring(imagePath[0].LastIndexOf(@"\"));
                    }
                    else
                    {
                        path = PortalSetting.PortalPath + "/Images/NoPic.jpg";
                    }
                    ListItem initLocal1 = new ListItem {
                        Value = info.Name,
                        Text = info.Name + "<img align=\"right\" width='100' height='100' src='" + path + "'/><br/>"
                    };
                    li = initLocal1;
                    _rblstTheme.Items.Add(li);
                }
            }
        }
        _rblstTheme.SelectedValue = theme;
    }

    }
}