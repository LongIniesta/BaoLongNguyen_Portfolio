using BusinessObject;
using DataAccess;
using DTO;
using Repository.Interface;

namespace Repository
{
    public class VoiceJobRepository : IVoiceJobRepository
    {
        public VoiceJob AddVoiceJob(VoiceJob voiceJob) => VoiceJobDAO.Instance.AddVoiceJob(voiceJob);

        public int countNumberOfBooking(int sellerId) => VoiceJobDAO.Instance.countNumberOfBooking(sellerId);

        public VoiceJob GetById(int id) => VoiceJobDAO.Instance.GetById(id);
        public VoiceJob GetByProjectIdForProjectSend(int projectId) => VoiceJobDAO.Instance.GetByProjectIdForProjectSend(projectId);

        public VoiceJob GetByProjectIdAndSellerId(int projectId, int sellerId) => VoiceJobDAO.Instance.GetByProjectIdAndSellerId(projectId, sellerId);

        public IEnumerable<VoiceJob> GetByProjectIdForProjectPost(int projectId) => VoiceJobDAO.Instance.GetByProjectIdForProjectPost(projectId);

        public VoiceJob UpdateVoiceJob(VoiceJob voiceJob) => VoiceJobDAO.Instance.UpdateVoiceJob(voiceJob);

        public Page<VoiceJob> GetBySellerId(int currentPage, int PageSize, int sellerId, int fromPrice, int toPrice, int duration, string search)
            => VoiceJobDAO.Instance.GetBySellerId(currentPage, PageSize, sellerId, fromPrice, toPrice, duration, search);
    }
}
