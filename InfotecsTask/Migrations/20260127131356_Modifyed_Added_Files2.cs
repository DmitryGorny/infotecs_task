using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfotecsTask.Migrations
{
    /// <inheritdoc />
    public partial class Modifyed_Added_Files2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Files_FilesId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Values_FilesId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FilesId",
                table: "Values");

            migrationBuilder.CreateIndex(
                name: "IX_Values_FileId",
                table: "Values",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Files_FileId",
                table: "Values",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Files_FileId",
                table: "Values");

            migrationBuilder.DropIndex(
                name: "IX_Values_FileId",
                table: "Values");

            migrationBuilder.AddColumn<int>(
                name: "FilesId",
                table: "Values",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Values_FilesId",
                table: "Values",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Files_FilesId",
                table: "Values",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
