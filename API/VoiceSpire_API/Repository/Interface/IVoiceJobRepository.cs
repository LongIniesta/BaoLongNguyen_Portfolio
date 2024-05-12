using BusinessObject;
using DTO;

namespace Repository.Interface
{
    public interface IVoiceJobRepository
    {
        public VoiceJob AddVoiceJob(VoiceJob voiceJob);
        VoiceJob GetByProjectIdForProjectSend(int projectId);
        VoiceJob GetByProjectIdAndSellerId(int projectId, int sellerId);
        VoiceJob UpdateVoiceJob(VoiceJob voiceJob);
        IEnumerable<VoiceJob> GetByProjectIdForProjectPost(int projectId);
        int countNumberOfBooking(int sellerId);
        VoiceJob GetById(int id);
        Page<VoiceJob> GetBySellerId(int currentPage, int PageSize, int sellerId, int fromPrice, int toPrice, int duration, string search);
    }
}
