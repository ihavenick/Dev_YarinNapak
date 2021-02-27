using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Dev_YarinNeYapak.Data.Migrations
{
    public partial class kiralik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KiralikEvler",
                columns: table => new
                {
                    KiralikEvlerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KiralikEvAdi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiralikEvler", x => x.KiralikEvlerId);
                });

            migrationBuilder.CreateTable(
                name: "KiralananTarih",
                columns: table => new
                {
                    KiralananTarihId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baslangic = table.Column<DateTime>(nullable: false),
                    Bitis = table.Column<DateTime>(nullable: false),
                    KiralikEvlerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiralananTarih", x => x.KiralananTarihId);
                    table.ForeignKey(
                        name: "FK_KiralananTarih_KiralikEvler_KiralikEvlerId",
                        column: x => x.KiralikEvlerId,
                        principalTable: "KiralikEvler",
                        principalColumn: "KiralikEvlerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KiralananTarih_KiralikEvlerId",
                table: "KiralananTarih",
                column: "KiralikEvlerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KiralananTarih");

            migrationBuilder.DropTable(
                name: "KiralikEvler");
        }
    }
}
