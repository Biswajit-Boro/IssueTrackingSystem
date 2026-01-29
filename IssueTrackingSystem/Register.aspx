<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="IssueTrackingSystem.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <h2>User Registration</h2>

        <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label><br />
        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br /><br />

        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label><br />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br /><br />

        <asp:Button ID="btnRegister" runat="server" Text="Register"
            OnClick="btnRegister_Click" />

    </form>
</body>
</html>
