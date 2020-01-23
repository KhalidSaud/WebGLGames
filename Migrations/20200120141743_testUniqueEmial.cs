using Microsoft.EntityFrameworkCore.Migrations;

namespace WebGLGames.Migrations
{
    public partial class testUniqueEmial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                newName: "Test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "Test",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_Email");
        }
    }
}
