using IssueTrackingSystem.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IssueTrackingSystem
{
    public partial class WebForm1 : Security.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LabelWelcome.Text = "Welcome Mr/Ms " + AuthContext.Username;
                Response.Write(AuthContext.Role); //Can comment if want. 

            }
        }

        protected void BtnViewIssues_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewIssues.aspx");
        }

        protected void CreateIssueButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateIssue.aspx");
        }

        protected void BtnLogOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("LOGIN.aspx", true);
        }
    }
}