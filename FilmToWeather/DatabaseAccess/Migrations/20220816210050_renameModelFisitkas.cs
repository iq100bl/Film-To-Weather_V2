using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccess.Migrations
{
    public partial class renameModelFisitkas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fisitka_Condition_ConditionCode",
                table: "Fisitka");

            migrationBuilder.DropForeignKey(
                name: "FK_Fisitka_Genres_GenreId",
                table: "Fisitka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fisitka",
                table: "Fisitka");

            migrationBuilder.RenameTable(
                name: "Fisitka",
                newName: "Fisitkas");

            migrationBuilder.RenameIndex(
                name: "IX_Fisitka_GenreId",
                table: "Fisitkas",
                newName: "IX_Fisitkas_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Fisitka_ConditionCode",
                table: "Fisitkas",
                newName: "IX_Fisitkas_ConditionCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fisitkas",
                table: "Fisitkas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fisitkas_Condition_ConditionCode",
                table: "Fisitkas",
                column: "ConditionCode",
                principalTable: "Condition",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fisitkas_Genres_GenreId",
                table: "Fisitkas",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fisitkas_Condition_ConditionCode",
                table: "Fisitkas");

            migrationBuilder.DropForeignKey(
                name: "FK_Fisitkas_Genres_GenreId",
                table: "Fisitkas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fisitkas",
                table: "Fisitkas");

            migrationBuilder.RenameTable(
                name: "Fisitkas",
                newName: "Fisitka");

            migrationBuilder.RenameIndex(
                name: "IX_Fisitkas_GenreId",
                table: "Fisitka",
                newName: "IX_Fisitka_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Fisitkas_ConditionCode",
                table: "Fisitka",
                newName: "IX_Fisitka_ConditionCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fisitka",
                table: "Fisitka",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fisitka_Condition_ConditionCode",
                table: "Fisitka",
                column: "ConditionCode",
                principalTable: "Condition",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fisitka_Genres_GenreId",
                table: "Fisitka",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
