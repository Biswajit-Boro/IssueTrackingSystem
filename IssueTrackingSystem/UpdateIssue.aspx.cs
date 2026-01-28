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
    public partial class UpdateIssue : System.Web.UI.Page
    {
        // Holds the Issue ID passed from ViewIssues via QueryString

        int issueId;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["user"] == null)
            {
                Response.Redirect("LOGIN.aspx");
            }

            if (!int.TryParse(Request.QueryString["IssueID"], out issueId))
            {
                Response.Redirect("ViewIssues.aspx");
            }

            if (!IsPostBack)
            {
                LoadIssue(issueId);
            }


        }
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string cs = System.Configuration.ConfigurationManager
                   .ConnectionStrings["dbcs2"]
                   .ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Issues 
                         SET Status = @status 
                         WHERE [Issue ID] = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
                    cmd.Parameters.AddWithValue("@id", issueId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Redirect("ViewIssues.aspx");

        }

        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewIssues.aspx");
        }
        void LoadIssue(int issueId)
        {
            string cs = System.Configuration.ConfigurationManager
                            .ConnectionStrings["dbcs2"]
                            .ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Title, Status 
                         FROM Issues 
                         WHERE [Issue ID] = @id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", issueId);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblTitle.Text = dr["Title"].ToString();
                        ddlStatus.SelectedValue = dr["Status"].ToString();
                    }
                }
            }
        }

    }
}