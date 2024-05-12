using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Post
{
    public class PostGeneralInformationResponse
    {
        public int? PostId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public int OwnerId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int? NumberOfReact { get; set; }
        public bool? Reacted { get; set; }
    }
}
