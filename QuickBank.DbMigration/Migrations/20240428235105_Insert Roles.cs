using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBank.DbMigration.Migrations
{
    /// <inheritdoc />
    public partial class InsertRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "AspNetRoles",
                ["Name", "NormalizedName"],
                ["string", "string"],
                ["Admin", "Admin"]
                );

            migrationBuilder.InsertData(
                "AspNetRoles",
                ["Name", "NormalizedName"],
                ["string", "string"],
                ["User", "User"]
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
