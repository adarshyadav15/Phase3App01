using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Phase3App01.Models
{
    public partial class EcommerceContext : DbContext
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartDetail11> CartDetail11s { get; set; }
        public virtual DbSet<CustomerDb> CustomerDbs { get; set; }
        public virtual DbSet<Productlist> Productlists { get; set; }
        public virtual DbSet<SellerDb> SellerDbs { get; set; }
        public virtual DbSet<SellerDb1> SellerDb1s { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server= H5CG1220K24\\MSSQLSERVER01;integrated Security=true;database=Ecommerce;Persist Security Info=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CartDetail11>(entity =>
            {
                entity.HasKey(e => e.Itemid);

                entity.ToTable("CartDetail11");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Itemname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("itemname");

                entity.Property(e => e.Itemprice).HasColumnName("itemprice");
            });

            modelBuilder.Entity<CustomerDb>(entity =>
            {
                entity.HasKey(e => e.Cid);

                entity.ToTable("CustomerDB");

                entity.Property(e => e.Cid)
                    .ValueGeneratedNever()
                    .HasColumnName("cid");

                entity.Property(e => e.Caddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("caddress");

                entity.Property(e => e.Cpassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cpassword");

                entity.Property(e => e.Cusername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cusername");
            });

            modelBuilder.Entity<Productlist>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("Productlist");

                entity.Property(e => e.Pid).ValueGeneratedNever();

                entity.Property(e => e.Pdetails)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SellerDb>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SellerDB");

                entity.Property(e => e.Saddress)
                    .IsUnicode(false)
                    .HasColumnName("SAddress");

                entity.Property(e => e.Sname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SName");

                entity.Property(e => e.Spassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SPassword");

                entity.Property(e => e.Susername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUsername");
            });

            modelBuilder.Entity<SellerDb1>(entity =>
            {
                entity.HasKey(e => e.Susername);

                entity.ToTable("SellerDB1");

                entity.Property(e => e.Susername).HasColumnName("SUsername");

                entity.Property(e => e.Saddress)
                    .HasMaxLength(50)
                    .HasColumnName("SAddress");

                entity.Property(e => e.Sname).HasColumnName("SName");

                entity.Property(e => e.Spassword)
                    .HasMaxLength(50)
                    .HasColumnName("SPassword");
            });

            modelBuilder.Entity<ShippingDetail>(entity =>
            {
                entity.HasKey(e=>e.Id);

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("state");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
