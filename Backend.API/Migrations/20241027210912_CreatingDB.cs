using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatingDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plantas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plantas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    CodigoChassi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    PlantaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhoes_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Caminhoes_Plantas_PlantaId",
                        column: x => x.PlantaId,
                        principalTable: "Plantas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modelos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "FH" },
                    { 2, "FM" },
                    { 3, "VM" }
                });

            migrationBuilder.InsertData(
                table: "Plantas",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Brasil" },
                    { 2, "Suécia" },
                    { 3, "Estados Unidos" },
                    { 4, "França" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_ModeloId",
                table: "Caminhoes",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_PlantaId",
                table: "Caminhoes",
                column: "PlantaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhoes");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Plantas");
        }
    }
}
