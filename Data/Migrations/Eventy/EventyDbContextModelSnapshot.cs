﻿// <auto-generated />
using eventy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace eventy.Data.Migrations.Eventy
{
    [DbContext(typeof(EventyDbContext))]
    partial class EventyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("eventy.Models.Event", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTime>("EventDate");

                    b.Property<string>("EventName");

                    b.Property<int>("MaxNumberOfFamilies");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("eventy.Models.EventsFamilies", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<long>("EventId");

                    b.Property<long>("FamilyId");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("EventsFamilies");
                });

            modelBuilder.Entity("eventy.Models.Family", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Name");

                    b.Property<string>("OldControlNumber");

                    b.Property<string>("OldFamilyNumber");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("eventy.Models.FamilyMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthday");

                    b.Property<DateTime?>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<long>("FamilyId");

                    b.Property<string>("FullName");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsHeadOfFamily");

                    b.Property<string>("UserCreated");

                    b.Property<string>("UserModified");

                    b.HasKey("Id");

                    b.ToTable("FamilyMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
