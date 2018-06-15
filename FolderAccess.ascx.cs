using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Telerik.Web.UI; 
namespace AssetMgmt
{
    public partial class FolderAccess : System.Web.UI.UserControl
    {
        AssetMgmtUtils utils = new AssetMgmtUtils();
        GridUtils gu = new AssetMgmt.GridUtils();
        List<GridUtils.FolderAccessStruct> RightsList = new List<GridUtils.FolderAccessStruct>();
        List<GridUtils.FolderAccessStructV2> RightsListV2 = new List<GridUtils.FolderAccessStructV2>();

        protected void Page_Load(object sender, EventArgs e)
        {
            SPListItem itm = SPContext.Current.ListItem;
            
            if (!Page.IsPostBack)
            {
                Building.Items.AddRange(utils.getListItemsAsDialogList(itm.Web, "Building"));
                FolderPrefix.Items.AddRange(utils.getListItemsAsDialogList(itm.Web, "RootFolders"));
                if (itm.ID > 0) { getFromBack(itm); } // not a new document ; 
                
                validateHides();
            }
            
            initLists(itm);
            
            utils.initStandard(panelMain);
            if (itm.ID>0)
            {
                // L_Errors.Text = (itm["Approver"] ?? "N/A").ToString() + " ^^^ " + (itm["WaitingApprover"] ?? "N/A").ToString() + " ^^^ " + (itm["Composer"] ?? "N/A").ToString() + " ^^^^^^" + (itm["Author"] ?? "N/A").ToString(); 
                 FolderDiffs.Text = getFolderDiffs(itm);
            }
           
            if (itm.ID < 30) { folderRightsV2.Visible = false;  } // DO NOT SHOW NEW VERSION IN OLD DOCUMENTS 
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
                    BLoadFolderDetails.Visible = true;
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
                    BLoadFolderDetails.Visible = false;
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
                BLoadFolderDetails.Visible = false;
            }
        }

        private bool validateData()
        {
            try
            {
               
                
              SPListItem itm = SPContext.Current.ListItem;
                L_Error.Text = "";
                string[] textFields = new string[] { "From#Από", "FolderPostfix#Folder" };
                string[] ddlFields = new string[] { "Building#Κτίριο", "FolderPrefix#Root Folder" };
                ArrayList textFieldsExtra = new ArrayList();
                ArrayList ddlFieldsExtra = new ArrayList();
                AssetMgmtUtils utils = new AssetMgmtUtils();
                string rs = utils.validateDataStandard(panelMain, textFields, ddlFields);
                rs += utils.validateDataApprover(itm, panelMain);
                string myRightsList = (ViewState["myRightsList"] ?? "N/A").ToString();
                string myRightsListV2 = (ViewState["myRightsListV2"] ?? "N/A").ToString();
                if (myRightsListV2.Trim().Equals(""))
                {
                    rs += "<li> Πρέπει να συμπληρώσετε τουλάχιστον μία γραμμή στον πίνακα δικαιωμάτων  </li>";
                }
                if (Name_1.AllEntities.Count > 0 || !AM_1.Text.Trim().Equals(""))
                {
                    if (Name_1.AllEntities.Count > 0) Name_1.ErrorMessage = "Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα Δικαιωμάτων αλλά ΔΕΝ  την έχετε εισάγει σε αυτόν. Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε την "; 
                    if (!AM_1.Text.Trim().Equals("")) AM_1.BackColor = utils.errColor;
                    
                    rs += "<li> Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα Δικαιωμάτων αλλά ΔΕΝ  την έχετε εισάγει σε αυτόν. Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε την  </li>";
                }
                else {
                    Name_1.ErrorMessage = ""; 
                    AM_1.BackColor = System.Drawing.Color.White;
                    
                }

                rs += validateFolder(true);

                if (!rs.Equals(""))
                {
                    rs = "<ul>" + rs + "</ul>";
                    L_Errors.Text = rs; ;
                    return false;
                }
                else { return true; }
            }
            catch (Exception e) { L_Error.Text = "validateData:" + e.Message; return false; }
        }

        private void showEditBody(bool showEdit)
        {
From.Visible = showEdit;
            L_From.Visible = !showEdit;
            Building.Visible = showEdit;
            L_Building.Visible = !showEdit;
            panelAdd.Visible = showEdit;
            FolderPostfix.Visible = showEdit;
            FolderPrefix.Visible = showEdit;
            L_Folder.Visible = !showEdit;
            
    }

        private void initLists(SPListItem itm)
        {
            if (Page.IsPostBack)
            {
                RightsList = gu.deserializeFolderAccessGrid((string) (ViewState["myRightsList"] ?? ""));
                RightsListV2 = gu.deserializeFolderAccessGridV2((string)(ViewState["myRightsListV2"] ?? ""));
                //     RightsListV2 = (List<GridUtils.FolderAccessStructV2>) (ViewState["myRightsListV2"] ?? "");
            }
            else
            {
                
                string rights = (itm["Rights"] ?? "").ToString();
                RightsList = gu.deserializeFolderAccessGrid(rights);
                ViewState["myRightsList"] = gu.serializeFolderAccessGrid(RightsList);
                string rightsV2 = (itm["RightsV2"] ?? "").ToString();
                RightsListV2 = gu.deserializeFolderAccessGridV2(rightsV2);
                ViewState["myRightsListV2"] = gu.serializeFolderAccessGridV2 (RightsListV2);
            }
            folderRights.DataSource = RightsList; 
            folderRights.DataBind();
            folderRightsV2.DataSource = RightsListV2;
            folderRightsV2.DataBind();
            if (RightsList.Count.Equals(0)) { folderRights.Visible = false;  }
           
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
                itm["From"] = From.Text;
                itm["Building"] = utils.serializeDialog(Building);
                itm["Folder"] = FolderPrefix.SelectedValue + FolderPostfix.Text.Trim();
                itm["FolderOldRights"] = L_FolderOldRights.Text;
                itm["FolderChecked"] = L_FolderChecked.Text; 
                //  itm["Rights"] = gu.serializeComplexGrid((List<GridUtils.ArrayStruct>)ViewState["myRightsList"]);
                itm["Rights"] = (string)(ViewState["myRightsList"] ?? "");
                itm["RightsV2"] = (string)(ViewState["myRightsListV2"] ?? "");
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
                Building.SelectedValue = (itm["Building"] ?? "").ToString();

                L_Building.Text = (itm["Building"] ?? "").ToString();
		From.Text = (itm["From"] ?? "").ToString();
		 L_From.Text = (itm["From"] ?? "").ToString();
                string folder = (itm["Folder"] ?? "").ToString().Trim();
                L_Folder.Text = folder; 
                if (!folder.Equals(""))
                {
                    bool found = false; 
                    foreach (ListItem  litm  in FolderPrefix.Items)
                    {
                        string val = litm.Value.Trim();
                        if (!val.Equals(""))
                        {
                            if (folder.StartsWith(litm.Value))
                            {
                                FolderPrefix.SelectedValue = litm.Value;
                                FolderPostfix.Text = folder.Replace(litm.Value, "");
                                found = true;
                            }
                            if (!found) { FolderPostfix.Text = folder; }
                        }
                    }
                }
                L_FolderOldRights.Text = (itm["FolderOldRights"] ?? "").ToString();
                L_FolderChecked.Text = (itm["FolderChecked"] ?? "").ToString();
            }
            
            catch (Exception e) { L_Error.Text += " --getFromBack:" + e.Message; }
        }






        protected void B_Add_Click(object sender, EventArgs e)
        {
           
                string gridErr = isValidNewGridRow();
            if (!gridErr.Trim().Equals(""))
            {
                L_FolderGridErr.Text = gridErr;
            }
            else
            {
                L_FolderGridErr.Text = "";
                GridUtils.FolderAccessStructV2 fts1 = new GridUtils.FolderAccessStructV2();
                if (Name_1.ResolvedEntities.Count > 0)
                {
                    SPFieldUserValueCollection col = utils.getPeopleEditorValue(SPContext.Current.Web, Name_1);
                    if (col.Count > 0)
                    {
                        fts1.Name = col[0].LookupValue;
                        fts1.AM = AM_1.Text.Trim().Replace("##", "_").Replace("^^", "_");
                        fts1.Rights = Rights_1.SelectedValue.Trim().Replace("##", "_").Replace("^^", "_");

                        RightsListV2.Add(fts1);
                    }
                }
                else
                {
                    L_FolderGridErr.Text = "Πρέπει να εισάγετε έγκυρο χρήστη";
                }
                Name_1.AllEntities.Clear();
                Name_1.ResolvedEntities.Clear();
                AM_1.Text = "";
            }
            ViewState["myRightsListV2"] = gu.serializeFolderAccessGridV2( RightsListV2);
            folderRightsV2.DataBind();
        }


        private void initLists()
        {
            SPList l = SPContext.Current.Web.Lists["RootFolders"];

        }

        private string isValidNewGridRow()
        {
           
            if (Name_1.ResolvedEntities.Count<1   )
            {
                return "Το όνομα του χρήστη πρέπει να συμπληρωθεί "; 
            }

            
            return "" ; 
        }

        protected void Rights_DeleteCommand(object source, GridCommandEventArgs e)
        {
	    SPListItem itm = SPContext.Current.ListItem;
		string _status = (itm["_status"] ?? "").ToString(); 
		if (!_status.Trim().Equals("") && !_status.Trim().Equals("draft") && !_status.Trim().Equals("back")) return ;
            GridUtils.FolderAccessStructV2 toremove = (GridUtils.FolderAccessStructV2)((GridDataItem)e.Item).DataItem;
            RightsListV2.Remove(toremove);
            ViewState["myRightsListV2"] = gu.serializeFolderAccessGridV2( RightsListV2);
            //retrive entity form the Db
            folderRightsV2.DataSource = RightsListV2; 
            folderRightsV2.DataBind();
        }



        private string validateFolder(bool isSubmit)
        {
            if (isSubmit && !(FolderPrefix.SelectedValue + FolderPostfix.Text).Trim().Equals(L_FolderChecked.Text.Trim(), StringComparison.InvariantCulture))
            {
                return  "Πρέπει να πατήσετε το 'Τρέχουσα Εικόνα Folder', για το folder που έχετε επιλέξει. ";
            }
            return ""; 
        }

        protected void BLoadFolderDetails_Click(object sender, EventArgs e)
        {
            string folder = "";
            string err = "";

            try
            {
                SPListItem itm = SPContext.Current.ListItem;

                string validateErr = validateFolder(false);
                if (!validateErr.Equals(""))
                {
                    validateErr = "<li>" + validateErr + "</li>";
                    validateErr = "<ul>" + validateErr + "</ul>";
                    L_LoadFolderDetailsErr.Text = validateErr;
                    return;
                }
                else { L_LoadFolderDetailsErr.Text = ""; }

                folder = FolderPrefix.SelectedValue + FolderPostfix.Text ;
                L_FolderChecked.Text = folder;

                if (!folder.Equals(""))
                {
                    ReportUtils ru = new AssetMgmt.ReportUtils();
                    L_FolderChecked.Text = folder;
                    SPListItem pitm = ru.getLastItemByFolder(itm.ParentList, folder);

                    if (pitm != null)
                    {
                        getListViewStateFromBack(pitm);
                        
                        folderRightsV2.DataBind();
                        
                        L_FolderOldRights.Text = (string) ViewState["myRightsListV2"];
                        


                        Page_Load(sender, e);
                    }
                    else { L_LoadFolderDetailsErr.Text = "Δεν βρέθηκε προηγούμενη υλοποιημένη αίτηση για αυτό το Folder (" + folder + ")"; }
                    // BRefresh_Click(sender, e); 
                }
            }
            catch (Exception exc) { L_LoadFolderDetailsErr.Text += "Error on BLoadFolderDetails_Click:" + err + "-" + exc.Message; }
        }


        private void getListViewStateFromBack(SPListItem itm)
        {
            RightsListV2 = new List<GridUtils.FolderAccessStructV2>();
          
            string grid = (itm["RightsV2"] ?? "").ToString();
            foreach (string row in grid.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries))
            {
                GridUtils.FolderAccessStructV2 rowStruct = new AssetMgmt.GridUtils.FolderAccessStructV2();
                string[] vals = row.Split(new string[] { "^^" }, StringSplitOptions.None);
                rowStruct.Name = vals[0];
                rowStruct.AM = vals[1];
                rowStruct.Rights = vals[2];
                RightsListV2.Add(rowStruct);
            }
            
            ViewState["myRightsListV2"] = gu.serializeFolderAccessGridV2(RightsListV2);
            
        }

        private string getFolderDiffs(SPListItem itm)
        {
            if (L_FolderOldRights.Text.Trim().Equals(""))
            {
                return "To αίτημα είναι το 1ο για την συγκεκριμένη email address";
            }
            string rs = "";
            string before = L_FolderOldRights.Text;
            string after = (itm["RightsV2"] ?? "").ToString();
            string rightsAdded = findAdds(before, after);
            if (!rightsAdded.Equals("")) { rs += "Δικαιώματα που Προστέθηκαν : " + rightsAdded + "</br>"; }
            string rightsRemoved = findAdds(after, before);
            if (!rightsRemoved.Equals("")) { rs += "Δικαιώματα που Αφαιρέθηκαν : " + rightsRemoved + "</br>"; }
            
            return rs;
        }


        string findAdds(string before, string after)
        {
            List<string> rs = new List<string>();
            foreach (string af in after.Split(new string[] { "##" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (!before.Contains(af)) rs.Add(af.Replace("^^", "-"));
            }
            if (rs.Count > 0) return String.Join(", ", rs.ToArray()); else { return ""; }
        }
    }
}