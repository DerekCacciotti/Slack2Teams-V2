﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Slack2Teams.Data;

#nullable disable

namespace Slack2Teams.Data.Migrations
{
    [DbContext(typeof(Slack2TeamsContext))]
    partial class Slack2TeamsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Slack2Teams.Data.Models.SlackMessageType", b =>
                {
                    b.Property<Guid>("SlackMessageTypePK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SlackMessageTypePK");

                    b.ToTable("SlackMessageType");
                });

            modelBuilder.Entity("Slack2Teams.Data.Models.StagedSlackChannel", b =>
                {
                    b.Property<Guid>("SlackChannelPK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<string>("ChannelDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(4);

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Editor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SlackCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SlackCreator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<Guid>("TenantFK")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<bool?>("isArchived")
                        .HasColumnType("bit");

                    b.Property<bool?>("isPrivate")
                        .HasColumnType("bit");

                    b.HasKey("SlackChannelPK");

                    b.ToTable("SlackChannels");
                });

            modelBuilder.Entity("Slack2Teams.Data.Models.StagedSlackMessage", b =>
                {
                    b.Property<Guid>("SlackMessagePK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("ChannelFK")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Editor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MesaageText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(2);

                    b.Property<DateTime?>("SlackCreateDate")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(4);

                    b.Property<Guid>("SlackMessageTypeFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SlackTimeStamp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnOrder(3);

                    b.HasKey("SlackMessagePK");

                    b.ToTable("SlackMessages");
                });

            modelBuilder.Entity("Slack2Teams.Data.Models.Tenant", b =>
                {
                    b.Property<Guid>("TenantPK")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Editor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserFK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TenantPK");

                    b.ToTable("Tenant");
                });
#pragma warning restore 612, 618
        }
    }
}
