using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dev_YarinNeYapak.Data.Migrations
{
    public partial class kiralika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KiralananTarih_KiralikEvler_KiralikEvlerId",
                table: "KiralananTarih");

            migrationBuilder.RenameColumn(
                name: "KiralikEvlerId",
                table: "KiralananTarih",
                newName: "kiralananevKiralikEvlerId");

            migrationBuilder.RenameIndex(
                name: "IX_KiralananTarih_KiralikEvlerId",
                table: "KiralananTarih",
                newName: "IX_KiralananTarih_kiralananevKiralikEvlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KiralananTarih_KiralikEvler_kiralananevKiralikEvlerId",
                table: "KiralananTarih",
                column: "kiralananevKiralikEvlerId",
                principalTable: "KiralikEvler",
                principalColumn: "KiralikEvlerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KiralananTarih_KiralikEvler_kiralananevKiralikEvlerId",
                table: "KiralananTarih");

            migrationBuilder.RenameColumn(
                name: "kiralananevKiralikEvlerId",
                table: "KiralananTarih",
                newName: "KiralikEvlerId");

            migrationBuilder.RenameIndex(
                name: "IX_KiralananTarih_kiralananevKiralikEvlerId",
                table: "KiralananTarih",
                newName: "IX_KiralananTarih_KiralikEvlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_KiralananTarih_KiralikEvler_KiralikEvlerId",
                table: "KiralananTarih",
                column: "KiralikEvlerId",
                principalTable: "KiralikEvler",
                principalColumn: "KiralikEvlerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
