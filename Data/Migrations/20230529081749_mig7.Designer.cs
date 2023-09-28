﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using outreach3.Data;

#nullable disable

namespace outreach3.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230529081749_mig7")]
    partial class mig7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("outreach3.Data.Aquaintance", b =>
                {
                    b.Property<int>("AquaintanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AquaintanceId"));

                    b.Property<DateTime>("AcquaintanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eight")
                        .HasColumnType("bit");

                    b.Property<bool>("Five")
                        .HasColumnType("bit");

                    b.Property<bool>("Four")
                        .HasColumnType("bit");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Nine")
                        .HasColumnType("bit");

                    b.Property<bool>("One")
                        .HasColumnType("bit");

                    b.Property<string>("PrayerHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrayerNeeds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestBusService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestPrayerRequestId")
                        .HasColumnType("int");

                    b.Property<int>("ResidentId")
                        .HasColumnType("int");

                    b.Property<bool>("Seven")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Six")
                        .HasColumnType("bit");

                    b.Property<bool>("Ten")
                        .HasColumnType("bit");

                    b.Property<bool>("Three")
                        .HasColumnType("bit");

                    b.Property<bool>("Two")
                        .HasColumnType("bit");

                    b.HasKey("AquaintanceId");

                    b.HasIndex("RequestPrayerRequestId");

                    b.HasIndex("ResidentId");

                    b.ToTable("Aquaintance");
                });

            modelBuilder.Entity("outreach3.Data.ChurchMember", b =>
                {
                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("ChurchId")
                        .HasColumnType("int");

                    b.HasKey("MemberId", "ChurchId");

                    b.HasIndex("ChurchId");

                    b.ToTable("ChurchMembers");
                });

            modelBuilder.Entity("outreach3.Data.Churches", b =>
                {
                    b.Property<int>("ChurchesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChurchesId"));

                    b.Property<string>("ChurchAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChurchFullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChurchPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PastorsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChurchesId");

                    b.ToTable("Churches");
                });

            modelBuilder.Entity("outreach3.Data.MapMarker", b =>
                {
                    b.Property<int>("MapMarkerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MapMarkerId"));

                    b.Property<int?>("MissionMapId")
                        .HasColumnType("int");

                    b.Property<string>("color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("markerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("markerArea")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("markerIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("markerLatLng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("number")
                        .HasColumnType("int");

                    b.HasKey("MapMarkerId");

                    b.HasIndex("MissionMapId");

                    b.ToTable("MapMarker");
                });

            modelBuilder.Entity("outreach3.Data.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<int?>("ChurchesId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.HasIndex("ChurchesId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("outreach3.Data.Mission", b =>
                {
                    b.Property<int>("MissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MissionId"));

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateLastCompleted")
                        .HasColumnType("date");

                    b.Property<int>("MissionMapId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MissionId");

                    b.HasIndex("MissionMapId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("outreach3.Data.MissionHistory", b =>
                {
                    b.Property<int>("MissionHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MissionHistoryId"));

                    b.Property<DateTime?>("AssignedDate")
                        .HasColumnType("date");

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("date");

                    b.Property<int>("MissionId")
                        .HasColumnType("int");

                    b.HasKey("MissionHistoryId");

                    b.HasIndex("MissionId");

                    b.ToTable("MissionHistory");
                });

            modelBuilder.Entity("outreach3.Data.MissionMap", b =>
                {
                    b.Property<int>("MissionMapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MissionMapId"));

                    b.Property<string>("MapData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapHeading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapTilt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapZoom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MissionMapId");

                    b.ToTable("MissionMaps");
                });

            modelBuilder.Entity("outreach3.Data.PrayerFollowUp", b =>
                {
                    b.Property<int>("PrayerFollowUpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrayerFollowUpId"));

                    b.Property<DateTime>("FollowUpDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FollowUpText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrayerRequestId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrayerFollowUpId");

                    b.HasIndex("PrayerRequestId");

                    b.ToTable("PrayerFollowUp");
                });

            modelBuilder.Entity("outreach3.Data.PrayerRequest", b =>
                {
                    b.Property<int>("PrayerRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrayerRequestId"));

                    b.Property<int>("AcquaintanceId")
                        .HasColumnType("int");

                    b.Property<int>("MinistryId")
                        .HasColumnType("int");

                    b.Property<string>("Request")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ResidentId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrayerRequestId");

                    b.ToTable("PrayerRequest");
                });

            modelBuilder.Entity("outreach3.Data.Resident", b =>
                {
                    b.Property<int>("ResidentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResidentId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMember")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProtected")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MapId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MarkerColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MissionId")
                        .HasColumnType("int");

                    b.Property<int>("number")
                        .HasColumnType("int");

                    b.HasKey("ResidentId");

                    b.HasIndex("MissionId");

                    b.ToTable("Resident");
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

            modelBuilder.Entity("outreach3.Data.Aquaintance", b =>
                {
                    b.HasOne("outreach3.Data.PrayerRequest", "Request")
                        .WithMany()
                        .HasForeignKey("RequestPrayerRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("outreach3.Data.Resident", "Resident")
                        .WithMany("Aquaintances")
                        .HasForeignKey("ResidentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Resident");
                });

            modelBuilder.Entity("outreach3.Data.ChurchMember", b =>
                {
                    b.HasOne("outreach3.Data.Churches", "Church")
                        .WithMany("ChurchMembers")
                        .HasForeignKey("ChurchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("outreach3.Data.Member", "Member")
                        .WithMany("ChurchMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Church");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("outreach3.Data.MapMarker", b =>
                {
                    b.HasOne("outreach3.Data.MissionMap", null)
                        .WithMany("MapMarkers")
                        .HasForeignKey("MissionMapId");
                });

            modelBuilder.Entity("outreach3.Data.Member", b =>
                {
                    b.HasOne("outreach3.Data.Churches", null)
                        .WithMany("Members")
                        .HasForeignKey("ChurchesId");
                });

            modelBuilder.Entity("outreach3.Data.Mission", b =>
                {
                    b.HasOne("outreach3.Data.MissionMap", "MissionMap")
                        .WithMany()
                        .HasForeignKey("MissionMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MissionMap");
                });

            modelBuilder.Entity("outreach3.Data.MissionHistory", b =>
                {
                    b.HasOne("outreach3.Data.Mission", null)
                        .WithMany("MissionHistories")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("outreach3.Data.PrayerFollowUp", b =>
                {
                    b.HasOne("outreach3.Data.PrayerRequest", null)
                        .WithMany("FollowUps")
                        .HasForeignKey("PrayerRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("outreach3.Data.Resident", b =>
                {
                    b.HasOne("outreach3.Data.Mission", null)
                        .WithMany("Residents")
                        .HasForeignKey("MissionId");
                });

            modelBuilder.Entity("outreach3.Data.Churches", b =>
                {
                    b.Navigation("ChurchMembers");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("outreach3.Data.Member", b =>
                {
                    b.Navigation("ChurchMembers");
                });

            modelBuilder.Entity("outreach3.Data.Mission", b =>
                {
                    b.Navigation("MissionHistories");

                    b.Navigation("Residents");
                });

            modelBuilder.Entity("outreach3.Data.MissionMap", b =>
                {
                    b.Navigation("MapMarkers");
                });

            modelBuilder.Entity("outreach3.Data.PrayerRequest", b =>
                {
                    b.Navigation("FollowUps");
                });

            modelBuilder.Entity("outreach3.Data.Resident", b =>
                {
                    b.Navigation("Aquaintances");
                });
#pragma warning restore 612, 618
        }
    }
}
