﻿// <auto-generated />
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PTTrainerApp;

namespace PTTrainerApp.Migrations
{
    [DbContext(typeof(TrainerContext))]
    partial class TrainerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PTTrainerApp.Trainer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<double>("Rating");

                    b.Property<string>("Email");

                    b.Property<string>("Gender");

                    b.Property<double>("Price");

                    b.HasKey("ID");

                    b.ToTable("Trainers");
                });

            modelBuilder.Entity("PTTrainerApp.Trainer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day");

                    b.Property<int?>("TrainerID");

                    b.Property<string>("Time");

                    b.Property<string>("Location");

                    b.HasKey("ID");

                    b.HasIndex("TrainerID");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("PTTrainerApp.Availability", b =>
                {
                    b.HasOne("PTTrainerApp.Trainer", "Trainer")
                        .WithMany("Availabilities")
                        .HasForeignKey("TrainerID");
                });
#pragma warning restore 612, 618
        }
    }
}