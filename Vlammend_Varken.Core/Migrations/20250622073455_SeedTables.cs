using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vlammend_Varken.Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_mergedTables",
                table: "mergedTables");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "mergedTables",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mergedTables",
                table: "mergedTables",
                column: "Id");

            migrationBuilder.InsertData(
                table: "tables",
                columns: new[] { "Id", "GroupSize", "Status", "TableNumber" },
                values: new object[,]
                {
                    { 1, 2, 0, 1 },
                    { 2, 2, 0, 2 },
                    { 3, 2, 0, 3 },
                    { 4, 2, 0, 4 },
                    { 5, 2, 0, 5 },
                    { 6, 2, 0, 6 },
                    { 7, 2, 0, 7 },
                    { 8, 2, 0, 8 },
                    { 9, 2, 0, 9 },
                    { 10, 2, 0, 10 },
                    { 11, 2, 0, 11 },
                    { 12, 2, 0, 12 },
                    { 13, 2, 0, 13 },
                    { 14, 2, 0, 14 },
                    { 15, 2, 0, 15 },
                    { 16, 2, 0, 16 },
                    { 17, 2, 0, 17 },
                    { 18, 2, 0, 18 },
                    { 19, 2, 0, 19 },
                    { 20, 2, 0, 20 },
                    { 21, 2, 0, 21 },
                    { 22, 2, 0, 22 },
                    { 23, 2, 0, 23 },
                    { 24, 2, 0, 24 },
                    { 25, 2, 0, 25 },
                    { 26, 2, 0, 26 },
                    { 27, 2, 0, 27 },
                    { 28, 2, 0, 28 },
                    { 29, 2, 0, 29 },
                    { 30, 2, 0, 30 },
                    { 31, 2, 0, 31 },
                    { 32, 2, 0, 32 },
                    { 33, 2, 0, 33 },
                    { 34, 2, 0, 34 },
                    { 35, 2, 0, 35 },
                    { 36, 2, 0, 36 },
                    { 37, 2, 0, 37 },
                    { 38, 2, 0, 38 },
                    { 39, 2, 0, 39 },
                    { 40, 2, 0, 40 },
                    { 41, 2, 0, 41 },
                    { 42, 2, 0, 42 },
                    { 43, 2, 0, 43 },
                    { 44, 2, 0, 44 },
                    { 45, 2, 0, 45 },
                    { 46, 2, 0, 46 },
                    { 47, 2, 0, 47 },
                    { 48, 2, 0, 48 },
                    { 49, 2, 0, 49 },
                    { 50, 2, 0, 50 },
                    { 51, 2, 0, 51 },
                    { 52, 2, 0, 52 },
                    { 53, 2, 0, 53 },
                    { 54, 2, 0, 54 },
                    { 55, 2, 0, 55 },
                    { 56, 2, 0, 56 },
                    { 57, 2, 0, 57 },
                    { 58, 2, 0, 58 },
                    { 59, 2, 0, 59 },
                    { 60, 2, 0, 60 },
                    { 61, 2, 0, 61 },
                    { 62, 2, 0, 62 },
                    { 63, 2, 0, 63 },
                    { 64, 2, 0, 64 },
                    { 65, 2, 0, 65 },
                    { 66, 2, 0, 66 },
                    { 67, 2, 0, 67 },
                    { 68, 2, 0, 68 },
                    { 69, 2, 0, 69 },
                    { 70, 2, 0, 70 },
                    { 71, 2, 0, 71 },
                    { 72, 2, 0, 72 },
                    { 73, 2, 0, 73 },
                    { 74, 2, 0, 74 },
                    { 75, 2, 0, 75 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_mergedTables_MergedTableId",
                table: "mergedTables",
                column: "MergedTableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_mergedTables",
                table: "mergedTables");

            migrationBuilder.DropIndex(
                name: "IX_mergedTables_MergedTableId",
                table: "mergedTables");

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "tables",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "mergedTables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mergedTables",
                table: "mergedTables",
                column: "MergedTableId");
        }
    }
}
