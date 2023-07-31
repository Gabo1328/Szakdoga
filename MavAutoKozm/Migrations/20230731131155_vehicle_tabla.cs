using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MavAutoKozm.Migrations
{
    public partial class vehicle_tabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AppUsers_AppUserID",
                table: "Vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicle");

            migrationBuilder.RenameTable(
                name: "Vehicle",
                newName: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Vehicles",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicle_AppUserID",
                table: "Vehicles",
                newName: "IX_Vehicles_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_AppUsers_AppUserId",
                table: "Vehicles",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_AppUsers_AppUserId",
                table: "Vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vehicles",
                table: "Vehicles");

            migrationBuilder.RenameTable(
                name: "Vehicles",
                newName: "Vehicle");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Vehicle",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_AppUserId",
                table: "Vehicle",
                newName: "IX_Vehicle_AppUserID");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserID",
                table: "Vehicle",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vehicle",
                table: "Vehicle",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AppUsers_AppUserID",
                table: "Vehicle",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "ID");
        }
    }
}
