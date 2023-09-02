using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace e_okul.Models;

public partial class OkulContext : DbContext
{
    public OkulContext()
    {
    }

    public OkulContext(DbContextOptions<OkulContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bolum> Bolums { get; set; }

    public virtual DbSet<Der> Ders { get; set; }

    public virtual DbSet<Donem> Donems { get; set; }

    public virtual DbSet<Harf> Harves { get; set; }

    public virtual DbSet<Notlar> Notlars { get; set; }

    public virtual DbSet<Ogrenci> Ogrencis { get; set; }

    public virtual DbSet<Ogretman> Ogretmen { get; set; }

    public virtual DbSet<SinifNotOrt> SinifNotOrts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=UMUT;Database=Okul;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bolum>(entity =>
        {
            entity.ToTable("bolum");

            entity.Property(e => e.BolumId).HasColumnName("bolum_id");
            entity.Property(e => e.BolumAdi)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("bolum_adi");
        });

        modelBuilder.Entity<Der>(entity =>
        {
            entity.HasKey(e => e.DersId);

            entity.ToTable("ders");

            entity.Property(e => e.DersId).HasColumnName("ders_id");
            entity.Property(e => e.Akts).HasColumnName("akts");
            entity.Property(e => e.DersAdi)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("ders_adi");
            entity.Property(e => e.HaftalikSaati).HasColumnName("haftalik_saati");
            entity.Property(e => e.Kredi).HasColumnName("kredi");
            entity.Property(e => e.OgretmenId).HasColumnName("ogretmen_id");

            entity.HasOne(d => d.Ogretmen).WithMany(p => p.Ders)
                .HasForeignKey(d => d.OgretmenId)
                .HasConstraintName("FK_ders_ogretmen");
        });

        modelBuilder.Entity<Donem>(entity =>
        {
            entity.ToTable("donem");

            entity.Property(e => e.DonemId).HasColumnName("donem_id");
            entity.Property(e => e.Dno).HasColumnName("dno");
            entity.Property(e => e.DonemSayisi).HasColumnName("donem_sayisi");
            entity.Property(e => e.OgrId).HasColumnName("ogr_id");
            entity.Property(e => e.TopAkts).HasColumnName("top_akts");
            entity.Property(e => e.TopDers).HasColumnName("top_ders");

            entity.HasOne(d => d.Ogr).WithMany(p => p.Donems)
                .HasForeignKey(d => d.OgrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_donem_ogrenci");
        });

        modelBuilder.Entity<Harf>(entity =>
        {
            entity.ToTable("harf");

            entity.Property(e => e.HarfId).HasColumnName("harf_id");
            entity.Property(e => e.Harf1)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("harf");
            entity.Property(e => e.Katsayi).HasColumnName("katsayi");
            entity.Property(e => e.Ortalama).HasColumnName("ortalama");
        });

        modelBuilder.Entity<Notlar>(entity =>
        {
            entity.HasKey(e => e.NotId);

            entity.ToTable("notlar");

            entity.Property(e => e.NotId).HasColumnName("not_id");
            entity.Property(e => e.AraSinav).HasColumnName("ara_sinav");
            entity.Property(e => e.DersId).HasColumnName("ders_id");
            entity.Property(e => e.Final).HasColumnName("final");
            entity.Property(e => e.HarfId).HasColumnName("harf_id");
            entity.Property(e => e.OgrId).HasColumnName("ogr_id");
            entity.Property(e => e.OgretmenId).HasColumnName("ogretmen_id");
            entity.Property(e => e.Ortalama).HasColumnName("ortalama");

            entity.HasOne(d => d.Ders).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.DersId)
                .HasConstraintName("FK_notlar_ders");

            entity.HasOne(d => d.Harf).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.HarfId)
                .HasConstraintName("FK_notlar_harf");

            entity.HasOne(d => d.Ogr).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.OgrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_notlar_ogrenci");

            entity.HasOne(d => d.Ogretmen).WithMany(p => p.Notlars)
                .HasForeignKey(d => d.OgretmenId)
                .HasConstraintName("FK_notlar_ogretmen");
        });

        modelBuilder.Entity<Ogrenci>(entity =>
        {
            entity.HasKey(e => e.OgrId);

            entity.ToTable("ogrenci");

            entity.Property(e => e.OgrId).HasColumnName("ogr_id");
            entity.Property(e => e.Gno).HasColumnName("gno");
            entity.Property(e => e.OgrAdi)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("ogr_adi");
            entity.Property(e => e.OgrBolumId).HasColumnName("ogr_bolum_id");
            entity.Property(e => e.OgrMail)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("ogr_mail");
            entity.Property(e => e.OgrNo).HasColumnName("ogr_no");
            entity.Property(e => e.OgrSoyadi)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("ogr_soyadi");

            entity.HasOne(d => d.OgrBolum).WithMany(p => p.Ogrencis)
                .HasForeignKey(d => d.OgrBolumId)
                .HasConstraintName("FK_ogrenci_bolum");
        });

        modelBuilder.Entity<Ogretman>(entity =>
        {
            entity.HasKey(e => e.OgretmenId);

            entity.ToTable("ogretmen");

            entity.Property(e => e.OgretmenId).HasColumnName("ogretmen_id");
            entity.Property(e => e.Adi)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("adi");
            entity.Property(e => e.Soyadi)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("soyadi");
        });

        modelBuilder.Entity<SinifNotOrt>(entity =>
        {
            entity.HasKey(e => e.OrtId);

            entity.ToTable("sinif_not_ort");

            entity.Property(e => e.OrtId).HasColumnName("ort_id");
            entity.Property(e => e.BolumId).HasColumnName("bolum_id");
            entity.Property(e => e.DersId).HasColumnName("ders_id");
            entity.Property(e => e.SinifOrt).HasColumnName("sinif_ort");

            entity.HasOne(d => d.Bolum).WithMany(p => p.SinifNotOrts)
                .HasForeignKey(d => d.BolumId)
                .HasConstraintName("FK_sinif_not_ort_bolum");

            entity.HasOne(d => d.Ders).WithMany(p => p.SinifNotOrts)
                .HasForeignKey(d => d.DersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sinif_not_ort_ders");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
