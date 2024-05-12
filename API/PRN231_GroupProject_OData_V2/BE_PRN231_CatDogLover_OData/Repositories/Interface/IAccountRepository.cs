using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;
using DTOs.Account;

namespace Repositories.Interface
{
    public interface IAccountRepository
    {
        Account AddAccount(Account account);
        Account RemoveAccount(int id);
        AccountDTO UpdateAccount(AccountUpdateProfileRequest updateProfileRequest);
        IEnumerable<Account> GetAll();
        IQueryable<Account> Search(AccountSearchRequest searchRequest);
        Account GetById(int id);
        Task<Account> GetAccountById(int id);
    }
}
