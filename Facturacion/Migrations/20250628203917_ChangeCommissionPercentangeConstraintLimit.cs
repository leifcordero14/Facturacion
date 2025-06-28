using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facturacion.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommissionPercentangeConstraintLimit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Prices",
                table: "Seller");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Prices",
                table: "Seller",
                sql: "[CommissionPercentage] BETWEEN 0 AND 100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Prices",
                table: "Seller");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Prices",
                table: "Seller",
                sql: "[CommissionPercentage] BETWEEN 1 AND 100");
        }
    }
}
