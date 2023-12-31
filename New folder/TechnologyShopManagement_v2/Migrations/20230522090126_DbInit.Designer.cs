﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnologyShopManagement_v2.Models.Entities;

#nullable disable

namespace TechnologyShopManagement_v2.Migrations
{
    [DbContext(typeof(Technology_ManagementContext))]
    [Migration("20230522090126_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Account", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Account__1788CCAC145696CC");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.AccountDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Permission")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AccountDetail", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Cart", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountID");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<DateTime?>("DateAdd")
                        .HasColumnType("date");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "ProductId")
                        .HasName("PK__Cart__FFDD69E809D3403E");

                    b.HasIndex("ProductId");

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Category", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SubCatCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("Tt")
                        .HasColumnType("int")
                        .HasColumnName("TT");

                    b.HasKey("Code")
                        .HasName("PK__Category__A25C5AA6B1697310");

                    b.HasIndex("SubCatCode");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("Code")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("DateDelivery")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DateOrder")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DateRecipt")
                        .HasColumnType("date");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("money");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int")
                        .HasColumnName("StaffID");

                    b.Property<int?>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StaffId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderDetailID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalEachOrder")
                        .HasColumnType("money")
                        .HasColumnName("totalEachOrder");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ProDescription")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pro_description");

                    b.Property<string>("ProImage")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("pro_image");

                    b.Property<string>("ProductName")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryCode");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.AccountDetail", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Account", "User")
                        .WithMany("AccountDetails")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__AccountDe__UserI__398D8EEE");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Cart", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Account", "Account")
                        .WithMany("Carts")
                        .HasForeignKey("AccountId")
                        .IsRequired()
                        .HasConstraintName("FK__Cart__AccountID__787EE5A0");

                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK__Cart__ProductID__797309D9");

                    b.Navigation("Account");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Category", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Category", "SubCatCodeNavigation")
                        .WithMany("InverseSubCatCodeNavigation")
                        .HasForeignKey("SubCatCode")
                        .HasConstraintName("FK__Category__SubCat__3E52440B");

                    b.Navigation("SubCatCodeNavigation");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Order", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Account", "Customer")
                        .WithMany("OrderCustomers")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__Order__CustomerI__4E88ABD4");

                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Account", "Staff")
                        .WithMany("OrderStaffs")
                        .HasForeignKey("StaffId")
                        .HasConstraintName("FK__Order__StaffID__4F7CD00D");

                    b.Navigation("Customer");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.OrderDetail", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK__OrderDeta__Order__74AE54BC");

                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__OrderDeta__Produ__75A278F5");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Product", b =>
                {
                    b.HasOne("TechnologyShopManagement_v2.Models.Entities.Category", "CategoryCodeNavigation")
                        .WithMany("Products")
                        .HasForeignKey("CategoryCode")
                        .HasConstraintName("FK__Product__Categor__4222D4EF");

                    b.Navigation("CategoryCodeNavigation");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Account", b =>
                {
                    b.Navigation("AccountDetails");

                    b.Navigation("Carts");

                    b.Navigation("OrderCustomers");

                    b.Navigation("OrderStaffs");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Category", b =>
                {
                    b.Navigation("InverseSubCatCodeNavigation");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TechnologyShopManagement_v2.Models.Entities.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
