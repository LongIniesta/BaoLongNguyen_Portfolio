using BusinessObjects;
using DataAccess;
using DTOs;
using DTOs.Account;
using DTOs.Pagination;
using Repositories.Interface;
using Repositories.Utility;
using System.Net;
using System.Net.Mail;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<Account> AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

        public IEnumerable<Account> GetAll() => AccountDAO.Instance.GetAll();

        public async Task<Account> RemoveAccount(int id) => await AccountDAO.Instance.RemoveAccount(id);

        public async Task<Account> GetAccountById(int id)
        {
            try
            {
                var account = await AccountDAO.Instance.GetByID(id);
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Account> UpdateAccount(Account account)
        {
            try
            {
                var res = await AccountDAO.Instance.UpdateAccount(account);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PagedList<Account> Search(AccountSearchRequest searchRequest)
        {
            var query = AccountDAO.Instance.GetAll().AsQueryable();
            // Apply search
            query = query.GetWithSearch(searchRequest).AsQueryable();
            // Apply sort
            //response.GetWithSort();
            // Apply pagination
            var pagingData = query.GetWithPaging(searchRequest);

            return pagingData;
        }

        public async Task BanAccountAsync(int id, string reason)
        {
            try
            {
                var account = await AccountDAO.Instance.GetByID(id);
                if (account.Status)
                {
                    account.Status = false;
                    account.BanReason = reason;
                    await AccountDAO.Instance.UpdateAccount(account);
                    SendBanNotificationEmail(account.Email, reason, "BAN_REASON");

                    var relatedPosts = PostDAO.Instance.GetAll().Where(p => p.OwnerId == id);
                    if (relatedPosts != null)
                    {
                        foreach(var post in relatedPosts) 
                        {
                            post.Status = false;
                            PostDAO.Instance.UpdatePost(post);
                        }
                    }
                }
                else throw new Exception("Account has already been banned!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UnbanAccountAsync(int id)
        {
            try
            {
                var account = await AccountDAO.Instance.GetByID(id);
                if (!account.Status)
                {
                    account.Status = true;
                    await AccountDAO.Instance.UpdateAccount(account);

                    var relatedPosts = PostDAO.Instance.GetAll().Where(p => p.OwnerId == id);
                    if (relatedPosts != null)
                    {
                        foreach (var post in relatedPosts)
                        {
                            post.Status = true;
                            PostDAO.Instance.UpdatePost(post);
                        }
                    }
                }
                else
                {
                    throw new Exception("Account has already been unbanned!");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void SendBanNotificationEmail(string email, string content, string emailType)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("hiep.bh.02@gmail.com", "bfmw yomx vhld lokz"),
                EnableSsl = true,
            };

            MailMessage mailMessage;
            switch (emailType)
            {
                case "BAN_REASON":
                    mailMessage = new MailMessage
                    {
                        From = new MailAddress("hiep.bh.02@gmail.com"),
                        Subject = "Banned account",
                        Body = $"Due to {content},"
                            + $"<div>Your account on 'Cat Dog Platform' has been banned❗</div> "
                            + $"<div>If you have any question on this decision, please contact via this email.</div>",
                        IsBodyHtml = true,
                    };
                    break;
                case "FORGET_PASSWORD":
                    mailMessage = new MailMessage
                    {
                        From = new MailAddress("hiep.bh.02@gmail.com"),
                        Subject = "Forget Password",
                        Body = $"Hello,"
                            + $"<div>Your current password on 'Cat Dog Platform' is <h1>{content}</h1></div> ",
                        IsBodyHtml = true,
                    };
                    break;
                default:
                    mailMessage = new MailMessage();
                    break;
            }
            mailMessage.To.Add(email);
            smtpClient.Send(mailMessage);
        }

        public async Task ResetPasswordAsync(int id, string currentPassword, string newPassword)
        {
            try
            {
                var account = await AccountDAO.Instance.GetByID(id);
                if (!account.Password.Equals(currentPassword)) throw new Exception("Current password's not correct❗");
                if (currentPassword.Equals(newPassword)) throw new Exception("Current paasword's equal with new password❗");
                account.Password = newPassword;
                await AccountDAO.Instance.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ForgetPasswordAsync(int id)
        {
            try
            {
                var account = await AccountDAO.Instance.GetByID(id);

                SendBanNotificationEmail(account.Email, account.Password, "FORGET_PASSWORD");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Account> UpdateVersion(int id) => AccountDAO.Instance.UpdateVersion(id);

        public Task<Account> GetByRefreshToken(string token) => AccountDAO.Instance.GetByRefreshToken(token);

        public IQueryable<Account> SearchWithoutPagiantion(AccountSearchRequest searchRequest)
        {
            var query = AccountDAO.Instance.GetAll().AsQueryable();
            // Apply search
            query = query.GetWithSearch(searchRequest).AsQueryable();
            // Apply sort
            //response.GetWithSort();

            return query;
        }
    }
}
