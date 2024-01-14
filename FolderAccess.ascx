<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FolderAccess.ascx.cs" Inherits="AssetMgmt.FolderAccess" EnableViewState="true" %>
<%@ Register Tagprefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.1.519.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register src="AdminPanel.ascx" tagname="AdminPanel" tagprefix="custom" %>
 <%@ Import Namespace="Microsoft.SharePoint" %>
 <%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
 <%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> 
<script src="/_layouts/_custom/js/jquery-3.2.1.min.js"></script>  
  <link rel="stylesheet" href="/_layouts/15/_custom/css/form.css" type="text/css"/>
<script type="javascript">
    /* IE11 Fix for SP2010 */  
    if (typeof(UserAgentInfo) != 'undefined' && !window.addEventListener) 
    {
        UserAgentInfo.strBrowser=1;    
    } 
</script>
<%-- <script type="text/javascript">
   $(document).ready(function () {
        alert('3'); 
             if (jQuery) {  
               // jQuery is loaded  
               alert("Yeah!");
             } else {
               // jQuery is not loaded
               alert("Doesn't Work");
             }
          });
        </script>--%>
<custom:AdminPanel ID="adminPanel" runat="server" />    

<asp:Panel ID="panelMain" runat="server">
<div id="errorContainer">
    </div>
  <table><tr >
    <th > <div align="center">Αίτηση Δημιουργίας-Μεταβολής Δικαιωμάτων σε Κοινόχρηστους Φακέλους</div>
    </th>
    </tr><tr><td>
<div align="right">
    Ημ. Υποβολής:  <asp:Label ID="L_Submitted" runat="server" style="font-weight: 700" Text="" />
        <br />
        Η αίτηση (αρ:<asp:Label ID="L_Serial" runat="server" style="font-weight: 700" Text="" />
        ) είναι σε κατάσταση :
        <asp:Label ID="Status" runat="server" style="font-weight: 700" Text="" />
        <br />
   
        Υπεύθυνος Κατάστασης:<asp:Label ID="L_WaitingApprover" runat="server" style="font-weight: 700" Text="" />
        &nbsp;
    </div>
        </td></tr></table>

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
    &nbsp;</p>


<table>
        <tr>
        <td class="col2label "><b>* Folder</b>
            <br />   
             </td>
        <td class="col2value"> 
            <asp:DropDownList ID="FolderPrefix" runat="server"></asp:DropDownList>
            <asp:TextBox ID="FolderPostfix" runat="server" Width="25%"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="L_Folder" runat="server" Visible="false"></asp:Label>
            &nbsp;<asp:Button ID="BLoadFolderDetails" runat="server"  Text="Τρέχουσα Εικόνα του Folder" OnClick="BLoadFolderDetails_Click" CssClass="submit" Width="220px" /> 
    <br />
    <asp:Label ID="L_LoadFolderDetailsErr" runat="server" ForeColor="red" style="font-weight: 700"></asp:Label>
    <asp:Label ID="L_FolderChecked" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="L_FolderOldRights" runat="server" Visible="false"></asp:Label>
            
        </td>
    </tr>
</table>

       <asp:UpdatePanel ID="panelRights" runat="server" ><ContentTemplate>
           <telerik:RadGrid  ID="folderRightsV2" runat="server" 
             MasterTableView-EditFormSettings-EditFormType="AutoGenerated"
            AllowAutomaticDeletes="true" AllowSorting="true" OnDeleteCommand="Rights_DeleteCommand"><MasterTableView  AutoGenerateColumns="False"  CommandItemDisplay="None"><Columns><Telerik:GridBoundColumn DataField="Name" HeaderText="Χρήστης (Λατινικά)" UniqueName="Name" Visible="true" /><Telerik:GridBoundColumn DataField="AM" HeaderText="ΑΜ Χρήστη" UniqueName="Name" Visible="true" /><Telerik:GridBoundColumn DataField="Rights" HeaderText="Δικαιώματα" UniqueName="Rights" Visible="true" /><Telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmText="Να διαγραφεί το όνομα από την λίστα?" ConfirmTitle="Διαγραφή" Text="Διαγραφή" /></Columns></MasterTableView></telerik:RadGrid>


            <asp:Panel ID="panelOld" runat="server">
                 <br />
                <hr color="gray" />
                Δικαιώματα (Προηγούμενη Version) 
            <Telerik:RadGrid ID="folderRights" runat="server" AllowAutomaticDeletes="false" AllowSorting="true" MasterTableView-EditFormSettings-EditFormType="AutoGenerated" 
                 ><MasterTableView AutoGenerateColumns="False"  CommandItemDisplay="None"><Columns><Telerik:GridBoundColumn DataField="Name" HeaderText="Χρήστης (Λατινικά)" UniqueName="Name" Visible="true" /><Telerik:GridBoundColumn DataField="AM" HeaderText="ΑΜ Χρήστη" UniqueName="Name" Visible="true" /><Telerik:GridBoundColumn DataField="Folder" HeaderText="Κοινόχρηστος Φάκελος" UniqueName="Folder" Visible="true" /><Telerik:GridBoundColumn DataField="Rights" HeaderText="Δικαιώματα" UniqueName="Rights" Visible="true" /><Telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmText="Να διαγραφεί το όνομα από την λίστα?" ConfirmTitle="Διαγραφή" Text="Διαγραφή" /></Columns></MasterTableView></Telerik:RadGrid>
            <br />
                <hr color="gray" />
                </asp:Panel>
                       <asp:Panel ID="panelAdd" runat="server">
                           <table ><tr><td  style="width:25%">
                      Νέα Εγγραφή: <br/> 
                      <SharePoint:ClientPeoplePicker ID="Name_1" runat="server"   
                    InitialHelpText="Εισάγετε Χρήστη (στα Λατινικά)" AllowMultipleEntities="false" Width="250px" /></td>
                               <td>
                           <Telerik:RadTextBox EmptyMessage="ΑΜ Χρήστη" ID="AM_1" runat="server" Width="15%"/> &nbsp; &nbsp;
                           
                           <asp:DropDownList ID="Rights_1" runat="server"><asp:ListItem Value="Read Access"  Selected="True"></asp:ListItem><asp:ListItem Value="Full Access" ></asp:ListItem></asp:DropDownList>
                      &nbsp;&nbsp;
                             <asp:Button ID="B_Add" Text="Εισαγωγή" runat="server" OnClick="B_Add_Click" />
                            <br />
                            <asp:Label ID="L_FolderGridErr" runat="server" ForeColor="red" style="font-weight: 700"></asp:Label>
                                   </td>
 </tr></table>
                             </asp:Panel>

          
            </ContentTemplate></asp:UpdatePanel>

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




<asp:Label ID="WaitingApprover" runat="server" Text=""></asp:Label>


<asp:Label ID="IsWaitingApprover" runat="server" Text=""></asp:Label>

    <asp:Label ID="L_Errors" runat="server" ForeColor="red" style="font-weight: 700" Text=""></asp:Label>

<br />

<br />

<asp:Button ID="BSave" runat="server" Text="Αποθηκευση" OnClick="BSave_Click" CssClass="submit" />
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
        <asp:Label ID="FolderDiffs" runat="server" Visible="true"></asp:Label>
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