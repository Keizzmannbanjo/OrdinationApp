using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdinationApp.Migrations
{
    public partial class addedroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2141d30e-f5f2-4494-a683-ab9224610844", "0dc82e8d-de6e-443b-8bc0-8bc20249a0c7", "Coder", "CODER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d922965c-50e2-4533-ba7d-67b6964d2297", "a99b154d-2519-4de5-8616-5431e7102bce", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2141d30e-f5f2-4494-a683-ab9224610844");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d922965c-50e2-4533-ba7d-67b6964d2297");
        }
    }
}
