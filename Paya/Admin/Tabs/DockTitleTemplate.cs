using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Paya.Admin.Tab
{
    public class DockTitleTemplate : ITemplate
    {
        // Fields
        private RadDock dock;
        private LinkButton lnk = new LinkButton();
        private LinkButton lnk2 = new LinkButton();
        private string title;
        private TextBox txt = new TextBox();

        // Methods
        public DockTitleTemplate(RadDock dock, string title)
        {
            this.dock = dock;
            this.title = title;
        }

        public void InstantiateIn(Control container)
        {
            lnk.ID = "lnk1";
            lnk.Text = title;
            lnk.Click += lnk_Click;
            lnk.CssClass = "LinkButtonTitle";
            txt.ID = "txt1";
            txt.Visible = false;
            lnk2.ID = "lnk2";
            lnk2.Click += lnk2_Click;
            lnk2.Text = "ok";
            lnk2.Visible = false;
            container.Controls.Add(lnk);
            container.Controls.Add(txt);
            container.Controls.Add(lnk2);
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            txt.Text = lnk.Text;
            txt.Visible = true;
            lnk2.Visible = true;
            lnk.Visible = false;
            string script = string.Format(
                "Sys.Application.add_load(function(){{enableDockDrag(false,'{0}','{1}');}});", dock.ClientID,
                txt.ClientID);
            ScriptManager.RegisterStartupScript(dock.Page, dock.Page.GetType(), "disableDrag", script, true);
        }

        protected void lnk2_Click(object sender, EventArgs e)
        {
            lnk.Text = txt.Text;
            txt.Visible = false;
            lnk2.Visible = false;
            lnk.Visible = true;
            string script = string.Format("Sys.Application.add_load(function(){{enableDockDrag(true,'{0}','{1}');}});",
                                          dock.ClientID, txt.ClientID);
            ScriptManager.RegisterStartupScript(dock.Page, dock.Page.GetType(), "enableDrag", script, true);
        }
    }



}
