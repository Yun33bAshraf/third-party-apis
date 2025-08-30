using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenProfileAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MadeGenderIdNullableInUserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Category_GenderId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Role");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "UserProfile",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Category_GenderId",
                table: "UserProfile",
                column: "GenderId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Category_GenderId",
                table: "UserProfile");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "UserProfile",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Category_GenderId",
                table: "UserProfile",
                column: "GenderId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
