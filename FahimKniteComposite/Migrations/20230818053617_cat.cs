using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FahimKniteComposite.Migrations
{
    public partial class cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryID",
                table: "products",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Categories_CategoryID",
                table: "products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_CategoryID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "products");
        }
    }
}
