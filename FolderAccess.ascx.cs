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
        protected void Page_Load(object sender, EventArgs e)
        {
            SPListItem itm = SPContext.Current.ListItem;
            if (!Page.IsPostBack)
            {
                Building.Items.AddRange(utils.getListItemsAsDialogList(itm.Web, "Building"));
                if (itm.ID > 0) { getFromBack(itm); } // not a new document ; 
                
                validateHides();
            }
            initLists(itm); 
            utils.initStandard(panelMain);
            if (itm.ID>0)
            {
                // L_Errors.Text = (itm["Approver"] ?? "N/A").ToString() + " ^^^ " + (itm["WaitingApprover"] ?? "N/A").ToString() + " ^^^ " + (itm["Composer"] ?? "N/A").ToString() + " ^^^^^^" + (itm["Author"] ?? "N/A").ToString(); 
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
                string[] textFields = new string[] { "From#Από" };
                string[] ddlFields = new string[] { "Building#Κτίριο" };
                ArrayList textFieldsExtra = new ArrayList();
                ArrayList ddlFieldsExtra = new ArrayList();
                AssetMgmtUtils utils = new AssetMgmtUtils();
                string rs = utils.validateDataStandard(panelMain, textFields, ddlFields);
                rs += utils.validateDataApprover(itm, panelMain);
                string myRightsList = (ViewState["myRightsList"] ?? "N/A").ToString();
                if (myRightsList.Trim().Equals(""))
                {
                    rs += "<li> Πρέπει να συμπληρώσετε τουλάχιστον μία γραμμή στον πίνακα δικαιωμάτων  </li>";
                }
                if (Name_1.AllEntities.Count > 0 || !AM_1.Text.Trim().Equals("") || !Folder_1.Text.Trim().Equals(""))
                {
                    if (Name_1.AllEntities.Count > 0) Name_1.ErrorMessage = "Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα Δικαιωμάτων αλλά ΔΕΝ  την έχετε εισάγει σε αυτόν. Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε την "; 
                    if (!AM_1.Text.Trim().Equals("")) AM_1.BackColor = utils.errColor;
                    if (!Folder_1.Text.Trim().Equals("")) Folder_1.BackColor = utils.errColor;
                    rs += "<li> Έχετε συμπληρώσει τιμή προς εισαγωγή στον πίνακα Δικαιωμάτων αλλά ΔΕΝ  την έχετε εισάγει σε αυτόν. Το σύστημα θεωρεί ότι δεν έχετε ολοκληρώσει την ενέργειά σας. Παρακαλούμε είτε εισάγετέ την στον πίνακα, είτε σβήστε την  </li>";
                }
                else {
                    Name_1.ErrorMessage = ""; 
                    AM_1.BackColor = System.Drawing.Color.White;
                    Folder_1.BackColor = System.Drawing.Color.White;
                }

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
            
        }

        private void initLists(SPListItem itm)
        {
            if (Page.IsPostBack)
            {
                RightsList = gu.deserializeFolderAccessGrid((string) (ViewState["myRightsList"] ?? "")); 
            }
            else
            {
                string rights = (itm["Rights"] ?? "").ToString();
                RightsList = gu.deserializeFolderAccessGrid(rights); 
                ViewState["myRightsList"] = gu.serializeFolderAccessGrid (RightsList);
            }
            folderRights.DataSource = RightsList; 
            folderRights.DataBind();
           
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
                //  itm["Rights"] = gu.serializeComplexGrid((List<GridUtils.ArrayStruct>)ViewState["myRightsList"]);
                itm["Rights"] = (string) (ViewState["myRightsList"] ?? "");
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
                    GridUtils.FolderAccessStruct fts1 = new GridUtils.FolderAccessStruct();
                if (Name_1.ResolvedEntities.Count > 0)
                {
                    SPFieldUserValueCollection col = utils.getPeopleEditorValue(SPContext.Current.Web, Name_1);
                    if (col.Count>0)
                    {
                        fts1.Name = col[0].LookupValue;
                        fts1.AM = AM_1.Text.Trim().Replace("##", "_").Replace("^^", "_");
                        string folder = Folder_1.Text.Trim().Replace("##", "_").Replace("^^", "_");
                        if (folder.EndsWith("\\")) { folder = folder.Substring(0, folder.Length - 1);  }
                        fts1.Folder = folder; 
                        fts1.Rights = Rights_1.SelectedValue.Trim().Replace("##", "_").Replace("^^", "_");
                      
                        RightsList.Add(fts1);
                    }
                }
                Name_1.AllEntities.Clear();
                Name_1.ResolvedEntities.Clear();
                    AM_1.Text = "";
                    Folder_1.Text = "";
                }
            
            ViewState["myRightsList"] = gu.serializeFolderAccessGrid( RightsList);
            folderRights.DataBind();
        }

        private string isValidNewGridRow()
        {
            string folder = Folder_1.Text.Trim(); 
            if (Name_1.ResolvedEntities.Count<1   || folder.Equals(""))
            {
                return "Το όνομα του χρήστη και ο κοινόχρηστος φάκελος πρέπει να συμπληρωθούν "; 
            }

            SPList l = SPContext.Current.Web.Lists["RootFolders"];
            bool found = false;
            string rs = ""; 
            foreach (SPListItem itm in l.Items)
            {
                if (folder.ToUpper().StartsWith(itm.Title.Trim().ToUpper())) { found = true; }
                rs += (itm.Title.Trim().ToUpper()) + ", "; 
            }
            if (!found) { return "Στο 'φάκελος' πρέπει να βάλετε ακριβώς το path. To path πρέπει να ξεκινάει από ένα από τα : " + rs; }
            return "" ; 
        }

        protected void Rights_DeleteCommand(object source, GridCommandEventArgs e)
        {
	    SPListItem itm = SPContext.Current.ListItem;
		string _status = (itm["_status"] ?? "").ToString(); 
		if (!_status.Trim().Equals("") && !_status.Trim().Equals("draft") && !_status.Trim().Equals("back")) return ;
            GridUtils.FolderAccessStruct toremove = (GridUtils.FolderAccessStruct)((GridDataItem)e.Item).DataItem;
            RightsList.Remove(toremove);
            ViewState["myRightsList"] = gu.serializeFolderAccessGrid( RightsList);
            //retrive entity form the Db
            folderRights.DataSource = RightsList; 
            folderRights.DataBind();
        }
    }
}