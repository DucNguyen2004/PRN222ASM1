using BusinessObjects.Models;
using DataAccessLayer;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO = new AccountDAO();

        public List<SystemAccount> GetAllUsers()
        {
            return _accountDAO.GetAllUsers();
        }

        public void DeleteUser(string userId)
        {
            _accountDAO.DeleteUser(userId);
        }

        public SystemAccount GetUserByEmailAndPassword(string email, string password)
        {
            return _accountDAO.GetUserByEmailAndPassword(email, password);
        }

        public void ToggleAccountStatus(short id)
        {
            _accountDAO.ToggleAccountStatus(id);
        }

        public void UpdateUserRole(short id, int role)
        {
            _accountDAO.UpdateUserRole(id, role);
        }
    }
}
