using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVoiceTransactionRepository
    {
        VoiceTransaction AddVoiceTransaction(VoiceTransaction voiceTransaction);
        IEnumerable<VoiceTransaction> GetByProjectId(int projectId);
        int CountTransactionOfProject(int projectId);
        VoiceTransaction getLastestTransactionByProjectIdAndSellerId(int projectId, int sellerId);
        VoiceTransaction getById(int transactionId);
        VoiceTransaction Update(VoiceTransaction voiceTransaction);
    }
}
