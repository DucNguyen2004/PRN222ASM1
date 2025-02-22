using BusinessObjects.Models;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public SystemAccount GetUserByEmailAndPassword(string email, string password)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.SystemAccounts.FirstOrDefault(u => u.AccountEmail == email && u.AccountPassword == password);
            }
        }
    }
}
