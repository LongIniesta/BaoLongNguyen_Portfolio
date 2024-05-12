using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject
{
    public partial class VoiceSpireContext : DbContext
    {
        public VoiceSpireContext()
        {
        }

        public VoiceSpireContext(DbContextOptions<VoiceSpireContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buyer> Buyers { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<VoiceDetail> VoiceDetails { get; set; } = null!;
        public virtual DbSet<VoiceJob> VoiceJobs { get; set; } = null!;
        public virtual DbSet<VoicePost> VoicePosts { get; set; } = null!;
        public virtual DbSet<VoiceProject> VoiceProjects { get; set; } = null!;
        public virtual DbSet<VoiceProperty> VoiceProperties { get; set; } = null!;
        public virtual DbSet<VoiceSeller> VoiceSellers { get; set; } = null!;
        public virtual DbSet<VoiceTransaction> VoiceTransactions { get; set; } = null!;
        public virtual DbSet<VoiceType> VoiceTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=extraordinary.database.windows.net;Uid=extraordinary;Pwd=JWTiasdb12344;Database=VoiceSpire");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.ToTable("Buyer");

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BankNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Content).HasMaxLength(300);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.HasOne(d => d.VoicePost)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.VoicePostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__VoicePo__123EB7A3");

                entity.HasOne(d => d.VoiceSellerNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.VoiceSeller)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comment__VoiceSe__114A936A");
            });

            modelBuilder.Entity<VoiceDetail>(entity =>
            {
                entity.ToTable("VoiceDetail");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MainVoiceLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VoiceGender).HasMaxLength(15);

                entity.Property(e => e.VoiceLocal).HasMaxLength(30);

                entity.Property(e => e.VoiceRegion).HasMaxLength(20);

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoiceDetails)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceDeta__Voice__7C4F7684");
            });

            modelBuilder.Entity<VoiceJob>(entity =>
            {
                entity.ToTable("VoiceJob");

                entity.Property(e => e.LinkDemo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VoiceJobStatus).HasMaxLength(50);

                entity.HasOne(d => d.VoiceProject)
                    .WithMany(p => p.VoiceJobs)
                    .HasForeignKey(d => d.VoiceProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceJob__VoiceP__7F2BE32F");

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoiceJobs)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceJob__VoiceS__00200768");
            });

            modelBuilder.Entity<VoicePost>(entity =>
            {
                entity.ToTable("VoicePost");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.LinkAudio)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(300);

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoicePosts1)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .HasConstraintName("FK__VoicePost__Voice__06CD04F7");

                entity.HasMany(d => d.VoiceSellers)
                    .WithMany(p => p.VoicePosts)
                    .UsingEntity<Dictionary<string, object>>(
                        "LikeListComment",
                        l => l.HasOne<VoiceSeller>().WithMany().HasForeignKey("VoiceSellerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LikeListC__Voice__160F4887"),
                        r => r.HasOne<VoicePost>().WithMany().HasForeignKey("VoicePostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LikeListC__Voice__151B244E"),
                        j =>
                        {
                            j.HasKey("VoicePostId", "VoiceSellerId");

                            j.ToTable("LikeListComment");
                        });

                entity.HasMany(d => d.VoiceSellersNavigation)
                    .WithMany(p => p.VoicePostsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "LikeListPost",
                        l => l.HasOne<VoiceSeller>().WithMany().HasForeignKey("VoiceSellerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LikeListP__Voice__0A9D95DB"),
                        r => r.HasOne<VoicePost>().WithMany().HasForeignKey("VoicePostId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__LikeListP__Voice__09A971A2"),
                        j =>
                        {
                            j.HasKey("VoicePostId", "VoiceSellerId");

                            j.ToTable("LikeListPost");
                        });
            });

            modelBuilder.Entity<VoiceProject>(entity =>
            {
                entity.ToTable("VoiceProject");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.LinkDocDemo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LinkDocMain)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.LinkThumbnail)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentStatus).HasMaxLength(50);

                entity.Property(e => e.ProjectStatus).HasMaxLength(50);

                entity.Property(e => e.ProjectType)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Request).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.VoiceGender).HasMaxLength(20);

                entity.Property(e => e.VoiceLocal).HasMaxLength(30);

                entity.Property(e => e.VoiceProperty).HasMaxLength(50);

                entity.Property(e => e.VoiceRegion).HasMaxLength(20);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.VoiceProjects)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceProj__Buyer__71D1E811");
            });

            modelBuilder.Entity<VoiceProperty>(entity =>
            {
                entity.HasKey(e => new { e.VoiceSellerId, e.VoicePropertyName });

                entity.ToTable("VoiceProperty");

                entity.Property(e => e.VoicePropertyName).HasMaxLength(50);

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoiceProperties)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceProp__Voice__76969D2E");
            });

            modelBuilder.Entity<VoiceSeller>(entity =>
            {
                entity.ToTable("VoiceSeller");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.AvatarLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BankNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(5);

                entity.Property(e => e.GoogleId).HasMaxLength(500);

                entity.Property(e => e.Introduce).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasMany(d => d.Followers)
                    .WithMany(p => p.VoiceSellers)
                    .UsingEntity<Dictionary<string, object>>(
                        "FollowList",
                        l => l.HasOne<VoiceSeller>().WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FollowLis__Follo__0E6E26BF"),
                        r => r.HasOne<VoiceSeller>().WithMany().HasForeignKey("VoiceSellerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FollowLis__Voice__0D7A0286"),
                        j =>
                        {
                            j.HasKey("VoiceSellerId", "FollowerId");

                            j.ToTable("FollowList");
                        });

                entity.HasMany(d => d.VoiceSellers)
                    .WithMany(p => p.Followers)
                    .UsingEntity<Dictionary<string, object>>(
                        "FollowList",
                        l => l.HasOne<VoiceSeller>().WithMany().HasForeignKey("VoiceSellerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FollowLis__Voice__0D7A0286"),
                        r => r.HasOne<VoiceSeller>().WithMany().HasForeignKey("FollowerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__FollowLis__Follo__0E6E26BF"),
                        j =>
                        {
                            j.HasKey("VoiceSellerId", "FollowerId");

                            j.ToTable("FollowList");
                        });
            });

            modelBuilder.Entity<VoiceTransaction>(entity =>
            {
                entity.ToTable("VoiceTransaction");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Feedback).HasMaxLength(500);

                entity.Property(e => e.LinkVoice)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.VoiceTransactionStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.VoiceProject)
                    .WithMany(p => p.VoiceTransactions)
                    .HasForeignKey(d => d.VoiceProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceTran__Voice__02FC7413");

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoiceTransactions)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceTran__Voice__03F0984C");
            });

            modelBuilder.Entity<VoiceType>(entity =>
            {
                entity.HasKey(e => new { e.VoiceSellerId, e.VoiceTypeDetail });

                entity.ToTable("VoiceType");

                entity.Property(e => e.VoiceTypeDetail).HasMaxLength(50);

                entity.HasOne(d => d.VoiceSeller)
                    .WithMany(p => p.VoiceTypes)
                    .HasForeignKey(d => d.VoiceSellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VoiceType__Voice__797309D9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
