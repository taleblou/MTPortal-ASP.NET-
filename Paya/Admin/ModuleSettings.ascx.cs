using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common.PortalCach;
using PayaBL.Control;

namespace Paya.Admin
{
    public partial class ModuleSettings : ModuleControl
    {
          // Fields
    // Methods
    protected void _btnResetSetting_Click(object sender, EventArgs e)
    {
        bool b = true;
        foreach (ModuleSetting setting in base.ModuleConfiguration.ModuleSettings)
        {
            b = setting.Delete();
            if (!b)
            {
                break;
            }
        }
        if (b)
        {
            var t = new Table
                          {
                              ID = "tblSettings"
                          };
            foreach (ModuleSetting item in ModuleConfiguration.ModuleSettings)
            {
                t.Rows.Add(DrawSetting(item));
            }
            _plhSettings.Controls.Clear();
            _plhSettings.Controls.Add(t);
            DisplayMessage("تنظیمات به حالت پیش فرض بازگشت.", true);
        }
        else
        {
            DisplayMessage("در بازگشت به حالت پیش فرض مشکلی پیش آمده است.", true);
        }
    }

    protected void _btnSaveSetting_Click(object sender, EventArgs e)
    {
        try
        {
            var t = (Table) _plhSettings.FindControl("tblSettings");
            if (t != null)
            {
                bool b = true;
                int i = 0;
                foreach (ModuleSetting ms in ModuleConfiguration.ModuleSettings)
                {
                    if (!b)
                        break;
                    string controlType;
                    CheckBox chkSet;
                    DropDownList drpSet;
                    b = ms.Delete()||ms.MSettID==0;
                    if (string.IsNullOrEmpty(ms.PossibleValues))
                    {
                        controlType = "T";
                    }
                    else if (ms.PossibleValues == "yes;no")
                    {
                        controlType = "C";
                    }
                    else
                    {
                        controlType = "D";
                    }
                    switch (controlType)
                    {
                        case "C":
                            chkSet = (CheckBox) t.FindControl("_cbx" + ms.SettingID);
                            string chk = "no";
                            if (chkSet.Checked)
                            {
                                chk = "yes";
                            }
                            if (chk != ms.DefaultValue)
                            {
                                i = ModuleSetting.Add(ModuleConfiguration.ModuleID, ms.SettingID, chk);
                            }
                            break;
                        case "D":
                            drpSet = (DropDownList) t.FindControl("_ddl" + ms.SettingID);
                            if (drpSet.SelectedItem.Value != ms.DefaultValue)
                            {
                                i = ModuleSetting.Add(ModuleConfiguration.ModuleID, ms.SettingID,
                                                      drpSet.SelectedItem.Value);
                            }
                            break;
                        case "T":
                            var txtSet = (TextBox) t.FindControl("_txt" + ms.SettingID);
                            if (txtSet.Text == "")
                            {
                                DisplayMessage("لطفا مقادير جعبه هاي متن را مشخص کنيد", true);
                            }
                            else if (txtSet.Text != ms.DefaultValue)
                            {
                                i = ModuleSetting.Add(ModuleConfiguration.ModuleID, ms.SettingID, txtSet.Text);
                            }
                            break;
                        default:
                            if (!(b && (i != -1)))
                            {
                                DisplayMessage("در ثبت تنظیمات مشکلی پیش آمده است.", true);
                                break;
                            }
                            break;
                    }
                }
                if (b && (i != -1))
                {
                    DisplayMessage("تنظیمات با موفقیت ثبت گردید.", false);
                    Caching.DeleteModulesSettingsCache();
                }
                t = new Table
                        {
                            ID = "tblSettings"
                        };
                foreach (ModuleSetting item in ModuleConfiguration.ModuleSettings)
                {
                    t.Rows.Add(DrawSetting(item));
                }
                _plhSettings.Controls.Clear();
                _plhSettings.Controls.Add(t);
            }
        }
        catch (Exception)
        {
            DisplayMessage("خطایی رخ داده است", true);
        }
    }

    private void DisplayMessage(string message, bool fail)
    {
        _lblFail.Visible = false;
        _lblSucc.Visible = false;
        Label lbl = fail ? _lblFail : _lblSucc;
        lbl.Visible = true;
        lbl.Text = message;
    }

    private static TableRow DrawSetting(ModuleSetting ms)
    {
        string controlType;
        TableRow initLocal1 = new TableRow {
            ID = "tr" + ms.SettingID
        };
        TableRow tr = initLocal1;
        TableCell initLocal2 = new TableCell {
            Width = Unit.Parse("40%"),
            Text = ms.SettingName
        };
        TableCell tcName = initLocal2;
        tr.Cells.Add(tcName);
        TableCell initLocal3 = new TableCell {
            Width = Unit.Parse("60%"),
            ID = "td" + ms.SettingID
        };
        TableCell tcSetting = initLocal3;
        if (string.IsNullOrEmpty(ms.PossibleValues))
        {
            controlType = "T";
        }
        else if (ms.PossibleValues == "yes;no")
        {
            controlType = "C";
        }
        else
        {
            controlType = "D";
        }
        string name = controlType;
        if (name != null)
        {
            if (!(name == "T"))
            {
                if (name == "C")
                {
                    CheckBox initLocal5 = new CheckBox {
                        ID = "_cbx" + ms.SettingID
                    };
                    CheckBox chkSellection = initLocal5;
                    string chkval = ms.SettingValue;
                    if (chkval == "")
                    {
                        chkval = ms.DefaultValue;
                    }
                    chkSellection.Checked = chkval == "yes";
                    tcSetting.Controls.Add(chkSellection);
                }
                else if (name == "D")
                {
                    var initLocal6 = new DropDownList {
                        ID = "_ddl" + ms.SettingID
                    };
                    DropDownList drpSetting = initLocal6;
                    string drpval = ms.SettingValue;
                    if (drpval == "")
                    {
                        drpval = ms.DefaultValue;
                    }
                    string[] strvals = ms.PossibleValues.Split(new char[] { ';' });
                    for (int i = 0; i < strvals.Length; i++)
                    {
                        drpSetting.Items.Add(new ListItem(strvals[i], strvals[i]));
                        if (drpSetting.Items[i].Value == drpval)
                        {
                            drpSetting.Items[i].Selected = true;
                        }
                    }
                    tcSetting.Controls.Add(drpSetting);
                }
            }
            else
            {
                TextBox initLocal4 = new TextBox {
                    ID = "_txt" + ms.SettingID,
                    Text = (ms.SettingValue != "") ? ms.SettingValue : ms.DefaultValue
                };
                TextBox txtSetting = initLocal4;
                tcSetting.Controls.Add(txtSetting);
            }
        }
        tr.Cells.Add(tcSetting);
        return tr;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var src =
            ModuleConfiguration.ModuleDef.DeskTopSRC.Substring(0,
                                                               ModuleConfiguration.ModuleDef.DeskTopSRC.Length -
                                                               5) + "Settings.ascx";
        try
        {
            ModuleControl myControl = (ModuleControl) Page.LoadControl(src);
            myControl.ModuleConfiguration = ModuleConfiguration;
            _pnlModuleSettings.Controls.Add(myControl);
            _pnlModuleSettings.Visible = true;
            _pnlPortalSettings.Visible = false;
        }
        catch
        {
            DisplayMessage("", false);
            _pnlPortalSettings.Visible = true;
            _pnlModuleSettings.Visible = false;
            if (ModuleConfiguration.ModuleSettings.Count == 0)
            {
                _btnSaveSetting.Visible = false;
                _btnResetSetting.Visible = false;
                _plhSettings.Visible = false;
                DisplayMessage("این برنامه تنظیم اختصاصی ندارد", false);
            }
            else
            {
                _btnSaveSetting.Visible = true;
                _btnResetSetting.Visible = true;
                _plhSettings.Visible = true;
                Table t = new Table {
                    ID = "tblSettings"
                };
                
                foreach (ModuleSetting item in ModuleConfiguration.ModuleSettings)
                {
                    t.Rows.Add(DrawSetting(item));
                }
                _plhSettings.Controls.Clear();
                _plhSettings.Controls.Add(t);
            }
        }
    }

    }
}