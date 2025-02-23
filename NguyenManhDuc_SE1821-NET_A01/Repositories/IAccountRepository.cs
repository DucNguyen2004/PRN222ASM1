using BusinessObjects.Models;

namespace Repository
{
    public interface IAccountRepository
    {
        SystemAccount GetUserByEmailAndPassword(string email, string password);
        List<SystemAccount> GetAllUsers();
        void DeleteUser(string id);
        void ToggleAccountStatus(short id);
        void UpdateUserRole(short id, int role);
    }
}
