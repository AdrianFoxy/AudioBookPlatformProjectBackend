﻿// <auto-generated />
using System;
using ABP_Backend.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ABP_Backend.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasDefaultValue(0.0);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("BookLanguageId");

                    b.HasIndex("BookSeriesId");

                    b.HasIndex("NarratorId");

                    b.ToTable("AudioBook", (string)null);
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookAudioFile", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("BookAudioFileId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "BookAudioFileId");

                    b.HasIndex("BookAudioFileId");

                    b.ToTable("AudioBookAudioFile", (string)null);
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookAuthor", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AudioBookAuthor", (string)null);
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookGenre", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("AudioBookGenre", (string)null);
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookSelection", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("BookSelectionId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "BookSelectionId");

                    b.HasIndex("BookSelectionId");

                    b.ToTable("AudioBookSelection", (string)null);
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

                    b.ToTable("Author", (string)null);
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

                    b.ToTable("BookAudioFile", (string)null);
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

                    b.ToTable("BookLanguage", (string)null);
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

                    b.ToTable("BookSelection", (string)null);
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

                    b.ToTable("BookSeries", (string)null);
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
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Genre", (string)null);
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

                    b.ToTable("Narrator", (string)null);
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

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookAudioFile", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookAudioFile")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.BookAudioFile", "BookAudioFile")
                        .WithMany("AudioBookBookAudioFile")
                        .HasForeignKey("BookAudioFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("BookAudioFile");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookAuthor", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookAuthor")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.Author", "Author")
                        .WithMany("AudioBookAuthor")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookGenre", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookGenre")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.Genre", "Genre")
                        .WithMany("AudioBookGenre")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBookSelection", b =>
                {
                    b.HasOne("ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookSelection")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ABP_Backend.Data.Entities.BookSelection", "BookSelection")
                        .WithMany("AudioBookSelection")
                        .HasForeignKey("BookSelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("BookSelection");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.Navigation("AudioBookAudioFile");

                    b.Navigation("AudioBookAuthor");

                    b.Navigation("AudioBookGenre");

                    b.Navigation("AudioBookSelection");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.Author", b =>
                {
                    b.Navigation("AudioBookAuthor");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookAudioFile", b =>
                {
                    b.Navigation("AudioBookBookAudioFile");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.BookSelection", b =>
                {
                    b.Navigation("AudioBookSelection");
                });

            modelBuilder.Entity("ABP_Backend.Data.Entities.Genre", b =>
                {
                    b.Navigation("AudioBookGenre");
                });
#pragma warning restore 612, 618
        }
    }
}
