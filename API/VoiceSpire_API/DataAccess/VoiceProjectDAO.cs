using BusinessObject;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess
{
    public class VoiceProjectDAO
    {
        private static VoiceProjectDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceProjectDAO() { }
        public static VoiceProjectDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceProjectDAO();
                    }
                    return instance;
                }
            }
        }

        public VoiceProject AddVoiceProject(VoiceProject voiceProject)
        {
            VoiceProject result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProjects.Add(voiceProject).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceProject UpdateVoiceProjectStatus(int id,string status)
        {
            VoiceProject voiceProject = GetByID(id);
            VoiceProject result;
            if (voiceProject == null) return null;
            try
            {
                voiceProject.ProjectStatus = status;
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProjects.Update(voiceProject).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceProject UpdateVoicePaymentStatus(int id, string paymentStatus)
        {
            VoiceProject voiceProject = GetByID(id);
            VoiceProject result;
            if (voiceProject == null) return null;
            try
            {
                voiceProject.PaymentStatus = paymentStatus;
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProjects.Update(voiceProject).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public VoiceProject GetByID(int id)
        {
            VoiceProject result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProjects.SingleOrDefault(p => p.VoiceProjectId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Page<VoiceProject> SearchByFilter(int currentPage, int PageSize, string search, int fromPrice,
            int toPrice, string region, string type, string gender, string property, int duration, string ProjectStatus, string projectType, string sortType)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceProject> result = new List<VoiceProject>();

            Page<VoiceProject> page;
            try
            {
                if (sortType == "new")
                {
                    var DBContext = new VoiceSpireContext();
                    int TotalItems = DBContext.VoiceProjects.Count(u => u.Title.ToLower().Contains(search.ToLower().Trim())
                    && (ProjectStatus != "" ? (u.ProjectStatus.ToLower().Trim().Equals(ProjectStatus.ToLower().Trim())) : true)
                    && u.ToalOutputPrice >= fromPrice && u.ToalOutputPrice <= toPrice
                    && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower())
                    && u.VoiceProperty.Trim().ToLower().Contains(property.Trim().ToLower())
                    && u.ProjectType.Trim().ToLower().Contains(type.Trim().ToLower())
                    && u.Duration <= duration
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceProjects.Where(u => u.Title.ToLower().Contains(search.ToLower().Trim())
                && (ProjectStatus != "" ? (u.ProjectStatus.ToLower().Trim().Equals(ProjectStatus.ToLower().Trim())) : true)
                && u.ToalOutputPrice >= fromPrice && u.ToalOutputPrice <= toPrice
                && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower())
                && u.VoiceProperty.Trim().ToLower().Contains(property.Trim().ToLower())
                && u.ProjectType.Trim().ToLower().Contains(type.Trim().ToLower())
                && u.Duration <= duration
                && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    ).OrderByDescending(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceProject>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }
                else
                {
                    var DBContext = new VoiceSpireContext();
                    List<int> listId = new List<int>();
                    int TotalItems = DBContext.VoiceProjects.Count(u => u.Title.ToLower().Contains(search.ToLower().Trim())
                    && (ProjectStatus != "" ? (u.ProjectStatus.ToLower().Trim().Equals(ProjectStatus.ToLower().Trim())) : true)
                    && u.ToalOutputPrice >= fromPrice && u.ToalOutputPrice <= toPrice
                    && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower())
                    && u.VoiceProperty.Trim().ToLower().Contains(property.Trim().ToLower())
                    && u.ProjectType.Trim().ToLower().Contains(type.Trim().ToLower())
                    && u.Duration <= duration
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceProjects.Where(u => u.Title.ToLower().Contains(search.ToLower().Trim())
                && (ProjectStatus != "" ? (u.ProjectStatus.ToLower().Trim().Equals(ProjectStatus.ToLower().Trim())) : true)
                && u.ToalOutputPrice >= fromPrice && u.ToalOutputPrice <= toPrice
                && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower())
                && u.VoiceProperty.Trim().ToLower().Contains(property.Trim().ToLower())
                && u.ProjectType.Trim().ToLower().Contains(type.Trim().ToLower())
                && u.Duration <= duration
                && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    ).OrderBy(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceProject>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return page;
        }

        public Page<VoiceProject> SearchProjectFilterForManager(int currentPage, int PageSize, string search,
             bool WaitApprove, bool NotApproved, bool Apply,bool Processing, bool Done,bool WaitToAccept, bool Denied, string projectType, string sortType)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceProject> result = new List<VoiceProject>();

            Page<VoiceProject> page;
            try
            {
                if (sortType == "new")
                {
                    var DBContext = new VoiceSpireContext();
                    int TotalItems = DBContext.VoiceProjects.Count(u => u.BankCode.ToLower().Contains(search.ToLower().Trim())
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    && ((WaitApprove && u.ProjectStatus == "WaitApprove")
                    || (NotApproved && u.ProjectStatus == "NotApproved")
                    || (Apply && u.ProjectStatus == "Apply")
                    || (Processing && u.ProjectStatus == "Processing")
                    || (Done && u.ProjectStatus == "Done")
                    || (WaitToAccept && u.ProjectStatus == "WaitToAccept")
                    || (Denied && u.ProjectStatus == "Denied"))
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceProjects.Where(u => u.BankCode.ToLower().Contains(search.ToLower().Trim())
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    && ((WaitApprove && u.ProjectStatus == "WaitApprove")
                    || (NotApproved && u.ProjectStatus == "NotApproved")
                    || (Apply && u.ProjectStatus == "Apply")
                    || (Processing && u.ProjectStatus == "Processing")
                    || (Done && u.ProjectStatus == "Done")
                    || (WaitToAccept && u.ProjectStatus == "WaitToAccept")
                    || (Denied && u.ProjectStatus == "Denied"))
                    ).OrderByDescending(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceProject>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }
                else
                {
                    var DBContext = new VoiceSpireContext();
                    List<int> listId = new List<int>();
                    int TotalItems = DBContext.VoiceProjects.Count(u => u.BankCode.ToLower().Contains(search.ToLower().Trim())
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    && ((WaitApprove && u.ProjectStatus == "WaitApprove")
                    || (NotApproved && u.ProjectStatus == "NotApproved")
                    || (Apply && u.ProjectStatus == "Apply")
                    || (Processing && u.ProjectStatus == "Processing")
                    || (Done && u.ProjectStatus == "Done")
                    || (WaitToAccept && u.ProjectStatus == "WaitToAccept")
                    || (Denied && u.ProjectStatus == "Denied"))
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceProjects.Where(u => u.BankCode.ToLower().Contains(search.ToLower().Trim())
                    && (projectType != "" ? (u.ProjectType.ToLower().Trim().Equals(projectType.Trim().ToLower())) : true)
                    && ((WaitApprove && u.ProjectStatus == "WaitApprove")
                    || (NotApproved && u.ProjectStatus == "NotApproved")
                    || (Apply && u.ProjectStatus == "Apply")
                    || (Processing && u.ProjectStatus == "Processing")
                    || (Done && u.ProjectStatus == "Done")
                    || (WaitToAccept && u.ProjectStatus == "WaitToAccept")
                    || (Denied && u.ProjectStatus == "Denied"))
                    ).OrderBy(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceProject>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return page;
        } 

        public VoiceProject GetNewestVoiceProject()
        {
            try
            {
                using (var dbContext = new VoiceSpireContext())
                {
                    var newestProject = dbContext.VoiceProjects
                    .OrderByDescending(p => p.VoiceProjectId) // Sắp xếp theo thời gian tạo giảm dần
                    .FirstOrDefault(); // Lấy dự án đầu tiên trong danh sách (tức là mới nhất)
                    return newestProject;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public IEnumerable<VoiceProject> GetByBuyerId( int buyerId)
        {
            List<VoiceProject> result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceProjects.Where(p => p.BuyerId == buyerId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public BankInfomationDTO GetPaymentInformationByProjectId(int projectId) {
            BankInfomationDTO result = new BankInfomationDTO();
            try
            {
                var DBContext = new VoiceSpireContext();
                Buyer buyer = DBContext.VoiceProjects.Include(p => p.Buyer).SingleOrDefault(p => p.VoiceProjectId == projectId).Buyer;
                if (buyer == null) throw new Exception();
                VoiceJob voiceJob = DBContext.VoiceJobs.Include(vj => vj.VoiceSeller).SingleOrDefault(p => p.VoiceProjectId == projectId
                && (p.VoiceJobStatus == "Processing" || p.VoiceJobStatus == "Done"));
                if (voiceJob != null) {
                    result.BankNumberSeller = voiceJob.VoiceSeller.BankNumber;
                    result.BankAccountNameSeller = voiceJob.VoiceSeller.BankAccountName;
                    result.BankNameSeller = voiceJob.VoiceSeller.BankName;
                }
                result.ProjectId = projectId;
                result.BankNameBuyer = buyer.BankName;
                result.BankAccountNameBuyer = buyer.BankAccountName;
                result.BankNumberBuyer = buyer.BankNumber;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return result;
        }

        
    }
}
