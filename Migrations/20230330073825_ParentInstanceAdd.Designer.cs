﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using gantt_backend.Data.DBContext;
using gantt_backend.Data.Models.Constructor;

#nullable disable

namespace gantt_backend.Migrations
{
    [DbContext(typeof(GanttContext))]
    [Migration("20230330073825_ParentInstanceAdd")]
    partial class ParentInstanceAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("gantt_backend.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Delay")
                        .HasColumnType("integer");

                    b.Property<int?>("Duration")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Mode")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Unit")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Decsription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.EntityFields", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("FieldId");

                    b.ToTable("EntityFields");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Decsription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<TypeJson>("Type")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Instance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Decsription")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("ParentInstanceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("ParentInstanceId");

                    b.ToTable("Instances");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Value", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool?>("BoolData")
                        .HasColumnType("boolean");

                    b.Property<Guid>("FieldId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid");

                    b.Property<decimal?>("NumericData")
                        .HasColumnType("numeric(30,6)");

                    b.Property<Guid?>("ParentValueId")
                        .HasColumnType("uuid");

                    b.Property<string>("TextData")
                        .HasColumnType("varchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("InstanceId");

                    b.HasIndex("ParentValueId");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SourceTaskId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TargetTaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SourceTaskId");

                    b.HasIndex("TargetTaskId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BaseEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("BaseStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Holder")
                        .HasColumnType("text");

                    b.Property<bool?>("Open")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentTaskId")
                        .HasColumnType("uuid");

                    b.Property<string>("Priority")
                        .HasColumnType("text");

                    b.Property<decimal>("Progress")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentTaskId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Assignment", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Task", "Task")
                        .WithMany("Resources")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.EntityFields", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Constructor.Entity", "Entity")
                        .WithMany("EntityField")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gantt_backend.Data.Models.Constructor.Field", "Field")
                        .WithMany("EntityField")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Instance", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Constructor.Entity", "Entity")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gantt_backend.Data.Models.Constructor.Instance", "ParentInstance")
                        .WithMany("SubInstances")
                        .HasForeignKey("ParentInstanceId");

                    b.Navigation("Entity");

                    b.Navigation("ParentInstance");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Value", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Constructor.Field", "Field")
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gantt_backend.Data.Models.Constructor.Instance", "Instance")
                        .WithMany()
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gantt_backend.Data.Models.Constructor.Value", "ParentValue")
                        .WithMany("SubValues")
                        .HasForeignKey("ParentValueId");

                    b.Navigation("Field");

                    b.Navigation("Instance");

                    b.Navigation("ParentValue");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Link", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Task", "SourceTask")
                        .WithMany()
                        .HasForeignKey("SourceTaskId");

                    b.HasOne("gantt_backend.Data.Models.Task", "TargetTask")
                        .WithMany()
                        .HasForeignKey("TargetTaskId");

                    b.Navigation("SourceTask");

                    b.Navigation("TargetTask");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Task", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.Task", "ParentTask")
                        .WithMany("SubTasks")
                        .HasForeignKey("ParentTaskId");

                    b.Navigation("ParentTask");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("gantt_backend.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("gantt_backend.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Entity", b =>
                {
                    b.Navigation("EntityField");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Field", b =>
                {
                    b.Navigation("EntityField");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Instance", b =>
                {
                    b.Navigation("SubInstances");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Constructor.Value", b =>
                {
                    b.Navigation("SubValues");
                });

            modelBuilder.Entity("gantt_backend.Data.Models.Task", b =>
                {
                    b.Navigation("Resources");

                    b.Navigation("SubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
