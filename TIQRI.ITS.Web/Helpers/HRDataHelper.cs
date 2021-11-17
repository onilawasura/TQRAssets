using System.Collections.Generic;
using TIQRI.ITS.Domain.Models;

namespace TIQRI.ITS.Web.Helpers
{
    public static class HRDataHelper
    {
        public static IList<UserProfile> GetBlankEmployeeList()
        {
            var employeeDT =
                SqlHelper.ExecuteStatement(
                    "SELECT [EmployeeId],[Name] FROM [dbo].[Employees] order by [Name]");

            var userProfileList = new List<UserProfile>();
            for (int i = 0; i < employeeDT.Rows.Count; i++)
            {
                userProfileList.Add(new UserProfile()
                {
                    Name = employeeDT.Rows[i]["Name"].ToString(),
                    UserName = employeeDT.Rows[i]["EmployeeId"].ToString()
                });
            }

            return userProfileList;
        }

        public static UserProfile GetEmployee(string email)
        {
            var employeeDT =
                SqlHelper.ExecuteStatement(
                    "SELECT * FROM [dbo].[Employees] where [IsActive] = 1 and [EmployeeId] ='" + email + "'");

            var userProfile = new UserProfile();
            if (employeeDT.Rows.Count > 0) {
                userProfile = new UserProfile()
                {
                    Name = employeeDT.Rows[0]["Name"].ToString(),
                    UserName = employeeDT.Rows[0]["EmployeeId"].ToString(),
                    CurrentProject = employeeDT.Rows[0]["CurrentProject"].ToString(),
                    Gender = employeeDT.Rows[0]["Gender"].ToString(),
                    Designation = employeeDT.Rows[0]["Designation"].ToString(),
                    ExpericeYears = employeeDT.Rows[0]["ExpericeYears"].ToString(),
                    YearsAtExilesoft = employeeDT.Rows[0]["YearsAtExilesoft"].ToString(),
                    Age = employeeDT.Rows[0]["Age"].ToString(),
                    Expertise = employeeDT.Rows[0]["Expertise"].ToString()
                };
            }
            return userProfile;
        }
    }
}