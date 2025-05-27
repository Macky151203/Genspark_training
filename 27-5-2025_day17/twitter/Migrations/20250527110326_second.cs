using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twitter.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashTagTweet");

            migrationBuilder.AddColumn<int>(
                name: "TweetId",
                table: "HashTags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HashTags_TweetId",
                table: "HashTags",
                column: "TweetId");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTags_Tweets_TweetId",
                table: "HashTags",
                column: "TweetId",
                principalTable: "Tweets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTags_Tweets_TweetId",
                table: "HashTags");

            migrationBuilder.DropIndex(
                name: "IX_HashTags_TweetId",
                table: "HashTags");

            migrationBuilder.DropColumn(
                name: "TweetId",
                table: "HashTags");

            migrationBuilder.CreateTable(
                name: "HashTagTweet",
                columns: table => new
                {
                    HashtagsId = table.Column<int>(type: "integer", nullable: false),
                    TweetsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagTweet", x => new { x.HashtagsId, x.TweetsId });
                    table.ForeignKey(
                        name: "FK_HashTagTweet_HashTags_HashtagsId",
                        column: x => x.HashtagsId,
                        principalTable: "HashTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashTagTweet_Tweets_TweetsId",
                        column: x => x.TweetsId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashTagTweet_TweetsId",
                table: "HashTagTweet",
                column: "TweetsId");
        }
    }
}
