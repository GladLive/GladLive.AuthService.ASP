using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using GladLive.AuthService.ASP;

namespace GladLive.AuthService.ASP.Migrations
{
    [DbContext(typeof(AccountDbContext))]
    [Migration("20160323040104_UpdatedAccount")]
    partial class UpdatedAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GladLive.AuthService.ASP.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<byte>("AccountStanding");

                    b.Property<string>("CreationIP")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.HasKey("AccountID");
                });
        }
    }
}
