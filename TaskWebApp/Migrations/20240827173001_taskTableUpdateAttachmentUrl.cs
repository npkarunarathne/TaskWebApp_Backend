using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskWebApp.Migrations
{
    /// <inheritdoc />
    public partial class taskTableUpdateAttachmentUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentUrl",
                table: "TaskItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "TaskItemStatus",
                columns: new[] { "Id", "DisplayOrder", "Status" },
                values: new object[,]
                {
                    { "59359278-0e7d-4da8-a410-8e94607fa13a", 2, "In Progress" },
                    { "e90dbba1-e4a0-4254-bad9-9d3ea888be32", 3, "Done" },
                    { "e92f0ecd-bec3-4d94-b6d9-be0ac612c3cc", 1, "Todo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "59359278-0e7d-4da8-a410-8e94607fa13a");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "e90dbba1-e4a0-4254-bad9-9d3ea888be32");

            migrationBuilder.DeleteData(
                table: "TaskItemStatus",
                keyColumn: "Id",
                keyValue: "e92f0ecd-bec3-4d94-b6d9-be0ac612c3cc");

            migrationBuilder.AlterColumn<string>(
                name: "AttachmentUrl",
                table: "TaskItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
