using DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Account
{
    public class AccountSearchRequest : BasePagingSearchRequest
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        //[Required(ErrorMessage = "Lack of start date")]
        //public DateTime? CreateDate { get; set; } = DateTime.MinValue;
        //[Required(ErrorMessage = "Lack of end date")]
        //public DateTime EndCreateDate { get; set; } = DateTime.MaxValue;
    }
}
