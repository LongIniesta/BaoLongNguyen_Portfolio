using BusinessObject;
using DataAccess;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VoiceTransactionRepository : IVoiceTransactionRepository
    {
        public VoiceTransaction AddVoiceTransaction(VoiceTransaction voiceTransaction) => VoiceTransactionDAO.Instance.AddVoiceTransaction(voiceTransaction);

        public int CountTransactionOfProject(int projectId) => VoiceTransactionDAO.Instance.CountTransactionOfProject(projectId);

        public VoiceTransaction getById(int transactionId) => VoiceTransactionDAO.Instance.getById(transactionId);

        public IEnumerable<VoiceTransaction> GetByProjectId(int projectId) => VoiceTransactionDAO.Instance.GetByProjectId(projectId);

        public VoiceTransaction getLastestTransactionByProjectIdAndSellerId(int projectId, int sellerId) => VoiceTransactionDAO.Instance.getLastestTransactionByProjectIdAndSellerId(projectId, sellerId);

        public VoiceTransaction Update(VoiceTransaction voiceTransaction) => VoiceTransactionDAO.Instance.Update(voiceTransaction);
    }
}
