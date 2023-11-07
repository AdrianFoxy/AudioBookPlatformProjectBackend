﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Re_ABP_Backend.Data.DB;

#nullable disable

namespace Re_ABP_Backend.Migrations
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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookDuration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

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

                    b.Property<int>("OrderInSeries")
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

                    b.Property<int>("ViewCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("BookLanguageId");

                    b.HasIndex("BookSeriesId");

                    b.HasIndex("NarratorId");

                    b.ToTable("AudioBook");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookAudioFile", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("BookAudioFileId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "BookAudioFileId");

                    b.HasIndex("BookAudioFileId");

                    b.ToTable("AudioBookAudioFile");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookAuthor", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AudioBookAuthor");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookGenre", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("AudioBookGenre");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookSelection", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("BookSelectionId")
                        .HasColumnType("int");

                    b.HasKey("AudioBookId", "BookSelectionId");

                    b.HasIndex("BookSelectionId");

                    b.ToTable("AudioBookSelection");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Author", b =>
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

                    b.Property<string>("EnDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookAudioFile", b =>
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

                    b.Property<int>("Duration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookSelection", b =>
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

                    b.Property<string>("EnDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Identity.Role", b =>
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

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varbinary(256)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varbinary(256)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("SocialAuth")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Token")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("");

                    b.Property<DateTime>("TokenCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("TokenExpires")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.LibraryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EnName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("LibraryStatus");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Narrator", b =>
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

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AudioBookId");

                    b.HasIndex("UserId");

                    b.ToTable("Review", t =>
                        {
                            t.HasTrigger("UpdateAudioBookRating");
                        });
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.UserLibrary", b =>
                {
                    b.Property<int>("AudioBookId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("LibraryStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DateTime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("AudioBookId", "UserId");

                    b.HasIndex("LibraryStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLibrary");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.BookLanguage", "BookLanguage")
                        .WithMany()
                        .HasForeignKey("BookLanguageId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.BookSeries", "BookSeries")
                        .WithMany()
                        .HasForeignKey("BookSeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.Narrator", "Narrator")
                        .WithMany()
                        .HasForeignKey("NarratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("BookLanguage");

                    b.Navigation("BookSeries");

                    b.Navigation("Narrator");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookAudioFile", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookAudioFile")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.BookAudioFile", "BookAudioFile")
                        .WithMany("AudioBookBookAudioFile")
                        .HasForeignKey("BookAudioFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("BookAudioFile");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookAuthor", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookAuthor")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.Author", "Author")
                        .WithMany("AudioBookAuthor")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookGenre", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookGenre")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.Genre", "Genre")
                        .WithMany("AudioBookGenre")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBookSelection", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("AudioBookSelection")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.BookSelection", "BookSelection")
                        .WithMany("AudioBookSelection")
                        .HasForeignKey("BookSelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("BookSelection");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Identity.User", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.Identity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Review", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany()
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.UserLibrary", b =>
                {
                    b.HasOne("Re_ABP_Backend.Data.Entities.AudioBook", "AudioBook")
                        .WithMany("UserLibrary")
                        .HasForeignKey("AudioBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.LibraryStatus", "LibraryStatus")
                        .WithMany("UserLibrary")
                        .HasForeignKey("LibraryStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Re_ABP_Backend.Data.Entities.Identity.User", "User")
                        .WithMany("UserLibrary")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AudioBook");

                    b.Navigation("LibraryStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.AudioBook", b =>
                {
                    b.Navigation("AudioBookAudioFile");

                    b.Navigation("AudioBookAuthor");

                    b.Navigation("AudioBookGenre");

                    b.Navigation("AudioBookSelection");

                    b.Navigation("UserLibrary");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Author", b =>
                {
                    b.Navigation("AudioBookAuthor");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookAudioFile", b =>
                {
                    b.Navigation("AudioBookBookAudioFile");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.BookSelection", b =>
                {
                    b.Navigation("AudioBookSelection");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Genre", b =>
                {
                    b.Navigation("AudioBookGenre");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.Identity.User", b =>
                {
                    b.Navigation("UserLibrary");
                });

            modelBuilder.Entity("Re_ABP_Backend.Data.Entities.LibraryStatus", b =>
                {
                    b.Navigation("UserLibrary");
                });
#pragma warning restore 612, 618
        }
    }
}
