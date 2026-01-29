using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace IssueTrackingSystem
{
    public partial class Register : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs2"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Basic validation
            if (username == "" || password == "")
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "error",
                    "<script>alert('All fields are required');</script>"
                );
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                // 1. Check if username already exists
                SqlCommand checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM LOGIN_TABLE WHERE username = @u",
                    con
                );
                checkCmd.Parameters.AddWithValue("@u", username);

                con.Open();
                int userExists = (int)checkCmd.ExecuteScalar();

                if (userExists > 0)
                {
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "exists",
                        "<script>alert('Username already exists');</script>"
                    );
                    return;
                }

                // 2. Insert new user (role forced to User)
                SqlCommand insertCmd = new SqlCommand(
                    @"INSERT INTO LOGIN_TABLE (username, password, role)
                      VALUES (@u, @p, 'User')",
                    con
                );

                insertCmd.Parameters.AddWithValue("@u", username);
                insertCmd.Parameters.AddWithValue("@p", password);

                insertCmd.ExecuteNonQuery();
            }

            // 3. Redirect to login
            Response.Redirect("~/LOGIN.aspx");
        }
    }
}