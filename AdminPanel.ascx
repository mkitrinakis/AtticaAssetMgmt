<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminPanel.ascx.cs" Inherits="AssetMgmt.AdminPanel" EnableViewState="true" %>
<%@ Register Tagprefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI, Version=2010.1.519.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
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


<asp:Panel ID="panelAdmin" runat="server">
    <div align="right">
        Διαχειριστικές Εργασίες για να κλείσει η ροή Αίτησης. <br />
         <b>ΠΡΟΣΟΧΗ</b>: Μόνο για troubleshooting σε προβληματικά έγγραφα.<br />
        <asp:LinkButton ID="AdminImplement" runat="server" OnClick="adminApprove_Click" OnClientClick="return confirm('ΠΡΟΣΟΧΗ: Η Αίτηση θα κλείσει ΥΛΟΠΟΙΗΜΕΝΗ, Είστε σίγουροι ότι θέλετε να συνεχίσετε?');" Text="ΥΛΟΠΟΙΗΘΗΚΕ">ΥΛΟΠΟΙΗΘΗΚΕ</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:LinkButton ID="AdminReject" runat="server" Text="ΑΠΟΡΡΙΦΘΗΚΕ" OnClick="adminReject_Click" OnClientClick="return confirm('ΠΡΟΟΧΗ: Η Αίτηση θα κλείσει ως ΑΠΟΡΡΙΦΘΕΙΣΑ, Είστε σίγουροι ότι θέλετε να συνεχίσετε?');" >ΑΠΟΡΡΙΦΘΗΚΕ</asp:LinkButton>
        <br />
        Σχόλια (Προαιρετικά): <asp:TextBox ID="AdminPanelComments" runat="server" width="30%" /> 
    
        </div>
</asp:Panel>
