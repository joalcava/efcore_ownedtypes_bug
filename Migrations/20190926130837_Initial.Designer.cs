﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using owned_bug;

namespace owned_bug.Migrations
{
    [DbContext(typeof(TasksContext))]
    [Migration("20190926130837_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("owned_bug.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("owned_bug.Task", b =>
                {
                    b.OwnsOne("owned_bug.DueDate", "DueDate", b1 =>
                        {
                            b1.Property<int>("TaskId")
                                .HasColumnType("INTEGER");

                            b1.Property<DateTimeOffset?>("Date")
                                .HasColumnType("TEXT");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
