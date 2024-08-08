﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelInspiration.API.Shared.Persistence.Migrations;

#nullable disable

namespace TravelInspiration.API.Shared.Persistance.Migrations
{
    [DbContext(typeof(TravelInspirationDbContext))]
    partial class TravelInspirationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TravelInspiration.API.Shared.Domain.Entities.Itinerary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Itineraries", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9987),
                            Description = "Five great days in Paris",
                            LastModifiedBy = "",
                            Name = "A Trip to Paris",
                            UserId = "dummyuserid"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9991),
                            Description = "A week in beautiful Antwerp",
                            LastModifiedBy = "",
                            Name = "Antwerp Extravaganza",
                            UserId = "dummyuserid"
                        });
                });

            modelBuilder.Entity("TravelInspiration.API.Shared.Domain.Entities.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItineraryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ItineraryId");

                    b.ToTable("Stops", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(261),
                            ImageUri = "https://localhost:7120/images/eiffeltower.jpg",
                            ItineraryId = 1,
                            LastModifiedBy = "",
                            Name = "The Eiffel Tower"
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(271),
                            ImageUri = "https://localhost:7120/images/louvre.jpg",
                            ItineraryId = 1,
                            LastModifiedBy = "",
                            Name = "The Louvre"
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(278),
                            ImageUri = "https://localhost:7120/images/perelachaise.jpg",
                            ItineraryId = 1,
                            LastModifiedBy = "",
                            Name = "Père Lachaise Cemetery"
                        },
                        new
                        {
                            Id = 4,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(285),
                            ImageUri = "https://localhost:7120/images/royalmuseum.jpg",
                            ItineraryId = 2,
                            LastModifiedBy = "",
                            Name = "The Royal Museum of Beautiful Arts"
                        },
                        new
                        {
                            Id = 5,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(292),
                            ImageUri = "https://localhost:7120/images/stpauls.jpg",
                            ItineraryId = 2,
                            LastModifiedBy = "",
                            Name = "Saint Paul's Church"
                        },
                        new
                        {
                            Id = 6,
                            CreatedBy = "DATASEED",
                            CreatedOn = new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(299),
                            ImageUri = "https://localhost:7120/images/michelin.jpg",
                            ItineraryId = 2,
                            LastModifiedBy = "",
                            Name = "Michelin Restaurant Visit"
                        });
                });

            modelBuilder.Entity("TravelInspiration.API.Shared.Domain.Entities.Stop", b =>
                {
                    b.HasOne("TravelInspiration.API.Shared.Domain.Entities.Itinerary", "Itinerary")
                        .WithMany("Stops")
                        .HasForeignKey("ItineraryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Itinerary");
                });

            modelBuilder.Entity("TravelInspiration.API.Shared.Domain.Entities.Itinerary", b =>
                {
                    b.Navigation("Stops");
                });
#pragma warning restore 612, 618
        }
    }
}
