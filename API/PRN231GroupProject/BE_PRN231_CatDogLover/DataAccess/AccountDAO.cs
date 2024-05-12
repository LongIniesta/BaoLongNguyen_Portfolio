using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<Account> GetByID(int id)
        {
            try
            {
                Account? result = null;
                var DBContext = new PRN231Context();
                result = await DBContext.Accounts.Include(a => a.Role).FirstOrDefaultAsync(u => u.AccountId == id);
                return result != null ? result : throw new Exception("Not found account!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> GetByRefreshToken(string token)
        {
            Account? result = null;
            try
            {   
                var DBContext = new PRN231Context();
                result = await DBContext.Accounts.Include(a => a.Role).FirstOrDefaultAsync(u => u.RefreshToken == token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Account AddAccount(Account Account)
        {
            Account result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Add(Account).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<Account> RemoveAccount(int id)
        {
            Account result;
            Account Account = await GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Remove(Account).Entity;
                await DBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public async Task<Account> UpdateAccount(Account Account)
        {
            Account result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Update(Account).Entity;
                await DBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<Account> UpdateVersion(int id)
        {
            Account result;
            result = await GetByID(id);
            result.Version = result.Version + 1;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Update(result).Entity;
                await DBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public IEnumerable<Account> GetAll()
        {
            List<Account> result = new List<Account>();
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Include(a => a.Role).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
