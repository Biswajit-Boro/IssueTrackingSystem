using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using IssueTrackingSystem.Security;

namespace IssueTrackingSystem
{
    public partial class CreateIssue : BasePage  // <-- Changed
    {
        //  Role-based authorization
        protected override string[] AllowedRoles =>
            new[] { "Admin" };
        string cs = ConfigurationManager.ConnectionStrings["dbcs2"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Optional: show welcome message from session
                // LabelWelcome.Text = "Welcome " + Session[SessionKeys.Username]?.ToString();
            }
        }

        protected void CreateButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Issues
                                 (Title, Description, Priority, Status, CreatedBy, CreatedOn)
                                 VALUES
                                 (@Title, @Description, @Priority, @Status, @CreatedBy, @CreatedOn)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", TitleTextBox.Text);
                    cmd.Parameters.AddWithValue("@Description", DescriptionTextBox.Text);
                    cmd.Parameters.AddWithValue("@Priority", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@Status", "Open");
                    cmd.Parameters.AddWithValue("@CreatedBy", Session[SessionKeys.Username]?.ToString());

                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('Issue created successfully');</script>");
        }

        protected void DashboardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}