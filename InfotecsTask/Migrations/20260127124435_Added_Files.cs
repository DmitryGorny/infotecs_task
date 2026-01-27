using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfotecsTask.Migrations
{
    /// <inheritdoc />
    public partial class Added_Files : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Results");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Values",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FilesId",
                table: "Values",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Results",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ResultId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Values_FilesId",
                table: "Values",
                column: "FilesId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ResultId",
                table: "Files",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Values_Files_FilesId",
                table: "Values",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Values_Files_FilesId",
                table: "Values");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Values_FilesId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FilesId",
                table: "Values");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Results");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Values",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Results",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
