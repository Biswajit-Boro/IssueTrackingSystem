<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="IssueTrackingSystem.LOGIN" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN</title>
    <style type="text/css">  /*Defining the style.*/
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 122px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">  <%--//Ensures it connects to the server.--%>
        <table cellpadding="4" cellspacing="4" class="auto-style1">
            <tr> <%--Table row--%>
                <td class="auto-style2">USERNAME</td>
                <td> <%--Table data--%>
                    <asp:TextBox ID="UserNameTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameFieldValidator" runat="server" ControlToValidate="UserNameTextBox" ErrorMessage="Please Enter Username." ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">PASSWORD</td> 
                <td>
                    <asp:TextBox ID="PasswordTextBox" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="PasswordFieldValidator" runat="server" ControlToValidate="PasswordTextBox" ErrorMessage="Please enter password" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    <asp:Button ID="LOGINButton" runat="server" OnClick="LOGINButton_Click" Text="LOGIN" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>