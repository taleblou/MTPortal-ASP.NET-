using System;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using PayaBL.Classes;
using PayaBL.Common;
using PayaBL.Common.PortalCach;
using PayaBL.Control;
using Telerik.Web.UI;

namespace Paya.Admin
{
    public partial class BlackList : ModuleControl
    {
        public string str="ALI";
        protected Label _lblFail;
        protected Label _lblSucc;
        protected RadGrid _rdgrBannedIpAddress;
        protected RadGrid _rdgrBannedIpNetwork;
        protected RadAjaxLoadingPanel _rdlpBlackListManager;
        protected RadMultiPage _rdmpBlackList;
        protected RadPageView _rdpBannedIpAddress;
        protected RadPageView _rdpBannedIpNetwork;
        protected RadAjaxPanel _rdpBlackListManager;
        protected RadTabStrip _rdtsBlackList;

        // Methods
        protected void _rdgrBannedIpAddress_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridDataItem)e.Item;
            if (BannedIpAddress.Delete(int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id"].ToString())))
            {
                DisplayMessage("اطلاعات با موفقیت حذف گردید", false);
                Caching.Remove("BannedIpAddress");
            }
            else
            {
                DisplayMessage("در حذف اطلاعات مشکلی پیش آمده است", true);
            }
            SetPageControl();
            SetPageData();
        }

        protected void _rdgrBannedIpAddress_InsertCommand(object sender, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                var insertedItem = (GridEditFormInsertItem)e.Item;
                string iPAddress = ((TextBox)insertedItem.FindControl("_txtIPAddress")).Text;
                if (!BlackListManager.IsValidIp(iPAddress))
                {
                    DisplayMessage("IP آدرس معتبر نمی باشد.", true);
                }
                else
                {
                    string comment = ((TextBox)insertedItem.FindControl("_txtComment")).Text;
                    if (BannedIpAddress.Add(iPAddress, comment, DateTime.Now) > 0)
                    {
                        DisplayMessage("اطلاعات با موفقیت ثبت گردید", false);
                        Caching.Remove("BannedIpAddress");
                    }
                    else
                    {
                        DisplayMessage("در ثبت اطلاعات مشکلی پیش آمده است", true);
                    }
                    SetPageControl();
                    SetPageData();
                }
            }
        }

        protected void _rdgrBannedIpAddress_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                var obj = e.Item.DataItem as BannedIpAddress;
                var lbl = (Label)e.Item.FindControl("_lblAddedDates");
                if ((lbl != null) && (obj != null))
                {
                    lbl.Text = PayaTools.SetDateWithCulture(obj.CreatedOn);
                }
            }
        }

        protected void _rdgrBannedIpAddress_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rdgrBannedIpAddress.DataSource = BannedIpAddress.GetAll();
        }

        protected void _rdgrBannedIpAddress_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                var editedItem = e.Item as GridEditableItem;
                int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                string iPAddress = ((TextBox)editedItem.FindControl("_txtIPAddress")).Text;
                if (!BlackListManager.IsValidIp(iPAddress))
                {
                    DisplayMessage("IP آدرس معتبر نمی باشد.", true);
                }
                else
                {
                    string comment = ((TextBox)editedItem.FindControl("_txtComment")).Text;
                    if (BannedIpAddress.Update(id, iPAddress, comment, DateTime.Now))
                    {
                        DisplayMessage("اطلاعات با موفقیت ویرایش گردید", false);
                        Caching.Remove("BannedIpAddress");
                    }
                    else
                    {
                        DisplayMessage("در ویرایش اطلاعات مشکلی پیش آمده است", true);
                    }
                    SetPageControl();
                    SetPageData();
                }
            }
        }

        protected void _rdgrBannedIpNetwork_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            var item = (GridDataItem)e.Item;
            if (BannedIpNetwork.Delete(int.Parse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id"].ToString())))
            {
                DisplayMessage("اطلاعات با موفقیت حذف گردید", false);
                Caching.Remove("BannedIpNetwork");
            }
            else
            {
                DisplayMessage("در حذف اطلاعات مشکلی پیش آمده است", true);
            }
            SetPageControl();
            SetPageData();
        }

        protected void _rdgrBannedIpNetwork_InsertCommand(object sender, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                var insertedItem = (GridEditFormInsertItem)e.Item;
                string startiPAddress = ((TextBox)insertedItem.FindControl("_txtStartIPAddress")).Text;
                string endiPAddress = ((TextBox)insertedItem.FindControl("_txtEndIpAddress")).Text;
                string iPeception = ((TextBox)insertedItem.FindControl("_txtIpException")).Text;
                if (iPeception.EndsWith(";"))
                {
                    iPeception.Remove(iPeception.LastIndexOf(";") - 1);
                }
                if (!((!Enumerable.Any<string>(iPeception.Split(";".ToCharArray()), (Func<string, bool>)(s => !BlackListManager.IsValidIp(s))) && BlackListManager.IsValidIp(startiPAddress)) && BlackListManager.IsValidIp(endiPAddress)))
                {
                    this.DisplayMessage("یکی از IP آدرس ها معتبر نمی باشد.", true);
                }
                else
                {
                    string comment = ((TextBox)insertedItem.FindControl("_txtComment")).Text;
                    if (BannedIpNetwork.Add(startiPAddress, endiPAddress, iPeception, comment, DateTime.Now) > 0)
                    {
                        DisplayMessage("اطلاعات با موفقیت ثبت گردید", false);
                        Caching.Remove("BannedIpNetwork");
                    }
                    else
                    {
                        DisplayMessage("در ثبت اطلاعات مشکلی پیش آمده است", true);
                    }
                    SetPageControl();
                    SetPageData();
                }
            }
        }

        protected void _rdgrBannedIpNetwork_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                var obj = e.Item.DataItem as BannedIpAddress;
                var lbl = (Label)e.Item.FindControl("_lblAddedDates");
                if ((lbl != null) && (obj != null))
                {
                    lbl.Text = PayaTools.SetDateWithCulture(obj.CreatedOn);
                }
            }
        }

        protected void _rdgrBannedIpNetwork_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rdgrBannedIpNetwork.DataSource = BannedIpNetwork.GetAll();
        }

        protected void _rdgrBannedIpNetwork_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            if (!Page.IsValid)
            {
                DisplayMessage("داده های صفحه معتبر نمی باشد.", true);
            }
            else
            {
                var editedItem = e.Item as GridEditableItem;
                int id = int.Parse(editedItem.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString());
                string startiPAddress = ((TextBox)editedItem.FindControl("_txtStartIPAddress")).Text;
                string endiPAddress = ((TextBox)editedItem.FindControl("_txtEndIpAddress")).Text;
                string iPeception = ((TextBox)editedItem.FindControl("_txtIpException")).Text;
                if (iPeception.EndsWith(";"))
                {
                    iPeception.Remove(iPeception.LastIndexOf(";") - 1);
                }
                if (!((!Enumerable.Any<string>(iPeception.Split(";".ToCharArray()), (Func<string, bool>)(s => !BlackListManager.IsValidIp(s))) && BlackListManager.IsValidIp(startiPAddress)) && BlackListManager.IsValidIp(endiPAddress)))
                {
                    DisplayMessage("یکی از IP آدرس ها معتبر نمی باشد.", true);
                }
                else
                {
                    string comment = ((TextBox)editedItem.FindControl("_txtComment")).Text;
                    if (BannedIpNetwork.Update(id, startiPAddress, endiPAddress, iPeception, comment, DateTime.Now))
                    {
                        DisplayMessage("اطلاعات با موفقیت ویرایش گردید", false);
                        Caching.Remove("BannedIpNetwork");
                    }
                    else
                    {
                        DisplayMessage("در ویرایش اطلاعات مشکلی پیش آمده است", true);
                    }
                    SetPageControl();
                    SetPageData();
                }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            _rdgrBannedIpAddress.Culture = new CultureInfo(PayaTools.CurrentCulture);
            _rdgrBannedIpAddress.Rebind();
            _rdgrBannedIpNetwork.Culture=new CultureInfo(PayaTools.CurrentCulture);
            _rdgrBannedIpNetwork.Rebind();
            if (ModuleConfiguration.HasNoPermission())
            {
                PayaTools.AccessDenied();
            }
            else if (!ModuleConfiguration.HasDefinedPermission(13))
            {
                PayaTools.AccessDenied();
            }
            Page.Title = string.Format("{0} لیست سیاه", PortalSetting.PortalName);
            PayaTools.RegisterCssInclude(Page, PortalSetting.PortalPath + "/UI/ShareCSS/RadControlFont.css");
            SetPageControl();
            SetPageData();
        }

        private void SetPageControl()
        {
        }

        private void SetPageData()
        {
            _rdgrBannedIpAddress.DataSource = BannedIpAddress.GetAll();
            _rdgrBannedIpAddress.DataBind();
            _rdgrBannedIpNetwork.DataSource = BannedIpNetwork.GetAll();
            _rdgrBannedIpNetwork.DataBind();
        }        

        // Nested Types
        private enum AuthRole
        {
            ModuleManagment = 13
        }

    }
}