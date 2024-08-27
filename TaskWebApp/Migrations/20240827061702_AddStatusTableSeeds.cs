using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusTableSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
