using BusinessObject;
using DataAccess;
using DTO;
using Repository.Interface;

namespace Repository
{
    public class VoiceProjectRepository : IVoiceProjectRepository
    {
       
        public VoiceProject AddVoiceProject(VoiceProject voiceProject)
            => VoiceProjectDAO.Instance.AddVoiceProject(voiceProject);

        public VoiceProject GetById(int id) => VoiceProjectDAO.Instance.GetByID(id);

        public Page<VoiceProject> SearchByFilter(int currentPage, int PageSize, string search, int fromPrice, int toPrice, string region, string type, string gender, string property, int duration, string ProjectStatus, string projectType, string sortType)
        => VoiceProjectDAO.Instance.SearchByFilter(currentPage, PageSize, search, fromPrice,
             toPrice, region, type, gender, property, duration, ProjectStatus, projectType, sortType);
        public VoiceProject GetNewestVoiceProject() => VoiceProjectDAO.Instance.GetNewestVoiceProject();

        public VoiceProject UpdateVoiceProjectStatus(int id, string status) => VoiceProjectDAO.Instance.UpdateVoiceProjectStatus(id, status);

        public VoiceProject UpdateVoicePaymentStatus(int id, string paymentStatus) => VoiceProjectDAO.Instance.UpdateVoicePaymentStatus(id, paymentStatus);

        public IEnumerable<VoiceProject> GetByBuyerId(int buyerId) => VoiceProjectDAO.Instance.GetByBuyerId(buyerId);

        public BankInfomationDTO GetPaymentInformationByProjectId(int projectId) => VoiceProjectDAO.Instance.GetPaymentInformationByProjectId(projectId);

        public Page<VoiceProject> SearchProjectFilterForManager(int currentPage, int PageSize, string search, bool WaitApprove, bool NotApproved, bool Apply, bool Processing, bool Done, bool WaitToAccept, bool Denied, string projectType, string sortType)
            => VoiceProjectDAO.Instance.SearchProjectFilterForManager( currentPage,  PageSize,  search,  WaitApprove,  NotApproved,  Apply,  Processing,  Done,  WaitToAccept,  Denied,  projectType,  sortType);
    }
}
