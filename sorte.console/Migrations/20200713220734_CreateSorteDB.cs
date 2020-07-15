using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sorte.console.Migrations
{
    public partial class CreateSorteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Megasenas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Concurso = table.Column<int>(nullable: false),
                    DataConcurso = table.Column<DateTime>(nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Megasenas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sorteados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(nullable: false),
                    Ordem = table.Column<int>(nullable: false),
                    MegaSenaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sorteados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sorteados_Megasenas_MegaSenaId",
                        column: x => x.MegaSenaId,
                        principalTable: "Megasenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sorteados_MegaSenaId",
                table: "Sorteados",
                column: "MegaSenaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sorteados");

            migrationBuilder.DropTable(
                name: "Megasenas");
        }
    }
}
