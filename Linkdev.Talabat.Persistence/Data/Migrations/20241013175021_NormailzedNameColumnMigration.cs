using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linkdev.Talabat.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormailzedNameColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_NormailzedName",
                table: "Products",
                column: "NormalizedName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_NormailzedName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Products");
        }
    }
}
