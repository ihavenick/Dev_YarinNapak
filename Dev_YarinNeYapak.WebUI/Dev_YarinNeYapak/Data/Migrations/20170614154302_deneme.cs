using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_YarinNeYapak.Data.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarBase64",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adsoyad",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "facebookid",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "facelekayitolmus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarBase64",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "adsoyad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "facebookid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "facelekayitolmus",
                table: "AspNetUsers");
        }
    }
}
