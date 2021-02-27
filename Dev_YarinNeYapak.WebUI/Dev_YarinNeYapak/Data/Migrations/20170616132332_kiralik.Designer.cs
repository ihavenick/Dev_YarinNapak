using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Dev_YarinNeYapak.Data;

namespace Dev_YarinNeYapak.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170616132332_kiralik")]
    partial class kiralik
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dev_YarinNeYapak.Controllers.KiralananTarih", b =>
                {
                    b.Property<int>("KiralananTarihId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Baslangic");

                    b.Property<DateTime>("Bitis");

                    b.Property<int?>("KiralikEvlerId");

                    b.HasKey("KiralananTarihId");

                    b.HasIndex("KiralikEvlerId");

                    b.ToTable("KiralananTarih");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Controllers.KiralikEvler", b =>
                {
                    b.Property<int>("KiralikEvlerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("KiralikEvAdi");

                    b.HasKey("KiralikEvlerId");

                    b.ToTable("KiralikEvler");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Data.Etkinlik", b =>
                {
                    b.Property<int>("EtkinlikID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EtkinlikAdi");

                    b.Property<DateTime>("NeZaman");

                    b.Property<int?>("YerID");

                    b.HasKey("EtkinlikID");

                    b.HasIndex("YerID");

                    b.ToTable("Etkinlikler");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Data.Yer", b =>
                {
                    b.Property<int>("YerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("YerAdi");

                    b.HasKey("YerID");

                    b.ToTable("Yerler");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarBase64");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int?>("EtkinlikID");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("adsoyad");

                    b.Property<string>("facebookid");

                    b.Property<bool>("facelekayitolmus");

                    b.HasKey("Id");

                    b.HasIndex("EtkinlikID");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Controllers.KiralananTarih", b =>
                {
                    b.HasOne("Dev_YarinNeYapak.Controllers.KiralikEvler")
                        .WithMany("NezamanKiralanmis")
                        .HasForeignKey("KiralikEvlerId");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Data.Etkinlik", b =>
                {
                    b.HasOne("Dev_YarinNeYapak.Data.Yer")
                        .WithMany("YerinEtkinliği")
                        .HasForeignKey("YerID");
                });

            modelBuilder.Entity("Dev_YarinNeYapak.Models.ApplicationUser", b =>
                {
                    b.HasOne("Dev_YarinNeYapak.Data.Etkinlik")
                        .WithMany("EtkinliğeKatılanKullanıcılar")
                        .HasForeignKey("EtkinlikID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Dev_YarinNeYapak.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Dev_YarinNeYapak.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dev_YarinNeYapak.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
