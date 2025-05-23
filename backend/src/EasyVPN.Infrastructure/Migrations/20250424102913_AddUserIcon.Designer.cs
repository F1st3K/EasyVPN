﻿// <auto-generated />
using System;
using EasyVPN.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyVPN.Infrastructure.Migrations
{
    [DbContext(typeof(EasyVpnDbContext))]
    [Migration("20250424102913_AddUserIcon")]
    partial class AddUserIcon
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EasyVPN.Domain.Entities.Connection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationTime")
                        .HasMaxLength(28)
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ServerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ServerId");

                    b.ToTable("Connections", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.ConnectionTicket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConnectionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasMaxLength(28)
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Days")
                        .HasColumnType("integer");

                    b.Property<string>("Images")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("ConnectionId");

                    b.ToTable("ConnectionTickets", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.DynamicPage", b =>
                {
                    b.Property<string>("Route")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("Route");

                    b.ToTable("DynamicPages", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.Protocol", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Protocols", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.Server", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConnectionString")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid>("ProtocolId")
                        .HasColumnType("uuid");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.HasIndex("ProtocolId");

                    b.ToTable("Servers", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.Connection", b =>
                {
                    b.HasOne("EasyVPN.Domain.Entities.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyVPN.Domain.Entities.Server", "Server")
                        .WithMany()
                        .HasForeignKey("ServerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Server");
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.ConnectionTicket", b =>
                {
                    b.HasOne("EasyVPN.Domain.Entities.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyVPN.Domain.Entities.Connection", null)
                        .WithMany()
                        .HasForeignKey("ConnectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("EasyVPN.Domain.Entities.Server", b =>
                {
                    b.HasOne("EasyVPN.Domain.Entities.Protocol", "Protocol")
                        .WithMany()
                        .HasForeignKey("ProtocolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Protocol");
                });
#pragma warning restore 612, 618
        }
    }
}
