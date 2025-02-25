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

        public List<SystemAccount> GetAllUsers()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.SystemAccounts.ToList();
            }
        }

        public void DeleteUser(string userId)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.FirstOrDefault(a => a.AccountId.Equals(userId));
                if (user != null)
                {
                    context.SystemAccounts.Remove(user);
                    context.SaveChanges();
                }
            }
        }
        public void ToggleAccountStatus(short id)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
                if (user != null)
                {
                    user.AccountRole = (user.AccountRole == -1) ? 1 : -1; // Change Role to Active or Inactive
                    context.SaveChanges();
                }
            }
        }
        public void UpdateUserRole(short id, int role)
        {
            using (var context = new FunewsManagementContext())
            {
                var user = context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
                if (user != null && (role == 1 || role == 2))
                {
                    user.AccountRole = role;
                    context.SaveChanges();
                }
            }
        }
        public SystemAccount? GetUserById(short id)
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(u => u.AccountId == id);
        }

        public void UpdateAccount(SystemAccount user)
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Update(user);
            context.SaveChanges();
        }
        public bool ChangePassword(short userId, string currentPassword, string newPassword)
        {
            using var context = new FunewsManagementContext();
            var user = GetUserById(userId);
            if (user == null || user.AccountPassword != currentPassword) return false;

            user.AccountPassword = newPassword;
            context.SystemAccounts.Update(user);
            context.SaveChanges();
            return true;
        }
    }
}
