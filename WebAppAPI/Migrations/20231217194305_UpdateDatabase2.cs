using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companys_EmployeeId",
                table: "Companys");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_EmployeeId",
                table: "Companys",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Companys_EmployeeId",
                table: "Companys");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_EmployeeId",
                table: "Companys",
                column: "EmployeeId");
        }
    }
}
