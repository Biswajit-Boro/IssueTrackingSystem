<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateIssue.aspx.cs" Inherits="IssueTrackingSystem.CreateIssue" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Issue</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Create Issue</h2>
        Title: <br />
        <asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox>
        <br /><br />
        Description:<br />
        <asp:TextBox ID="DescriptionTextBox" runat="server" Rows="4" TextMode="MultiLine"></asp:TextBox>
        <br /><br />
        Priority:<br />
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>Low</asp:ListItem>
            <asp:ListItem>Medium</asp:ListItem>
            <asp:ListItem>High</asp:ListItem>
        </asp:DropDownList>
        <br /><br />
        <asp:Button ID="CreateButton" runat="server" OnClick="CreateButton_Click" Text="CREATE ISSUE" />
        
        <br /><br />
        <asp:Button ID="BackToDashboardButton" runat="server" OnClick="DashboardButton_Click" Text="Back to Dashboard" />

    </form>
</body>
</html>