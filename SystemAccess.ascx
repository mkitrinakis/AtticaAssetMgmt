<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemAccess.ascx.cs" Inherits="AssetMgmt.SystemAccess" %>
<%@ Register Tagprefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.1.519.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
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
     <asp:CheckBoxList ID="Systems" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" >
        <asp:ListItem>T24</asp:ListItem>
         <asp:ListItem>WINDOWS</asp:ListItem>
        <asp:ListItem>ORACLE</asp:ListItem>
        <asp:ListItem>AROTRON</asp:ListItem>
        <asp:ListItem>U/SWITCHWARE</asp:ListItem>
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
        <td class="col2label required" >Άλλο</td>  
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

      <asp:Panel ID="panelOracle" runat="server" > 
          <p>
    &nbsp;</p>
<table>
    <tr>
        <th >Για ORACLE</th>
    </tr>
    <tr>
          <td><asp:DropDownList ID="OracleAccess" runat="server" ><asp:ListItem Value="-Επιλέξτε-" /> <asp:ListItem Value="ΚΑΤΑΧΩΡΗΣΗ" /><asp:ListItem Value="ΕΓΚΡΙΣΗ" /><asp:ListItem Value="ΠΡΟΒΟΛΗ ΣΤΟΙΧΕΙΩΝ" /></asp:DropDownList>
    <asp:Label ID="L_OracleAccess" runat="server" Text=""></asp:Label>
          </td>
    </tr>
  
</table>

          </asp:Panel>

      <asp:Panel ID="panelArotron" runat="server" > 
          <p>
    &nbsp;</p>
<table>
    <tr>
        <th colspan="2">Για AROTRON</th>
        
    </tr>
    <tr>
        <td class="col2label required">* Ρόλος στην Ομάδα</td>
        <td class="col2value"> 
<asp:TextBox ID="ArotronRole" runat="server"></asp:TextBox>
        <asp:Label ID="L_ArotronRole" runat="server" Text=""></asp:Label>      
        </td>
    </tr>
    <tr>
        <td class="col2label required">* Ομάδα Εργασίας</td>
        <td class="col2value"> 
<asp:TextBox ID="ArotronGroup" runat="server"></asp:TextBox>
        <asp:Label ID="L_ArotronGroup" runat="server" Text=""></asp:Label>    
        </td>
    </tr>
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
