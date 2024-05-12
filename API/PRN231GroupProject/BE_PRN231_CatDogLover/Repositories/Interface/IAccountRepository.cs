using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using DTOs.Account;
using DTOs.Pagination;

namespace Repositories.Interface
{
    public interface IAccountRepository
    {
        Task<Account> AddAccount(Account account);
        Task<Account> RemoveAccount(int id);
        Task<Account> GetAccountById(int id);
        Task<Account> UpdateAccount(Account account);
        IEnumerable<Account> GetAll();
        PagedList<Account> Search(AccountSearchRequest searchRequest);
        IQueryable<Account> SearchWithoutPagiantion(AccountSearchRequest searchRequest);
        Task BanAccountAsync(int id, string reason);
        Task UnbanAccountAsync(int id);
        Task ResetPasswordAsync(int id, string currentPassword, string newPassword);
        Task ForgetPasswordAsync(int id);

        Task<Account> UpdateVersion(int id);
        Task<Account> GetByRefreshToken(string token);

    }
}
