<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemAccess.ascx.cs" Inherits="AssetMgmt.SystemAccess" %>
<%@ Register Tagprefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.1.519.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register src="AdminPanel.ascx" tagname="AdminPanel" tagprefix="custom" %>

<script src="/_layouts/_custom/js/jquery-3.2.1.min.js"></script>
  <link rel="stylesheet" href="/_layouts/15/_custom/css/form.css" type="text/css"/>

<meta http-equiv="x-ua-compatible" content="IE=9"/>
<script type="javascript">
    /* IE11 Fix for SP2010 */
    if (typeof(UserAgentInfo) != 'undefined' && !window.addEventListener) 
    {
        UserAgentInfo.strBrowser=1; 
    } 
</script>

<custom:AdminPanel ID="adminPanel" runat="server" />       

<asp:Panel ID="panelMain" runat="server" >
<div id="errorContainer">
    </div>
<table><tr >
    <th > <div align="center">Αίτηση Δημιουργίας-Μεταβολής Λογαριασμού Χρήστη  </div>
    </th>
   
        </tr></tr>
    <tr>
        <td>
            <div align="right">
                Ημ. Υποβολής:
                <asp:Label ID="L_Submitted" runat="server" style="font-weight: 700" Text="" />
                <br />
                Η αίτηση (αρ:<asp:Label ID="L_Serial" runat="server" style="font-weight: 700" Text="" />
                ) είναι σε κατάσταση :
                <asp:Label ID="Status" runat="server" style="font-weight: 700" Text="" />
                <br />
                Υπεύθυνος Κατάστασης:<asp:Label ID="L_WaitingApprover" runat="server" style="font-weight: 700" Text="" />
                &nbsp;
            </div>
        </td>
    </tr>
    </table>


<br /> 
<table>
    <tr>
 <td class="col2label required">* Από</td>
        <td class="col2value">
    <asp:TextBox ID="From" runat="server" ></asp:TextBox>
 &nbsp;
    <asp:Label ID="L_From" runat="server" Text=""></asp:Label>
        </td>
</tr>
<tr>
        <td class="col2label">Αιτών</td>
        <td class="col2value">
    <asp:Label ID="Composer" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col2label required" >* Κτίριο</td>
        <td class="col2value"> 
            <asp:DropDownList ID="Building" runat="server" >
            </asp:DropDownList>
        &nbsp;
    <asp:Label ID="L_Building" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col2label ">Πρός</td>
        <td class="col2value">Υποδιεύθυνση Υποδομών &amp; Συστημάτων</td>
    </tr>
</table>
<p>
    </p>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
     <asp:CheckBoxList ID="Systems" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" AutoPostBack="true" >
        <asp:ListItem>T24</asp:ListItem>
         <asp:ListItem>WINDOWS</asp:ListItem>
        
        <asp:ListItem>AROTRON</asp:ListItem>
        <asp:ListItem>G4</asp:ListItem>
        <asp:ListItem>3D Secure</asp:ListItem>
         <asp:ListItem>Document Management</asp:ListItem>
         <asp:ListItem>ORACLE</asp:ListItem>
          <asp:ListItem>OMNIA Ebanking</asp:ListItem>  
          <asp:ListItem>NEWTON</asp:ListItem>
  	<asp:ListItem>Αμοιβαία Κεφάλαια NEWTON</asp:ListItem>
  	<asp:ListItem>Demat (Μετοχολόγιο)</asp:ListItem>
          <asp:ListItem Value="ΕΙΣΑΓΩΓΕΣ-ΕΞΑΓΩΓΕΣ" Text="ΕΙΣΑΓΩΓΕΣ-ΕΞΑΓΩΓΕΣ (MCI)"></asp:ListItem>   
          <asp:ListItem>SWIFT</asp:ListItem>
         <asp:ListItem>SWIFT ALLIANCE</asp:ListItem>
          <asp:ListItem>ΕΚΤΙΜΗΣΕΙΣ</asp:ListItem>
          <asp:ListItem>FCM AML</asp:ListItem>
         <asp:ListItem>SAS AML</asp:ListItem>
         <asp:ListItem>RegTek AML</asp:ListItem>
          <asp:ListItem>MOTIVIAN</asp:ListItem>     
         <asp:ListItem>Leasing</asp:ListItem>     
         <asp:ListItem>FACTORING</asp:ListItem>     
         <asp:ListItem>UTS FIRST DATA</asp:ListItem>     
         <asp:ListItem Value="ΔΙΑΧΕΙΡΙΣΗ ΚΩΔΙΚΑ ΔΕΟΝΤΟΛΟΓΙΑΣ" Text="ΔΙΑΧΕΙΡΙΣΗ ΚΩΔΙΚΑ ΔΕΟΝΤΟΛΟΓΙΑΣ (MITOS)"></asp:ListItem>
         <asp:ListItem>E-ΚΑΤΑΣΧΕΤΗΡΙΑ</asp:ListItem>
         <asp:ListItem>BUSINESS OBJECT</asp:ListItem>
         <asp:ListItem>ΑΚΙΝΗΤΑ THESIS.NET</asp:ListItem>
         <asp:ListItem>CCR Κινήσεις Πιστωτικών Καρτών & Ανοικτών Δανείων</asp:ListItem>  
         <asp:ListItem>SCAN-HRMS</asp:ListItem>     
         <asp:ListItem>ΣΜΠΚ</asp:ListItem>   
         <asp:ListItem>α/σ-Spring</asp:ListItem>     
         <asp:ListItem>QUANTUM-FIS</asp:ListItem>     
         <asp:ListItem>Code</asp:ListItem>     
    </asp:CheckBoxList>

      <br />
     <br />

    <table>  
    <tr>
        <td class="col2label required">* Θέμα</td>
        <td class="col2value"><asp:DropDownList ID="Subject" runat="server" ><asp:ListItem Value="-Επιλέξτε-" /> 
            <asp:ListItem Value="ΔΗΜΙΟΥΡΓΙΑ ΝΕΟΥ ΛΟΓΑΡΙΑΣΜΟΥ ΧΡΗΣΤΗ" />
            <asp:ListItem Value="ΟΡΙΣΤΙΚΗ ΔΙΑΓΡΑΦΗ ΛΟΓΑΡΙΑΣΜΟΥ ΧΡΗΣΤΗ" />
            <asp:ListItem Value="ΠΡΟΣΩΡΙΝΗ ΑΠΕΝΕΡΓΟΠΟΙΗΣΗ ΛΟΓΑΡΙΑΣΜΟΥ ΧΡΗΣΤΗ" />
            <asp:ListItem Value="ΕΝΕΡΓΟΠΟΙΗΣΗ ΛΟΓΑΡΙΑΣΜΟΥ ΧΡΗΣΤΗ" />
            <asp:ListItem Value="ΜΕΤΑΘΕΣΗ / ΜΕΤΑΚΙΝΗΣΗ" />
            <asp:ListItem Value="ΑΛΛΑΓΗ ΠΡΟΦΙΛ" />
            <asp:ListItem Value="ΔΗΜΙΟΥΡΓΙΑ ΝΕΟΥ ΣΥΝΘΗΜΑΤΙΚΟΥ" />
                              </asp:DropDownList>
    <asp:Label ID="L_Subject" runat="server" Text=""></asp:Label>
        </td>
    </tr>
   
       
         <tr>
        <td class="col2label" >Άλλα Συστήματα Εκτός Λίστας</td>  
        <td class="col2value"> 
            <asp:TextBox ID="SubjectOther" runat="server" >
            </asp:TextBox>
        &nbsp;
    <asp:Label ID="L_SubjectOther" runat="server" Text=""></asp:Label>
        </td>
    </tr>
        </table>

      <asp:Label ID="L_Systems" runat="server" Text="" style="font-weight: 700"></asp:Label>
       

   
<table>
    <tr>
        <td class="col4label required" >* AM</td>
        <td class="col4value"> 
<asp:TextBox  ID="AM" runat="server" Width="130px"></asp:TextBox>
<asp:Label ID="L_AM" runat="server" Text=""></asp:Label>

        </td>
        <td class="col4label" >Ρόλος στην Μονάδα</td>
        <td class="col4value"> 
<asp:TextBox ID="Role" runat="server"></asp:TextBox>
<asp:Label ID="L_Role" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col4label">Τηλ. Εργασίας</td>
        <td class="col4value"> 
<asp:TextBox ID="Phone" runat="server" Width="130px"></asp:TextBox>
<asp:Label ID="L_Phone" runat="server" Text=""></asp:Label>    
        </td>
        <td class="col4label"> Λήξη Πρακτικής</td>
        <td class="col4value">
             <SharePoint:DateTimeControl LocaleId="1032"  DateOnly="true" IsRequiredField="false" id="EndDate" runat="server" MinDate="1/1/2000" MaxDate="1/1/2100"  /> 
<asp:Label ID="L_EndDate" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col4label required">* Όνομα</td>
        <td class="col4value"> 
<asp:TextBox ID="Name" runat="server"></asp:TextBox>
<asp:Label ID="L_Name" runat="server" Text=""></asp:Label>
        </td>
        <td class="col4label required">* Επώνυμο</td>
        <td class="col4value"> 
<asp:TextBox ID="Surname" runat="server"></asp:TextBox>
<asp:Label ID="L_Surname" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col4label required">* Όνομα (στα Λατινικά)</td>
        <td class="col4value"> 
<asp:TextBox ID="NameLatin" runat="server"></asp:TextBox>
<asp:Label ID="L_NameLatin" runat="server" Text=""></asp:Label>
        </td>
        <td class="col4label required">* Επώνυμο (στα Λατινικά)</td>
        <td class="col4value"> 
<asp:TextBox ID="SurnameLatin" runat="server"></asp:TextBox>
            
          
                       
<asp:Label ID="L_SurnameLatin" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

      <asp:Panel ID="panelWindows" runat="server" >  
           <asp:CheckBoxList ID="BasicAccess" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" >
        <asp:ListItem Text="Internet" Value="Internet"/>
        <asp:ListItem Text="Ηλ. Ταχυδρομείο" Value="Mail"/>
        <asp:ListItem Text="H: Προσωπικός Χώρος στον Server" Value="PersonalFolder"/>
        
    </asp:CheckBoxList>
          <asp:Label ID="L_BasicAccess" runat="server" Text="" style="font-weight: 700"></asp:Label>
          </asp:Panel>

      <asp:Panel ID="panelT24" runat="server" >   
<table>
    <tr>
        <th colspan="2">Για Τ24</th>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td class="col2label">Teller ID</td>
        <td class="col2value"> 
<asp:TextBox ID="T24TellerID" runat="server" Width="130px"></asp:TextBox>
<asp:Label ID="L_T24TellerID" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="col2label required">* Στοιχεία Profile Νέου Χρήστη</td>
        <td class="col2value"> 
<asp:TextBox ID="T24Profile" runat="server"></asp:TextBox>
<asp:Label ID="L_T24Profile" runat="server" Text=""></asp:Label>
</td>
    </tr>
</table><br />
          <table>
    <tr>
        <th colspan="2">Για Σ.Μ.Π.Κ.</th>
        
    </tr>
    <tr>
        <td class="col2label">Κωδικός Ρόλου</td>
        <td class="col2value"> 
<asp:TextBox ID="OSRole" runat="server" Width="130px" ></asp:TextBox>
        <asp:Label ID="L_OSRole" runat="server" Text=""></asp:Label>    
        </td>
    </tr>
    <tr>
        <td class="col2label">Κωδικός Διεύθυνσης</td>
        <td class="col2value"> 
<asp:TextBox ID="OSDep" runat="server" Width="130px" ></asp:TextBox>

        <asp:Label ID="L_OSDep" runat="server" Text=""></asp:Label>    

        </td>
    </tr>
</table>
          </asp:Panel>

      <asp:Panel ID="panelOS" runat="server" > 
          <p>
    &nbsp;</p>


          </asp:Panel>

      

      <asp:Panel ID="panelArotron" runat="server" > 
          <p>&nbsp;</p>
<table><tr><th colspan="2">Για AROTRON</th></tr>
    <tr><td class="col2label required">* Ρόλος στην Ομάδα</td>
        <td class="col2value"> <asp:TextBox ID="ArotronRole" runat="server"></asp:TextBox>
        <asp:Label ID="L_ArotronRole" runat="server" Text=""></asp:Label>        
        </td></tr>
    <tr><td class="col2label required">* Ομάδα Εργασίας</td>
        <td class="col2value"> <asp:TextBox ID="ArotronGroup" runat="server"></asp:TextBox><asp:Label ID="L_ArotronGroup" runat="server" Text=""></asp:Label></td></tr>
</table>
          </asp:Panel>


        <asp:Panel ID="panelDocumentManagement" runat="server" > 
          <p>&nbsp;</p>
<table><tr><th colspan="2">Document Management</th></tr>
    <tr><td class="col2label required">* Πρόσβαση σε Βιβλιοθήκη</td>
        <td class="col2value"> <asp:DropDownList ID="DocumentManagementLibrary" runat="server"></asp:DropDownList>
        <asp:Label ID="L_DocumentManagementLibrary" runat="server" Text=""></asp:Label>        
        </td></tr>
    
</table>
          </asp:Panel>

    <%--  <asp:Panel ID="panelDocumentManagement" runat="server" >             
          <p>&nbsp;</p>
<table>
<tr><th colspan="4">Για Document Management</th></tr>  
       <tr><td colspan="4">Επιλέξτε το είδος πρόσβασης που αιτούστε ανά βιβλιοθήκη,αφήστε κενό για τις υπόλοιπες </td></tr>
  <tr> <td class="col4label">LMD_Customers </td>
      <td class="col4value"><asp:DropDownList ID="DM_LMD_Customers" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_LMD_Customers" runat="server" Text=""></asp:Label></td>
<td class="col4label">IMPEX</td>
      <td class="col4value"><asp:DropDownList ID="DM_IMPEX" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_IMPEX" runat="server" Text=""></asp:Label></td></tr>
<tr> <td class="col4label">DANEIA</td>
    <td class="col4value"><asp:DropDownList ID="DM_DANEIA" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_DANEIA" runat="server" Text=""></asp:Label></td>
<td class="col4label">BOA_AITISEIS</td>
    <td class="col4value"><asp:DropDownList ID="DM_BOA_AITISEIS" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_BOA_AITISEIS" runat="server" Text=""></asp:Label></td></tr>
<tr> <td class="col4label">BOA</td>
    <td class="col4value"><asp:DropDownList ID="DM_BOA" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_BOA" runat="server" Text=""></asp:Label></td>
<td class="col4label">BOA_ML_LIB</td>
    <td class="col4value"><asp:DropDownList ID="DM_BOA_ML_LIB" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_BOA_ML_LIB" runat="server" Text=""></asp:Label></td></tr>
<tr> <td class="col4label">BOA_HR_LIB</td>
    <td class="col4value"><asp:DropDownList ID="DM_BOA_HR_LIB" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_BOA_HR_LIB" runat="server" Text=""></asp:Label></td>
<td class="col4label">BOA_LEGAL_LIB</td>
    <td class="col4value"><asp:DropDownList ID="DM_BOA_LEGAL_LIB" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_BOA_LEGAL_LIB" runat="server" Text=""></asp:Label></td></tr>
<tr><td class="col4label">XORHGHSEIS</td>
    <td class="col4value"><asp:DropDownList ID="DM_XORHGHSEIS" runat="server" ></asp:DropDownList><asp:Label ID="L_DM_XORHGHSEIS" runat="server" Text=""></asp:Label></td>
   <td class="col4label">Επωνυμία Καταστήματος</td>
    <td class="col4value"> <asp:TextBox ID="DM_Branch" runat="server"></asp:TextBox><asp:Label ID="L_DM_Branch" runat="server" Text=""></asp:Label></td></tr>
  
  </table>
          </asp:Panel>  --%>

    <asp:Panel ID="panelOracle" runat="server" >   
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">ORACLE (Δικαιώματα)</td><td><asp:DropDownList ID="OracleAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_OracleAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

   

     <asp:Panel ID="panelNEWTON" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">NEWTON (Δικαιώματα)</td><td><asp:DropDownList ID="NEWTONAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_NEWTONAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

     <asp:Panel ID="panelImportExport" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">Εισαγωγές Εξαγωγές (Δικαιώματα)</td><td><asp:DropDownList ID="ImportExportAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_ImportExportAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>
     <asp:Panel ID="panelSWIFT" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">SWIFT (Δικαιώματα)</td><td><asp:DropDownList ID="SWIFTAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_SWIFTAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

  

     <asp:Panel ID="panelAssesments" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">Εκτιμήσεις (Δικαιώματα)</td><td><asp:DropDownList ID="AssesmentsAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_AssesmentsAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>
     

     <asp:Panel ID="panelFCMAML" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">FCM AML (Δικαιώματα) </td><td><asp:DropDownList ID="FCMAMLAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_FCMAMLAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

     <asp:Panel ID="panelSASAML" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">SAS AML (Δικαιώματα) </td><td><asp:DropDownList ID="SASAMLAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_SASAMLAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

      <asp:Panel ID="panelRegTekAML" runat="server" >   
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">Reg Tek (Δικαιώματα) </td><td><asp:DropDownList ID="RegTekAMLAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_RegTekAMLAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

        <asp:Panel ID="panelMOTIVIAN" runat="server" > 
          <p>&nbsp;</p>
<table>
    
    <tr><td class="col2label required">MOTIVIAN (Δικαιώματα)</td></td><td><asp:DropDownList ID="MOTIVIANAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_MOTIVIANAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

    <asp:Panel ID="panelLeasing" runat="server" > 
          <p>&nbsp;</p>
<table>
    
    <tr><td class="col2label required">Leasing (Δικαιώματα)</td></td><td><asp:DropDownList ID="LeasingAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_LeasingAccess" runat="server" Text=""></asp:Label></td></tr>  
  </table>
          </asp:Panel>

     <asp:Panel ID="panelFACTORING" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">FACTORING (Δικαιώματα)</td><td><asp:DropDownList ID="FACTORINGAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_FACTORINGAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

    <asp:Panel ID="panelEthicsCode" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">Διαχείριση Κώδικα Δεοντολογίας (Δικαιώματα)</td><td><asp:DropDownList ID="EthicsCodeAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_EthicsCodeAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>

     <asp:Panel ID="panelBankAttachments" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">E-ΚΑΤΑΣΧΕΤΗΡΙΑ (Δικαιώματα)</td><td><asp:DropDownList ID="BankAttachmentsAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_BankAttachmentsAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>  
          </asp:Panel>

     <asp:Panel ID="panelSCANHRMS" runat="server" >   
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required">SCAN-HRMS (Δικαιώματα)</td><td><asp:DropDownList ID="SCANHRMSAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_SCANHRMSAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>
  <asp:Panel ID="panelSpring" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required"> α/σ-Spring (Δικαιώματα)</td><td><asp:DropDownList ID="SpringAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_SpringAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>
    <asp:Panel ID="panelCode" runat="server" > 
          <p>&nbsp;</p>
<table>
    <tr><td class="col2label required"> Code (Δικαιώματα)</td><td><asp:DropDownList ID="CodeAccess" runat="server" ></asp:DropDownList>
    <asp:Label ID="L_CodeAccess" runat="server" Text=""></asp:Label></td></tr>
  </table>
          </asp:Panel>


    </ContentTemplate>
</asp:UpdatePanel>    
<p>
   
</p>

  







<p>
    &nbsp;</p>
  <table><tr><td class="col2label">
Υπεύθυνος Έγκρισης : </td><td>  <asp:Label ID="L_ddlApprover" runat="server" Text="" Font-Bold="true"></asp:Label>
        <SharePoint:ClientPeoplePicker ID="ddlApprover" runat="server"   
                    InitialHelpText="Εισάγετε Υπεύθυνο (στα Λατινικά)"  />  
                </td></tr></table>
&nbsp;
   
<br /> 
<br /> <asp:Literal ID="L_Comments" Text="Σχόλια Εγκρίνοντα (αποθηκεύονται στο Ιστορικό): " runat="server" /> 
<Telerik:RadTextBox ID="Comments" TextMode="MultiLine" Width="60%" Height="50px" runat="server" /> 


    <br /> 

<asp:Label ID="WaitingApprover" runat="server" Text=""></asp:Label>


<asp:Label ID="IsWaitingApprover" runat="server" Text=""></asp:Label>

    <asp:Label ID="L_Errors" runat="server" ForeColor="red" style="font-weight: 700" Text=""></asp:Label>

<br />



<asp:Button ID="BSave" runat="server" Text="Αποθηκευση" OnClick="BSave_Click"  CssClass="submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BSubmit" runat="server" Text="Υποβολη" OnClick="BSubmit_Click" CssClass="submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BBack" runat="server" Text="Πισω για Διορθωσεις " OnClick="BBack_Click" CssClass="submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BReject" runat="server" Text="Απορριψη" OnClick="BReject_Click"  CssClass="submit" />
&nbsp;<p>


<asp:Label ID="History" runat="server" Text=""></asp:Label>
</p>
<p>
    <asp:Label ID="L_Error" runat="server" style="font-weight: 700"></asp:Label>
</p>
<p>
    &nbsp;</p>

    </asp:Panel>

<div align="right">
   <a href="javascript:window.print()">&nbsp&nbsp&nbsp&nbsp&nbsp <img  src="/_layouts/15/images/printerfriendly.gif" title="Εκτύπωση" alt="Εκτύπωση" /> &nbsp&nbsp&nbsp&nbsp&nbsp</a>

</div>
