using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementRepository.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasurementDataHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstValue = table.Column<double>(type: "float", nullable: false),
                    FirstUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondValue = table.Column<double>(type: "float", nullable: false),
                    SecondUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculatedResult = table.Column<double>(type: "float", nullable: false),
                    ComparisonResult = table.Column<bool>(type: "bit", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementDataHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementDataHistory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
