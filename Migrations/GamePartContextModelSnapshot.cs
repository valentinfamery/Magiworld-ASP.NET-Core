﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace magiworld_asp_net_backend.Migrations
{
    [DbContext(typeof(GamePartContext))]
    partial class GamePartContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("GamePart", b =>
                {
                    b.Property<int>("GamePartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("playerVictorious")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("GamePartId");

                    b.ToTable("GameParts");
                });
#pragma warning restore 612, 618
        }
    }
}
