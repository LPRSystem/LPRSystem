namespace LPRSystem.Web.API.Manager.Constants
{
    public static class RoleConstants
    {
        public static string GetRoles => new string("[api].[uspGetRoles]");
        public static string GetRoleById => new string("[api].[uspGetRoleById]");
        public static string InsertOrUpdateRole => new string("[api].[uspInsertOrUpdateRole]");
        public static string InsertRole => new string("[api].[uspInsertRole]");
        public static string UpdateRole => new string("[api].[uspUpdateRole]");
        public static string DeleteRole => new string("[api].[uspDeleteRole]");
    }
}
