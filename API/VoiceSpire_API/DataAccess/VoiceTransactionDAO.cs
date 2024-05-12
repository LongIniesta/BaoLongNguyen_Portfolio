using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoiceTransactionDAO
    {
        private static VoiceTransactionDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceTransactionDAO() { }
        public static VoiceTransactionDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceTransactionDAO();
                    }
                    return instance;
                }
            }
        }

        public VoiceTransaction AddVoiceTransaction(VoiceTransaction voiceTransaction)
        {
            VoiceTransaction result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.Add(voiceTransaction).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public IEnumerable<VoiceTransaction> GetByProjectId(int projectId) {
            List<VoiceTransaction> result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.Include(v => v.VoiceSeller).Where(v => v.VoiceProjectId == projectId).ToList();
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int CountTransactionOfProject(int projectId) {
            int result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.Count(v => v.VoiceProjectId == projectId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceTransaction getLastestTransactionByProjectIdAndSellerId(int projectId, int sellerId) {
            VoiceTransaction result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.OrderByDescending(v => v.VoiceTransactionId).FirstOrDefault(v => v.VoiceProjectId == projectId && v.VoiceSellerId == sellerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceTransaction getById(int transactionId)
        {
            VoiceTransaction result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.SingleOrDefault(v => v.VoiceTransactionId == transactionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceTransaction Update(VoiceTransaction voiceTransaction) {
            VoiceTransaction result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceTransactions.Update(voiceTransaction).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
