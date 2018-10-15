﻿// <auto-generated />
using System;
using JustReadMe.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JustReadMe.Migrations
{
    [DbContext(typeof(BloghostContext))]
    partial class BloghostContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JustReadMe.Models.BlogArticleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<int?>("BlogModelId");

                    b.Property<DateTime>("DateChange");

                    b.Property<string>("Message");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("BlogModelId");

                    b.ToTable("BlogArticles");
                });

            modelBuilder.Entity("JustReadMe.Models.BlogModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreate");

                    b.Property<string>("Description");

                    b.Property<string>("Title");

                    b.Property<int>("UserId");

                    b.Property<int?>("UserModelId");

                    b.HasKey("Id");

                    b.HasIndex("UserModelId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("JustReadMe.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JustReadMe.Models.BlogArticleModel", b =>
                {
                    b.HasOne("JustReadMe.Models.BlogModel", "BlogModel")
                        .WithMany()
                        .HasForeignKey("BlogModelId");
                });

            modelBuilder.Entity("JustReadMe.Models.BlogModel", b =>
                {
                    b.HasOne("JustReadMe.Models.UserModel", "UserModel")
                        .WithMany()
                        .HasForeignKey("UserModelId");
                });
#pragma warning restore 612, 618
        }
    }
}