using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskWebApp.Migrations
{
    /// <inheritdoc />
    public partial class taskTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "95220f8e-620b-4f7c-8cfe-652c4df064be");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "961f013d-cef4-41b0-93de-d468c0175b98");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "c09bc5d5-ca57-45a2-84e9-dc9b98475cdf");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "TaskItems",
                newName: "Status");

            migrationBuilder.InsertData(
                table: "TaskItemStatus",
                columns: new[] { "Id", "DisplayOrder", "Status" },
                values: new object[,]
                {
                    { "1ac7b30e-6e57-4fdf-ac91-5172eb2470bf", 1, "Todo" },
                    { "5b27052d-9961-4eae-8055-09f0298b4326", 2, "In Progress" },
                    { "b1321b53-bef0-40f9-8dd2-260334ad433a", 3, "Done" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "1ac7b30e-6e57-4fdf-ac91-5172eb2470bf");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "5b27052d-9961-4eae-8055-09f0298b4326");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "b1321b53-bef0-40f9-8dd2-260334ad433a");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TaskItems",
                newName: "StatusId");

            migrationBuilder.InsertData(
                table: "TaskItemStatus",
                columns: new[] { "Id", "DisplayOrder", "Status" },
                values: new object[,]
                {
                    { "95220f8e-620b-4f7c-8cfe-652c4df064be", 1, "Todo" },
                    { "961f013d-cef4-41b0-93de-d468c0175b98", 3, "Done" },
                    { "c09bc5d5-ca57-45a2-84e9-dc9b98475cdf", 2, "In Progress" }
                });
        }
    }
}
