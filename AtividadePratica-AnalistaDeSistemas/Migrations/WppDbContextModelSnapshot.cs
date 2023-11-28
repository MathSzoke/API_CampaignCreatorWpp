﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WppCampaign.Migrations
{
    [DbContext(typeof(WppDbContext))]
    partial class WppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.CommunityData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CommunityID")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("CommunityID");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Name");

                    b.HasKey("ID");

                    b.HasIndex("CommunityID");

                    b.ToTable("CommunityData", "WppCampaign");
                });

            modelBuilder.Entity("Models.LogData", b =>
                {
                    b.Property<int>("LogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LogID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogID"));

                    b.Property<bool>("IsReadMessage")
                        .HasColumnType("bit")
                        .HasColumnName("IsReadMessage");

                    b.Property<bool>("IsReceivedMessage")
                        .HasColumnType("bit")
                        .HasColumnName("IsReceivedMessage");

                    b.Property<string>("MessageID")
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("MessageID");

                    b.Property<string>("MessageSent")
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("MessageSent");

                    b.Property<string>("ResponseMessage")
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("ResponseMessage");

                    b.Property<short?>("StatusCode")
                        .HasColumnType("smallint")
                        .HasColumnName("StatusCode");

                    b.HasKey("LogID");

                    b.ToTable("LogData", "WppCampaign");
                });

            modelBuilder.Entity("Models.MemberGroup", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MemberID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberID"));

                    b.Property<int>("GroupID")
                        .HasColumnType("int")
                        .HasColumnName("GroupID");

                    b.Property<bool>("IsMember")
                        .HasColumnType("bit")
                        .HasColumnName("IsMember");

                    b.Property<string>("Phones")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Phones");

                    b.HasKey("MemberID");

                    b.HasIndex("GroupID");

                    b.ToTable("MemberGroup", "WppCampaign");
                });

            modelBuilder.Entity("Models.MessageData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("IsActive");

                    b.Property<bool>("IsPollMessage")
                        .HasColumnType("bit")
                        .HasColumnName("IsPollMessage");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("MessageText");

                    b.HasKey("ID");

                    b.HasIndex("MessageText");

                    b.ToTable("MessageData", "WppCampaign");
                });

            modelBuilder.Entity("Models.PollOptions", b =>
                {
                    b.Property<int>("PollOptionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PollOptionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PollOptionID"));

                    b.Property<int>("PollID")
                        .HasColumnType("int")
                        .HasColumnName("PollID");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("OptionName");

                    b.HasKey("PollOptionID");

                    b.HasIndex("PollID");

                    b.HasIndex("name");

                    b.ToTable("PollOptions", "WppCampaign");
                });

            modelBuilder.Entity("Models.SendPoll", b =>
                {
                    b.Property<int>("PollID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PollID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PollID"));

                    b.Property<int>("MessageID")
                        .HasColumnType("int")
                        .HasColumnName("MessageID");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Phone");

                    b.Property<int?>("pollMaxOptions")
                        .HasColumnType("int")
                        .HasColumnName("PollMaxOptions");

                    b.HasKey("PollID");

                    b.HasIndex("MessageID")
                        .IsUnique();

                    b.ToTable("SendPoll", "WppCampaign");
                });

            modelBuilder.Entity("Models.WppGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CommunityID")
                        .HasColumnType("int")
                        .HasColumnName("CommunityID");

                    b.Property<string>("GroupID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("GroupID");

                    b.Property<string>("InvitationLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("InvitationLink");

                    b.Property<bool>("IsGroupAnnouncement")
                        .HasColumnType("bit")
                        .HasColumnName("IsGroupAnnouncement");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("ID");

                    b.HasIndex("CommunityID");

                    b.HasIndex("Name");

                    b.ToTable("WppGroup", "WppCampaign");
                });

            modelBuilder.Entity("Models.MemberGroup", b =>
                {
                    b.HasOne("Models.WppGroup", "Group")
                        .WithMany("MemberGroup")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Models.PollOptions", b =>
                {
                    b.HasOne("Models.SendPoll", null)
                        .WithMany("PollOptions")
                        .HasForeignKey("PollID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.SendPoll", b =>
                {
                    b.HasOne("Models.MessageData", "MessageSettings")
                        .WithOne("SendPoll")
                        .HasForeignKey("Models.SendPoll", "MessageID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MessageSettings");
                });

            modelBuilder.Entity("Models.WppGroup", b =>
                {
                    b.HasOne("Models.CommunityData", "CommunityData")
                        .WithMany("WppGroups")
                        .HasForeignKey("CommunityID");

                    b.Navigation("CommunityData");
                });

            modelBuilder.Entity("Models.CommunityData", b =>
                {
                    b.Navigation("WppGroups");
                });

            modelBuilder.Entity("Models.MessageData", b =>
                {
                    b.Navigation("SendPoll");
                });

            modelBuilder.Entity("Models.SendPoll", b =>
                {
                    b.Navigation("PollOptions");
                });

            modelBuilder.Entity("Models.WppGroup", b =>
                {
                    b.Navigation("MemberGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
