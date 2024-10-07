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
                DocumentManagementLibrary.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementLibrary"));
                OracleAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "OracleAccess"));
                MOTIVIANAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "MOTIVIANAccess"));
                FACTORINGAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "FACTORINGAccess"));
                NEWTONAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "NEWTONAccess"));
                SWIFTAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "SWIFTAccess"));
                ImportExportAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "ImportExportAccess"));
                AssesmentsAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "AssesmentsAccess"));
                FCMAMLAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "FCMAMLAccess"));
                SASAMLAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "SASAMLAccess"));
                RegTekAMLAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "RegTekAMLAccess"));
                LeasingAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "LeasingAccess"));
                EthicsCodeAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "EthicsCodeAccess"));
                BankAttachmentsAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "BankAttachmentsAccess"));
                SCANHRMSAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "SCANHRMSAccess"));
                SpringAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "SpringAccess"));
                CodeAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "CodeAccess"));
                //DM_LMD_Customers.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_IMPEX.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_DANEIA.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_BOA_AITISEIS.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_BOA.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_BOA_ML_LIB.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_BOA_HR_LIB.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_BOA_LEGAL_LIB.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                //DM_XORHGHSEIS.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "DocumentManagementAccess"));
                BPMDIGITAL1Access.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "BPMDIGITAL1Access"));
                BPMLOSSMECorporateAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "BPMLOSSMECorporateAccess"));
                BPMLOSConsumeAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "BPMLOSConsumeAccess"));
                ACSWebsiteAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "ACSWebsiteAccess"));
                ACSConnectWebsiteAccess.Items.AddRange(utils.getListItemsFromKeywordsList(itm.Web, "ACSConnectWebsiteAccess"));
                // SPList listSystems = SPContext.Current.Web.Lists["Systems"];
                if (itm.ID > 0)
                {
                    utils.EnsureDropDownList(itm, "Building", ref Building);
                    utils.EnsureDropDownList(itm, "DocumentManagementLibrary", ref DocumentManagementLibrary);
                    utils.EnsureDropDownList(itm, "OracleAccess", ref OracleAccess);
                    utils.EnsureDropDownList(itm, "MOTIVIANAccess", ref MOTIVIANAccess);
                    utils.EnsureDropDownList(itm, "FACTORINGAccess", ref FACTORINGAccess);
                    utils.EnsureDropDownList(itm, "NEWTONAccess", ref NEWTONAccess);
                    utils.EnsureDropDownList(itm, "SWIFTAccess", ref SWIFTAccess);
                    utils.EnsureDropDownList(itm, "ImportExportAccess", ref ImportExportAccess);
                    utils.EnsureDropDownList(itm, "AssesmentsAccess", ref AssesmentsAccess);
                    utils.EnsureDropDownList(itm, "FCMAMLAccess", ref FCMAMLAccess);
                    utils.EnsureDropDownList(itm, "SASAMLAccess", ref SASAMLAccess);
                    utils.EnsureDropDownList(itm, "RegTekAMLAccess", ref RegTekAMLAccess);
                    utils.EnsureDropDownList(itm, "LeasingAccess", ref LeasingAccess);
                    utils.EnsureDropDownList(itm, "EthicsCodeAccess", ref EthicsCodeAccess);
                    utils.EnsureDropDownList(itm, "ImportExportAccess", ref ImportExportAccess);
                    utils.EnsureDropDownList(itm, "BankAttachmentsAccess", ref BankAttachmentsAccess);
                    utils.EnsureDropDownList(itm, "SCANHRMSAccess", ref SCANHRMSAccess);
                    utils.EnsureDropDownList(itm, "SpringAccess", ref SpringAccess);
                    utils.EnsureDropDownList(itm, "CodeAccess", ref CodeAccess);
                    utils.EnsureDropDownList(itm, "BPMDIGITAL1Access", ref BPMDIGITAL1Access);
                    utils.EnsureDropDownList(itm, "BPMLOSSMECorporateAccess", ref BPMLOSSMECorporateAccess);
                    utils.EnsureDropDownList(itm, "BPMLOSConsumeAccess", ref BPMLOSConsumeAccess);
                    utils.EnsureDropDownList(itm, "ACSWebsiteAccess", ref ACSWebsiteAccess);
                    utils.EnsureDropDownList(itm, "ACSConnectWebsiteAccess", ref ACSConnectWebsiteAccess);
                }

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
                if (li.Value.Equals("Document Management")) { panelDocumentManagement.Visible = li.Selected; }
                if (li.Value.Equals("ORACLE")) { panelOracle.Visible = li.Selected; }
                if (li.Value.Equals("AROTRON")) { panelArotron.Visible = li.Selected; }
                if (li.Value.Equals("U/SWITCHWARE")) { panelOS.Visible = li.Selected; }
                if (li.Value.Equals("MOTIVIAN")) { panelMOTIVIAN.Visible = li.Selected; }
                if (li.Value.Equals("FACTORING")) { panelFACTORING.Visible = li.Selected; }
                if (li.Value.Equals("NEWTON")) { panelNEWTON.Visible = li.Selected; }
                if (li.Value.Equals("ΕΙΣΑΓΩΓΕΣ-ΕΞΑΓΩΓΕΣ")) { panelImportExport.Visible = li.Selected; }
                if (li.Value.Equals("SWIFT")) { panelSWIFT.Visible = li.Selected; }
                if (li.Value.Equals("ΕΚΤΙΜΗΣΕΙΣ")) { panelAssesments.Visible = li.Selected; }
                if (li.Value.Equals("FCM AML")) { panelFCMAML.Visible = li.Selected; }
                if (li.Value.Equals("SAS AML")) { panelSASAML.Visible = li.Selected; }
                if (li.Value.Equals("RegTek AML")) { panelRegTekAML.Visible = li.Selected; }
                if (li.Value.Equals("Leasing", StringComparison.InvariantCultureIgnoreCase)) { panelLeasing.Visible = li.Selected; }
                if (li.Value.Equals("ΔΙΑΧΕΙΡΙΣΗ ΚΩΔΙΚΑ ΔΕΟΝΤΟΛΟΓΙΑΣ")) { panelEthicsCode.Visible = li.Selected; }
                if (li.Value.Equals("E-ΚΑΤΑΣΧΕΤΗΡΙΑ")) { panelBankAttachments.Visible = li.Selected; }
                if (li.Value.Equals("SCAN-HRMS")) { panelSCANHRMS.Visible = li.Selected; }
                if (li.Value.Equals("α/σ-Spring")) { panelSpring.Visible = li.Selected; }
                if (li.Value.Equals("Code")) { panelCode.Visible = li.Selected; }
                if (li.Value.Equals("BPM DIGITAL-1")) { panelBPMDIGITAL1.Visible = li.Selected; }
                if (li.Value.Equals("BPM LOS SME & CORPORATE")) { panelBPMLOSSMECorporate.Visible = li.Selected; }
                if (li.Value.Equals("BPM LOS ΚΑΤΑΝΑΛΩΤΙΚΩΝ & ΚΑΡΤΩΝ")) { panelBPMLOSConsume.Visible = li.Selected; }
                if (li.Value.Equals("ACS Website Application")) { panelACSWebsite.Visible = li.Selected; }
                if (li.Value.Equals("ACS Connect Website Application")) { panelACSConnectWebsite.Visible = li.Selected; }
            }
        }


        private bool validateData()
        {
            try
            {


                SPListItem itm = SPContext.Current.ListItem;
                // only in draft status validates  
                string _status = "";
                if (itm.ID > 0) { _status = (itm["_status"] ?? "").ToString(); }
                if (!_status.Equals("") && !_status.Equals("back") && !_status.Equals("draft")) return true;
                if (_status.Trim().Equals("")) { itm["_status"] = "draft"; }
                string[] textFields = new string[] { "From#Από", "AM#ΑΜ", "Name#Όνομα", "Surname#Επώνυμο", "NameLatin#Όνομα (στα Λατινικά)", "SurnameLatin#Επώνυμο (στα Λατινικά)" };
                string[] ddlFields = new string[] { "Building#Κτίριο", "Subject#Θέμα" };
                ArrayList textFieldsExtra = new ArrayList();
                ArrayList ddlFieldsExtra = new ArrayList();

                string rs = utils.validateDataStandard(panelMain, textFields, ddlFields);
                bool notSelected = true;
                bool toCheckDM = false;
                foreach (ListItem cb in Systems.Items)
                {
                    if (cb.Selected)
                    {
                        notSelected = false;
                        if (cb.Value.Equals("T24")) { textFieldsExtra.Add("T24Profile#T24 - Στοιχεία Profile Νέου Χρήστη"); }
                        //  if (cb.Value.Equals("U/SWITCHWARE")) { textFieldsExtra.Add("OSRole#U/SWITCHWARE- Κωδικός Ρόλου"); textFieldsExtra.Add("OSDep#U/SWITCHWARE- Κωδικός Διεύθυνσης"); }
                        if (cb.Value.Equals("Document Management")) { ddlFieldsExtra.Add("DocumentManagementLibrary#Document Management Library"); }
                        if (cb.Value.Equals("ORACLE")) { ddlFieldsExtra.Add("OracleAccess#Για Oracle"); }
                        if (cb.Value.Equals("AROTRON")) { textFieldsExtra.Add("ArotronRole#AROTRON - Ρόλος στην Ομάδα"); textFieldsExtra.Add("ArotronGroup#AROTRON - Ομάδα Εργασίας"); }
                        if (cb.Value.Equals("MOTIVIAN")) { ddlFieldsExtra.Add("MOTIVIANAccess#MOTIVIAN"); }
                        if (cb.Value.Equals("FACTORING")) { ddlFieldsExtra.Add("FACTORINGAccess#FACTORING"); }
                        if (cb.Value.Equals("NEWTON")) { ddlFieldsExtra.Add("NEWTONAccess#NEWTON"); }
                        if (cb.Value.Equals("ΕΙΣΑΓΩΓΕΣ-ΕΞΑΓΩΓΕΣ")) { ddlFieldsExtra.Add("ImportExportAccess#ΕΙΣΑΓΩΓΕΣ-ΕΞΑΓΩΓΕΣ"); }
                        if (cb.Value.Equals("SWIFT")) { ddlFieldsExtra.Add("SWIFTAccess#SWIFT"); }
                        if (cb.Value.Equals("ΕΚΤΙΜΗΣΕΙΣ")) { ddlFieldsExtra.Add("AssesmentsAccess#ΕΚΤΙΜΉΣΕΙΣ"); }
                        if (cb.Value.Equals("FCM AML")) { ddlFieldsExtra.Add("FCMAMLAccess#FCM AML"); }
                        if (cb.Value.Equals("SAS AML")) { ddlFieldsExtra.Add("SASAMLAccess#SAS AML"); }
                        if (cb.Value.Equals("RegTek AML")) { ddlFieldsExtra.Add("RegTekAMLAccess#RegTek AML"); }
                        // if (cb.Value.Equals("Document Management")) { toCheckDM = true;   }
                        if (cb.Value.Equals("Leasing")) { ddlFieldsExtra.Add("LeasingAccess#Leasing"); }
                        if (cb.Value.Equals("ΔΙΑΧΕΙΡΙΣΗ ΚΩΔΙΚΑ ΔΕΟΝΤΟΛΟΓΙΑΣ")) { ddlFieldsExtra.Add("EthicsCodeAccess#ΔΙΑΧΕΙΡΙΣΗ ΚΩΔΙΚΑ ΔΕΟΝΤΟΛΟΓΙΑΣ"); }
                        if (cb.Value.Equals("E-ΚΑΤΑΣΧΕΤΗΡΙΑ")) { ddlFieldsExtra.Add("BankAttachmentsAccess#E-ΚΑΤΑΣΧΕΤΗΡΙΑ"); }
                        if (cb.Value.Equals("SCAN-HRMS")) { ddlFieldsExtra.Add("SCANHRMSAccess#SCAN-HRMS"); }
                        if (cb.Value.Equals("α/σ-Spring")) { ddlFieldsExtra.Add("SpringAccess#α/σ-Spring"); }
                        if (cb.Value.Equals("Code")) { ddlFieldsExtra.Add("CodeAccess#Code"); }
                        if (cb.Value.Equals("BPM DIGITAL-1")) { ddlFieldsExtra.Add("BPMDIGITAL1Access#BPM DIGITAL-1"); }
                        if (cb.Value.Equals("BPM LOS SME & CORPORATE")) { ddlFieldsExtra.Add("BPMLOSSMECorporateAccess#BPM LOS SME & CORPORATE"); }
                        if (cb.Value.Equals("BPM LOS ΚΑΤΑΝΑΛΩΤΙΚΩΝ & ΚΑΡΤΩΝ")) { ddlFieldsExtra.Add("BPMLOSConsumeAccess#BPM LOS ΚΑΤΑΝΑΛΩΤΙΚΩΝ & ΚΑΡΤΩΝ"); }
                        if (cb.Value.Equals("ACS Website Application")) { ddlFieldsExtra.Add("ACSWebsiteAccess#ACS Website Application"); }
                        if (cb.Value.Equals("ACS Connect Website Application")) { ddlFieldsExtra.Add("ACSConnectWebsiteAccess#ACS Connect Website Application"); }
                    }
                }
                if (notSelected && SubjectOther.Text.Trim().Equals(""))
                {
                    rs += "<li> Τουλάχιστον ένα Σύστημα πρέπει να επιλεχθεί από τις δύο λίστες ή να συμπληρωθεί στο πεδίο 'Άλλα Συστήματα Εκτός Λίστας' </li>";
                }
                rs += utils.validateDataStandard(panelMain, (string[])textFieldsExtra.ToArray(typeof(string)), (string[])ddlFieldsExtra.ToArray(typeof(string)));
                rs += utils.validateDataApprover(itm, panelMain);
                if (!EndDate.IsValid)
                {
                    rs += "<li> Δεν είναι έγκυρη η ημερομηνία στην Λήξη Πρακτικής</li>";
                }
                //if (toCheckDM)
                //{

                //    string dmSelected = DM_BOA.SelectedValue + DM_BOA_AITISEIS.SelectedValue + DM_BOA_HR_LIB.SelectedValue + DM_BOA_LEGAL_LIB.SelectedValue + DM_BOA_ML_LIB.SelectedValue;
                //    dmSelected += DM_DANEIA.SelectedValue + DM_IMPEX.SelectedValue + DM_LMD_Customers.SelectedValue + DM_XORHGHSEIS.SelectedValue;
                //    //rs += "<li> ---" + dmSelected + "</li>";
                //    if (dmSelected.Trim().Equals(""))
                //    {
                //        rs += "<li> Έχετε δηλώσει πρόσβαση στο Document Management. Πρέπει να επιλέξετε επίπεδο πρόσβασης που επιθυμείτε σε τουλάχιστον μία βιβλιοθήκη</li>";
                //    }
                //}
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

                AssetMgmtUtils utils = new AssetMgmtUtils();
                SPListItem itm = SPContext.Current.ListItem;
                if (!utils.isSubmitted(itm))
                {

                    if (validateData())
                    {
                        saveToBack(itm);
                        utils.BSubmit_ClickStandard(itm, panelMain, Page);
                    }
                }
                else
                {
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
                //  saveToBack(itm);  in new version , no data to save 
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
                //  saveToBack(itm); in new version , no data to save 
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
                    if (!EndDate.IsDateEmpty) { itm["EndDate"] = EndDate.SelectedDate; } else { itm["EndDate"] = null; }
                }
                itm["Surname"] = Surname.Text;
                itm["SurnameLatin"] = SurnameLatin.Text;
                itm["T24TellerID"] = T24TellerID.Text;
                itm["T24Profile"] = T24Profile.Text;
                itm["BasicAccess"] = utils.serializeCheckbox(BasicAccess);
                itm["OSRole"] = OSRole.Text;
                itm["OSDep"] = OSDep.Text;
                itm["DocumentManagementLibrary"] = DocumentManagementLibrary.SelectedValue;
                itm["OracleAccess"] = OracleAccess.SelectedValue;
                itm["ArotronRole"] = ArotronRole.Text;
                itm["ArotronGroup"] = ArotronGroup.Text;
                itm["Systems"] = utils.serializeCheckbox(Systems);

                itm["MOTIVIANAccess"] = MOTIVIANAccess.SelectedValue;
                itm["FACTORINGAccess"] = FACTORINGAccess.SelectedValue;
                itm["NEWTONAccess"] = NEWTONAccess.SelectedValue;
                itm["ImportExportAccess"] = ImportExportAccess.SelectedValue;
                itm["SWIFTAccess"] = SWIFTAccess.SelectedValue;
                itm["AssesmentsAccess"] = AssesmentsAccess.SelectedValue;
                itm["FCMAMLAccess"] = FCMAMLAccess.SelectedValue;
                itm["SASAMLAccess"] = SASAMLAccess.SelectedValue;
                itm["RegTekAMLAccess"] = RegTekAMLAccess.SelectedValue;
                itm["LeasingAccess"] = LeasingAccess.SelectedValue;
                itm["EthicsCodeAccess"] = EthicsCodeAccess.SelectedValue;
                itm["BankAttachmentsAccess"] = BankAttachmentsAccess.SelectedValue;
                itm["SCANHRMSAccess"] = SCANHRMSAccess.SelectedValue;
                itm["SpringAccess"] = SpringAccess.SelectedValue;
                itm["CodeAccess"] = CodeAccess.SelectedValue;
                //itm["DM_LMD_Customers"] = DM_LMD_Customers.SelectedValue;
                //itm["DM_IMPEX"] = DM_IMPEX.SelectedValue;
                //itm["DM_DANEIA"] = DM_DANEIA.SelectedValue;
                //itm["DM_BOA_AITISEIS"] = DM_BOA_AITISEIS.SelectedValue;
                //itm["DM_BOA"] = DM_BOA.SelectedValue;
                //itm["DM_BOA_ML_LIB"] = DM_BOA_ML_LIB.SelectedValue;
                //itm["DM_BOA_HR_LIB"] = DM_BOA_HR_LIB.SelectedValue;
                //itm["DM_BOA_LEGAL_LIB"] = DM_BOA_LEGAL_LIB.SelectedValue;
                //itm["DM_XORHGHSEIS"] = DM_XORHGHSEIS.SelectedValue;
                //itm["DM_Branch"] = DM_Branch.Text; 
                itm["BPMDIGITAL1Access"] = BPMDIGITAL1Access.SelectedValue;
                itm["BPMLOSSMECorporateAccess"] = BPMLOSSMECorporateAccess.SelectedValue;
                itm["BPMLOSConsumeAccess"] = BPMLOSConsumeAccess.SelectedValue;
                itm["ACSWebsiteAccess"] = ACSWebsiteAccess.SelectedValue;
                itm["ACSConnectWebsiteAccess"] = ACSConnectWebsiteAccess.SelectedValue;
                //  itm.SystemUpdate(); In this verion , Only ONE Update 
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
                L_Subject.Text = (itm["Subject"] ?? "").ToString();
                SubjectOther.Text = (itm["SubjectOther"] ?? "").ToString();
                L_SubjectOther.Text = (itm["SubjectOther"] ?? "").ToString();

                string lSystems = (itm["Systems"] ?? "").ToString().Replace(";#", ", ");
                if (lSystems.StartsWith(",")) { lSystems = lSystems.Substring(1); }
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

                if (!(itm["EndDate"] ?? "").ToString().Trim().Equals(""))
                {
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

                DocumentManagementLibrary.SelectedValue = (itm["DocumentManagementLibrary"] ?? "").ToString();
                L_DocumentManagementLibrary.Text = (itm["DocumentManagementLibrary"] ?? "").ToString();
                OracleAccess.SelectedValue = (itm["OracleAccess"] ?? "").ToString();
                L_OracleAccess.Text = (itm["OracleAccess"] ?? "").ToString();
                ArotronRole.Text = (itm["ArotronRole"] ?? "").ToString();
                L_ArotronRole.Text = (itm["ArotronRole"] ?? "").ToString();
                ArotronGroup.Text = (itm["ArotronGroup"] ?? "").ToString();
                L_ArotronGroup.Text = (itm["ArotronGroup"] ?? "").ToString();

                L_MOTIVIANAccess.Text = (itm["MOTIVIANAccess"] ?? "").ToString();
                MOTIVIANAccess.SelectedValue = (itm["MOTIVIANAccess"] ?? "").ToString();
                L_FACTORINGAccess.Text = (itm["FACTORINGAccess"] ?? "").ToString();
                FACTORINGAccess.SelectedValue = (itm["FACTORINGAccess"] ?? "").ToString();
                L_NEWTONAccess.Text = (itm["NEWTONAccess"] ?? "").ToString();
                NEWTONAccess.SelectedValue = (itm["NEWTONAccess"] ?? "").ToString();
                L_ImportExportAccess.Text = (itm["ImportExportAccess"] ?? "").ToString();
                ImportExportAccess.SelectedValue = (itm["ImportExportAccess"] ?? "").ToString();

                L_SWIFTAccess.Text = (itm["SWIFTAccess"] ?? "").ToString();
                SWIFTAccess.SelectedValue = (itm["SWIFTAccess"] ?? "").ToString();
                AssesmentsAccess.SelectedValue = (itm["AssesmentsAccess"] ?? "").ToString();
                L_AssesmentsAccess.Text = (itm["AssesmentsAccess"] ?? "").ToString();
                FCMAMLAccess.SelectedValue = (itm["FCMAMLAccess"] ?? "").ToString();
                L_FCMAMLAccess.Text = (itm["FCMAMLAccess"] ?? "").ToString();
                SASAMLAccess.SelectedValue = (itm["SASAMLAccess"] ?? "").ToString();
                L_SASAMLAccess.Text = (itm["SASAMLAccess"] ?? "").ToString();
                RegTekAMLAccess.SelectedValue = (itm["RegTekAMLAccess"] ?? "").ToString();
                L_RegTekAMLAccess.Text = (itm["RegTekAMLAccess"] ?? "").ToString();
                SCANHRMSAccess.SelectedValue = (itm["SCANHRMSAccess"] ?? "").ToString();
                L_SCANHRMSAccess.Text = (itm["SCANHRMSAccess"] ?? "").ToString();
                SpringAccess.SelectedValue = (itm["SpringAccess"] ?? "").ToString();
                L_SpringAccess.Text = (itm["SpringAccess"] ?? "").ToString();
                CodeAccess.SelectedValue = (itm["CodeAccess"] ?? "").ToString();
                L_CodeAccess.Text = (itm["CodeAccess"] ?? "").ToString();
                //L_DM_LMD_Customers.Text = (itm["DM_LMD_Customers"] ?? "").ToString();
                //L_DM_IMPEX.Text = (itm["DM_IMPEX"] ?? "").ToString();
                //L_DM_DANEIA.Text = (itm["DM_DANEIA"] ?? "").ToString();
                //L_DM_BOA_AITISEIS.Text = (itm["DM_BOA_AITISEIS"] ?? "").ToString();
                //L_DM_BOA.Text = (itm["DM_BOA"] ?? "").ToString();
                //L_DM_BOA_ML_LIB.Text = (itm["DM_BOA_ML_LIB"] ?? "").ToString();
                //L_DM_BOA_HR_LIB.Text = (itm["DM_BOA_HR_LIB"] ?? "").ToString();
                //L_DM_BOA_LEGAL_LIB.Text = (itm["DM_BOA_LEGAL_LIB"] ?? "").ToString();
                //L_DM_XORHGHSEIS.Text = (itm["DM_XORHGHSEIS"] ?? "").ToString();
                //L_DM_Branch.Text = (itm["DM_Branch"] ?? "").ToString();
                //L_DM_LMD_Customers.Text = (itm["DM_LMD_Customers"] ?? "").ToString();

                //DM_LMD_Customers.SelectedValue = (itm["DM_LMD_Customers"] ?? "").ToString();
                //DM_IMPEX.SelectedValue = (itm["DM_IMPEX"] ?? "").ToString();
                //DM_DANEIA.SelectedValue = (itm["DM_DANEIA"] ?? "").ToString();
                //DM_BOA_AITISEIS.SelectedValue = (itm["DM_BOA_AITISEIS"] ?? "").ToString();
                //DM_BOA.SelectedValue = (itm["DM_BOA"] ?? "").ToString();
                //DM_BOA_ML_LIB.SelectedValue = (itm["DM_BOA_ML_LIB"] ?? "").ToString();
                //DM_BOA_HR_LIB.SelectedValue = (itm["DM_BOA_HR_LIB"] ?? "").ToString();
                //DM_BOA_LEGAL_LIB.SelectedValue = (itm["DM_BOA_LEGAL_LIB"] ?? "").ToString();
                //DM_XORHGHSEIS.SelectedValue = (itm["DM_XORHGHSEIS"] ?? "").ToString();
                //DM_Branch.Text = (itm["DM_Branch"] ?? "").ToString();

                LeasingAccess.SelectedValue = (itm["LeasingAccess"] ?? "").ToString();
                L_LeasingAccess.Text = (itm["LeasingAccess"] ?? "").ToString();
                EthicsCodeAccess.SelectedValue = (itm["EthicsCodeAccess"] ?? "").ToString();
                L_EthicsCodeAccess.Text = (itm["EthicsCodeAccess"] ?? "").ToString();
                L_BankAttachmentsAccess.Text = (itm["BankAttachmentsAccess"] ?? "").ToString();
                BankAttachmentsAccess.SelectedValue = (itm["BankAttachmentsAccess"] ?? "").ToString();

                BPMDIGITAL1Access.SelectedValue = (itm["BPMDIGITAL1Access"] ?? "").ToString();
                L_BPMDIGITAL1Access.Text = (itm["BPMDIGITAL1Access"] ?? "").ToString();
                BPMLOSSMECorporateAccess.SelectedValue = (itm["BPMLOSSMECorporateAccess"] ?? "").ToString();
                L_BPMLOSSMECorporateAccess.Text = (itm["BPMLOSSMECorporateAccess"] ?? "").ToString();
                BPMLOSConsumeAccess.SelectedValue = (itm["BPMLOSConsumeAccess"] ?? "").ToString();
                L_BPMLOSConsumeAccess.Text = (itm["BPMLOSConsumeAccess"] ?? "").ToString();
                ACSWebsiteAccess.SelectedValue = (itm["ACSWebsiteAccess"] ?? "").ToString();
                L_ACSWebsiteAccess.Text = (itm["ACSWebsiteAccess"] ?? "").ToString();
                ACSConnectWebsiteAccess.SelectedValue = (itm["ACSConnectWebsiteAccess"] ?? "").ToString();
                L_ACSConnectWebsiteAccess.Text = (itm["ACSConnectWebsiteAccess"] ?? "").ToString();
            }
            catch (Exception e) { L_Error.Text += " --getFromBack:" + e.Message; }
        }


        private void showEditBody(bool showEdit)
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
            DocumentManagementLibrary.Visible = showEdit;
            L_DocumentManagementLibrary.Visible = !showEdit;
            OracleAccess.Visible = showEdit;
            L_OracleAccess.Visible = !showEdit;
            MOTIVIANAccess.Visible = showEdit;
            FACTORINGAccess.Visible = showEdit;
            NEWTONAccess.Visible = showEdit;
            ImportExportAccess.Visible = showEdit;
            SWIFTAccess.Visible = showEdit;
            AssesmentsAccess.Visible = showEdit;
            FCMAMLAccess.Visible = showEdit;
            SASAMLAccess.Visible = showEdit;
            RegTekAMLAccess.Visible = showEdit;
            BankAttachmentsAccess.Visible = showEdit;
            SCANHRMSAccess.Visible = showEdit;
            SpringAccess.Visible = showEdit;
            CodeAccess.Visible = showEdit;
            LeasingAccess.Visible = showEdit;
            EthicsCodeAccess.Visible = showEdit;
            L_MOTIVIANAccess.Visible = !showEdit;
            L_FACTORINGAccess.Visible = !showEdit;
            L_NEWTONAccess.Visible = !showEdit;
            L_ImportExportAccess.Visible = !showEdit;
            L_SWIFTAccess.Visible = !showEdit;
            L_AssesmentsAccess.Visible = !showEdit;
            L_FCMAMLAccess.Visible = !showEdit;
            L_SASAMLAccess.Visible = !showEdit;
            L_RegTekAMLAccess.Visible = !showEdit;
            L_LeasingAccess.Visible = !showEdit;
            L_EthicsCodeAccess.Visible = !showEdit;
            L_BankAttachmentsAccess.Visible = !showEdit;
            L_SCANHRMSAccess.Visible = !showEdit;
            L_SpringAccess.Visible = !showEdit;
            L_CodeAccess.Visible = !showEdit;
            //DM_LMD_Customers.Visible = showEdit;
            //DM_IMPEX.Visible = showEdit;
            //DM_DANEIA.Visible = showEdit;
            //DM_BOA_AITISEIS.Visible = showEdit;
            //DM_BOA.Visible = showEdit;
            //DM_BOA_ML_LIB.Visible = showEdit;
            //DM_BOA_HR_LIB.Visible = showEdit;
            //DM_BOA_LEGAL_LIB.Visible = showEdit;
            //DM_XORHGHSEIS.Visible = showEdit;
            //DM_Branch.Visible = showEdit;
            //L_DM_LMD_Customers.Visible = !showEdit;
            //L_DM_IMPEX.Visible = !showEdit;
            //L_DM_DANEIA.Visible = !showEdit;
            //L_DM_BOA_AITISEIS.Visible = !showEdit;
            //L_DM_BOA.Visible = !showEdit;
            //L_DM_BOA_ML_LIB.Visible = !showEdit;
            //L_DM_BOA_HR_LIB.Visible = !showEdit;
            //L_DM_BOA_LEGAL_LIB.Visible = !showEdit;
            //L_DM_XORHGHSEIS.Visible = !showEdit;
            //L_DM_Branch.Visible = !showEdit;
            BPMDIGITAL1Access.Visible = showEdit;
            BPMLOSSMECorporateAccess.Visible = showEdit;
            BPMLOSConsumeAccess.Visible = showEdit;
            ACSWebsiteAccess.Visible = showEdit;
            ACSConnectWebsiteAccess.Visible = showEdit;
            L_BPMDIGITAL1Access.Visible = !showEdit;
            L_BPMLOSSMECorporateAccess.Visible = !showEdit;
            L_BPMLOSConsumeAccess.Visible = !showEdit;
            L_ACSWebsiteAccess.Visible = !showEdit;
            L_ACSConnectWebsiteAccess.Visible = !showEdit;
        }
    }
}