namespace VacationHire.Authentication.Interfaces
{
    /// <summary>
    ///     When a request is made to the AP, after the check for Authorization header, for basic authentication
    /// the credentials are extracted from the header, decoded and validated against a user store.
    ///     This is a dummy implementation, that returns the roles of a user: for hardcoded values, the user has the role "User", "Admin"
    /// </summary>
    public interface IUserService
    {
        Task<List<string>> GetUserRoles(string userId);
    }
}