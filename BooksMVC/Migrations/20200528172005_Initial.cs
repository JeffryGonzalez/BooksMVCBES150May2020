using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksMVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    NumberOfPages = table.Column<int>(nullable: false),
                    InInventory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 1, "Thoreau", true, 212, "Walden" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 2, "Emerson", true, 253, "Nature" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "InInventory", "NumberOfPages", "Title" },
                values: new object[] { 3, "C.G. Jung", false, 332, "Memories, Dreams, Reflections" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
