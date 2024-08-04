using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ImageStor__QuizI__300424B4",
                table: "ImageStorage");

            migrationBuilder.AddForeignKey(
                name: "FK__ImageStor__QuizI__300424B4",
                table: "ImageStorage",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__ImageStor__QuizI__300424B4",
                table: "ImageStorage");

            migrationBuilder.AddForeignKey(
                name: "FK__ImageStor__QuizI__300424B4",
                table: "ImageStorage",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "Id");
        }
    }
}
