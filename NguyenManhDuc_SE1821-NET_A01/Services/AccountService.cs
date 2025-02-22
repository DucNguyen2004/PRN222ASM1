using BusinessObjects.Models;
using Repository;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository = new AccountRepository();

        public SystemAccount ValidateUser(string email, string password)
        {
            return _accountRepository.GetUserByEmailAndPassword(email, password);
        }
    }
}
