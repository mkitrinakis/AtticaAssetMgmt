using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Telerik.Web.UI;
using Microsoft.SharePoint; 
namespace AssetMgmt
{
    public partial class AdminPanel : System.Web.UI.UserControl
    {
        AssetMgmtUtils utils = new AssetMgmtUtils();
        protected override void OnLoad(EventArgs e)
        {
            bool toShow = true;
            if (!(SPContext.Current.Item.ID > 0)) toShow = false;
            if (!SPContext.Current.Web.SiteGroups["HelpDesk"].ContainsCurrentUser) toShow = false;
            panelAdmin.Visible = toShow; 
        }

        protected void adminReject_Click(Object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPListItem itm = SPContext.Current.ListItem;
                if (!AdminPanelComments.Text.Trim().Equals(""))
                {
                    itm["History"] = writeHistory(itm, "Reject", AdminPanelComments.Text);
                }
                itm["WaitingApprover"] = null;
                itm["_status"] = "rejected";
                itm["Status"] = "ΑΠΟΡΡΙΦΘΗΚΕ";

             
                itm.SystemUpdate(); 
            });
            string rurl = utils.makeResultUrl("Reject", this.Page.Request.Url.Query);
            this.Page.Response.Redirect(rurl, true);
        }


        protected void adminApprove_Click(Object sender, EventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate ()
            {
                SPListItem itm = SPContext.Current.ListItem;

                if (!AdminPanelComments.Text.Trim().Equals(""))
                {
                    itm["History"] = writeHistory(itm, "Approve", AdminPanelComments.Text);
                }
                itm["WaitingApprover"] = null;
                itm["_status"] = "closed";
                itm["Status"] = "ΕΚΛΕΙΣΕ";

                itm.SystemUpdate();
            });
            string rurl = utils.makeResultUrl("Reject", this.Page.Request.Url.Query);
            this.Page.Response.Redirect(rurl, true);
        }

        public string writeHistory(SPListItem itm, string action, string comments)
        {
            SPUser u = SPContext.Current.Web.CurrentUser;
            string _status = (itm["_status"] ?? "").ToString();
            string _history = (itm["History"] ?? "").ToString();
            string actionName = ""; 

            if (action.Equals("Reject")) { actionName = "Μαρκαρίστηκε με Administration Action ως ΑΠΟΡΡΙΦΘΕΙΣΑ από"; }
            if (action.Equals("Approve")) { actionName = "Μαρκαρίστηκε με Administration Action ως ΥΛΟΠΟΙΗΜΕΝΗ από"; }

            comments = comments.Trim().Equals("") ? "" : ", με σχόλια <br>\"" + comments + "\"";
            _history += string.Format("<ul><li>{0}: {1} τον/την {2} στις {3}{4}</li></ul>", _status, actionName, u.Name, utils.myDate(System.DateTime.Now, true), comments);
            return _history;
        }
    }
}