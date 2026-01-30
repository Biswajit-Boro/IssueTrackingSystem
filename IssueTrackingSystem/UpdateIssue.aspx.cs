using IssueTrackingSystem.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IssueTrackingSystem
{
    public partial class UpdateIssue : System.Web.UI.Page
    {
        int issueId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthContext.IsAuthenticated)
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
            string role = Session["Role"].ToString();
            int userId = Convert.ToInt32(Session["UserId"]);

            string newStatus = ddlStatus.SelectedValue;
            string workDescription = txtWorkLog.Text.Trim();

            /* ======================
               SERVER-SIDE VALIDATION
               ====================== */

            if (role == "User")
            {
                if (newStatus != "InProgress" && newStatus != "Closed")
                {
                    lblMessage.Text = "You are not allowed to set this status.";
                    return;
                }

                if (string.IsNullOrWhiteSpace(workDescription))
                {
                    lblMessage.Text = "Work description is mandatory.";
                    return;
                }
            }

            string cs = ConfigurationManager
                            .ConnectionStrings["dbcs2"]
                            .ConnectionString;

            bool isSuccess = false;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlTransaction tx = con.BeginTransaction();

                try
                {
                    // 1️⃣ Update Issue Status
                    SqlCommand updateCmd = new SqlCommand(
                        @"UPDATE Issues
                  SET Status = @Status
                  WHERE [Issue ID] = @IssueId AND IsDeleted = 0",
                        con, tx);

                    updateCmd.Parameters.AddWithValue("@Status", newStatus);
                    updateCmd.Parameters.AddWithValue("@IssueId", issueId);
                    updateCmd.ExecuteNonQuery();

                    // 2️⃣ Insert Work Log (if provided)
                    if (!string.IsNullOrWhiteSpace(workDescription))
                    {
                        SqlCommand logCmd = new SqlCommand(
                            @"INSERT INTO IssueWorkLogs
                      (IssueId, UserId, UserRole, StatusAtUpdate, WorkDescription)
                      VALUES
                      (@IssueId, @UserId, @Role, @Status, @Desc)",
                            con, tx);

                        logCmd.Parameters.AddWithValue("@IssueId", issueId);
                        logCmd.Parameters.AddWithValue("@UserId", userId);
                        logCmd.Parameters.AddWithValue("@Role", role);
                        logCmd.Parameters.AddWithValue("@Status", newStatus);
                        logCmd.Parameters.AddWithValue("@Desc", workDescription);

                        logCmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                    isSuccess = true;
                }
                catch
                {
                    tx.Rollback();
                    lblMessage.Text = "An error occurred while updating the issue.";
                }
            }

            // 🚨 Redirect MUST be outside transaction scope
            if (isSuccess)
            {
                Response.Redirect("ViewIssues.aspx");
            }
        }


        protected void ButtonBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewIssues.aspx");
        }

        void LoadIssue(int issueId)
        {
            string cs = ConfigurationManager
                            .ConnectionStrings["dbcs2"]
                            .ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT Title, Status
                                 FROM Issues
                                 WHERE [Issue ID] = @id AND IsDeleted = 0";

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
                    else
                    {
                        Response.Redirect("ViewIssues.aspx");
                    }
                }
            }
        }
    }
}