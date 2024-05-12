using BusinessObjects;
using DataAccess;
using DTOs.Account;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Utility;
using DTOs;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public Account AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

        public IEnumerable<Account> GetAll() => AccountDAO.Instance.GetAll();

        public Account RemoveAccount(int id) => AccountDAO.Instance.RemoveAccount(id);

        public AccountDTO UpdateAccount(Account account)
        {
            var result = AccountDAO.Instance.UpdateAccount(account);
            return null;
        }

        public IQueryable<Account> Search(AccountSearchRequest searchRequest)
        {
         
            var query = AccountDAO.Instance.GetAll().AsQueryable();
            // Apply search
            query = query.GetWithSearch(searchRequest).AsQueryable();
            // Apply sort
            //response.GetWithSort();
            return query;
        }
        public async Task<Account> GetAccountById(int id)
        {
            try
            {
                var account =  AccountDAO.Instance.GetByID(id);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Account GetById(int id) =>  AccountDAO.Instance.GetByID(id);

        public AccountDTO UpdateAccount(AccountUpdateProfileRequest updateProfileRequest)
        {
            throw new NotImplementedException();
        }
    }
}
