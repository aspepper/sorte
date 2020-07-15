using Microsoft.EntityFrameworkCore.Migrations;

namespace sorte.console.Migrations
{
    public partial class CidadesMegasena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Megasenas");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Megasenas");

            migrationBuilder.CreateTable(
                name: "MegasenaCidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MegasenaId = table.Column<int>(nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: true),
                    UF = table.Column<string>(type: "char(2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MegasenaCidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MegasenaCidades_Megasenas_MegasenaId",
                        column: x => x.MegasenaId,
                        principalTable: "Megasenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MegasenaCidades_MegasenaId",
                table: "MegasenaCidades",
                column: "MegasenaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MegasenaCidades");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Megasenas",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Megasenas",
                type: "char(2)",
                nullable: true);
        }
    }
}
