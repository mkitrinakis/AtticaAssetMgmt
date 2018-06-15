using System;
using System.Collections.Generic;
using System.Collections; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint; 

namespace AssetMgmt
{
    public partial class SystemAccess : System.Web.UI.UserControl
    {
        AssetMgmtUtils utils = new AssetMgmtUtils();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            SPListItem itm = SPContext.Current.ListItem;
            if (!Page.IsPostBack)
            {
                Building.Items.AddRange(utils.getListItemsAsDialogList(itm.Web, "Building"));
                if (itm.ID > 0) { getFromBack(itm); } // not a new document ; 
                validateHides(); 
            }
            utils.initStandard(panelMain);
           
            hideSystemSections();
            
            
        }


        private void hideSystemSections()
        {
            foreach (ListItem li in Systems.Items)
            {
                if (li.Value.Equals("T24")) { panelT24.Visible = li.Selected; }
                if (li.Value.Equals("WINDOWS")) { panelWindows.Visible = li.Selected; }
                if (li.Value.Equals("ORACLE")) { panelOracle.Visible = li.Selected; }
                if (li.Value.Equals("AROTRON")) { panelArotron.Visible = li.Selected; }
                if (li.Value.Equals("U/SWITCHWARE")) { panelOS.Visible = li.Selected; }
                
            }
        }


        private bool validateData()
        {
            try {

             
               SPListItem itm = SPContext.Current.ListItem; 
            string[] textFields = new string[] { "From#Από", "AM#ΑΜ",  "Name#Όνομα", "Surname#Επώνυμο", "NameLatin#Όνομα (στα Λατινικά)", "SurnameLatin#Επώνυμο (στα Λατινικά)" };
            string[] ddlFields = new string[] { "Building#Κτίριο", "Subject#Θέμα" };
                ArrayList textFieldsExtra = new ArrayList();
                ArrayList ddlFieldsExtra = new ArrayList(); 
            
            string rs = utils.validateDataStandard( panelMain, textFields, ddlFields);
                foreach (ListItem cb in Systems.Items)
                {
                    if (cb.Selected)
                    {
                       if ( cb.Value.Equals("T24")) {  textFieldsExtra.Add("T24Profile#T24 - Στοιχεία Profile Νέου Χρήστη"); }
                      //  if (cb.Value.Equals("U/SWITCHWARE")) { textFieldsExtra.Add("OSRole#U/SWITCHWARE- Κωδικός Ρόλου"); textFieldsExtra.Add("OSDep#U/SWITCHWARE- Κωδικός Διεύθυνσης"); }
                        if (cb.Value.Equals("ORACLE")) { ddlFieldsExtra.Add("OracleAccess#Για Oracle");}
                        if (cb.Value.Equals("AROTRON")) { textFieldsExtra.Add("ArotronRole#AROTRON - Ρόλος στην Ομάδα"); textFieldsExtra.Add("ArotronGroup#AROTRON - Ομάδα Εργασίας"); }
                    }
                }
                rs += utils.validateDataStandard( panelMain, (string []) textFieldsExtra.ToArray(typeof(string)), (string []) ddlFieldsExtra.ToArray(typeof(string)));
                rs += utils.validateDataApprover(itm, panelMain); 
                if (!EndDate.IsValid)
                {
                    rs += "<li> Δεν είναι έγκυρη η ημερομηνία στην Λήξη Πρακτικής</li>"; 
                }
                if (!rs.Equals(""))
                {
                    rs = "<ul>" + rs + "</ul>";
                    L_Errors.Text = rs; ;
                    return false;
                }
                else { return true; }
                }
            catch (Exception e) { L_Error.Text = "validateData:" + e.Message; return false;  }
        }


        private void validateHides() 
        {
            SPListItem itm = SPContext.Current.ListItem;
            bool isWaitingApprover = utils.isWaitingApprover(itm, "Systems");
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



        protected void BSave_Click(object sender, EventArgs e)
        {
            try
            {
                L_Errors.Text = "";
                SPListItem itm = SPContext.Current.ListItem;
               
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
                itm["Subject"] = utils.serializeDialog(Subject);
                itm["SubjectOther"] = SubjectOther.Text; 
                itm["AM"] = AM.Text;
                itm["Phone"] = Phone.Text;
                itm["Name"] = Name.Text;
                itm["NameLatin"] = NameLatin.Text;
                itm["Role"] = Role.Text;
                if (_status.Trim().Equals("") || _status.Trim().Equals("draft") || _status.Trim().Equals("back"))
                {
                    if (!EndDate.IsDateEmpty) { itm["EndDate"] = EndDate.SelectedDate; } else { itm["EndDate"] = null;  }
                }
                itm["Surname"] = Surname.Text;
                itm["SurnameLatin"] = SurnameLatin.Text;
                itm["T24TellerID"] = T24TellerID.Text;
                itm["T24Profile"] = T24Profile.Text;
                itm["BasicAccess"] = utils.serializeCheckbox(BasicAccess);
                itm["OSRole"] = OSRole.Text;
                itm["OSDep"] = OSDep.Text;
                itm["OracleAccess"] = OracleAccess.SelectedValue;
                itm["ArotronRole"] = ArotronRole.Text;
                itm["ArotronGroup"] = ArotronGroup.Text;
                itm["Systems"] = utils.serializeCheckbox(Systems); 
                itm.SystemUpdate(); 
            }
            catch (Exception e) { L_Error.Text += " --getFromBack:" + e.Message; }

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

                Subject.SelectedValue = (itm["Subject"] ?? "").ToString();
                L_Subject.Text  = (itm["Subject"] ?? "").ToString();
                SubjectOther.Text = (itm["SubjectOther"] ?? "").ToString();
                L_SubjectOther.Text = (itm["SubjectOther"] ?? "").ToString();

                string lSystems = (itm["Systems"] ?? "").ToString().Replace(";#", ", ");
                if (lSystems.StartsWith(",")) { lSystems = lSystems.Substring(1);  }
                AM.Text = (itm["AM"] ?? "").ToString();
                L_AM.Text = (itm["AM"] ?? "").ToString();
                utils.deserializeCheckbox(itm, "Systems", Systems);
                Phone.Text = (itm["Phone"] ?? "").ToString();
                L_Phone.Text = (itm["Phone"] ?? "").ToString();
                Name.Text = (itm["Name"] ?? "").ToString();
                L_Name.Text = (itm["Name"] ?? "").ToString();
                NameLatin.Text = (itm["NameLatin"] ?? "").ToString();
                L_NameLatin.Text = (itm["NameLatin"] ?? "").ToString();
                Role.Text = (itm["Role"] ?? "").ToString();
                L_Role.Text = (itm["Role"] ?? "").ToString();
         
                if (!(itm["EndDate"] ?? "").ToString().Trim().Equals("")) {
                    try { EndDate.SelectedDate = (DateTime)itm["EndDate"]; } catch (Exception e) { };
         
                }
         
                L_EndDate.Text = (itm["EndDate"] ?? "").ToString();
         
                Surname.Text = (itm["Surname"] ?? "").ToString();
                L_Surname.Text = (itm["Surname"] ?? "").ToString();
                SurnameLatin.Text = (itm["SurnameLatin"] ?? "").ToString();
                L_SurnameLatin.Text = (itm["SurnameLatin"] ?? "").ToString();
                utils.deserializeCheckbox(itm, "BasicAccess", BasicAccess);
               

                T24TellerID.Text = (itm["T24TellerID"] ?? "").ToString();
                L_T24TellerID.Text = (itm["T24TellerID"] ?? "").ToString();
                T24Profile.Text = (itm["T24Profile"] ?? "").ToString();
                L_T24Profile.Text = (itm["T24Profile"] ?? "").ToString();
                OSRole.Text = (itm["OSRole"] ?? "").ToString();
                L_OSRole.Text = (itm["OSRole"] ?? "").ToString();
                OSDep.Text = (itm["OSDep"] ?? "").ToString();
                L_OSDep.Text = (itm["OSDep"] ?? "").ToString();
                OracleAccess.SelectedValue= (itm["OracleAccess"] ?? "").ToString();
                L_OracleAccess.Text = (itm["OracleAccess"] ?? "").ToString();
                ArotronRole.Text = (itm["ArotronRole"] ?? "").ToString();
                L_ArotronRole.Text = (itm["ArotronRole"] ?? "").ToString();
                ArotronGroup.Text = (itm["ArotronGroup"] ?? "").ToString();
                L_ArotronGroup.Text = (itm["ArotronGroup"] ?? "").ToString();
            }
            catch (Exception e) { L_Error.Text += " --getFromBack:" +   e.Message;  }
        }


        private void showEditBody (bool showEdit)
        {
From.Visible = showEdit;
            L_From.Visible = !showEdit;
            Building.Visible = showEdit;
            L_Building.Visible = !showEdit;
            Subject.Visible = showEdit;
            L_Subject.Visible = !showEdit;
            SubjectOther.Visible = showEdit;
            L_SubjectOther.Visible = !showEdit; 

            Systems.Enabled = showEdit;
            BasicAccess.Enabled = showEdit;
            AM.Visible = showEdit;
            L_AM.Visible = !showEdit;
            Role.Visible = showEdit;
            L_Role.Visible = !showEdit;
            Phone.Visible = showEdit;
            L_Phone.Visible = !showEdit;
            EndDate.Visible = showEdit;
            L_EndDate.Visible = !showEdit;
            Name.Visible = showEdit;
            L_Name.Visible = !showEdit;
            Surname.Visible = showEdit;
            L_Surname.Visible = !showEdit;
            NameLatin.Visible = showEdit;
            L_NameLatin.Visible = !showEdit;
            SurnameLatin.Visible = showEdit;
            L_SurnameLatin.Visible = !showEdit;
            T24TellerID.Visible = showEdit;
            L_T24TellerID.Visible = !showEdit;
            T24Profile.Visible = showEdit;
            L_T24Profile.Visible = !showEdit;
            OSRole.Visible = showEdit;
            L_OSRole.Visible = !showEdit;
            OSDep.Visible = showEdit;
            L_OSDep.Visible = !showEdit;
            ArotronRole.Visible = showEdit;
            L_ArotronRole.Visible = !showEdit;
            ArotronGroup.Visible = showEdit;
            L_ArotronGroup.Visible = !showEdit;
            OracleAccess.Visible = showEdit;
            L_OracleAccess.Visible = !showEdit;
        }


        //private string myBasicAccessDisplay(SPListItem itm)
        //{
        //  string rs =   (itm["BasicAccess"] ?? "").ToString().Replace(";#", ", ");
        //    if (rs.StartsWith(",")) { rs = rs.Substring(1); }
        //    rs = rs.Replace("Internet", "Internet");
        //    rs = rs.Replace("Mail", "Ηλ. Ταχυδρομείο");
        //    rs = rs.Replace("PersonalFolder", "H: Προσωπικός Χώρος στον Server");
        //    rs = rs.Replace("DeptFolder", "K: Δικτυακός χώρος ανταλλαγής αρχείων μεταξύ Διευθύνεσων μόνο");
        //    rs = rs.Replace("CommonFolderSTMTS30062002", "Πρόσβαση στο Common Folder 'Deposit Accounts through STMTS 30062002(\\atticaserver)");
        //    return rs; 
        //}

    }
}