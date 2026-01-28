using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace IssueTrackingSystem.Security
{
    public abstract class BasePage : Page
    {
        protected virtual string[] AllowedRoles => null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Login check
            if (!AuthContext.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx", true);
                return;
            }

            // Role check (if defined)
            if (AllowedRoles != null && AllowedRoles.Length > 0)
            {
                string role = AuthContext.Role;

                if (string.IsNullOrEmpty(role) ||
                    !AllowedRoles.Contains(role))
                {
                    Response.Redirect("~/Unauthorized.aspx", true);
                }
            }
        }
    }
}