using Microsoft.EntityFrameworkCore;
using WebAPI.Datas;

namespace WebAPI
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Categories_Documents> Categories_Documents { get; set; }
        public DbSet<Categories_News> Categories_News { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Questions> Questions { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.ToTable("Accounts");
                entity.HasKey(ac => ac.Id);
                entity.Property(ac => ac.Email).IsRequired();
                entity.Property(ac => ac.Name).IsRequired().HasMaxLength(100);
                entity.Property(ac => ac.Password).IsRequired().HasMaxLength(100);
                entity.Property(ac => ac.Username).IsRequired().HasMaxLength(100);
                entity.HasIndex(ac => ac.Username).IsUnique();
            });

            modelBuilder.Entity<Categories_Documents>(entity =>
            {
                entity.ToTable("Categories_Documents");
                entity.HasKey(cd => cd.Id);
                entity.Property(cd => cd.Title).IsRequired();
            });

            modelBuilder.Entity<Documents>(entity =>
            {
                entity.ToTable("Documents");
                entity.HasKey(d => d.Id);
                //entity.Property(d => d.CreatedDate).HasDefaultValueSql("getutcdate()");
                entity.HasOne(d => d.Categories_Documents)
                    .WithMany(d => d.Documents)
                    .HasForeignKey(d => d.Category_Documents_id)
                    .HasConstraintName("FK_Documents_Category_Documents");
            });

            modelBuilder.Entity<Categories_News>(entity =>
            {
                entity.ToTable("Categories_News");
                entity.HasKey(cn => cn.Id);
                entity.Property(cn => cn.Title).IsRequired();
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("News");
                entity.HasKey(n => n.Id);
                //entity.Property(n => n.CreatedDate).HasDefaultValueSql("getutcdate()");
                entity.HasOne(n => n.Categories_News)
                    .WithMany(n => n.News)
                    .HasForeignKey(n => n.Category_News_id)
                    .HasConstraintName("FK_News_Category_News");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.ToTable("Questions");
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Email).IsRequired();
                entity.Property(q => q.Name).IsRequired().HasMaxLength(100);
                entity.Property(q => q.Title).IsRequired();
                entity.Property(q => q.Question).IsRequired();
            });
        }
    }
}
