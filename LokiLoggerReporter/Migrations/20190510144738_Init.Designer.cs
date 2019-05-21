﻿// <auto-generated />
using System;
using LokiLoggerReporter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LokiLoggerReporter.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190510144738_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("LokiLoggerReporter.Models.Log", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Class");

                    b.Property<string>("Data");

                    b.Property<string>("Exception");

                    b.Property<int>("Line");

                    b.Property<int>("LogLevel");

                    b.Property<int>("LogTyp");

                    b.Property<string>("Message");

                    b.Property<string>("Method");

                    b.Property<string>("Name");

                    b.Property<DateTime>("Time");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
