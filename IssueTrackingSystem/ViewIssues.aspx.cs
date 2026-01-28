using System;
// Gives access to basic .NET types like DateTime, EventArgs, etc.

using System.Collections.Generic;
// NOT actually used here, but often included by default
// Can be removed safely (no logic impact)

using System.Linq;
// NOT used here either; safe to remove

using System.Web;
// Core ASP.NET features (Request, Response, Session)

using System.Web.UI;
// Required for ASP.NET Web Forms Page lifecycle

using System.Web.UI.WebControls;
// Required for controls like GridView, Button

using System.Configuration;
// Allows reading values from web.config (connection string)

using System.Data.SqlClient;
// SQL Server specific classes (SqlConnection, SqlCommand)

using System.Data;
// Provides DataTable and related classes


namespace IssueTrackingSystem
{
    // This class is the backend (code-behind) for ViewIssues.aspx
    // It runs on the SERVER, not in the browser
    public partial class ViewIssues : System.Web.UI.Page
    {
        // STEP 0:
        // Read the connection string named "dbcs2" from web.config
        // This tells the application:
        // - which SQL Server to connect to
        // - which database to use
        // - how to authenticate
        string cs = ConfigurationManager
                    .ConnectionStrings["dbcs2"]
                    .ConnectionString;

        // Page_Load is the FIRST method that runs
        // every time this page is requested
        protected void Page_Load(object sender, EventArgs e)
        {
            // STEP 1: SECURITY CHECK
            // Session["user"] is set during LOGIN
            // If it is NULL, the user is NOT logged in
            if (Session["user"] == null)
            {
                // Redirect unauthenticated users to LOGIN page
                // This prevents direct URL access
                Response.Redirect("LOGIN.aspx");
            }

            // STEP 2: LOAD DATA ONLY ON FIRST LOAD
            // IsPostBack = false → page opened for the first time
            // IsPostBack = true  → page reloaded due to a button click
            if (!IsPostBack)
            {
                // Only load data ONCE
                // Otherwise GridView would reload unnecessarily
                LoadIssues();
            }
        }

        // This method is responsible for:
        // - connecting to database
        // - fetching issues
        // - binding data to GridView
        private void LoadIssues()
        {
            // STEP 3: CREATE SQL CONNECTION
            // using ensures:
            // - connection is closed automatically
            // - resources are freed even if error occurs
            using (SqlConnection con = new SqlConnection(cs))
            {
                // STEP 4: WRITE SQL QUERY
                // [Issue ID] needs square brackets because the column name has a SPACE
                // Without brackets → SQL syntax error
                string query = @"SELECT 
                                    [Issue ID],
                                    Title,
                                    Status,
                                    Priority
                                 FROM Issues";

                // STEP 5: CREATE SQL COMMAND
                // This ties:
                // - the SQL query
                // - the database connection
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // STEP 6: CREATE DATA ADAPTER
                    // SqlDataAdapter:
                    // - executes the SELECT query
                    // - stores result in a DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        // STEP 7: CREATE EMPTY DATATABLE (IN MEMORY)
                        DataTable dt = new DataTable();

                        // STEP 8: FILL DATATABLE
                        // Internally:
                        // - opens connection
                        // - runs query
                        // - fetches rows
                        // - closes connection
                        da.Fill(dt);

                        // STEP 9: BIND DATA TO GRIDVIEW
                        // DataSource = data to show
                        gvIssues.DataSource = dt;

                        // DataBind() tells GridView:
                        // "Now render the data on the page"
                        gvIssues.DataBind();
                    }
                }
            }
            // At this point:
            // - connection is closed
            // - memory is released
            // - GridView has data
        }

        // This method runs when the Back button is clicked
        protected void btnBack_Click(object sender, EventArgs e)
        {
            // Simple navigation
            // Redirect user back to Dashboard
            Response.Redirect("Dashboard.aspx");
        }
        protected void gvIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            int issueId = Convert.ToInt32(gvIssues.SelectedDataKey.Value);

            Response.Redirect("UpdateIssue.aspx?IssueID=" + issueId);
        }
    }
}