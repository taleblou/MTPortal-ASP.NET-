using System;
using System.IO;
using Paya.UI.Templates;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Control;

namespace Paya
{
    public partial class PageManagment : PortalPage
    {
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            string tracePage = Request.QueryString["TracePage"];
            if (!(string.IsNullOrEmpty(tracePage) || !(tracePage.ToLower() == "true")))
            {
                EndTime = DateTime.Now.Ticks;
                TimeSpan time = new TimeSpan(EndTime - StartTime);
                _lblLoadTime.Visible = true;
                _lblLoadTime.Text = time.TotalSeconds.ToString();
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            StartTime = DateTime.Now.Ticks;
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int moduleId = PayaTools.QueryStringInt("ModuleID", -1);
            string control = Request.QueryString["Page"].Replace("ROOT/", "../");
            if (moduleId != -1)
            {
                Module m = Module.GetSingleByID(moduleId);
                string templatePath = PayaTools.SetStyle(Context.User.Identity.Name, false) + "/Templates/PageTemplate.ascx";
                if (!File.Exists(Server.MapPath(templatePath)))
                {
                    templatePath = "~/UI/Templates/Template.ascx";
                }
                var temp = (Template)Page.LoadControl(templatePath);
                temp.ModuleLoad = m;
                temp.PageLoad = control;
                temp.ID = "PageTemplate_ctrl";
                _plhControl.Controls.Add(temp);
            }
            else
            {
                PayaTools.AccessDenied();
            }
        }

    }
}