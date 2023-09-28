using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aquaintance_Resident_ResidentId",
                table: "Aquaintance");

            migrationBuilder.DropForeignKey(
                name: "FK_Resident_Missions_MissionId",
                table: "Resident");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resident",
                table: "Resident");

            migrationBuilder.DropIndex(
                name: "IX_Resident_MissionId",
                table: "Resident");

            migrationBuilder.RenameTable(
                name: "Resident",
                newName: "Residents");

            migrationBuilder.AlterColumn<string>(
                name: "MissionId",
                table: "Residents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissionId1",
                table: "Residents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Residents",
                table: "Residents",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Residents_MissionId1",
                table: "Residents",
                column: "MissionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Aquaintance_Residents_ResidentId",
                table: "Aquaintance",
                column: "ResidentId",
                principalTable: "Residents",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residents_Missions_MissionId1",
                table: "Residents",
                column: "MissionId1",
                principalTable: "Missions",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aquaintance_Residents_ResidentId",
                table: "Aquaintance");

            migrationBuilder.DropForeignKey(
                name: "FK_Residents_Missions_MissionId1",
                table: "Residents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Residents",
                table: "Residents");

            migrationBuilder.DropIndex(
                name: "IX_Residents_MissionId1",
                table: "Residents");

            migrationBuilder.DropColumn(
                name: "MissionId1",
                table: "Residents");

            migrationBuilder.RenameTable(
                name: "Residents",
                newName: "Resident");

            migrationBuilder.AlterColumn<int>(
                name: "MissionId",
                table: "Resident",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resident",
                table: "Resident",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Resident_MissionId",
                table: "Resident",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aquaintance_Resident_ResidentId",
                table: "Aquaintance",
                column: "ResidentId",
                principalTable: "Resident",
                principalColumn: "ResidentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resident_Missions_MissionId",
                table: "Resident",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "MissionId");
        }
    }
}
