using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureMvc.Migrations
{
    public partial class seg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "PaisId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Paises",
                keyColumn: "PaisId",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Paises",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImagemPais",
                table: "Paises",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagemEstado",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemPais",
                table: "Paises");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Paises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagemEstado",
                table: "Estados",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                values: new object[] { 1, "../assets/porsche/porsche_911_Turbo.jpg", "Rio de Janeiro", 1 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 2, "../assets/porsche/porsche_911_Turbo.jpg", "São Paulo", 1 });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "ImagemEstado", "Nome", "PaisId" },
                values: new object[] { 3, "../assets/porsche/porsche_911_Turbo.jpg", "Minas Gerais", 1 });
        }
    }
}
