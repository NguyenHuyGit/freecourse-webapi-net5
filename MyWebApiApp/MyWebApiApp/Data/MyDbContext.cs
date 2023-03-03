using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApiApp.Data
{
    public class MyDbContext: DbContext
    {
        public MyDbContext(DbContextOptions options): base(options)
        {
           
        }

        #region DbSet
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e => {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NguoiNhan).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ChiTietDonHang>(entity=> {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e=> new { e.MaDH, e.MaHH});


                entity.HasOne(e => e.DonHang)
                      .WithMany(e => e.ChiTietDonHangs)
                      .HasForeignKey(e => e.MaDH)
                      .HasConstraintName("FK_DonHangCT_DonHang");

                entity.HasOne(e => e.HangHoa)
                      .WithMany(e => e.ChiTietDonHangs)
                      .HasForeignKey(e => e.MaHH)
                      .HasConstraintName("FK_DonHangCT_HangHoa");
            });
        }
    }
}
