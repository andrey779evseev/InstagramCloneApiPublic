﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SupabaseDbContext))]
    [Migration("20220901195858_v18")]
    partial class v18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Comment.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CommentedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("commented_at");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Like.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("LikedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("liked_at");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("likes", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Post.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("posted_at");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("Domain.Models.RefreshToken.RefreshToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("expires_at");

                    b.Property<bool>("Revoked")
                        .HasColumnType("boolean")
                        .HasColumnName("revoked");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Token");

                    b.HasIndex("UserId");

                    b.ToTable("refresh_tokens", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Avatar")
                        .HasColumnType("text")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<List<Guid>>("Followers")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("followers");

                    b.Property<List<Guid>>("Following")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("following");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("GoogleId")
                        .HasColumnType("text")
                        .HasColumnName("google_id");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("text")
                        .HasColumnName("hashed_password");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Comment.Comment", b =>
                {
                    b.HasOne("Domain.Models.Post.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Like.Like", b =>
                {
                    b.HasOne("Domain.Models.Post.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Post.Post", b =>
                {
                    b.HasOne("Domain.Models.User.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.RefreshToken.RefreshToken", b =>
                {
                    b.HasOne("Domain.Models.User.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Post.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Domain.Models.User.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Posts");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
