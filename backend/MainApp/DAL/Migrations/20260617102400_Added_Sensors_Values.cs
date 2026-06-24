using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Added_Sensors_Values : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SensorsMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    SensorId = table.Column<long>(type: "bigint", nullable: false),
                    SensorName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorsMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SensorValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SensorsMapId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SensorValues_SensorsMap_SensorsMapId",
                        column: x => x.SensorsMapId,
                        principalTable: "SensorsMap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorsMap_Id",
                table: "SensorsMap",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SensorsMap_Type_SensorId",
                table: "SensorsMap",
                columns: new[] { "Type", "SensorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SensorValues_SensorsMapId",
                table: "SensorValues",
                column: "SensorsMapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorValues");

            migrationBuilder.DropTable(
                name: "SensorsMap");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");
        }
    }
}
