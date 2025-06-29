using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Facturacion.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCommissionPercentageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Seller DROP CONSTRAINT CK_Prices");

            migrationBuilder.AlterColumn<int>(
                name: "CommissionPercentage",
                table: "Seller",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)0);

            migrationBuilder.Sql("ALTER TABLE Seller ADD CONSTRAINT CK_Prices CHECK (CommissionPercentage >= 0 AND CommissionPercentage <= 100)");
    }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "CommissionPercentage",
                table: "Seller",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
