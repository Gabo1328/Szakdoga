using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MavAutoKozm.Migrations
{
    public partial class orders_tabla_letrehozas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Outer = table.Column<bool>(type: "bit", nullable: false),
                    Inner = table.Column<bool>(type: "bit", nullable: false),
                    Polish = table.Column<bool>(type: "bit", nullable: false),
                    Wax = table.Column<bool>(type: "bit", nullable: false),
                    Ceramic = table.Column<bool>(type: "bit", nullable: false),
                    Ppf = table.Column<bool>(type: "bit", nullable: false),
                    Quality = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
