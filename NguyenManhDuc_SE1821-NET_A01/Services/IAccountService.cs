using BusinessObjects.Models;

namespace Services
{
    public interface IAccountService
    {
        SystemAccount ValidateUser(string email, string password);
        List<SystemAccount> GetAllUsers();
        void DeleteUser(string id);
        void ToggleAccountStatus(short id);
        void UpdateUserRole(short id, int role);
    }
}
