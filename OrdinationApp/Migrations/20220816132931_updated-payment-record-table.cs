using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrdinationApp.Migrations
{
    public partial class updatedpaymentrecordtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8042fe00-795d-4283-873b-185d3896b494");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b1127c7-00fc-4c2e-a929-93d6453dd3c1");

            migrationBuilder.AddColumn<string>(
                name: "MembershipId",
                table: "PaymentRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "0b264745-ae1e-445f-a2f5-a493279c8f55", "507036f4-cb70-48b8-b286-9d8656535d89", "UserRole", "Coder", "CODER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "3f545e3f-b0ef-47aa-b278-e4d28508dcde", "2e0dcdb6-759e-4f60-a8b1-131f8cffbc74", "UserRole", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b264745-ae1e-445f-a2f5-a493279c8f55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f545e3f-b0ef-47aa-b278-e4d28508dcde");

            migrationBuilder.DropColumn(
                name: "MembershipId",
                table: "PaymentRecords");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8042fe00-795d-4283-873b-185d3896b494", "f68ee0b3-d0b3-49d8-9438-20851fcce4de", "UserRole", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "8b1127c7-00fc-4c2e-a929-93d6453dd3c1", "a56f22e8-fe95-409f-bca0-bdd9e3f6e3e9", "UserRole", "Coder", "CODER" });
        }
    }
}
