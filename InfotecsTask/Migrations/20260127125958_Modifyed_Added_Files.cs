using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTask.Migrations
{
    /// <inheritdoc />
    public partial class Modifyed_Added_Files : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Results_ResultId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ResultId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Results_FileId",
                table: "Results",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Files_FileId",
                table: "Results",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Files_FileId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_FileId",
                table: "Results");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Files",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_ResultId",
                table: "Files",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Results_ResultId",
                table: "Files",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");
        }
    }
}
