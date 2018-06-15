using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Telerik.Web.UI;
using System.Collections;



namespace AssetMgmt
{
    public partial class MailAccess : System.Web.UI.UserControl
    {

        List<GridUtils.NameStruct> ForwardToList = new List<GridUtils.NameStruct>();
        List<GridUtils.NameStruct> SendAsList = new List<GridUtils.NameStruct>();
        AssetMgmtUtils utils = new AssetMgmtUtils();
        GridUtils gu = new AssetMgmt.GridUtils();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            SPListItem itm = SPContext.Current.ListItem;
            if (!Page.IsPostBack)
            {
                Building.Items.AddRange(utils.getListItemsAsDialogList(SPContext.Current.Web, "Building"));
                EMailAddressPostfix.Items.AddRange(utils.getListItemsAsDialogList(SPContext.Current.Web, "EMailAddressPostfix"));
                if (itm.ID > 0) { getFromBack(itm); } // not a new document
                validateHides();
            }
            initLists(itm);
            utils.initStandard(panelMain);
            if (itm.ID > 0) EMailAddressDiffs.Text = getEMailAddressDiffs(itm);
        }


        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool isDraft = utils.isDraft(SPContext.Current.ListItem);
            if (!isDraft)
            {
                SendAs.MasterTableView.Columns[1].Visible = false;
                ForwardTo.MasterTableView.Columns[1].Visible = false;
                //ForwardTo.Rebind(); 
                //SendAs.Rebind();
            }
        }


        private void validateHides() // custom
        {

            SPListItem itm = SPContext.Current.ListItem;
            bool isWaitingApprover = utils.isWaitingApprover(itm, "Mail");
            bool isDraft = utils.isDraft(itm);
            if (isWaitingApprover)
            {
                if (isDraft && isWaitingApprover)
                {
                    showEditBody(true);
                    BSave.Visible = true;
                    BSubmit.Visible = true;
                    BBack.Visible = false;
                    BReject.Visible = false;
                    Comments.Visible = false;
                    L_Comments.Visible = false;
                    ddlApprover.Visible = true;
                    
    }
                if (!isDraft)
                {
                    showEditBody(false);
                    Comments.Visible = isWaitingApprover;
                    L_Comments.Visible = isWaitingApprover;
                    BSave.Visible = false;
                    BSubmit.Visible = isWaitingApprover;
                    BBack.Visible = isWaitingApprover;
                    BReject.Visible = isWaitingApprover;
                    BSubmit.Text = (itm["_status"] ?? "").ToString().Trim().Equals("pendingImplementation") ? "Υλοποιηση" : "Εγκριση";
                    ddlApprover.Visible = false;
                }
            }
            else // is not waiting approver 
            {
                showEditBody(false);
                BSave.Visible = false;
                BSubmit.Visible = false;
                BBack.Visible = false;
                BReject.Visible = false;
                Comments.Visible = false;
                L_Comments.Visible = false;
                ddlApprover.Visible = false;

            }
        }

        private bool validateData()
        {
            try
            {
                SPListItem itm = SPContext.Current.ListItem;
                L_Error.Text = "";
                string[] textFields = new string[] { "From#Από", "DisplayName#Display Name", "EMailAddressPrefix#EMail Address" };
                string[] ddlFields = new string[] { "Building#Κτίριο", "EMailAddressPostfix#EMail Address (Domain)" };
                ArrayList textFieldsExtra = new ArrayList();
                ArrayList ddlFieldsExtra = new ArrayList();
                AssetMgmtUtils utils = new AssetMgmtUtils();
                string rs = utils.validateDataStandard(panelMain, textFields, ddlFields);
                rs += utils.validateDataApprover(itm, panelMain);
                if (ForwardToAdd_1.AllEntities.Count > 0)
                {
                    string errMsg = "Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα 'Forward To'  αλλά ΔΕΝ την έχετε εισάγει σε αυτόν.Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε τη";
                    ForwardToAdd_1.ErrorMessage = errMsg;
                    rs += "<li>" + errMsg + "</li>";
                }
                else { ForwardToAdd_1.ErrorMessage = ""; };
                //if (!SendAsAdd_1.Text.Trim().Equals(""))  TOCHANGE
                if (SendAsAdd_1.AllEntities.Count > 0)
                {
                    string errMsg = "Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα 'Send As'  αλλά ΔΕΝ την έχετε εισάγει σε αυτόν.Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε τη";
                    SendAsAdd_1.ErrorMessage = errMsg;
                    rs += "<li>" + errMsg + "</li>";
                }
                else { SendAsAdd_1.ErrorMessage = ""; }
                rs += validateMail(true); 
                if (!rs.Equals(""))
                {
                    rs = "<ul>" + rs + "</ul>";
                    L_Errors.Text = rs; 
                    return false;
                }
                else { return true; }
            }
            catch (Exception e) { L_Error.Text = "validateData:" + e.Message; return false; }
        }



        protected void BSave_Click(object sender, EventArgs e)
        {
            try
            {
                L_Errors.Text = "";
                SPListItem itm = SPContext.Current.ListItem;
                AssetMgmtUtils utils = new AssetMgmtUtils();
                saveToBack(itm);

                utils.BSave_ClickStandard(itm, panelMain, Page);

            }
            catch (Exception exc) { L_Errors.Text += exc.Message; }
        }

        protected void BSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                L_Errors.Text = "";
                if (validateData())
                {
                    AssetMgmtUtils utils = new AssetMgmtUtils();
                    SPListItem itm = SPContext.Current.ListItem;
                    saveToBack(itm);
                    itm = itm.ParentList.GetItemById(itm.ID);  // get the item from the back end 
                    utils.BSubmit_ClickStandard(itm, panelMain, Page);
                }
            }
            catch (Exception exc) { L_Errors.Text = exc.Message; }
        }

        protected void BBack_Click(object sender, EventArgs e)
        {
            try
            {
                AssetMgmtUtils utils = new AssetMgmtUtils();
                SPListItem itm = SPContext.Current.ListItem;
                saveToBack(itm);
                utils.BBack_ClickStandard(itm, panelMain, Page);

            }
            catch (Exception exc) { L_Errors.Text = exc.Message; }
        }

        protected void BReject_Click(object sender, EventArgs e)
        {
            try
            {
                AssetMgmtUtils utils = new AssetMgmtUtils();
                SPListItem itm = SPContext.Current.ListItem;
                saveToBack(itm);
                utils.BReject_ClickStandard(itm, panelMain, Page);

            }
            catch (Exception exc) { L_Errors.Text = exc.Message; }
        }



        private void saveToBack(SPListItem itm)
        {
            try
            {
                AssetMgmtUtils utils = new AssetMgmtUtils();
                utils.saveToBackStandard(itm, panelMain);
                string _status = (itm["_status"] ?? "").ToString();
                if (_status.Trim().Equals("")) { itm["_status"] = "draft"; }
                itm["Building"] = utils.serializeDialog(Building);
                itm["From"] = From.Text;
                itm["DisplayName"] = DisplayName.Text;
                itm["EMailAddressPrefix"] = EMailAddressPrefix.Text.Trim();
                itm["EMailAddressPostfix"] = utils.serializeDialog(EMailAddressPostfix);
                itm["EMailAddress"] = EMailAddressPrefix.Text.Trim() + "@" + utils.serializeDialog(EMailAddressPostfix);
                itm["EMailAddressChecked"] = EMailAddressChecked.Text;
                itm["EMailAddressDiffs"] = EMailAddressDiffs.Text;
                itm["EMailAddressOldForwardTo"] = EMailAddressOldForwardTo.Text;
                itm["EMailAddressOldSendAs"] = EMailAddressOldSendAs.Text;
                itm["MaxRecipients"] = MaxRecipients.Text;
                itm["ForwardTo"] = gu.serializeSimpleGrid((List<GridUtils.NameStruct>)ViewState["myForwardToList"]);
                itm["SendAs"] = gu.serializeSimpleGrid((List<GridUtils.NameStruct>)ViewState["mySendAsList"]);
                itm.SystemUpdate();
            }
            catch (Exception e) { L_Error.Text += " --saveToBack:" + e.Message; }

        }


        private void getFromBack(SPListItem itm)
        {
            try
            {
                AssetMgmtUtils utils = new AssetMgmt.AssetMgmtUtils();

                utils.getFromBackStandard(itm, panelMain);
                //fields & labels
                From.Text = (itm["From"] ?? "").ToString();
                L_From.Text = (itm["From"] ?? "").ToString();
                Building.SelectedValue = (itm["Building"] ?? "").ToString();
                L_Building.Text = (itm["Building"] ?? "").ToString();
                DisplayName.Text = (itm["DisplayName"] ?? "").ToString();
                L_DisplayName.Text = (itm["DisplayName"] ?? "").ToString();
                EMailAddressPrefix.Text = (itm["EMailAddressPrefix"] ?? "").ToString();
                EMailAddressPostfix.Text = (itm["EMailAddressPostfix"] ?? "").ToString();
                L_EMailAddress.Text = EMailAddressPrefix.Text.Trim() + "@" + EMailAddressPostfix.Text;
                MaxRecipients.Text = (itm["MaxRecipients"] ?? "").ToString();
                L_MaxRecipients.Text = (itm["MaxRecipients"] ?? "").ToString();
                EMailAddressChecked.Text = (itm["EMailAddressChecked"] ?? "").ToString();
                EMailAddressOldForwardTo.Text = (itm["EMailAddressOldForwardTo"] ?? "").ToString();
                EMailAddressOldSendAs.Text = (itm["EMailAddressOldSendAs"] ?? "").ToString();
                EMailAddressDiffs.Text = (itm["EMailAddressDiffs"] ?? "").ToString();
               

            }
            catch (Exception e) { L_Error.Text += " --getFromBack:" + e.Message; }
        }


        private void showEditBody(bool showEdit)
        {
            From.Visible = showEdit;
            L_From.Visible = !showEdit;
            Building.Visible = showEdit;
            L_Building.Visible = !showEdit;
            EMailAddressPrefix.Visible = showEdit;
            EMailAddressPostfix.Visible = showEdit;
            EMailAt.Visible = showEdit; 
            L_EMailAddress.Visible = !showEdit;
            L_EMailAddress.Visible = !showEdit;
            DisplayName.Visible = showEdit;
            L_DisplayName.Visible = !showEdit;
            MaxRecipients.Visible = showEdit;
            L_MaxRecipients.Visible = !showEdit;
            ForwardTo.Enabled = showEdit;
            SendAs.Enabled = showEdit;
            panelForwardToAdd.Visible = showEdit;
            panelSendAsAdd.Visible = showEdit;
            BLoadMailDetails.Visible = showEdit; 
        }

        private void initLists(SPListItem itm)
        {
            if (Page.IsPostBack)
            {
                ForwardToList = (List<GridUtils.NameStruct>)ViewState["myForwardToList"];
                SendAsList = (List<GridUtils.NameStruct>)ViewState["mySendAsList"];
            }
            else
            {
                getListsViewStateFromBack(itm);
            }
            ForwardTo.DataSource = ViewState["myForwardToList"];
            ForwardTo.DataBind();
            SendAs.DataSource = ViewState["mySendAsList"];
            SendAs.DataBind();
        }

        private void getListsViewStateFromBack(SPListItem itm)
        {
            ForwardToList = new List<GridUtils.NameStruct>();
            SendAsList = new List<GridUtils.NameStruct>();
            string forwardTo = (itm["ForwardTo"] ?? "").ToString();
            foreach (string f in forwardTo.Split('#'))
            {
                if (!f.Trim().Equals(""))
                { GridUtils.NameStruct fts1 = new GridUtils.NameStruct(); fts1.Name = f; ForwardToList.Add(fts1); }
            }
            string sendAs = (itm["SendAs"] ?? "").ToString();
            foreach (string f in sendAs.Split('#'))
            {
                if (!f.Trim().Equals(""))
                { GridUtils.NameStruct fts1 = new GridUtils.NameStruct(); fts1.Name = f; SendAsList.Add(fts1); }
            }

            ViewState["myForwardToList"] = ForwardToList;
            ViewState["mySendAsList"] = SendAsList;
        }





        protected void ForwardTo_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridUtils.NameStruct toremove = (GridUtils.NameStruct)((GridDataItem)e.Item).DataItem;
            ForwardToList.Remove(toremove);
            ViewState["myForwardToList"] = ForwardToList;
            //retrive entity form the Db
            ForwardTo.DataSource = ViewState["myForwardToList"];
            ForwardTo.DataBind();
        }


        protected void SendAs_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridUtils.NameStruct toremove = (GridUtils.NameStruct)((GridDataItem)e.Item).DataItem;
            SendAsList.Remove(toremove);
            ViewState["mySendAsList"] = SendAsList;
            //retrive entity form the Db
            SendAs.DataSource = ViewState["mySendAsList"];
            SendAs.DataBind();
        }




        protected void B_ForwardTo_Add_Click(object sender, EventArgs e)
        {
            //string emailAddress = ForwardToAdd_1.Text.Trim(); 
            //if (!emailAddress.Equals(""))
            //{
            //    if (!utils.IsValidMailAddress(emailAddress))
            //    {
            //        L_ForwardToErr.Text = "Πρέπει να εισάγετε έγκυρη email address";
            //    }
            //    else
            //    {
            //        L_ForwardToErr.Text = ""; 
            //        GridUtils.NameStruct fts1 = new GridUtils.NameStruct();
            //        fts1.Name = ForwardToAdd_1.Text.Trim().Replace('#', '_');
            //        ForwardToList.Add(fts1);
            //        ForwardToAdd_1.Text = "";
            //    }
            //}
            if (ForwardToAdd_1.ResolvedEntities.Count > 0)
            {
                GridUtils.NameStruct fts1 = new GridUtils.NameStruct();

                SPFieldUserValueCollection col = utils.getPeopleEditorValue(SPContext.Current.Web, ForwardToAdd_1);
                if (col.Count > 0)
                {
                    //  string emailUser = col[0].LoginName + "@" + col[0].LookupValue + "@" + col[0].User.LoginName;
                    L_ForwardToErr.Text = "";

                    fts1.Name = col[0].LookupValue;
                    ForwardToList.Add(fts1);
                }
            }
            else
            {
                L_ForwardToErr.Text = "Πρέπει να εισάγετε έγκυρο χρήστη";
            }
            ViewState["myForwardToList"] = ForwardToList;
            ForwardTo.DataBind();
            ForwardToAdd_1.AllEntities.Clear();
            ForwardToAdd_1.ResolvedEntities.Clear();
        }


        protected void B_SendAs_Add_Click(object sender, EventArgs e)
        {



            if (SendAsAdd_1.ResolvedEntities.Count > 0)
            {
                GridUtils.NameStruct fts1 = new GridUtils.NameStruct();

                SPFieldUserValueCollection col = utils.getPeopleEditorValue(SPContext.Current.Web, SendAsAdd_1);
                if (col.Count > 0)
                {
                    //  string emailUser = col[0].LoginName + "@" + col[0].LookupValue + "@" + col[0].User.LoginName;
                    L_SendAsErr.Text = "";

                    fts1.Name = col[0].LookupValue;
                    SendAsList.Add(fts1);
                }
            }
            else
            {
                L_SendAsErr.Text = "Πρέπει να εισάγετε έγκυρο χρήστη";
            }

            ViewState["mySendAsList"] = SendAsList;
            SendAs.DataBind();
            SendAsAdd_1.AllEntities.Clear();
            SendAsAdd_1.ResolvedEntities.Clear();


        }






        protected void BLoadMailDetails_Click(object sender, EventArgs e)
        {
            string eMailAddress = "";
            string err = "";
            
            try
            {
                SPListItem itm = SPContext.Current.ListItem;
                
                string validateErr = validateMail(false);
                if (!validateErr.Equals(""))
                {
                    validateErr = "<li>" + validateErr + "</li>";
                    validateErr = "<ul>" + validateErr + "</ul>";
                    L_LoadMailDetailsErr.Text = validateErr;
                    return;
                }
                else { L_LoadMailDetailsErr.Text = ""; }

                eMailAddress = EMailAddressPrefix.Text + "@" + EMailAddressPostfix.SelectedValue;
               
                
                if (!eMailAddress.Equals(""))
                {
                    ReportUtils ru = new AssetMgmt.ReportUtils();
                    EMailAddressChecked.Text = eMailAddress;
                    SPListItem pitm = ru.getLastItemByEMailAccount(itm.ParentList, eMailAddress);

                    if (pitm != null)
                    {
                        
                        DisplayName.Text = (pitm["DisplayName"] ?? "").ToString();
                        MaxRecipients.Text = (pitm["MaxRecipients"] ?? "").ToString();
                        getListsViewStateFromBack(pitm);
                        ForwardTo.DataBind();
                        SendAs.DataBind();
                        EMailAddressOldForwardTo.Text  = gu.serializeSimpleGrid((List<GridUtils.NameStruct>)ViewState["myForwardToList"]);
                        EMailAddressOldSendAs.Text = gu.serializeSimpleGrid((List<GridUtils.NameStruct>)ViewState["mySendAsList"]);
                        
                        Page_Load(sender, e);
                    }
                    else { L_LoadMailDetailsErr.Text = "Δεν βρέθηκε προηγούμενη υλοποιημένη αίτηση για αυτό το Group Mail (" + eMailAddress + ")"; }
                    // BRefresh_Click(sender, e); 
                }
            }
            catch (Exception exc) { L_LoadMailDetailsErr.Text += "Error on BLoadMailDetails_Click:" + err + "-" + exc.Message; }
        }

        private string validateMail(bool isSubmit)
        {
            EMailAddressPrefix.BackColor = System.Drawing.Color.White;
            EMailAddressPostfix.BackColor = System.Drawing.Color.White;
            AssetMgmtUtils utils = new AssetMgmtUtils(); 
            string eMailAddressPrefix = EMailAddressPrefix.Text;
            string err = ""; 
            string eMailAddressPostFix = (EMailAddressPostfix.SelectedValue ?? "");
            if (eMailAddressPostFix.Trim().Equals(""))
            {
                EMailAddressPrefix.BackColor = utils.errColor;
                err = "Πρέπει να επιλέξετε email address. ";
            }
            if (eMailAddressPostFix.Trim().Equals(""))
            {
                EMailAddressPostfix.BackColor = utils.errColor;
                err =  "Πρέπει να επιλέξετε email address domain. ";
            }
            if (isSubmit && !(eMailAddressPrefix + "@" + eMailAddressPostFix).Trim().Equals(EMailAddressChecked.Text.Trim(),StringComparison.InvariantCulture ))
            {
                err = "Πρέπει να πατήσετε το 'Τρέχουσα Εικόνα GroupMail', για το mail που έχετε επιλέξει. ";
            }
            bool ok = false;
            for (int i = 0; i < eMailAddressPrefix.Length; i++)
            {
                ok = false; 
                char c = eMailAddressPrefix[i];
                if (c >= 'a' && c <= 'z') ok = true;
                if (c >= 'A' && c <= 'Z') ok = true;
                if (c >= '0' && c <= '9') ok = true;
                if (c.Equals('-') || c.Equals('_') || c.Equals('.')) ok = true;
                if (!ok)
                {
                    EMailAddressPrefix.BackColor = utils.errColor;
                    err +=  "Στο email address επιτρέπονται  μόνο λατινικοί χαρακτήρες , αριθμοί και οι χαρακτήρες '-' '_' '.' ";
                }
            }
            
            return err; // passed validation 
        }


        private string getEMailAddressDiffs(SPListItem itm)
        {
            if (EMailAddressOldSendAs.Text.Trim().Equals("") && EMailAddressOldForwardTo.Text.Trim().Equals(""))
            {
                return "To αίτημα είναι το 1ο για την συγκεκριμένη email address";
            }
            string rs = ""; 
            string before = EMailAddressOldSendAs.Text;
            string after = (itm["SendAs"] ?? "").ToString();
            string sendAsAdded = findAdds(before, after);
            if (!sendAsAdded.Equals("")) { rs += "Προστέθηκαν στο Send As: " + sendAsAdded + "</br>";  }
            string sendAsRemoved = findAdds(after, before);
            if (!sendAsRemoved.Equals("")) { rs += "Αφαιρέθηκαν από το Send As: " + sendAsRemoved + "</br>"; }
            before = EMailAddressOldForwardTo.Text;
            after = (itm["ForwardTo"] ?? "").ToString();
            string forwardToAdded = findAdds(before, after);
            if (!forwardToAdded.Equals("")) { rs += "Προστέθηκαν στο Forwrad To: " + forwardToAdded + "</br>"; }
            string forwardToRemoved = findAdds(after, before);
            if (!forwardToRemoved.Equals("")) { rs += "Αφαιρέθηκαν από το Forward To: " + forwardToRemoved + "</br>"; }
            return rs; 
        }

        string findAdds (string before, string after)
        {
            List<string> rs = new List<string>();  
            foreach (string af in after.Split('#'))
            {
                if (!before.Contains(af)) rs.Add(af); 
            }
            if (rs.Count > 0) return String.Join(",", rs.ToArray()); else { return "";  }
        }


    }

}