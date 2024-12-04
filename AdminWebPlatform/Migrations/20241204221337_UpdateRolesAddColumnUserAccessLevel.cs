using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWebPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolesAddColumnUserAccessLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccessLevel",
                table: "Roles",
                newName: "UserAccessLevel");

            migrationBuilder.AddColumn<int>(
                name: "ContentAccessLevel",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentAccessLevel",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UserAccessLevel",
                table: "Roles",
                newName: "AccessLevel");
        }
    }
}
