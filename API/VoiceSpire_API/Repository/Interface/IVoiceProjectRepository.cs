using BusinessObject;
using DTO;

namespace Repository.Interface
{
    public interface IVoiceProjectRepository
    {
        VoiceProject AddVoiceProject(VoiceProject voiceProject);
        Page<VoiceProject> SearchByFilter(int currentPage, int PageSize, string search, int fromPrice,
            int toPrice, string region, string type, string gender, string property, int duration, string ProjectStatus, string projectType, string sortType);
        VoiceProject GetById(int id);
        VoiceProject GetNewestVoiceProject();
        VoiceProject UpdateVoiceProjectStatus(int id, string status);
        VoiceProject UpdateVoicePaymentStatus(int id, string paymentStatus);
        IEnumerable<VoiceProject> GetByBuyerId(int buyerId);
        BankInfomationDTO GetPaymentInformationByProjectId(int projectId);
        Page<VoiceProject> SearchProjectFilterForManager(int currentPage, int PageSize, string search,
             bool WaitApprove, bool NotApproved, bool Apply, bool Processing, bool Done, bool WaitToAccept, bool Denied, string projectType, string sortType);

    }
}
