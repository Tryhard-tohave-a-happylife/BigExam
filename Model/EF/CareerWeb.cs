namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CareerWeb : DbContext
    {
        public CareerWeb()
            : base("name=CareerWeb")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<JobMajor> JobMajors { get; set; }
        public virtual DbSet<OfferJob> OfferJobs { get; set; }
        public virtual DbSet<TypeOfEnterprise> TypeOfEnterprises { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EnterpriseArea> EnterpriseAreas { get; set; }
        public virtual DbSet<EnterpriseJob> EnterpriseJobs { get; set; }
        public virtual DbSet<OfferJobMajor> OfferJobMajors { get; set; }
        public virtual DbSet<UserCertificate> UserCertificates { get; set; }
        public virtual DbSet<UserMajor> UserMajors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.TypeOfAccount)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.ImageLogo)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.EnterpriseSize)
                .IsUnicode(false);

            modelBuilder.Entity<Enterprise>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.OfferDescription)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<OfferJob>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            modelBuilder.Entity<University>()
                .Property(e => e.UniversityLogo)
                .IsUnicode(false);

            modelBuilder.Entity<University>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<University>()
                .Property(e => e.Mobile)
                .IsUnicode(false);

            modelBuilder.Entity<University>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserMobile)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserImage)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserExperience)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Sex)
                .IsUnicode(false);

            modelBuilder.Entity<UserCertificate>()
                .Property(e => e.NameCertificate)
                .IsFixedLength();

            modelBuilder.Entity<UserCertificate>()
                .Property(e => e.ImageCertificate)
                .IsUnicode(false);
        }
    }
}
