using BusinessObject;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VoiceDetailDAO
    {
        private static VoiceDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoiceDetailDAO() { }
        public static VoiceDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoiceDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public VoiceDetail GetByID(int voiceSellerId)
        {
            VoiceDetail result = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceDetails.Include(u => u.VoiceSeller).SingleOrDefault(u => u.VoiceSellerId == voiceSellerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public VoiceDetail AddVoiceDetail(VoiceDetail VoiceDetail)
        {
            VoiceDetail result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceDetails.Add(VoiceDetail).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
            return result;
        }

        public VoiceDetail UpdateVoiceDetail(VoiceDetail VoiceDetail)
        {
            VoiceDetail result;
            try
            {
                var DBContext = new VoiceSpireContext();
                result = DBContext.VoiceDetails.Update(VoiceDetail).Entity;
                DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public Page<VoiceDetail> GetPage(int currentPage, int PageSize, string search, string sortType, bool isApproved)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceDetail> result = new List<VoiceDetail>();
            Page<VoiceDetail> page = null;
            try
            {
                var DBContext = new VoiceSpireContext();
                if (sortType == "new")
                {
                    int TotalItems = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Count(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Where(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    ).OrderByDescending(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceDetail>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }
                else {
                    int TotalItems = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Count(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Where(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    ).OrderBy(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceDetail>
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

        public Page<VoiceDetail> SearchByFilter(int currentPage, int PageSize, string search, bool isApproved, int fromPrice,
            int toPrice, int tone, string region, string gender, string property, int rate, string type)
        {
            if (currentPage == 0) currentPage = 1;
            if (PageSize == 0) PageSize = 100;
            if (search == null) search = "";
            List<VoiceDetail> result = new List<VoiceDetail>();

            Page<VoiceDetail> page;
            try
            {
                var DBContext = new VoiceSpireContext();
                List<int> listId = new List<int>();
                bool checkProType = false;
                if (property != null && property != "") {
                    listId.AddRange(DBContext.VoiceProperties.Where(v => v.VoicePropertyName.Trim().ToLower().Contains(property.Trim().ToLower()))
                        .Select(v => v.VoiceSellerId).ToList());
                    checkProType = true;
                }
                if (type != null && type != "")
                {
                    listId.AddRange(DBContext.VoiceTypes.Where(v => v.VoiceTypeDetail.Trim().ToLower().Contains(type.Trim().ToLower()))
                        .Select(v => v.VoiceSellerId).ToList());
                    checkProType |= true;
                }
                if (checkProType)
                {
                    int TotalItems = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Count(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    && u.Price >= fromPrice && u.Price <= toPrice && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower()) && u.VoiceSeller.RateAvg >= rate && listId.Contains(u.VoiceSellerId)
                    );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Where(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    && u.Price >= fromPrice && u.Price <= toPrice && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower()) && u.VoiceSeller.RateAvg >= rate && listId.Contains(u.VoiceSellerId)
                    ).OrderByDescending(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceDetail>
                    {
                        results = result,
                        Count = result.Count(),
                        PageIndex = currentPage,
                        TotalCount = TotalItems,
                        TotalPages = TotalPages,
                    };
                }
                else {
                    int TotalItems = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Count(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    && u.Price >= fromPrice && u.Price <= toPrice && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower())
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower()) && u.VoiceSeller.RateAvg >= rate && (tone!=0?u.VoiceTone==tone:true)
                        );
                    int TotalPages = (int)Math.Ceiling((double)TotalItems / PageSize);
                    result = DBContext.VoiceDetails.Include(v => v.VoiceSeller).Where(u => u.VoiceSeller.Fullname.ToLower().Contains(search.ToLower().Trim()) && u.IsApprove == isApproved
                    && u.Price >= fromPrice && u.Price <= toPrice  && u.VoiceRegion.Trim().ToLower().Contains(region.Trim().ToLower()) && (tone != 0 ? u.VoiceTone == tone : true)
                    && u.VoiceGender.Trim().ToLower().Contains(gender.Trim().ToLower()) && u.VoiceSeller.RateAvg >= rate
                    ).OrderByDescending(u => u.CreateDate)
                        .Skip((currentPage - 1) * PageSize)
                        .Take(PageSize)
                        .ToList();
                    page = new Page<VoiceDetail>
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
    }
}
