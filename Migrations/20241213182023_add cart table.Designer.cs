﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241213182023_add cart table")]
    partial class addcarttable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("API.Data.Models.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("file_name");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("file_path");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_asset");

                    b.ToTable("asset", "public");
                });

            modelBuilder.Entity("API.Data.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("service_id");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_cart");

                    b.ToTable("cart", "public");
                });

            modelBuilder.Entity("API.Data.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("TEXT")
                        .HasColumnName("asset_id");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("category_name");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER")
                        .HasColumnName("sort_order");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_category");

                    b.ToTable("category", "public");
                });

            modelBuilder.Entity("API.Data.Models.PriceCatalogue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("currency");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER")
                        .HasColumnName("price");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_price_catalogue");

                    b.ToTable("price_catalogue", "public");
                });

            modelBuilder.Entity("API.Data.Models.Services", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("TEXT")
                        .HasColumnName("asset_id");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("TEXT")
                        .HasColumnName("category_id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<Guid>("PriceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("price_id");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("service_name");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER")
                        .HasColumnName("sort_order");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.HasKey("Id")
                        .HasName("pk_services");

                    b.ToTable("services", "public");
                });

            modelBuilder.Entity("API.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsEmailVerified")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_email_verified");

                    b.Property<bool>("IsMobileNumberVerified")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_mobile_number_verified");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("phone_number");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("role");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("salt");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserLockId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_lock_id");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.ToTable("user", "public");
                });

            modelBuilder.Entity("API.Data.Models.UserLock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<int>("FailedLogInCount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("failed_log_in_count");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_locked");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_lock");

                    b.ToTable("user_lock", "public");
                });

            modelBuilder.Entity("BookingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("BookedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("booked_date");

                    b.Property<Guid>("CarpenterId")
                        .HasColumnType("TEXT")
                        .HasColumnName("carpenter_id");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_created");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_updated");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_active");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT")
                        .HasColumnName("service_id");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_booking_detail");

                    b.ToTable("booking_detail", "public");
                });

            modelBuilder.Entity("Entities.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<Guid>("AccessToken")
                        .HasColumnType("TEXT")
                        .HasColumnName("access_token");

                    b.Property<DateTime>("ExpireTime")
                        .HasColumnType("TEXT")
                        .HasColumnName("expire_time");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_revoked");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_refresh_token");

                    b.ToTable("refresh_token", "public");
                });
#pragma warning restore 612, 618
        }
    }
}
