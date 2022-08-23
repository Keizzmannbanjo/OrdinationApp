﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrdinationApp.Data;

#nullable disable

namespace OrdinationApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "2141d30e-f5f2-4494-a683-ab9224610844",
                            ConcurrencyStamp = "0dc82e8d-de6e-443b-8bc0-8bc20249a0c7",
                            Name = "Coder",
                            NormalizedName = "CODER"
                        },
                        new
                        {
                            Id = "d922965c-50e2-4533-ba7d-67b6964d2297",
                            ConcurrencyStamp = "a99b154d-2519-4de5-8616-5431e7102bce",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OrdinationApp.Models.CMC", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("CMCs");

                    b.HasData(
                        new
                        {
                            Name = "CMC 1"
                        },
                        new
                        {
                            Name = "CMC 2"
                        },
                        new
                        {
                            Name = "CMC 3"
                        },
                        new
                        {
                            Name = "CMC 4"
                        },
                        new
                        {
                            Name = "CMC 5"
                        },
                        new
                        {
                            Name = "CMC 6"
                        },
                        new
                        {
                            Name = "CMC 7"
                        },
                        new
                        {
                            Name = "CMC 8"
                        },
                        new
                        {
                            Name = "CMC 9"
                        },
                        new
                        {
                            Name = "CMC 10"
                        },
                        new
                        {
                            Name = "CMC 11"
                        },
                        new
                        {
                            Name = "CMC 12"
                        });
                });

            modelBuilder.Entity("OrdinationApp.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CurrentRankTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentRankYear")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberShipId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrdinationYear")
                        .HasColumnType("int");

                    b.Property<string>("Othername")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentRecordId")
                        .HasColumnType("int");

                    b.Property<string>("ProvinceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetRankTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentRankTitle");

                    b.HasIndex("PaymentRecordId");

                    b.HasIndex("ProvinceName");

                    b.HasIndex("TargetRankTitle");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("OrdinationApp.Models.OrdinationBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("IronStaffPrice")
                        .HasColumnType("money");

                    b.Property<decimal>("OrdinationFee")
                        .HasColumnType("money");

                    b.Property<string>("RankTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TrainingFee")
                        .HasColumnType("money");

                    b.Property<decimal>("WoodenStaffPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("OrdinationBills");
                });

            modelBuilder.Entity("OrdinationApp.Models.PaymentRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<string>("MembershipId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentYear")
                        .HasColumnType("int");

                    b.Property<string>("RankTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TallyNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("RankTitle");

                    b.ToTable("PaymentRecords");
                });

            modelBuilder.Entity("OrdinationApp.Models.Province", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CmcName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.HasIndex("CmcName");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("OrdinationApp.Models.Rank", b =>
                {
                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RankTitle")
                        .HasColumnType("int");

                    b.HasKey("Title");

                    b.HasIndex("RankTitle")
                        .IsUnique()
                        .HasFilter("[RankTitle] IS NOT NULL");

                    b.ToTable("Ranks");

                    b.HasData(
                        new
                        {
                            Title = "Brother",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Sister",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Army Of Salvation",
                            Gender = "Both"
                        },
                        new
                        {
                            Title = "Army Of Christ",
                            Gender = "Both"
                        },
                        new
                        {
                            Title = "Aladura",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Lady Aladura",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Leader",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Lady Leader",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Rabbi",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Dorcas",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Pastor",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Deborah",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Evangelist",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Mary",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Apostle",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Prophetess",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Supervising Apostle",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Mother In Israel",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Senior Apostle",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Senior Mother In Israel",
                            Gender = "Female"
                        },
                        new
                        {
                            Title = "Special Senior Apostle",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Apostle General",
                            Gender = "Male"
                        },
                        new
                        {
                            Title = "Supervising Apostle General",
                            Gender = "Male"
                        });
                });

            modelBuilder.Entity("OrdinationApp.Models.TrackerUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("TrackerUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrdinationApp.Models.Member", b =>
                {
                    b.HasOne("OrdinationApp.Models.Rank", "CurrentRank")
                        .WithMany("CurrentMembers")
                        .HasForeignKey("CurrentRankTitle")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("OrdinationApp.Models.PaymentRecord", "PaymentRecord")
                        .WithMany()
                        .HasForeignKey("PaymentRecordId");

                    b.HasOne("OrdinationApp.Models.Province", "Province")
                        .WithMany("Members")
                        .HasForeignKey("ProvinceName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrdinationApp.Models.Rank", "TargetRank")
                        .WithMany("TargetMembers")
                        .HasForeignKey("TargetRankTitle")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CurrentRank");

                    b.Navigation("PaymentRecord");

                    b.Navigation("Province");

                    b.Navigation("TargetRank");
                });

            modelBuilder.Entity("OrdinationApp.Models.PaymentRecord", b =>
                {
                    b.HasOne("OrdinationApp.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrdinationApp.Models.Rank", "Rank")
                        .WithMany("RankPayments")
                        .HasForeignKey("RankTitle")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("OrdinationApp.Models.Province", b =>
                {
                    b.HasOne("OrdinationApp.Models.CMC", "CMC")
                        .WithMany("Provinces")
                        .HasForeignKey("CmcName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CMC");
                });

            modelBuilder.Entity("OrdinationApp.Models.Rank", b =>
                {
                    b.HasOne("OrdinationApp.Models.OrdinationBill", "Bill")
                        .WithOne("Rank")
                        .HasForeignKey("OrdinationApp.Models.Rank", "RankTitle");

                    b.Navigation("Bill");
                });

            modelBuilder.Entity("OrdinationApp.Models.CMC", b =>
                {
                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("OrdinationApp.Models.OrdinationBill", b =>
                {
                    b.Navigation("Rank")
                        .IsRequired();
                });

            modelBuilder.Entity("OrdinationApp.Models.Province", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("OrdinationApp.Models.Rank", b =>
                {
                    b.Navigation("CurrentMembers");

                    b.Navigation("RankPayments");

                    b.Navigation("TargetMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
