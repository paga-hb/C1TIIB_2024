﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.Data;

#nullable disable

namespace Todo.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Todo.Data.Entities.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    b.Property<string>("Comments")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("TodoItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comments = "Maybe take Coursera Courses too?",
                            Completed = true,
                            Name = "Learn app development",
                            Notes = "Take Microsoft Learn Courses"
                        },
                        new
                        {
                            Id = 2,
                            Comments = "Maybe use Visual Studio Code instead?",
                            Completed = false,
                            Name = "Develop apps",
                            Notes = "Use Visual Studio and Visual Studio for Mac"
                        },
                        new
                        {
                            Id = 3,
                            Completed = false,
                            Name = "Publish apps",
                            Notes = "All app stores"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}