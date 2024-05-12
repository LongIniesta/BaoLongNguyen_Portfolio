using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VoiceProjectDTO
    {
        public int VoiceProjectId { get; set; }
        public int BuyerId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Request { get; set; }
        public string LinkDocDemo { get; set; } = null!;
        public string LinkDocMain { get; set; } = null!;
        public string? LinkThumbnail { get; set; }
        public string BankCode { get; set; } = null!;
        public int Price { get; set; }
        public int ToalOutputPrice { get; set; }
        public string? VoiceProperty { get; set; }
        public int? Duration { get; set; }
        public int? TextLength { get; set; }
        public string? VoiceGender { get; set; }
        public int? VoiceTone { get; set; }
        public string? VoiceRegion { get; set; }
        public string? VoiceLocal { get; set; }
        public int? VoiceInspirational { get; set; }
        public int? VoiceStress { get; set; }
        public int? VoicePronouce { get; set; }
        public int? VoiceSpeed { get; set; }
        public int NumberOfEdit { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public string ProjectStatus { get; set; } = null!;
        public string? PaymentStatus { get; set; }
        public string? ProjectType { get; set; }
        public bool Status { get; set; }
    }
}
