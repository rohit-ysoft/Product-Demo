using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Migrations
{
    /// <inheritdoc />
    public partial class name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargateComplactionDate",
                table: "Products",
                newName: "TargetComplactionDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetComplactionDate",
                table: "Products",
                newName: "TargateComplactionDate");
        }
    }
}
