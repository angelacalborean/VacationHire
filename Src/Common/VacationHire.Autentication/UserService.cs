using VacationHire.Authentication.Interfaces;

namespace VacationHire.Authentication
{
    public class UserService : IUserService
    {
        private static readonly Dictionary<string, List<string>> UserRolesDictionary = new()
        {
            { "user", ["User"] },
            { "admin", ["Admin", "User"] },
            { "user@vacationhire.com", ["Admin", "User"] } // hard coded user with "User" and "Admin" roles
        };

        public Task<List<string>> GetUserRoles(string userId)
        {
            return GetUserRolesInternally(userId);
        }

        private static async Task<List<string>> GetUserRolesInternally(string userId)
        {
            await Task.Delay(100); // Simulate a delay
            return UserRolesDictionary.TryGetValue(userId, out var value) ? value : [];
        }
    }
}