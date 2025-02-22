using BusinessObjects.Models;

namespace Services
{
    public interface IAccountService
    {
        SystemAccount ValidateUser(string email, string password);
    }
}
