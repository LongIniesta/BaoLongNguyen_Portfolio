using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObjects
{
    public partial class PRN231Context : DbContext
    {
        public PRN231Context()
        {
        }

        public PRN231Context(DbContextOptions<PRN231Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Gift> Gifts { get; set; } = null!;
        public virtual DbSet<GiftComment> GiftComments { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<React> Reacts { get; set; } = null!;
        public virtual DbSet<ReactType> ReactTypes { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceScheduler> ServiceSchedulers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:prn231version2.database.windows.net,1433;Initial Catalog=PRN231;Persist Security Info=False;User ID=PRN231;Password=Catdoglover!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.AvatarLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BanReason).HasMaxLength(500);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RefreshToken).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__RoleId__5EBF139D");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(500);
            });

            modelBuilder.Entity<Gift>(entity =>
            {
                entity.ToTable("Gift");

                entity.Property(e => e.GiftId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.GiftName).HasMaxLength(200);

                entity.Property(e => e.ImageLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Gifts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Gift__PostId__6D0D32F4");
            });

            modelBuilder.Entity<GiftComment>(entity =>
            {
                entity.ToTable("GiftComment");

                entity.Property(e => e.ApproveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.GiftId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.GiftComments)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiftComme__Accou__70DDC3D8");

                entity.HasOne(d => d.Gift)
                    .WithMany(p => p.GiftComments)
                    .HasForeignKey(d => d.GiftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GiftComme__GiftI__6FE99F9F");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ItemType)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__AccountId__02FC7413");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShipAddress).HasMaxLength(500);

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__ItemI__06CD04F7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__05D8E0BE");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__OwnerId__656C112C");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.ProductId })
                    .HasName("PK__Product__B93E4FE726D1C344");

                entity.ToTable("Product");

                entity.HasIndex(e => e.ItemId, "UQ__Product__727E838A0EB89F18")
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName).HasMaxLength(200);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__787EE5A0");

                entity.HasOne(d => d.Item)
                    .WithOne(p => p.Product)
                    .HasForeignKey<Product>(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__ItemId__76969D2E");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__PostId__778AC167");
            });

            modelBuilder.Entity<React>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.PostId })
                    .HasName("PK__React__AE3C83A747684AD6");

                entity.ToTable("React");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__React__AccountId__68487DD7");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__React__PostId__693CA210");

                entity.HasOne(d => d.ReactType)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.ReactTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__React__ReactType__6A30C649");
            });

            modelBuilder.Entity<ReactType>(entity =>
            {
                entity.ToTable("ReactType");

                entity.Property(e => e.ReactType1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ReactType");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Report");

                entity.Property(e => e.Content).HasMaxLength(1000);

                entity.HasOne(d => d.ReportedPerson)
                    .WithMany()
                    .HasForeignKey(d => d.ReportedPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Reported__09A971A2");

                entity.HasOne(d => d.Reporter)
                    .WithMany()
                    .HasForeignKey(d => d.ReporterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Reporter__08B54D69");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceName).HasMaxLength(200);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Service__PostId__7B5B524B");
            });

            modelBuilder.Entity<ServiceScheduler>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.ServiceId, e.StartDate, e.EndDate })
                    .HasName("PK__ServiceS__5884831240D448B0");

                entity.ToTable("ServiceScheduler");

                entity.HasIndex(e => e.ItemId, "UQ__ServiceS__727E838AF1CD7EBD")
                    .IsUnique();

                entity.Property(e => e.ItemId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Item)
                    .WithOne(p => p.ServiceScheduler)
                    .HasForeignKey<ServiceScheduler>(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceSc__ItemI__7F2BE32F");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceSchedulers)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServiceSc__Servi__00200768");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
