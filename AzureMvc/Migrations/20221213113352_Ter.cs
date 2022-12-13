using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureMvc.Migrations
{
    public partial class Ter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "ImagemPais", "Nome" },
                values: new object[] { 1, null, "Brasil" });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "ImagemPais", "Nome" },
                values: new object[] { 2, null, "Argentina" });

            migrationBuilder.InsertData(
                table: "Paises",
                columns: new[] { "PaisId", "ImagemPais", "Nome" },
                values: new object[] { 3, null, "Croacia" });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 1, "/assets/porsche/riodejaneiro.png", "Rio De Janeiro", 1 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 2, "/assets/porsche/Zagrebe.jpeg", "Zagrebe", 3 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 3, "/assets/porsche/buenosaires.png", "Buenos Aires", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "EstadoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "EstadoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "EstadoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "PaisId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "PaisId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "PaisId",
                keyValue: 3);
        }
    }
}
