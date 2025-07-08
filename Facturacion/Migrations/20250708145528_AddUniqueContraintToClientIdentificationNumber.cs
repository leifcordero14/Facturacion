using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facturacion.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueContraintToClientIdentificationNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentificationNumber",
                table: "Client",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Client_IdentificationNumber",
                table: "Client",
                column: "IdentificationNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Client_IdentificationNumber",
                table: "Client");

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationNumber",
                table: "Client",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
