﻿// <auto-generated />
using System;
using ClientSVH.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClientSVH.DataAccess.Migrations
{
    [DbContext(typeof(ClientSVHDbContext))]
    partial class ClientSVHDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.DocumentEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("did")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("DocDate")
                        .HasMaxLength(5)
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("docdate");

                    b.Property<Guid>("DocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Guid")
                        .HasColumnName("docid");

                    b.Property<Guid?>("DocRecordId")
                        .HasColumnType("uuid");

                    b.Property<string>("IdSha256")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("idsha256");

                    b.Property<string>("Idmd5")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("idmd5");

                    b.Property<string>("ModeCode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("modecode");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modify_date");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("number");

                    b.Property<int>("Pid")
                        .HasColumnType("integer")
                        .HasColumnName("pid");

                    b.Property<int>("SizeDoc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("size_doc");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SizeDoc"));

                    b.HasKey("Id");

                    b.HasIndex("DocRecordId");

                    b.ToTable("documents", (string)null);
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.PackageEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("pid");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modify_date");

                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("status");

                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Guid")
                        .HasColumnName("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("Guid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("packages", (string)null);
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.StatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("MkRes")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("mkres");

                    b.Property<int>("NewSt")
                        .HasColumnType("integer");

                    b.Property<int>("OldSt")
                        .HasColumnType("integer");

                    b.Property<bool>("RunWf")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("runwf");

                    b.Property<bool>("SendMess")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("sendmess");

                    b.Property<string>("StatusName")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("stname");

                    b.HasKey("Id");

                    b.HasIndex("OldSt")
                        .IsUnique();

                    b.ToTable("pkg_status", (string)null);
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.StatusGraphEntity", b =>
                {
                    b.Property<int>("OldSt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("oldst");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OldSt"));

                    b.Property<int>("NewSt")
                        .HasColumnType("integer")
                        .HasColumnName("newst");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("OldSt");

                    b.ToTable("pkg_status_graph", (string)null);
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Guid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Hidden")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClientSVH.DocsBodyDataAccess.Entities.DocRecordEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocId")
                        .HasColumnType("uuid");

                    b.Property<string>("DocText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("DocRecordEntity");
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.DocumentEntity", b =>
                {
                    b.HasOne("ClientSVH.DocsBodyDataAccess.Entities.DocRecordEntity", "DocRecord")
                        .WithMany()
                        .HasForeignKey("DocRecordId");

                    b.HasOne("ClientSVH.DataAccess.Entities.PackageEntity", "Package")
                        .WithMany("Documents")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocRecord");

                    b.Navigation("Package");
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.PackageEntity", b =>
                {
                    b.HasOne("ClientSVH.DataAccess.Entities.StatusEntity", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClientSVH.DataAccess.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.StatusEntity", b =>
                {
                    b.HasOne("ClientSVH.DataAccess.Entities.StatusGraphEntity", "StatusGraph")
                        .WithOne("Status")
                        .HasForeignKey("ClientSVH.DataAccess.Entities.StatusEntity", "OldSt")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusGraph");
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.PackageEntity", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("ClientSVH.DataAccess.Entities.StatusGraphEntity", b =>
                {
                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
