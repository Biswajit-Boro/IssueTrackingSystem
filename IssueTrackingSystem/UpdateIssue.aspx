<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateIssue.aspx.cs" Inherits="IssueTrackingSystem.UpdateIssue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Update Issue</title>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Update Issue</h2>

        Issue Title:<br />
        <asp:Label ID="lblTitle" runat="server" Text="Login Bug"></asp:Label>
        <br /><br />

        Status:<br />
        <asp:DropDownList ID="ddlStatus" runat="server">
            <asp:ListItem>Open</asp:ListItem>
            <asp:ListItem>In Progress</asp:ListItem>
            <asp:ListItem>Closed</asp:ListItem>
        </asp:DropDownList>
        <br /><br />
        <asp:Label ID="lblWorkLog" runat="server" 
    Text="Work Progress / Work Log">
</asp:Label>
<br />

<asp:TextBox 
    ID="txtWorkLog"
    runat="server"
    TextMode="MultiLine"
    Rows="5"
    Columns="70">
</asp:TextBox>

<br /><br />
        <asp:Label 
    ID="lblMessage"
    runat="server"
    ForeColor="Red">
</asp:Label>

<br />


        <asp:Button ID="ButtonUpdate" runat="server" Text="Update Issue"
            OnClick="ButtonUpdate_Click" />
        <br /><br />

        <asp:Button ID="btnBack" runat="server" Text="Back"
            OnClick="ButtonBack_Click" />


    </form>
</body>
</html>