using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public partial class VoiceSeller
    {
        public VoiceSeller()
        {
            Comments = new HashSet<Comment>();
            VoiceDetails = new HashSet<VoiceDetail>();
            VoiceJobs = new HashSet<VoiceJob>();
            VoicePosts1 = new HashSet<VoicePost>();
            VoiceProperties = new HashSet<VoiceProperty>();
            VoiceTransactions = new HashSet<VoiceTransaction>();
            VoiceTypes = new HashSet<VoiceType>();
            Followers = new HashSet<VoiceSeller>();
            VoicePosts = new HashSet<VoicePost>();
            VoicePostsNavigation = new HashSet<VoicePost>();
            VoiceSellers = new HashSet<VoiceSeller>();
        }

        public int VoiceSellerId { get; set; }
        public string Fullname { get; set; } = null!;
        public string? Phone { get; set; }
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Introduce { get; set; }
        public string? Address { get; set; }
        public string Gender { get; set; } = null!;
        public string? AvatarLink { get; set; }
        public double? RateAvg { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }
        public string? BankAccountName { get; set; }
        public string? GoogleId { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<VoiceDetail> VoiceDetails { get; set; }
        public virtual ICollection<VoiceJob> VoiceJobs { get; set; }
        public virtual ICollection<VoicePost> VoicePosts1 { get; set; }
        public virtual ICollection<VoiceProperty> VoiceProperties { get; set; }
        public virtual ICollection<VoiceTransaction> VoiceTransactions { get; set; }
        public virtual ICollection<VoiceType> VoiceTypes { get; set; }

        public virtual ICollection<VoiceSeller> Followers { get; set; }
        public virtual ICollection<VoicePost> VoicePosts { get; set; }
        public virtual ICollection<VoicePost> VoicePostsNavigation { get; set; }
        public virtual ICollection<VoiceSeller> VoiceSellers { get; set; }
    }
}
