using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureMvc.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    AmigoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemAmigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aniversario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaisOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstadoOrigem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmigosAmigoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.AmigoId);
                    table.ForeignKey(
                        name: "FK_Amigos_Amigos_AmigosAmigoId",
                        column: x => x.AmigosAmigoId,
                        principalTable: "Amigos",
                        principalColumn: "AmigoId");
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    PaisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.PaisId);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemEstado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoId);
                    table.ForeignKey(
                        name: "FK_Estados_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "PaisId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "Nome" },
                values: new object[] { 1, "Brasil" });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "Nome" },
                values: new object[] { 2, "Argentina" });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "Nome" },
                values: new object[] { 3, "Espanha" });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 1, "/assets/porsche/porsche_911_Turbo.jpg", "Rio de Janeiro", 1 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 2, "/assets/porsche/porsche_911_Turbo.jpg", "São Paulo", 1 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 3, "/assets/porsche/porsche_911_Turbo.jpg", "Minas Gerais", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Amigos_AmigosAmigoId",
                table: "Amigos",
                column: "AmigosAmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_PaisId",
                table: "Estados",
                column: "PaisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amigos");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
