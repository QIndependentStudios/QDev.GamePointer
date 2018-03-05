﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using QDev.GamePointer.Model;
using QDev.GamePointer.Persist;
using System;

namespace QDev.GamePointer.Persist.Migrations
{
    [DbContext(typeof(WatchedExecutionContext))]
    [Migration("20180305182752_InitialCreation")]
    partial class InitialCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("QDev.GamePointer.Model.WatchedExecution", b =>
                {
                    b.Property<int>("WatchedExecutionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExecutionType");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Path");

                    b.HasKey("WatchedExecutionId");

                    b.ToTable("WatchedExecutions");
                });
#pragma warning restore 612, 618
        }
    }
}
