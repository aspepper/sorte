using Microsoft.EntityFrameworkCore.Migrations;

namespace sorte.console.Migrations
{
    public partial class AtualizacaoColunasMegaSena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Acumulado",
                table: "Megasenas",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AcumuladoVirada",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ArredacaoTotal",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Megasenas",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EstimativaPremio",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Ganhadores",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GanhadoresQuadra",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GanhadoresQuina",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rateio",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RateioQuadra",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RateioQuina",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Megasenas",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValorAcumulado",
                table: "Megasenas",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acumulado",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "AcumuladoVirada",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "ArredacaoTotal",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "EstimativaPremio",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "Ganhadores",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "GanhadoresQuadra",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "GanhadoresQuina",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "Rateio",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "RateioQuadra",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "RateioQuina",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "ValorAcumulado",
                table: "Megasenas");
        }
    }
}
