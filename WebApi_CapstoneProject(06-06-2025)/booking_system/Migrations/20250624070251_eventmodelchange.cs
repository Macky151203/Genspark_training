using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_system.Migrations
{
    /// <inheritdoc />
    public partial class eventmodelchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Context",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Imageurl",
                table: "Events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Ticketcount",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Context",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Imageurl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Ticketcount",
                table: "Events");
        }
    }
}
