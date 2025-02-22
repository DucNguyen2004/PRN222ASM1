using BusinessObjects.Models;

namespace Repository
{
    public interface IAccountRepository
    {
        SystemAccount GetUserByEmailAndPassword(string email, string password);
    }
}
