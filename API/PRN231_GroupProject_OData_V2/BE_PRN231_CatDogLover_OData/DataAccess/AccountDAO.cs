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
        public Account GetByID(int id)
        {
            Account result = null;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.SingleOrDefault(u => u.AccountId == id);
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

        public Account RemoveAccount(int id)
        {
            Account result;
            Account Account = GetByID(id);
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Remove(Account).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Account UpdateAccount(Account Account)
        {
            Account result;
            try
            {
                var DBContext = new PRN231Context();
                result = DBContext.Accounts.Update(Account).Entity;
                DBContext.SaveChanges();
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
