using System;
using System.Collections; 
using System.Collections.Generic;
using System.Collections.Specialized;  
using System.Web;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls; 
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq; 

namespace AssetMgmt
{
    public class AssetMgmtUtils
    {
        public string err = "";
        public System.Drawing.Color errColor = System.Drawing.Color.LightPink;
       
        public void initStandard(Panel panelMain)
        {
            SPListItem itm = SPContext.Current.ListItem;
            Label Composer = (Label)panelMain.FindControl("Composer");
            if (!(itm.ID > 0) && Composer.Text.Equals(""))
            {
                Composer.Text = SPContext.Current.Web.CurrentUser.Name;
            }

        Button bReject = (Button)panelMain.FindControl("BReject");
            bReject.CssClass = "alert"; 
            bReject.Attributes.Add("onclick", "rsp=confirm('Είστε σίγουροι ότι θέλετε να απορρίψετε την Αίτηση');return(rsp);");
            bReject.CausesValidation = false;
        }

        public string validateDataApprover(SPListItem itm, Panel panelMain)
        {
            string rs = "";

            if (itm.ID <= 0 || (itm["Approver"] ?? "").ToString().Equals(""))
            {
              //  PeopleEditor ddlApprover = (PeopleEditor)panelMain.FindControl("ddlApprover");
              ClientPeoplePicker ddlApprover = (ClientPeoplePicker)panelMain.FindControl("ddlApprover");
                ddlApprover.BackColor = System.Drawing.Color.White;
                ddlApprover.ErrorMessage = "";
                if (ddlApprover.ResolvedEntities.Count <= 0)
                {
                    rs += "<li> Πρέπει να εισάγετε Υπεύθυνο Έγκρισης</li>";
                    ddlApprover.BackColor = errColor;
                    ddlApprover.ErrorMessage = "Πρέπει να εισάγετε Υπεύθυνο Έγκρισης";
                    ddlApprover.ForeColor = errColor; 
                    
                }
            }
            return rs; 
        }
        public string validateDataStandard( Panel panelMain, string[] textFields, string[] ddlFields)
        {
            string rs = "";
            if (textFields != null)
            {
                foreach (string textField in textFields)
                {
                    string f = textField.Split('#')[0];
                    string txt = "";
                    try
                    {
                        TextBox tb = (TextBox)panelMain.FindControl(f);
                        tb.BackColor = System.Drawing.Color.White;
                        if (tb.Text.Trim().Equals(""))
                        { rs += "<li> Το πεδίο:'" + textField.Split('#')[1] + "' δεν μπορεί να μείνει κενό</li>"; tb.BackColor = errColor; }
                    }
                    catch
                    {
                        RadTextBox tb = (RadTextBox)panelMain.FindControl(f);
                        tb.BackColor = System.Drawing.Color.White;
                        if (tb.Text.Trim().Equals(""))
                        { rs += "<li> Το πεδίο:'" + textField.Split('#')[1] + "' δεν μπορεί να μείνει κενό</li>"; tb.BackColor = errColor; }
                    }
                }
            }

            if (ddlFields != null)
            {
                foreach (string ddlField in ddlFields)
                {
                    string f = ddlField.Split('#')[0];
                    DropDownList tb = (DropDownList)panelMain.FindControl(f);
                    tb.BackColor = System.Drawing.Color.White;
                    if (tb.SelectedIndex <= 0)
                    { { rs += "<li> Το πεδίο:'" + ddlField.Split('#')[1] + "' δεν μπορεί να μείνει κενό</li>"; tb.BackColor = errColor; } }
                }
            }

           
            return rs; 
        }


        public ListItem[] getListItemsFromKeywordsList(SPWeb web, string keywordName)
        {
            List<ListItem> rs = new List<ListItem>();

            try
            {
                SPList l = web.Lists["Keywords"];
                SPQuery qry = new SPQuery();
                string sqry = "<Where><Eq><FieldRef Name='Title'/><Value Type='Text'>" + keywordName + "</Value></Eq></Where>";
                qry.Query = sqry; 
                //qry.Query = "<OrderBy><FieldRef Name='Title' Ascending='True'/> </OrderBy>";
                //qry.ViewFields = "<FieldRef Name='Title'/>";
                SPListItemCollection col = l.GetItems(qry);
                rs.Add(new ListItem() { Value = "", Text = "-Επιλέξτε-" });
                if (col.Count>0) {
                    SPListItem kitm = col[0];
                    string[] values = (kitm["Value"] ?? "").ToString().Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries); 
                    List<string> sorted = new List<string>(values);
                    sorted.Sort();
                    foreach (string el in sorted)
                    {
                        rs.Add(new ListItem(el));
                    }
                }
                return rs.ToArray();
            }
            catch (Exception e) { string error = "Error: " + e.Message; rs.Add(new ListItem() { Text = error }); return rs.ToArray(); }
        }


        public ListItem[] getListItemsAsDialogList(SPWeb web, string listName)
        { 
         List<ListItem> rs = new List<ListItem>();

            try
            {
                SPList l = web.Lists[listName];
                SPQuery qry = new SPQuery();
                 qry.Query = "<OrderBy><FieldRef Name='Title' Ascending='True'/> </OrderBy>";
                qry.ViewFields = "<FieldRef Name='Title'/>";
                SPListItemCollection col = l.GetItems(qry);
                rs.Add(new ListItem() { Value = "", Text = "-Επιλέξτε-" });
                foreach (SPListItem itm in col)
                {
                    rs.Add(new ListItem(itm.Title));
                }

                return rs.ToArray();
            }
            catch (Exception e) { string error = "Error: " + e.Message; rs.Add(new ListItem() { Text = error }); return rs.ToArray(); }
        }

        public ListItem[] getGroupUsersAsDialogList(string groupName)
        {

            List<ListItem> rs = new List<ListItem>();

            try
            {

                SPGroup g = SPContext.Current.Web.SiteGroups[groupName];
                List<KeyValuePair<string, SPUser>> s = new List<KeyValuePair<string, SPUser>>();
                rs.Add(new ListItem() { Value = "", Text = "-Επιλέξτε-" });
                
                foreach (SPUser u in SPContext.Current.Web.SiteUsers) { 
                // foreach (SPUser u in g.Users) {
                    rs.Add(new ListItem() { Value = u.LoginName, Text = u.Name + " - " + u.LoginName });

                }

                return rs.ToArray();

            }
            catch (Exception e) { string error = "Error: " + e.Message; rs.Add(new ListItem() { Text = error }); return rs.ToArray(); }
        }

        public List<SPPrincipal> getFieldUsers(SPListItem itm, string fname)
        {
            List<SPPrincipal> rs = new List<SPPrincipal>();
            if (itm[fname] == null || (itm[fname] ?? "").ToString().Equals("")) { return rs;  }
            try
            {
                string temp = (itm[fname] ?? "").ToString();
             //   writeLog(itm.Web, "getFieldUsers", "fieldValue:" + temp, false);
                try
                {
                    SPFieldUserValueCollection fuvc = (SPFieldUserValueCollection)itm[fname];
                    foreach (SPFieldUserValue fuv in fuvc)
                    {
                        try
                        {
                            if (fuv.User != null)
                            {
               //                 writeLog(itm.Web, "getFieldUsers", temp + ": isUser", false); 
                                rs.Add(fuv.User);
                            }
                            else {
                 //               writeLog(itm.Web, "getFieldUsers", temp + ": isGroup", false);
                                rs.Add(itm.Web.SiteGroups.GetByID(fuv.LookupId)); }
                        }
                        catch (Exception e) { err += " --getFieldUsers-internal:" + e.Message;
                   //         writeLog(itm.Web, "getFieldUsers", err, false);
                        }
                    }
                    return rs;
                }
                catch // is single value 
                {
                    try
                    {
                     //   writeLog(itm.Web, "getFieldUsers", "is single vlaue", false);
                        int id = Convert.ToInt32(temp.Split(';')[0]);
                        SPUser u = null;
                        try { u = itm.Web.SiteUsers.GetByID(id); } catch (Exception exc) { err += "--Not User";
                       //     writeLog(itm.Web, "getFieldUsers", err, false);
                        }
                        if (u != null)
                        {
                            rs.Add((SPPrincipal)u);
                        }
                        else
                        {
                            try
                            {
                         //       writeLog(itm.Web, "getFieldUsers", "single Group?", false);
                                SPGroup g = itm.Web.SiteGroups.GetByID(id);
                                rs.Add((SPPrincipal)g);
                            }
                            catch (Exception exc) { err += "--Not Even Group" + exc.Message;
                           //     writeLog(itm.Web, "getFieldUsers", err, false);
                            }
                        }
                        return rs;
                    }
                    catch (Exception e) { err += " --getFieldUsersInternal2:" + e.Message;
                        //  writeLog(itm.Web, "getFieldUsers", err, false); 
                        return rs;
                    }
                }
            }
            catch (Exception e) { err += " --getFieldUsers:" + e.Message;
                //      writeLog(itm.Web, "getFieldUsers", err, false); 
                return rs;
            }
        }

        public List<SPPrincipal> getWaitingApprover(SPListItem itm)
        {
            return getFieldUsers(itm, "WaitingApprover");
        }


        public string getDispStatus(string _status)
        {
            if (_status.Equals("draft") || _status.Trim().Equals("")) return "ΠΡΟΧΕΙΡΟ";
            if (_status.Equals("back")) return "ΠΡΟΣ ΔΙΟΡΘΩΣΗ";
            if (_status.Equals("pendingApproval1")) return "ΕΚΚΡΕΜΕΙ ΕΓΚΡΙΣΗ";
            if (_status.Equals("pendingImplementation")) return "ΕΚΚΡΕΜΕΙ ΥΛΟΠΟΙΗΣΗ";
            if (_status.Equals("closed")) return "ΕΚΛΕΙΣΕ";
            if (_status.Equals("rejected")) return "ΑΠΟΡΡΙΦΘΗΚΕ";
            return _status;
        }

        public string getApproveFullStatus(string _status)
        {
            string tmp = _status.Contains("#") ? _status.Split('#')[0] : _status;
            if (tmp.Equals("draft")) return "pendingApproval1#ΕΚΚΡΕΜΕΙ ΕΓΚΡΙΣΗ";
            if (tmp.Equals("back")) return "pendingApproval1#ΕΚΚΡΕΜΕΙ ΕΓΚΡΙΣΗ";
            if (tmp.Equals("pendingApproval1")) return "pendingImplementation#ΕΚΚΡΕΜΕΙ ΥΛΟΠΟΙΗΣΗ#";
            if (tmp.Equals("pendingImplementation")) return "closed#ΕΚΛΕΙΣΕ";
            if (tmp.Equals("rejected")) return "rejected#ΑΠΟΡΡΙΦΘΗΚΕ";
            if (tmp.Equals("closed")) return "closed#ΕΚΛΕΙΣΕ";
            return _status;
        }

        private void closedActions(SPListItem itm)
        {
            itm["Implemented"] = System.DateTime.Now;
        }

        public string getLookupDispValue(SPListItem itm, string fname)
        {
            string val = (itm[fname] ?? "").ToString();
            if (val.Contains(";#")) { val = val.Split('#')[1]; }
            return val;
        }
        public string getMultiLookupDispValue(SPListItem itm, string fname)
        {
            string[] valarr;
            string val = (itm[fname] ?? "").ToString();
            if (val.Contains(";#")) { valarr = val.Split('#'); }
            else { return val; }
            List<string> rs = new List<string>();
            bool isOdd = true;
            foreach (string sval in valarr) { isOdd = !isOdd; if (isOdd) rs.Add(sval); }
            return String.Join(", ", rs.ToArray());
        }

        public SPFieldUserValueCollection getPeopleEditorValue(SPWeb oWeb, ClientPeoplePicker people)
        {
            SPFieldUserValueCollection rs = new SPFieldUserValueCollection();
            if (people.ResolvedEntities.Count > 0)
            {
                for (int i = 0; i < people.ResolvedEntities.Count; i++)
                {
                    try
                    {
                        PickerEntity user = (PickerEntity)people.ResolvedEntities[i];

                        SPUser webUser = oWeb.EnsureUser(user.Key);
                        rs.Add(new SPFieldUserValue(oWeb, webUser.ID, webUser.Name));

                        //switch ((string)user.EntityData["PrincipalType"])
                        //{
                        //    case "User":
                        //        SPUser webUser = oWeb.EnsureUser(user.Key);
                        //        rs.Add(new SPFieldUserValue(oWeb, webUser.ID, webUser.Name));
                        //        break;
                        //    case "SharePointGroup":  
                        //        SPGroup siteGroup = oWeb.SiteGroups[user.EntityData["AccountName"].ToString()];
                        //        rs.Add(new SPFieldUserValue(oWeb, siteGroup.ID, siteGroup.Name));
                        //        break;
                        //    default:
                        //        //  SPUser spUser = oWeb.EnsureUser(people.Accounts[i].ToString());
                        //        //  rs.Add(new SPFieldUserValue(oWeb, spUser.ID, spUser.Name));
                        //        writeLog(oWeb, "getPeopleEditorValue", "No User No Group", false); 
                        //        break;
                        //}
                    }
                    catch (Exception ex)
                    {
                        writeLog(oWeb, "getPeopleEditorValue", ex.Message , false);
                    }
                }
            }
            return rs;
        }

        public void setWaitingApprover(string _status, SPListItem itm, string form)
        {
            try
            {
                string tmp = _status.Contains("#") ? _status.Split('#')[0] : _status;
                List<SPPrincipal> rs = new List<SPPrincipal>();
                if (tmp.Equals("back") || tmp.Equals("draft") || tmp.Equals(""))
                {
                    itm["WaitingApprover"] = itm["Composer"] ;
                    //if (itm.ID > 0) { rs.Add((SPUser)itm["Composer"]); }
                    //else { rs.Add(SPContext.Current.Web.CurrentUser); }
                }

                if (tmp.Equals("pendingApproval1"))
                {
                    itm["WaitingApprover"] = itm["Approver"];

                }

                if (tmp.Equals("pendingImplementation"))
                {
                  
                    if (form.Equals("System")  )

                         
                    {

                        SPGroup g = itm.Web.SiteGroups["HelpDesk"];
                        SPFieldUserValueCollection fuvc = new SPFieldUserValueCollection();
                        fuvc.Add(new SPFieldUserValue(itm.Web, g.ID, g.Name));
                        itm["WaitingApprover"] = fuvc;
                    }
                }
                if (tmp.Equals("closed") || tmp.Equals("rejected")) { itm["WaitingApprover"] = null;  }
                return;
            } catch (Exception exc) { err += " --setWaitingApprovers:" + exc.Message; }
        }


        // public void getFromBackStandard(AssetMgmtUtils utils, SPListItem itm, Label History, Label Status, Label Composer, Label L_WaitingApprover, Label L_ddlApprover, DropDownList ddlApprover, Panel panelMain)
        public void getFromBackStandard( SPListItem itm, Panel panelMain)
        {

            if (itm["Approver"] != null)
            {

                try
                {
                    
                    Label L_ddlApprover = (Label)panelMain.FindControl("L_ddlApprover");
                    string appr = (itm["Approver"] ?? "").ToString(); if (appr.Contains("#")) { appr = appr.Split('#')[1]; }
                    L_ddlApprover.Text = appr; 
                    //if (appr.Contains(";")) { appr = appr.Split(';')[0]; }
                    //SPUser u = ((SPFieldUserValueCollection)itm["Approver"])[0].User;
                    //PeopleEditor ddlApprover = (PeopleEditor)panelMain.FindControl("ddlApprover");
                    //Label L_ddlApprover = (Label)panelMain.FindControl("L_ddlApprover");
                    //L_ddlApprover.Text = u.Name;

                }
                catch (Exception e) { err += "getFromBackStandard:" + err;  }
                Label History = (Label)panelMain.FindControl("History");
                Label Status = (Label)panelMain.FindControl("Status");
                Label L_Serial = (Label)panelMain.FindControl("L_Serial");
                Label L_Submitted = (Label)panelMain.FindControl("L_Submitted");
                Label Composer = (Label)panelMain.FindControl("Composer");
                Label L_WaitingApprover = (Label)panelMain.FindControl("L_WaitingApprover");
               // DropDownList ddlApprover = (DropDownList)panelMain.FindControl("ddlApprover");
                History.Text = (itm["History"] ?? "").ToString();
                Status.Text = getDispStatus((itm["_status"] ?? "").ToString());
                L_Serial.Text = (itm["Serial"] ?? "").ToString();
                string submitted =   myDate(itm["Submitted"] ?? "", false) ; 
                
                L_Submitted.Text = submitted;
                //    Composer.Text = itm.ID > 0 ? getLookupDispValue(itm, "Composer") : SPContext.Current.Web.CurrentUser.Name;
                Composer.Text = !(itm["Composer"] ?? "").ToString().Trim().Equals("") ? getLookupDispValue(itm, "Composer") : SPContext.Current.Web.CurrentUser.Name;
                L_WaitingApprover.Text = getMultiLookupDispValue(itm, "WaitingApprover");
               
                //if (itm["Approver"] != null)  // shows only the first 
                //{
                //    try
                //    {
                //        SPFieldUserValueCollection fuv = (SPFieldUserValueCollection)itm["Approver"];
                //        L_ddlApprover.Text = fuv[0].User.Name;
                //        ddlApprover.SelectedValue = fuv[0].User.LoginName;
                //    }
                //    catch (Exception e) { L_ddlApprover.Text = "error:" + e.Message; }
            }
        }


        public void saveToBackStandard(SPListItem itm, Panel panelMain)
        {
            ClientPeoplePicker ddlApprover = (ClientPeoplePicker)panelMain.FindControl("ddlApprover");
            Label L_Error = (Label)panelMain.FindControl("L_Error"); 
            if (!(itm.ID > 0))
            {
                SPUser u = SPContext.Current.Web.CurrentUser;
                itm["Composer"] = u;
                itm["Title"] = "Αίτηση από: " + u.Name; 
            }
            try
            {
                string _status = (itm["_status"] ?? "").ToString();
                if (_status.Trim().Equals("")) { itm["_status"] = "draft"; itm["Status"] = getDispStatus ("draft"); }
                SPFieldUserValueCollection  uvc = getPeopleEditorValue(itm.Web, ddlApprover); 
                if (!(uvc == null) && uvc.Count>0)
                {
                    //SPUser u = SPContext.Current.Web.SiteUsers[appr];
                    SPUser u = uvc[0].User; 
                    itm["Approver"] = u;
                }
                { err += "saveToBack: Approveris null  ";  }
            }
            catch (Exception e) { err += "saveToBack:" + e.Message; }
        }


        private SPUser getCreatedByAsUser(SPListItem itm)
        {
            try
            {
                
                int userId = Convert.ToInt32( (itm["Author"] ?? "").ToString().Split(';')[0]);

                SPUser u = itm.Web.SiteUsers.GetByID(userId); 
                return u; 
            }
            catch (Exception e)
            {
                writeLog(itm.Web, "getCreatedByAsUser", e.Message, true);
                return null; 
            }
        }

        private void setListItemPermissions(SPListItem itm, List<SPPrincipal> principals)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite eSite = new SPSite(itm.Web.Site.ID))   
                    {
                        using (SPWeb eWeb = eSite.OpenWeb(itm.Web.ID))
                        {
                            
                            eWeb.AllowUnsafeUpdates = true;
                               SPList lst = eWeb.Lists[itm.ParentList.Title];  // INTTRUST ListTitle 
                           // SPList lst = eWeb.Lists[itm.ParentList.RootFolder.Name];
                            SPListItem eItm = lst.GetItemById(itm.ID);

                        if (!itm.HasUniqueRoleAssignments)
                            {
                                try
                                {
                                      SPGroup g = eWeb.SiteGroups[ itm.ParentList.Title + "_Readers"]; // INTTRUST 
                                  //  SPGroup g = eWeb.SiteGroups[itm.ParentList.RootFolder.Name + "_Readers"];
                                    addWrite(g, eItm, eWeb);
                                    SPUser u = getCreatedByAsUser(itm); 
                                    if (u != null) { addWrite(u, eItm, eWeb);  }
                                }
                                catch (Exception e)  {
                                    // writeLog(eWeb, "setListItemPermissions", "adding:" + itm.ParentList.Title + "_Readers" + "-->" + e.Message, true);
                                    writeLog(eWeb, "setListItemPermissions", "adding:" + itm.ParentList.RootFolder.Name + "_Readers" + "-->" + e.Message, true);
                                }; 
                            }
                            foreach (SPPrincipal principal in principals)
                            { addWrite(principal, eItm, eWeb); }

                          
                            eWeb.AllowUnsafeUpdates = false;
                        }

                    }
                });
                
            }
            catch (Exception e) { err += "setListItemPermissions:" + e.Message; }
        }


        private void getSerial(SPListItem itm)
        {
            try
            {
                SPList lSerial = itm.Web.Lists["Profile"];
                int iSerial = -1;
                foreach (SPListItem temp in lSerial.Items)
                {
                    if (temp.Title.Equals("Serial"))
                    {
                        if (int.TryParse((temp["Value"] ?? "0").ToString(), out iSerial))
                        {
                            temp["Value"] = (iSerial + 1).ToString(); temp.SystemUpdate();
                            break;
                        }
                    }
                }
                if (iSerial > -1) { itm["Serial"] = iSerial; }
            }
            catch (Exception e) { err += "getSerial:" + e.Message;  }
        }
    

        private void getSerialElevated(SPListItem itm)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    using (SPSite eSite = new SPSite(itm.Web.Site.ID))
                    {
                        using (SPWeb eWeb = eSite.OpenWeb(itm.Web.ID))
                        {
                            eWeb.AllowUnsafeUpdates = true;
                            SPList lst = eWeb.Lists[itm.ParentList.Title];
                            SPListItem eItm = lst.GetItemById(itm.ID);
                            SPList lSerial = eWeb.Lists["Profile"];
                            int iSerial = -1;
                            foreach (SPListItem temp in lSerial.Items)
                            {
                                if (temp.Title.Equals("Serial"))
                                {
                                    if (int.TryParse((temp["Value"] ?? "0").ToString(), out iSerial))
                                    {
                                        temp["Value"] = (iSerial + 1).ToString();temp.SystemUpdate(); 
                                        break;
                                    }
                                }
                            }
                            if (iSerial > -1) { itm["Serial"] = iSerial; itm.SystemUpdate(); }
                        }
                    }
                });
            }
            catch (Exception e) { err += "getSerial:" + e.Message; }
        }

        public void sendWaitingMail (SPWeb web, SPListItem itm, string _previousStatus  )
        {
            
            try
            {
                List<string> tolist = new List<string>(); 
                string to = "";
                string cc = "";
                string approver = "";
                string composer = ""; 
                string from = "";
                string mailSeparator = getMailSeparator(web);
                try {
                    List<SPPrincipal> composers = getFieldUsers(itm, "Composer");
                    composer = ((SPUser)composers[0]).Email;
                } catch (Exception e) { composer = e.Message; }
                try
                {
                    List<SPPrincipal> approvers = getFieldUsers(itm, "Approver");
                    approver = ((SPUser)approvers[0]).Email;
                }
                catch (Exception e) { composer = e.Message; }
                SPList lcc = web.Lists["Profile"];

              
                string requestTypeDisp = "";
                 string listTitle = itm.ParentList.Title; // INTTRUST LIST TILE 
                // string listTitle = itm.ParentList.RootFolder.Name; 
                    if (listTitle.Equals("MailAccess")) { requestTypeDisp = "e-mail λογαριασμού"; }
                if (listTitle.Equals("SystemAccess")) { requestTypeDisp = "λογαριασμού συστήματος"; }
                string actionDisp = "";

                // FIND IMPLEMENTATION MAIL 
                List<string> implMails = new List<string>();
                if (listTitle.Equals("SystemsAccess"))
                {
                    string systems = (itm["Systems"] ?? "").ToString().ToUpper();
                    string subjectOther = (itm["SubjectOther"] ?? "").ToString().Trim();
                    bool isForWindows = systems.ToUpper().Contains("WINDOWS");
                    string other = systems.Replace( "WINDOWS", "");
                     other = other.Replace( ",", "");
                    bool isForOther = !other.Trim().Equals("") || !subjectOther.Equals("");
                    foreach (SPListItem temp in lcc.Items)
                    {
                        if (isForWindows && temp.Title.Equals("MailPendingImplementationWindows", StringComparison.InvariantCultureIgnoreCase))
                        {
                            implMails.Add((temp["Value"] ?? "").ToString());

                        }
                        if (isForOther && temp.Title.Equals("MailPendingImplementationOther", StringComparison.InvariantCultureIgnoreCase))
                        {
                            implMails.Add((temp["Value"] ?? "").ToString());
                        }
                        if (!isWorkingHour(web))
                        {
                            if (isForWindows && temp.Title.Equals("MailPendingImplementationWindowsNonWorkingHours", StringComparison.InvariantCultureIgnoreCase))
                            {
                                implMails.Add((temp["Value"] ?? "").ToString());

                            }
                            if (isForOther && temp.Title.Equals("MailPendingImplementationOtherNonWorkingHours", StringComparison.InvariantCultureIgnoreCase))
                            {
                                implMails.Add((temp["Value"] ?? "").ToString());
                            }
                        }
                    }
                }
                else
                {
                    foreach (SPListItem temp in lcc.Items)
                    {
                       
                        if (temp.Title.Equals("MailPendingImplementation", StringComparison.InvariantCultureIgnoreCase))
                        { implMails.Add((temp["Value"] ?? "").ToString()); }
                    }
                }
                // CHANGE END 

                    string _status = (itm["_status"] ?? "").ToString();
                foreach (SPListItem temp in lcc.Items)
                {
                    //if (temp.Title.Equals("MailAdminCC", StringComparison.InvariantCultureIgnoreCase))
                    //{ cc = (temp["Value"] ?? "").ToString(); }
                    if (temp.Title.Equals("MailAdminFrom", StringComparison.InvariantCultureIgnoreCase))
                    { from = (temp["Value"] ?? "").ToString(); }
                    if (temp.Title.Equals("Mail" + _status))
                        { actionDisp = " περιμένει την ενέργειά σας"; to = (temp["Value"] ?? "").ToString(); }
                }

                // Special Case SystemsAccess 
              

                if (listTitle.Equals("SystemsAccess") && _status.Equals("pendingImplementation"))
                {
                    
                    actionDisp = " περιμένει την ενέργειά σας";
                   to = String.Join(mailSeparator, implMails);
                    
                }

                
                if (actionDisp.Equals(""))
                {
                    if (_status.Equals("rejected")) { actionDisp = " απορρίφθηκε"; to = composer;  }
                    if (_status.Equals("closed")) { actionDisp = " υλοποιήθηκε"; to = composer;  }
                    if (_status.Equals("back")) { actionDisp = " εστάλει για Διορθώσεις "; to = composer; }
                }
                if (actionDisp.Equals("")) {
                    try
                    {
                        List<SPPrincipal> approvers = getFieldUsers(itm, "WaitingApprover");
                        approver = ((SPUser)approvers[0]).Email;
                    }
                    catch (Exception e) { approver = e.Message; }
                    actionDisp = " περιμένει την ενέργειά σας"; to = approver; }
                
                    StringDictionary messageHeaders = new StringDictionary();
                string subject = "Αίτημα πρόσβασης (αρ. " + (itm["Serial"] ?? "N/A") +  " )"  + actionDisp;
                string body = subject;
                string url = web.Url + "/Lists/" + listTitle  + "/DispForm.aspx?ID=" + itm.ID.ToString();
                body += "<br> Μπορείτε να <a href=\"" + url + "\"> κάνετε κλικ εδώ </a> για να το δείτε";
                if (_status.Equals("back")) {
                    body += "<br/> Στο ιστορικό της αίτησης μπορείτε να δείτε την τεκμηρίωση της επιστροφής για διορθώσεις";
                    if (_previousStatus.Equals("pendingImplementation", StringComparison.InvariantCulture))
                    {
                        cc = String.Join(mailSeparator, implMails) + mailSeparator + approver;
                    }
                    else { cc = approver; }
                }
                if (_status.Equals("rejected")) { body += "<br/> Στο ιστορικό της αίτησης μπορείτε να δείτε την τεκμηρίωση της απόρριψης";
                    if (_previousStatus.Equals("pendingImplementation", StringComparison.InvariantCulture))
                    {
                      cc =   String.Join(mailSeparator, implMails) + mailSeparator + approver;
                    }
                    else { cc = approver; }
                } 
                if (_status.Equals("closed")) { cc = String.Join(mailSeparator, implMails) + mailSeparator + approver;  }

                    messageHeaders.Add("to", to);
                messageHeaders.Add("from", from);
                messageHeaders.Add("subject", subject);
                messageHeaders.Add("cc", cc);

                Microsoft.SharePoint.Utilities.SPUtility.SendEmail(web, messageHeaders, body);
                writeLog(web, "SendMail", "to:" + to + ", cc:" + cc + ", subject:" + subject + ", body:" + body + ", _status:" + _status + ", approver:" + approver + ", composer:" + composer , false); 
            }
            catch (Exception e) {  err += " sendWaitingMail:" + e.Message;  }
        }


        private string getMailSeparator(SPWeb web)
        {
            try
            {
                SPList lcc = web.Lists["Profile"];
                foreach (SPListItem itm in lcc.Items)
                {
                    if (itm.Title.Equals("MailSeparator"))
                    { return (itm["Value"] ?? ",").ToString().Trim(); }
                }
                return ",";
            }
            catch (Exception e) { err += " getMailSeparator:" + e.Message; return ","; }
        }

        private bool isWorkingHour(SPWeb web)
        {
            try
            {
                int from = 0;
                int to = 100; 
                SPList lcc = web.Lists["Profile"];
                foreach (SPListItem itm in lcc.Items)
                {
                    if (itm.Title.Equals("WorkingHoursFrom")) from = getMinutesAsInt((itm["Value"] ?? "").ToString());
                    if (itm.Title.Equals("WorkingHoursTo")) to = getMinutesAsInt((itm["Value"] ?? "").ToString());
                    if (from < 0) from = 0; // err-handling 
                    if (to < 0) to = 100; // err-handling 
                }
                DateTime dt = DateTime.Now;
                int nowMM = getMinutesAsInt(dt.Hour.ToString() + "." + dt.Minute.ToString());
                return (nowMM >= from && nowMM <= to); 
            }
            catch (Exception e) { err += " isWorkingHour:" + e.Message; return true; }
        }

        private int getMinutesAsInt(string val)
        {
            try {
                string hh = val.Split('.')[0];
                string mm = val.Split('.')[1];
                return Convert.ToInt32(hh) * 100 + Convert.ToInt32(mm); 
            }
            catch (Exception e)
            {
                err += " getMinutesAsInt:" + e.Message;
                return -1; 
            }
        }

        public void BSave_ClickStandard(SPListItem itm, Panel panelMain, Page page)
        {
            
            RadTextBox Comments = (RadTextBox)panelMain.FindControl("Comments");
            Label L_Errors = (Label)panelMain.FindControl("L_Errors");
            
            Label History = (Label)panelMain.FindControl("History");
            itm["History"] = writeHistory(itm, "Save", Comments.Text);
            
            if ((itm["WaitingApprover"] ?? "").ToString().Trim().Equals(""))
            {

                setWaitingApprover((itm["_status"] ?? "").ToString(), itm, "System");
                
                if (err.Trim().Equals(""))
                {
                    itm.Update();
                    
                    setListItemPermissions(itm, getWaitingApprover(itm));
                }
                else { L_Errors.Text = err; }

            }
            else
            { itm.Update(); }

            if (err.Trim().Equals(""))
            {
                string rurl = makeResultUrl("Approve", page.Request.Url.Query);
                page.Response.Redirect(rurl, true);
            }
        }

        public void BSubmit_ClickStandard( SPListItem itm, Panel panelMain, Page page)
        {
            RadTextBox Comments = (RadTextBox)panelMain.FindControl("Comments");
            Label L_Errors = (Label)panelMain.FindControl("L_Errors");
            itm["History"] = writeHistory(itm, "Submit", Comments.Text);
            string _status = (itm["_status"] ?? "draft").ToString();
            string _previousStatus = _status; 
            if ((itm["Submitted"] ?? "").ToString().Equals(""))
            { itm["Submitted"] = System.DateTime.Now;  }
            if (_status.Trim().Equals("")) { _status = "draft"; }
            bool toGetSerial = _status.Trim().Equals("draft") && ((itm["Serial"] ?? "").ToString().Trim().Equals("") || (itm["Serial"] ?? "").ToString().Trim().Equals("0")); 
            string _newstatus = getApproveFullStatus(_status);
            if (_newstatus.Contains("#"))
            {
                itm["_status"] = _newstatus.Split('#')[0];
                itm["Status"] = _newstatus.Split('#')[1];
            }
            else
            {
                itm["_status"] = _newstatus;
                itm["Status"] = _newstatus;
            }
            if ((itm["_status"] ?? "").ToString().Equals("closed", StringComparison.InvariantCultureIgnoreCase)) { closedActions(itm); }
            setWaitingApprover(_newstatus, itm, "System");
            if (err.Trim().Equals(""))
            {
                if (toGetSerial)
                {
                    getSerial(itm);
                }
                itm.Update();
                
               
                sendWaitingMail(itm.Web, itm, _previousStatus);
                setListItemPermissions(itm, getWaitingApprover(itm));
                if (!err.Trim().Equals(""))
                {
                    L_Errors.Text = err;
                }
                else
                {
                    string rurl = makeResultUrl("Approve", page.Request.Url.Query);
                    page.Response.Redirect(rurl, true);
                }
            }
            else { L_Errors.Text = err; }
           
        }

        public void BBack_ClickStandard(SPListItem itm, Panel panelMain, Page page)
        {
            RadTextBox Comments = (RadTextBox)panelMain.FindControl("Comments");
            Label L_Errors = (Label)panelMain.FindControl("L_Errors");
            itm["History"] = writeHistory(itm, "Back", Comments.Text);
            string _previousStatus = (itm["_status"] ?? "draft").ToString();
            string _status = "back";
            string Status = getDispStatus(_status);
            itm["_status"] = _status;
            itm["Status"] = Status;

            setWaitingApprover(_status, itm, "System");
            if (err.Trim().Equals(""))
            {
                itm.Update();
                setListItemPermissions(itm, getWaitingApprover(itm));
                sendWaitingMail(itm.Web, itm, _previousStatus);
                string rurl = makeResultUrl("Exit", page.Request.Url.Query);
                page.Response.Redirect(rurl, true);
            }
            else { L_Errors.Text = err; }
           
        }

        public void BReject_ClickStandard(SPListItem itm, Panel panelMain, Page page)
        {
            RadTextBox Comments = (RadTextBox)panelMain.FindControl("Comments");
            Label L_Errors = (Label)panelMain.FindControl("L_Errors");
            itm["History"] = writeHistory(itm, "Reject", Comments.Text);
            string _previousStatus = (itm["_status"] ?? "draft").ToString();
            string _status = "rejected";
            string Status = getDispStatus(_status);
            itm["_status"] = _status;
            itm["Status"] = Status;

            setWaitingApprover(_status, itm, "System");
            if (err.Trim().Equals(""))
            {
                itm.Update();
                setListItemPermissions(itm, getWaitingApprover(itm));
                sendWaitingMail(itm.Web, itm, _previousStatus);
                string rurl = makeResultUrl("Reject", page.Request.Url.Query);
                page.Response.Redirect(rurl, true);
            }
            else { L_Errors.Text = err; }
           
        }


        public  bool isSubmitted(SPListItem itm)
        {
            string _status = "";
            if (!(itm.ID > 0)) { return false; }
            _status = (itm["_status"] ?? "").ToString();
            if (!_status.Equals("") && !_status.Equals("back") && !_status.Equals("draft")) return true;
            return false;
        }
        private void addWrite(SPPrincipal pr, SPListItem item, SPWeb web)
        {
            try
            {
                var roleDef = web.RoleDefinitions.GetByType(SPRoleType.Contributor);
                var roleAsgt = new SPRoleAssignment(pr);
                roleAsgt.RoleDefinitionBindings.Add(roleDef);
                if (!item.HasUniqueRoleAssignments) { item.BreakRoleInheritance(false); }
                item.RoleAssignments.Add(roleAsgt);
                return;
            }
            catch (Exception e)
            {
                err += "--AddWrite:" + e.Message;
            }
        }



        public SPFieldLookupValueCollection serializeMultiSelect(RadListBox rlist)
        {
            SPFieldLookupValueCollection fluc = new SPFieldLookupValueCollection(); 
            foreach (RadListBoxItem rlbi in rlist.Items)
            {
                SPFieldLookupValue flu = new SPFieldLookupValue(Convert.ToInt32(rlbi.Value), rlbi.Text);
                fluc.Add(flu);
            }
            return fluc; 
        }

        public void initMultiSelectSource(RadListBox sourceList, RadListBox targetList, SPList l)
        {
            bool onlyActive = (l.Fields.ContainsField("Active"));
            List<string> rs = new List<string>();
            Hashtable ht = new Hashtable();
            List<string> alreadySelected = new List<string>();
            foreach (RadListBoxItem i in targetList.Items) { alreadySelected.Add(i.Text); }

            foreach (SPListItem itm in l.Items)
            {
                if (!onlyActive || ((bool)itm["Active"]))
                {
                    string title = itm.Title;
                  
                    if (!alreadySelected.Contains(title))
                    {
                        if (!ht.ContainsKey(itm.Title))
                        {
                            ht[title] = itm.ID;
                            rs.Add(itm.Title);
                        }
                    }
                }
            }
            rs.Sort();
            foreach (string title in rs)
            { sourceList.Items.Add(new RadListBoxItem(title,   ht[title].ToString())); }
        }

        public void trimMultiSelectSource(RadListBox sourceList, RadListBox targetList)
        {
            List<string> alreadySelected = new List<string>();
            List<RadListBoxItem> toRemove = new List<RadListBoxItem>(); 
            foreach (RadListBoxItem i in targetList.Items) { alreadySelected.Add(i.Text); }
            foreach (RadListBoxItem i in sourceList.Items)
            {
                if (alreadySelected.Contains(i.Text)) { toRemove.Add (i); }
            }

            foreach (RadListBoxItem i in toRemove) { i.Remove();  }
        }

        public void deserializeMultiSelect (SPListItem itm, string fname, RadListBox rlist)
        {
            SPFieldLookupValueCollection fluc = new SPFieldLookupValueCollection((itm[fname] ?? "").ToString()); 
            foreach (SPFieldLookupValue flu  in fluc)
            {
                RadListBoxItem i = new RadListBoxItem(flu.LookupValue, flu.LookupId.ToString());
                rlist.Items.Add(i); 
            }
        }



        public SPFieldMultiChoiceValue serializeCheckbox(CheckBoxList cbList)
        {
            SPFieldMultiChoiceValue v = new SPFieldMultiChoiceValue();
            foreach (ListItem cb in cbList.Items)
            {
                if (cb.Selected) v.Add(cb.Value);
            }
            return v; 
        }

        public void  deserializeCheckbox(SPListItem itm, string fname, CheckBoxList cbl)
        {
            
            string rs = (itm[fname] ?? "").ToString();
            
            foreach (string val in rs.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries))
            {
                bool found = false;
                foreach (ListItem li in cbl.Items)
                { if (li.Value.Trim().Equals(val.Trim())) { li.Selected = true; found = true; } }

                if (!found) {
            
                        ListItem li = new ListItem(val); li.Selected = true;
                    cbl.Items.Add(li);  }
            }
        }


        public string serializeDialog(DropDownList ddl)
        {
            if (ddl.SelectedIndex <= 0) return "";
            if (ddl.SelectedValue.Trim().Equals("")) return "";
            if (ddl.SelectedValue.Equals("-Επιλέξτε-")) return "";
            return ddl.SelectedValue; 
        }

        public bool isDraft(SPListItem itm)
        {
            if (!(itm.ID > 0)) return true;
            string _status = (itm["_status"] ?? "").ToString().Trim();
            if (_status.Equals("") || _status.Equals("draft") || _status.Equals("back"))
            { return true; }
            else { return false; }
        }

        public bool isWaitingApprover(SPListItem itm, string form)
        {
            if (!(itm.ID > 0)) return true;
            SPUser u = SPContext.Current.Web.CurrentUser;

            if (isDraft(itm))
            {
                try
                {
                    string composer = (itm["Composer"] ?? "").ToString();
                    string id = composer.Split(';')[0];
                    return u.ID.ToString().Trim().Equals(id.ToString().Trim());
                }
                catch (Exception e) { err += " --" + e.Message; return false; }
            }
            string _status = (itm["_status"] ?? "").ToString();
            if (_status.Equals("closed")) return false;
            if (_status.Equals("rejected")) return false;
            if (form.Equals("Systems") || form.Equals("Mail"))
            {
                    if (_status.Equals("pendingImplementation"))
                    {
                        return (itm.Web.SiteGroups["HelpDesk"].ContainsCurrentUser); 
                    }
                   else
                    {
                        try
                        {
                        string waitingApprover= (itm["WaitingApprover"] ?? "").ToString();
                        string id = waitingApprover.Split(';')[0];
                        return u.ID.ToString().Trim().Equals(id.ToString().Trim());
                    }
                        catch (Exception e) { err += " --" + e.Message; return false;  }
                    }
            }
            return false; 
        }


        public string writeHistory(SPListItem itm, string action, string comments)
        {
            SPUser u = SPContext.Current.Web.CurrentUser;
            string _status = (itm["_status"] ?? "").ToString();
            string Status = getDispStatus(_status); 
            string _history = (itm["History"] ?? "").ToString();
            string actionName = "Εγκρίθηκε από"; 
            if (action.Equals("Save")) { actionName = "Αποθηκεύθηκε από"; }
            if (action.Equals("Reject")) { actionName = "Απορρίφθηκε από"; }
            if (action.Equals("Back")) { actionName = "Εστάλει στον Συντάκτη για Διορθώσεις από"; }
            if (action.Equals("Submit")) {
                if (isDraft(itm))
                {
                    actionName = "Υποβλήθηκε από";
                }
                else if (_status.ToLower().Contains("implementation"))
                {
                    actionName = "Υλοποιήθηκε από";
                }
                else { actionName = "Εγκρίθηκε από"; }
            }
            comments = comments.Trim().Equals("") ? "" : ", με σχόλια <br>\"" + comments + "\""; 
            _history += string.Format("<ul><li>{0}: {1} τον/την {2} στις {3}{4}</li></ul>", Status,actionName, u.Name , myDate( System.DateTime.Now, true), comments);
            return _history; 
        }

        public  string makeResultUrl(string action, string url)
        {
           
            url = HttpUtility.UrlDecode(url);
          
            string rurl = "/_layouts/_custom/Result.aspx?OpenPage&Action=" + action;
            bool found = false;
            string args = url.Contains("?") ? url.Split('?')[1] : "";
            foreach (string q in args.Split('&'))
            {
                if (q.StartsWith("Source=", StringComparison.InvariantCulture))
                {
                    found = true;
                    rurl += "&" + q;
                }
            }
            if (!found) { rurl += "&Source=" + SPContext.Current.Web.Url; }
            return rurl;
        }

        public bool IsValidMailAddress(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public string myDate(Object dtObj, bool showTime)
        {
            try
            {
                DateTime dt = (DateTime)dtObj; 
                string sd = dt.Day + "/" + dt.Month + "/" + dt.Year; 
                if (showTime) { sd += " " + dt.Hour.ToString().PadLeft(2, '0') + ":" + dt.Minute.ToString().PadLeft(2, '0') + ":" + dt.Second.ToString().PadLeft(2, '0');  }
                return sd; 
            }
            catch
            {
                string sd = dtObj.ToString();
                if (!showTime) { sd = sd.Contains(" ") ? sd.Split(' ')[0] : sd; }
                return sd;

            }
            }
        

        public void writeLog(SPWeb web, string title, string msg, bool isError)
        {
            SPList l = web.Lists["Log"];
            SPListItem log = l.AddItem();
            log["Title"] = title;
            log["Value"] = msg;
            log["Error"] = isError;
            log.Update(); 
        }

        public void EnsureDropDownList(SPListItem itm, string fieldName,  ref DropDownList ddl)
        {
            if (itm.ID>0)
            {
                string val = (itm[fieldName] ?? "").ToString();
                if (!val.Trim().Equals(""))
                {
                    foreach (ListItem li in ddl.Items)
                    {
                        if (li.Value.Equals(val)) return;
                    }
                    ddl.Items.Add(val);
                }
            }
        }

       
    }
}