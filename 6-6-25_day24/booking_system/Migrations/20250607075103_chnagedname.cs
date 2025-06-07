using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace booking_system.Migrations
{
    /// <inheritdoc />
    public partial class chnagedname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Admins_CreatedById",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Events",
                newName: "CreatorEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CreatedById",
                table: "Events",
                newName: "IX_Events_CreatorEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Admins_CreatorEmail",
                table: "Events",
                column: "CreatorEmail",
                principalTable: "Admins",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Admins_CreatorEmail",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "CreatorEmail",
                table: "Events",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Events_CreatorEmail",
                table: "Events",
                newName: "IX_Events_CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Admins_CreatedById",
                table: "Events",
                column: "CreatedById",
                principalTable: "Admins",
                principalColumn: "Email",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
