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
    public partial class LOGIN : System.Web.UI.Page
    {
        // Reads the connection string from web.config using the key "dbcs2"
        string cs = ConfigurationManager.ConnectionStrings["dbcs2"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Runs every time the login page loads
            // No logic needed here for now
        }

        protected void LOGINButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
            SELECT ID, username, role
            FROM LOGIN_TABLE
            WHERE username = @user AND password = @pass";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@user", UserNameTextBox.Text.Trim());
                cmd.Parameters.AddWithValue("@pass", PasswordTextBox.Text.Trim());

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) // better than HasRows
                {
                    // CENTRALIZED session creation
                    Session[SessionKeys.UserId] = dr["ID"];
                    Session[SessionKeys.Username] = dr["username"].ToString();
                    Session[SessionKeys.Role] = dr["role"].ToString();

                    Response.Redirect("~/Dashboard.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "loginFail",
                        "<script>alert('Invalid Username or Password');</script>"
                    );
                }
            }
        }
    }
}
