<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MailAccess.ascx.cs" Inherits="AssetMgmt.MailAccess" EnableViewState="true" %>
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


<style type="text/css">
    .auto-style1 {
        font-weight: normal;
    }
</style>

<custom:AdminPanel ID="adminPanel" runat="server" />    
<asp:Panel ID="panelMain" runat="server" >
<div id="errorContainer">
    </div>
    <table><tr >
    <th > <div align="center">Αίτηση Δημιουργίας-Τροποποίησης Λίστας Αλληλογραφίας (Group Mail)</div>
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


<p>
    &nbsp;</p>
<table>
        <tr>
        <td class="col2label "><b>* EMail Address</b>
            <br />   
            (Ηλ. Διεύθυνση π.χ. hr@atticabank.gr) </td>
        <td class="col2value"> 
            <asp:TextBox ID="EMailAddressPrefix" runat="server" Width="25%"></asp:TextBox><asp:Label ID="EMailAt" runat="server" Text="@" /> <asp:DropDownList ID="EMailAddressPostfix" runat="server" ></asp:DropDownList>
            &nbsp

<asp:Label ID="L_EMailAddress" runat="server" Text=""></asp:Label>&nbsp&nbsp
             <asp:Button ID="BLoadMailDetails" runat="server"  Text="Τρέχουσα Εικόνα του Group Mail" OnClick="BLoadMailDetails_Click" CssClass="submit" Width="220px" /> 
    <br />
    <asp:Label ID="L_LoadMailDetailsErr" runat="server" ForeColor="red" style="font-weight: 700"></asp:Label>
    <asp:Label ID="EMailAddressChecked" runat="server"  Visible="false"></asp:Label>
    <asp:Label ID="EMailAddressOldForwardTo" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="EMailAddressOldSendAs" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>

    <tr>
        <td class="col2label"><b>* Display Name</b>
            <br />
            (Το όνομα της ηλεκτρονικής διεύθυνσης που θα εμφανίζεται στα Εισερχόμενα&nbsp; πχ.Attica Bank Human Resources) </td>
        <td class="col2value required"> 
<asp:TextBox ID="DisplayName" runat="server" Width="50%"></asp:TextBox>



<asp:Label ID="L_DisplayName" runat="server" Text=""></asp:Label>



        </td>
    </tr>
    <tr>
        <td class="col2label ">Max Recipients <br/> Η ανάγκη αποστολής σε περισσότερο από 50 παραλήπτες (Να αιτιολογήσετε την ανάγκη)</td>
        <td class="col2value">
            <Telerik:RadTextBox ID="MaxRecipients" runat="server" Height="50px" TextMode="MultiLine" Width="60%" />
            <asp:Label ID="L_MaxRecipients" runat="server" Text=""></asp:Label>  
        </td>
    </tr>
</table>


<p> 
    &nbsp;</p>


   
<table>
    <tr>
        <th width="50%" >Forward To permission<br /> <span class="auto-style1">Αποδέκτες της Λίστας</span></th>
        <th width="50%">Send As Permission
            <br />
            <span class="auto-style1">Δυνατότητα του χρήστη να στέλνει μήνυμα από την Λίστα&nbsp; Αλληλογραφίας</span></th>
        
    </tr>
    <tr>
        <td >
                     <asp:UpdatePanel ID="panelForwardTo" runat="server" >  
                         <ContentTemplate>
                     <telerik:RadGrid  ID="ForwardTo" runat="server" 
             MasterTableView-EditFormSettings-EditFormType="AutoGenerated"
            AllowAutomaticDeletes="true" AllowSorting="true" OnDeleteCommand="ForwardTo_DeleteCommand"       
                      
                         >
            
            <MasterTableView  AutoGenerateColumns="False"  CommandItemDisplay="None">

                <Columns>  
                    
                    <telerik:GridBoundColumn DataField="Name" HeaderText="Ονομα"     
                        UniqueName="Name"  Visible="true"  />
                    
                    <telerik:GridButtonColumn ConfirmText="Να διαγραφεί το όνομα από την λίστα?" ConfirmDialogType="RadWindow"  
                        ConfirmTitle="Διαγραφή" ButtonType="LinkButton" Text="Διαγραφή" CommandName="Delete" />  
                        </Columns>
               </MasterTableView>
                         </telerik:RadGrid>
            <br/> 
                             <asp:Panel ID="panelForwardToAdd" runat="server">
                             Όνομα Χρήστη:
                              <SharePoint:ClientPeoplePicker ID="ForwardToAdd_1" runat="server" 
                   AllowMultipleEntities="false" InitialHelpText="Εισάγετε Χρήστη (στα Λατινικά)"/> 
                             &nbsp;&nbsp;
                             
                                    <asp:Button ID="B_ForwardTo_Add" Text="Εισαγωγή"  runat="server" OnClick="B_ForwardTo_Add_Click" />
                            <asp:Label ID="L_ForwardToErr" runat="server" ForeColor="red" style="font-weight: 700"></asp:Label>
                             <br/>
                             </asp:Panel>
                                 </ContentTemplate>
                         </asp:UpdatePanel>
                </td>
        <td > 
                  <asp:UpdatePanel ID="panelSendAs" runat="server" ><ContentTemplate>
            <Telerik:RadGrid ID="SendAs" runat="server" AllowAutomaticDeletes="true" AllowSorting="true" MasterTableView-EditFormSettings-EditFormType="AutoGenerated" OnDeleteCommand="SendAs_DeleteCommand" >
                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None">
                    <Columns>
                        <Telerik:GridBoundColumn DataField="Name" HeaderText="Ονομα" UniqueName="Name" Visible="true" />
                        <Telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Delete" ConfirmDialogType="RadWindow" ConfirmText="Να διαγραφεί το όνομα από την λίστα?" ConfirmTitle="Διαγραφή" Text="Διαγραφή" />
                    </Columns>
                </MasterTableView>
            </Telerik:RadGrid>
            <br />
                       <asp:Panel ID="panelSendAsAdd" runat="server">
                      Όνομα Χρήστη:   
                            <SharePoint:ClientPeoplePicker ID="SendAsAdd_1" runat="server"  InitialHelpText="Εισάγετε Χρήστη (στα Λατινικά)"
                   AllowMultipleEntities="false"  /> 
                  
                      &nbsp;&nbsp;
                             <asp:Button ID="B_SendAs_Add" Text="Εισαγωγή" runat="server" OnClick="B_SendAs_Add_Click" />
                            <asp:Label ID="L_SendAsErr" runat="server" ForeColor="red" style="font-weight: 700"></asp:Label>
                             </asp:Panel>

            </ContentTemplate></asp:UpdatePanel>
  
        </td>
    </tr>
   
</table>




      

<p>
    &nbsp;</p>

    <table><tr><td class="col2label">
Υπεύθυνος Έγκρισης : </td><td>  <asp:Label ID="L_ddlApprover" runat="server" Text="" Font-Bold="true"></asp:Label>
        <SharePoint:ClientPeoplePicker ID="ddlApprover" runat="server" 
                    InitialHelpText="Εισάγετε Υπεύθυνο (στα Λατινικά)"  />  
                </td></tr></table>
    
&nbsp;
   
<br /> 
<br /> 
<asp:Literal ID="L_Comments" Text="Σχόλια Εγκρίνοντα (αποθηκεύονται στο Ιστορικό): " runat="server" /> 
<Telerik:RadTextBox ID="Comments" TextMode="MultiLine" Width="60%" Height="50px" runat="server" /> 


    <br />




<asp:Label ID="WaitingApprover" runat="server" Text=""></asp:Label>
<br />



<asp:Label ID="IsWaitingApprover" runat="server" Text=""></asp:Label>
    <asp:Label ID="L_Errors" runat="server" ForeColor="red" Text="" style="font-weight: 700"></asp:Label>

    <br /> 
<asp:Button ID="BSave" runat="server" Text="Αποθηκευση" OnClick="BSave_Click" CssClass="submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BSubmit" runat="server" Text="Υποβολη" OnClick="BSubmit_Click" CssClass="submit" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BBack" runat="server" Text="Πισω για Διορθωσεις" OnClick="BBack_Click" CssClass="submit"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;



<asp:Button ID="BReject" runat="server" Text="Απορριψη" OnClick="BReject_Click" CssClass="alert" />
&nbsp;<p>


<asp:Label ID="History" runat="server" Text=""></asp:Label>
</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="EMailAddressDiffs" runat="server" Visible="true"></asp:Label>
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