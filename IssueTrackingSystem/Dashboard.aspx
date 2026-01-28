<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="IssueTrackingSystem.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LabelWelcome" runat="server" Text="Welcome "></asp:Label>
         <br /><br />
        <asp:Button ID="BtnCreateIssue" runat="server" OnClick="CreateIssueButton_Click" Text="Create Issue" />
         <br /><br />
        <asp:Button ID="BtnViewIssues" runat="server" OnClick="BtnViewIssues_Click" Text="View Issues" />
         <br /><br />
        <asp:Button ID="BtnLogOut" runat="server" OnClick="BtnLogOut_Click" Text="LOG OUT" />
    </form>
</body>
</html>