﻿// <auto-generated />
using System;
using AgileRap_Process.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgileRap_Process.Migrations
{
    [DbContext(typeof(AgileRap_ProcessContext))]
    [Migration("20240124101616_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgileRap_Process.Models.Provider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("WorkID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("WorkID");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("AgileRap_Process.Models.ProviderLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("WorkLogID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("WorkLogID");

                    b.ToTable("ProviderLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Status", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("AgileRap_Process.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LineID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Work", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HeaderID")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Project")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("StatusID");

                    b.ToTable("Work");
                });

            modelBuilder.Entity("AgileRap_Process.Models.WorkLog", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("No")
                        .HasColumnType("int");

                    b.Property<string>("Project")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusID")
                        .HasColumnType("int");

                    b.Property<int?>("UpdateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("StatusID");

                    b.HasIndex("WorkID");

                    b.ToTable("WorkLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Provider", b =>
                {
                    b.HasOne("AgileRap_Process.Models.User", "User")
                        .WithMany("Provider")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgileRap_Process.Models.Work", "Work")
                        .WithMany("Provider")
                        .HasForeignKey("WorkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("AgileRap_Process.Models.ProviderLog", b =>
                {
                    b.HasOne("AgileRap_Process.Models.User", "User")
                        .WithMany("ProviderLog")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgileRap_Process.Models.WorkLog", "WorkLog")
                        .WithMany("ProviderLog")
                        .HasForeignKey("WorkLogID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Work", b =>
                {
                    b.HasOne("AgileRap_Process.Models.Status", "Status")
                        .WithMany("Work")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("AgileRap_Process.Models.WorkLog", b =>
                {
                    b.HasOne("AgileRap_Process.Models.Status", "Status")
                        .WithMany("WorkLog")
                        .HasForeignKey("StatusID");

                    b.HasOne("AgileRap_Process.Models.Work", "Work")
                        .WithMany("WorkLog")
                        .HasForeignKey("WorkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Status", b =>
                {
                    b.Navigation("Work");

                    b.Navigation("WorkLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.User", b =>
                {
                    b.Navigation("Provider");

                    b.Navigation("ProviderLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.Work", b =>
                {
                    b.Navigation("Provider");

                    b.Navigation("WorkLog");
                });

            modelBuilder.Entity("AgileRap_Process.Models.WorkLog", b =>
                {
                    b.Navigation("ProviderLog");
                });
#pragma warning restore 612, 618
        }
    }
}
