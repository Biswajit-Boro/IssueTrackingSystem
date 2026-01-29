<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewIssues.aspx.cs" Inherits="IssueTrackingSystem.ViewIssues" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>View Issues</title>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Issue List</h2>

        <asp:GridView ID="gvIssues"
    runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="Issue ID"
    OnSelectedIndexChanged="gvIssues_SelectedIndexChanged"
    OnRowCommand="gvIssues_RowCommand"
    BorderWidth="1"
    CellPadding="6">

    <Columns>
        <asp:BoundField HeaderText="Issue ID" DataField="Issue ID" />
        <asp:BoundField HeaderText="Title" DataField="Title" />
        <asp:BoundField HeaderText="Status" DataField="Status" />
        <asp:BoundField HeaderText="Priority" DataField="Priority" />

         <%--THIS is the key --%>
        <asp:CommandField ShowSelectButton="True" />

                <asp:TemplateField HeaderText="Delete">
    <ItemTemplate>
        <asp:Button
            ID="btnDelete"
            runat="server"
            Text="Delete"
            CommandName="DeleteIssue"
            CommandArgument='<%# Eval("Issue ID") %>'
            Visible='<%# IssueTrackingSystem.Security.AuthContext.Role == "Admin" %>' />
        

    </ItemTemplate>
</asp:TemplateField>
    </Columns>
</asp:GridView>


       

       


       

        <br /><br />

        <asp:Button
            ID="btnBack"
            runat="server"
            Text="Back"
            OnClick="btnBack_Click" />

    </form>
</body>
</html>