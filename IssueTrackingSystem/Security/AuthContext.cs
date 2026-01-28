using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IssueTrackingSystem.Security
{
    public static class AuthContext
    {
        public static bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.Session[SessionKeys.UserId] != null;
            }
        }

        public static string Role
        {
            get
            {
                return HttpContext.Current.Session[SessionKeys.Role] as string;
            }
        }

        public static string Username
        {
            get
            {
                return HttpContext.Current.Session[SessionKeys.Username] as string;
            }
        }
    }
}