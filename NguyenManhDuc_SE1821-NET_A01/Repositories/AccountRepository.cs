using BusinessObjects.Models;
using DataAccessLayer;

namespace Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO = new AccountDAO();
        public SystemAccount GetUserByEmailAndPassword(string email, string password)
        {
            return _accountDAO.GetUserByEmailAndPassword(email, password);
        }
    }
}
