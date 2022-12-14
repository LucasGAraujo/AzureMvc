using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureMvc.Migrations
{
    public partial class SemiF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Amigos",
                newName: "AmigoNome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmigoNome",
                table: "Amigos",
                newName: "Nome");
        }
    }
}
