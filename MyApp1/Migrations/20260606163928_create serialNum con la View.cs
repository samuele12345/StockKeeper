using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp1.Migrations
{
    /// <inheritdoc />
    public partial class createserialNumconlaView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SerialNumber",
                keyColumn: "Id",
                keyValue: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SerialNumber",
                columns: new[] { "Id", "Name" },
                values: new object[] { 11, "HDP210" });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "IdSerial", "Name", "Price" },
                values: new object[] { 3, 11, "Cuffie", 0.0 });
        }
    }
}
