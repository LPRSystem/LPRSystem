using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
    public class UserConstants
    {
        public static string GetUsers => new string("[api].[uspGetUsers]");
        public static string GetUserById => new string("[api].[uspGetUserById]");
        public static string InsertOrUpdateUser => new string("[api].[uspInsertOrUpdateUser]");
        public static string InsertUser => new string("[api].[uspInsertUser]");
        public static string UpdateUser => new string("[api].[uspUpdateUser]");
        public static string DeleteUser => new string("[api].[uspDeleteUser]");
    }
}
