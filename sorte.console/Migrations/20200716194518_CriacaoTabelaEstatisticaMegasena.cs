using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace sorte.console.Migrations
{
    public partial class CriacaoTabelaEstatisticaMegasena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstatisticaMegasenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataConcurso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuantDez01 = table.Column<int>(type: "int", nullable: false),
                    QuantDez02 = table.Column<int>(type: "int", nullable: false),
                    QuantDez03 = table.Column<int>(type: "int", nullable: false),
                    QuantDez04 = table.Column<int>(type: "int", nullable: false),
                    QuantDez05 = table.Column<int>(type: "int", nullable: false),
                    QuantDez06 = table.Column<int>(type: "int", nullable: false),
                    QuantDez07 = table.Column<int>(type: "int", nullable: false),
                    QuantDez08 = table.Column<int>(type: "int", nullable: false),
                    QuantDez09 = table.Column<int>(type: "int", nullable: false),
                    QuantDez10 = table.Column<int>(type: "int", nullable: false),
                    QuantDez11 = table.Column<int>(type: "int", nullable: false),
                    QuantDez12 = table.Column<int>(type: "int", nullable: false),
                    QuantDez13 = table.Column<int>(type: "int", nullable: false),
                    QuantDez14 = table.Column<int>(type: "int", nullable: false),
                    QuantDez15 = table.Column<int>(type: "int", nullable: false),
                    QuantDez16 = table.Column<int>(type: "int", nullable: false),
                    QuantDez17 = table.Column<int>(type: "int", nullable: false),
                    QuantDez18 = table.Column<int>(type: "int", nullable: false),
                    QuantDez19 = table.Column<int>(type: "int", nullable: false),
                    QuantDez20 = table.Column<int>(type: "int", nullable: false),
                    QuantDez21 = table.Column<int>(type: "int", nullable: false),
                    QuantDez22 = table.Column<int>(type: "int", nullable: false),
                    QuantDez23 = table.Column<int>(type: "int", nullable: false),
                    QuantDez24 = table.Column<int>(type: "int", nullable: false),
                    QuantDez25 = table.Column<int>(type: "int", nullable: false),
                    QuantDez26 = table.Column<int>(type: "int", nullable: false),
                    QuantDez27 = table.Column<int>(type: "int", nullable: false),
                    QuantDez28 = table.Column<int>(type: "int", nullable: false),
                    QuantDez29 = table.Column<int>(type: "int", nullable: false),
                    QuantDez30 = table.Column<int>(type: "int", nullable: false),
                    QuantDez31 = table.Column<int>(type: "int", nullable: false),
                    QuantDez32 = table.Column<int>(type: "int", nullable: false),
                    QuantDez33 = table.Column<int>(type: "int", nullable: false),
                    QuantDez34 = table.Column<int>(type: "int", nullable: false),
                    QuantDez35 = table.Column<int>(type: "int", nullable: false),
                    QuantDez36 = table.Column<int>(type: "int", nullable: false),
                    QuantDez37 = table.Column<int>(type: "int", nullable: false),
                    QuantDez38 = table.Column<int>(type: "int", nullable: false),
                    QuantDez39 = table.Column<int>(type: "int", nullable: false),
                    QuantDez40 = table.Column<int>(type: "int", nullable: false),
                    QuantDez41 = table.Column<int>(type: "int", nullable: false),
                    QuantDez42 = table.Column<int>(type: "int", nullable: false),
                    QuantDez43 = table.Column<int>(type: "int", nullable: false),
                    QuantDez44 = table.Column<int>(type: "int", nullable: false),
                    QuantDez45 = table.Column<int>(type: "int", nullable: false),
                    QuantDez46 = table.Column<int>(type: "int", nullable: false),
                    QuantDez47 = table.Column<int>(type: "int", nullable: false),
                    QuantDez48 = table.Column<int>(type: "int", nullable: false),
                    QuantDez49 = table.Column<int>(type: "int", nullable: false),
                    QuantDez50 = table.Column<int>(type: "int", nullable: false),
                    QuantDez51 = table.Column<int>(type: "int", nullable: false),
                    QuantDez52 = table.Column<int>(type: "int", nullable: false),
                    QuantDez53 = table.Column<int>(type: "int", nullable: false),
                    QuantDez54 = table.Column<int>(type: "int", nullable: false),
                    QuantDez55 = table.Column<int>(type: "int", nullable: false),
                    QuantDez56 = table.Column<int>(type: "int", nullable: false),
                    QuantDez57 = table.Column<int>(type: "int", nullable: false),
                    QuantDez58 = table.Column<int>(type: "int", nullable: false),
                    QuantDez59 = table.Column<int>(type: "int", nullable: false),
                    QuantDez60 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstatisticaMegasenas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstatisticaMegasenas");
        }
    }
}
