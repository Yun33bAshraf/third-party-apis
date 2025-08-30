using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenProfileAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewAttrInUserJobPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationLimit",
                table: "UserJobPreference",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyId",
                table: "UserJobPreference",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationLimit",
                table: "UserJobPreference");

            migrationBuilder.DropColumn(
                name: "FrequencyId",
                table: "UserJobPreference");
        }
    }
}
