using System.Collections.Generic;
using System.Text;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.Helpers
{
    public static class ScriptObjectHelper
    {
        public static string GetProfileListObjectScript(IList<UserProfile> userProfiles)
        {
            var sb = new StringBuilder();

            foreach (var userProfile in userProfiles)
            {
                if (!string.IsNullOrEmpty(sb.ToString()))
                    sb.Append(",");
                sb.Append(string.Format("{{email: '{0}', name: '{1}'}}", userProfile.UserName, userProfile.Name));
            }

            return string.Format("[{0}]", sb);
        }
    }
}