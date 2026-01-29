using IssueTrackingSystem.Security;
using System;
// Gives access to basic .NET types like DateTime, EventArgs, etc.

using System.Collections.Generic;
// Required for controls like GridView, Button

using System.Configuration;
// SQL Server specific classes (SqlConnection, SqlCommand)

using System.Data;
// Allows reading values from web.config (connection string)

using System.Data.SqlClient;
// NOT actually used here, but often included by default
// Can be removed safely (no logic impact)

using System.Linq;
// NOT used here either; safe to remove

using System.Web;
// Core ASP.NET features (Request, Response, Session)

using System.Web.UI;
// Required for ASP.NET Web Forms Page lifecycle

using System.Web.UI.WebControls;
// Provides DataTable and related classes


namespace IssueTrackingSystem
{
    // Backend for ViewIssues.aspx
    // Runs on the SERVER
    public partial class ViewIssues : Security.BasePage
    {
        // Component C: both Admin and User can view issues
        protected override string[] AllowedRoles => new[] { "Admin", "User" };

        // Connection string from web.config
        string cs = ConfigurationManager
                    .ConnectionStrings["dbcs2"]
                    .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ❌ NO manual authentication check here
            // ✅ BasePage.OnInit already enforces login + role

            if (!IsPostBack)
            {
                LoadIssues();
            }
        }

        // Fetch and bind issues to GridView
        private void LoadIssues()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT 
                                    [Issue ID],
                                    Title,
                                    Status,
                                    Priority
                                 FROM Issues
                                 WHERE IsDeleted = 0";

                using (SqlCommand cmd = new SqlCommand(query, con))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvIssues.DataSource = dt;
                    gvIssues.DataBind();
                }
            }
        }

        // Back to dashboard
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        // Row selection → Update page
        protected void gvIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            int issueId = Convert.ToInt32(gvIssues.SelectedDataKey.Value);
            Response.Redirect("UpdateIssue.aspx?IssueID=" + issueId);
        }

        // Handle delete command
        protected void gvIssues_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteIssue")
            {
                // SECOND LINE OF DEFENCE (correct)
                if (AuthContext.Role != "Admin")
                {
                    Response.Redirect("Unauthorized.aspx", true);
                    return;
                }

                int issueId = Convert.ToInt32(e.CommandArgument);
                LogicalDeleteIssue(issueId);
                LoadIssues();
            }
        }

        // Logical delete (soft delete)
        private void LogicalDeleteIssue(int issueId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Issues
                                 SET IsDeleted = 1
                                 WHERE [Issue ID] = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", issueId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}