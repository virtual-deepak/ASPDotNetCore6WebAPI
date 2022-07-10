using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetCoreWebAPI.Migrations
{
    public partial class AddedMoreSeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 2, "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.", "Cathedral of Our Lady" });

            migrationBuilder.UpdateData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 2, "The the finest example of railway architecture in Belgium.", "Antwerp Central Station" });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 5, 3, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower" });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[] { 6, 3, "The world's largest museum.", "The Louvre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 3, "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.", "Eiffel Tower" });

            migrationBuilder.UpdateData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CityId", "Description", "Name" },
                values: new object[] { 3, "The world's largest museum.", "The Louvre" });
        }
    }
}
