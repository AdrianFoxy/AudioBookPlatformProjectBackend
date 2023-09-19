﻿// <auto-generated />
using System;
using ABP_Backend.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABP_Backend.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20230919083707_ConfigUpdate")]
    partial class ConfigUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("BookDuration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValueSql("'00:00:00'");

                    b.Property<int>("BookLanguageId")
                        .HasColumnType("int");

                    b.Property<int>("BookSeriesId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("NarratorId")
                        .HasColumnType("int");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("BookLanguageId");

                    b.HasIndex("BookSeriesId");

                    b.HasIndex("NarratorId");

                    b.ToTable("AudioBook");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookAudioFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AudioFileUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<TimeSpan>("Duration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("time")
                        .HasDefaultValueSql("'00:00:00'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("BookAudioFile");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("BookLanguage");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookSelection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("BookSelection");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("BookSeries");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.Narrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("MediaUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Narrator");
                });

            modelBuilder.Entity("AudioBookAuthor", b =>
                {
                    b.Property<int>("AudioBooksId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("AudioBooksId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AudioBookAuthor");
                });

            modelBuilder.Entity("AudioBookBookAudioFile", b =>
                {
                    b.Property<int>("AudioBooksId")
                        .HasColumnType("int");

                    b.Property<int>("BookAudioFileId")
                        .HasColumnType("int");

                    b.HasKey("AudioBooksId", "BookAudioFileId");

                    b.HasIndex("BookAudioFileId");

                    b.ToTable("AudioBookBookAudioFile");
                });

            modelBuilder.Entity("AudioBookBookSelection", b =>
                {
                    b.Property<int>("AudioBooksId")
                        .HasColumnType("int");

                    b.Property<int>("BookSelectionId")
                        .HasColumnType("int");

                    b.HasKey("AudioBooksId", "BookSelectionId");

                    b.HasIndex("BookSelectionId");

                    b.ToTable("AudioBookBookSelection");
                });

            modelBuilder.Entity("AudioBookGenre", b =>
                {
                    b.Property<int>("AudioBooksId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("AudioBooksId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("AudioBookGenre");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.BookLanguage", "BookLanguage")
                        .WithMany()
                        .HasForeignKey("BookLanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.BookSeries", "BookSeries")
                        .WithMany()
                        .HasForeignKey("BookSeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.Narrator", "Narrator")
                        .WithMany()
                        .HasForeignKey("NarratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BookLanguage");

                    b.Navigation("BookSeries");

                    b.Navigation("Narrator");
                });

            modelBuilder.Entity("AudioBookAuthor", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", null)
                        .WithMany()
                        .HasForeignKey("AudioBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioBookBookAudioFile", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", null)
                        .WithMany()
                        .HasForeignKey("AudioBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.BookAudioFile", null)
                        .WithMany()
                        .HasForeignKey("BookAudioFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioBookBookSelection", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", null)
                        .WithMany()
                        .HasForeignKey("AudioBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.BookSelection", null)
                        .WithMany()
                        .HasForeignKey("BookSelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AudioBookGenre", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", null)
                        .WithMany()
                        .HasForeignKey("AudioBooksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
