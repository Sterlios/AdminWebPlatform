using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminWebPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersAddColumnUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
