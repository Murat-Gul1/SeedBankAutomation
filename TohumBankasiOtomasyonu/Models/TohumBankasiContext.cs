using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TohumBankasiOtomasyonu.Models;

public partial class TohumBankasiContext : DbContext
{
    public TohumBankasiContext()
    {
    }

    public TohumBankasiContext(DbContextOptions<TohumBankasiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BitkiCevirileri> BitkiCevirileris { get; set; }

    public virtual DbSet<BitkiGorselleri> BitkiGorselleris { get; set; }

    public virtual DbSet<Bitkiler> Bitkilers { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<SahteBlokzincir> SahteBlokzincirs { get; set; }

    public virtual DbSet<SatisDetaylari> SatisDetaylaris { get; set; }

    public virtual DbSet<Satislar> Satislars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=TohumBankasi.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BitkiCevirileri>(entity =>
        {
            entity.HasKey(e => e.CeviriId);

            entity.ToTable("Bitki_Cevirileri");

            entity.Property(e => e.CeviriId).HasColumnName("CeviriID");
            entity.Property(e => e.BitkiAdi).IsRequired();
            entity.Property(e => e.BitkiId).HasColumnName("BitkiID");
            entity.Property(e => e.DilKodu).IsRequired();

            entity.HasOne(d => d.Bitki).WithMany(p => p.BitkiCevirileris).HasForeignKey(d => d.BitkiId);
        });

        modelBuilder.Entity<BitkiGorselleri>(entity =>
        {
            entity.HasKey(e => e.GorselId);

            entity.ToTable("Bitki_Gorselleri");

            entity.Property(e => e.GorselId).HasColumnName("GorselID");
            entity.Property(e => e.BitkiId).HasColumnName("BitkiID");
            entity.Property(e => e.DosyaYolu).IsRequired();

            entity.HasOne(d => d.Bitki).WithMany(p => p.BitkiGorselleris).HasForeignKey(d => d.BitkiId);
        });

        modelBuilder.Entity<Bitkiler>(entity =>
        {
            entity.HasKey(e => e.BitkiId);

            entity.ToTable("Bitkiler");

            entity.Property(e => e.BitkiId).HasColumnName("BitkiID");
            entity.Property(e => e.Aktif).HasDefaultValue(1);
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId);

            entity.ToTable("Kullanicilar");

            entity.HasIndex(e => e.Email, "IX_Kullanicilar_Email").IsUnique();

            entity.HasIndex(e => e.KullaniciAdi, "IX_Kullanicilar_KullaniciAdi").IsUnique();

            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.KullaniciAdi).IsRequired();
            entity.Property(e => e.KullaniciTipi)
                .IsRequired()
                .HasDefaultValue("Kullanici");
            entity.Property(e => e.SifreHash).IsRequired();
        });

        modelBuilder.Entity<SahteBlokzincir>(entity =>
        {
            entity.HasKey(e => e.BlokId);

            entity.ToTable("Sahte_Blokzincir");

            entity.Property(e => e.BlokId).HasColumnName("BlokID");
            entity.Property(e => e.Hash).IsRequired();
            entity.Property(e => e.OncekiHash).IsRequired();
            entity.Property(e => e.Veri).IsRequired();
            entity.Property(e => e.ZamanDamgasi).IsRequired();
        });

        modelBuilder.Entity<SatisDetaylari>(entity =>
        {
            entity.HasKey(e => e.SatisDetayId);

            entity.ToTable("Satis_Detaylari");

            entity.Property(e => e.SatisDetayId).HasColumnName("SatisDetayID");
            entity.Property(e => e.BitkiId).HasColumnName("BitkiID");
            entity.Property(e => e.SatisId).HasColumnName("SatisID");

            entity.HasOne(d => d.Bitki).WithMany(p => p.SatisDetaylaris)
                .HasForeignKey(d => d.BitkiId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Satis).WithMany(p => p.SatisDetaylaris).HasForeignKey(d => d.SatisId);
        });

        modelBuilder.Entity<Satislar>(entity =>
        {
            entity.HasKey(e => e.SatisId);

            entity.ToTable("Satislar");

            entity.HasIndex(e => e.MakbuzNo, "IX_Satislar_MakbuzNo").IsUnique();

            entity.Property(e => e.SatisId).HasColumnName("SatisID");
            entity.Property(e => e.KullaniciId).HasColumnName("KullaniciID");
            entity.Property(e => e.SatisTarihi).IsRequired();

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Satislars)
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
