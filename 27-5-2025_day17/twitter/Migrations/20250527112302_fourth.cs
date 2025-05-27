using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twitter.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_UserId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_UserId1",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_UserId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_UserId1",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Follows");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "FollowedId",
                table: "Follows",
                newName: "FollowingId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowingId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowerId",
                table: "Follows");

            migrationBuilder.DropIndex(
                name: "IX_Follows_FollowingId",
                table: "Follows");

            migrationBuilder.RenameColumn(
                name: "FollowingId",
                table: "Follows",
                newName: "FollowedId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Follows",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Follows",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId",
                table: "Follows",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Follows_UserId1",
                table: "Follows",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_UserId",
                table: "Follows",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_UserId1",
                table: "Follows",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
