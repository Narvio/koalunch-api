﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using koalunch_api.Models;

namespace koalunch_api.Migrations
{
    [DbContext(typeof(FeedbackContext))]
    [Migration("20211013201034_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("koalunch_api.Models.FeedbackItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("RestaurantName")
                        .HasColumnType("TEXT");

                    b.Property<string>("RestaurantUrl")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FeedbackItems");
                });
#pragma warning restore 612, 618
        }
    }
}
