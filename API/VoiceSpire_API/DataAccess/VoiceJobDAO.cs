using BusinessObject;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess
{
    public class VoiceJobDAO
    {
        private static VoiceJobDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceJobDAO() { }
        public static VoiceJobDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceJobDAO();
                    }
                    return instance;
                }
            }
        }

        public VoiceJob AddVoiceJob(VoiceJob voiceJob)
        {
            VoiceJob result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.Add(voiceJob).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }
        public VoiceJob UpdateVoiceJob(VoiceJob voiceJob)
        {
            VoiceJob result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.Update(voiceJob).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceJob GetByProjectIdForProjectSend(int projectId)
        {
            VoiceJob result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.FirstOrDefault(p => p.VoiceProjectId == projectId);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public VoiceJob GetByProjectIdAndSellerId(int projectId, int sellerId)
        {
            VoiceJob result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.SingleOrDefault(p => p.VoiceProjectId == projectId && p.VoiceSellerId == sellerId);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public VoiceJob GetById(int id)
        {
            VoiceJob result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.SingleOrDefault(p => p.VoiceJobId == id);
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public IEnumerable<VoiceJob> GetByProjectIdForProjectPost(int projectId) {
            List<VoiceJob> result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.Include(v => v.VoiceSeller).Where(p => p.VoiceProjectId == projectId).ToList();
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public int countNumberOfBooking(int sellerId) { 
            int result = 0;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceJobs.Count(v => v.VoiceSellerId == sellerId && v.VoiceJobStatus.ToLower().Trim() == "done");
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return result;
        }
        public Page<VoiceJob> GetBySellerId(int currentPage, int PageSize, int sellerId, int fromPrice, int toPrice, int duration, string search)
        {
            if(currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceJob> result = new List<VoiceJob>();

            Page<VoiceJob> page;
            try {
                var DBContext = new VoiceSpireContext();
                int TotalItems = DBContext.VoiceJobs.Include(j => j.VoiceProject).Count(u => u.VoiceSellerId == sellerId
                && u.VoiceProject.ToalOutputPrice >= fromPrice && u.VoiceProject.ToalOutputPrice <= toPrice
                && u.VoiceProject.Duration <= duration
                && u.VoiceProject.Title.ToLower().Contains(search.ToLower().Trim())
                );
                int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                result = DBContext.VoiceJobs.Include(j => j.VoiceProject).Where(u => u.VoiceSellerId == sellerId
                && u.VoiceProject.ToalOutputPrice >= fromPrice && u.VoiceProject.ToalOutputPrice <= toPrice
                && u.VoiceProject.Duration <= duration
                && u.VoiceProject.Title.ToLower().Contains(search.ToLower().Trim())
                )       .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                page = new Page<VoiceJob>
                {
                    results = result,
                    Count = result.Count(),
                    PageIndex = currentPage,
                    TotalCount = TotalItems,
                    TotalPages = TotalPages,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return page;
        }
    }
}
