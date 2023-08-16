using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS_MVC.Migrations
{
    /// <inheritdoc />
    public partial class iformfileAuthorq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Author1",
                newName: "Path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Path",
                table: "Author1",
                newName: "ImagePath");
        }
    }
}
