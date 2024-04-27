using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class changedpropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Borrows",
                newName: "Returned");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Borrows",
                newName: "Borrowed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Returned",
                table: "Borrows",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Borrowed",
                table: "Borrows",
                newName: "EndDate");
        }
    }
}
