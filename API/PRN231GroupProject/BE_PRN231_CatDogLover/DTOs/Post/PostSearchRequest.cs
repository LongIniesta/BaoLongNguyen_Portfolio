using DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Post
{
    public class PostSearchRequest : BasePagingSearchRequest
    {
        public string? Title { get; set; } = "";
        public string? Content { get; set; } = "";
        //query account owner instead
        //public int? OwnerId { get; set; }
        public string? Type { get; set; } = "";
        //[Required(ErrorMessage ="Lack of start date")]
        //public DateTime StartCreateDate { get; set; } = DateTime.MinValue;
        //[Required(ErrorMessage = "Lack of end date")]
        //public DateTime EndCreateDate { get; set; } = DateTime.MaxValue;
        public bool Status { get; set; }
        public int? NumberOfReact { get; set; }
    }
}
