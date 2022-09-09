﻿// <auto-generated />
using System;
using CSServerDemo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CSServerDemo.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20220909022031_Adjust")]
    partial class Adjust
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("CSServerDemo.Models.UserAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("account");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("create_at");

                    b.Property<long?>("CreateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("create_by");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("datetime")
                        .HasColumnName("last_login_at");

                    b.Property<string>("Nickname")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("nickname");

                    b.Property<string>("Password")
                        .HasColumnType("CHAR(64)")
                        .HasColumnName("password");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("datetime")
                        .HasColumnName("update_at");

                    b.Property<long?>("UpdateBy")
                        .HasColumnType("bigint")
                        .HasColumnName("update_by");

                    b.HasKey("Id");

                    b.ToTable("cs_user_account");
                });
#pragma warning restore 612, 618
        }
    }
}
