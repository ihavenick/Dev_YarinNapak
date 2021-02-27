using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dev_YarinNeYapak.Data.Migrations
{
    public partial class yeni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "EtkinlikID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Yerler",
                columns: table => new
                {
                    YerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    YerAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yerler", x => x.YerID);
                });

            migrationBuilder.CreateTable(
                name: "Etkinlikler",
                columns: table => new
                {
                    EtkinlikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EtkinlikAdi = table.Column<string>(nullable: true),
                    NeZaman = table.Column<DateTime>(nullable: false),
                    YerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinlikler", x => x.EtkinlikID);
                    table.ForeignKey(
                        name: "FK_Etkinlikler_Yerler_YerID",
                        column: x => x.YerID,
                        principalTable: "Yerler",
                        principalColumn: "YerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EtkinlikID",
                table: "AspNetUsers",
                column: "EtkinlikID");

            migrationBuilder.CreateIndex(
                name: "IX_Etkinlikler_YerID",
                table: "Etkinlikler",
                column: "YerID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Etkinlikler_EtkinlikID",
                table: "AspNetUsers",
                column: "EtkinlikID",
                principalTable: "Etkinlikler",
                principalColumn: "EtkinlikID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Etkinlikler_EtkinlikID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Etkinlikler");

            migrationBuilder.DropTable(
                name: "Yerler");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EtkinlikID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EtkinlikID",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}
