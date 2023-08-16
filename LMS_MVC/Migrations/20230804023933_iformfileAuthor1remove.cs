using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_MVC.Migrations
{
    /// <inheritdoc />
    public partial class iformfileAuthor1remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Author1",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Author1",
                newName: "Path");
        }
    }
}
