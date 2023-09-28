using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace outreach3.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissionMaps",
                columns: table => new
                {
                    MissionMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapZoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapTilt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MapHeading = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionMaps", x => x.MissionMapId);
                });

            migrationBuilder.CreateTable(
                name: "PrayerRequest",
                columns: table => new
                {
                    PrayerRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    AcquaintanceId = table.Column<int>(type: "int", nullable: false),
                    MinistryId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerRequest", x => x.PrayerRequestId);
                });

            migrationBuilder.CreateTable(
                name: "MapMarker",
                columns: table => new
                {
                    MapMarkerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionMapId = table.Column<int>(type: "int", nullable: true),
                    markerLatLng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    markerIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    number = table.Column<int>(type: "int", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    markerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    markerArea = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapMarker", x => x.MapMarkerId);
                    table.ForeignKey(
                        name: "FK_MapMarker_MissionMaps_MissionMapId",
                        column: x => x.MissionMapId,
                        principalTable: "MissionMaps",
                        principalColumn: "MissionMapId");
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    MissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MissionMapId = table.Column<int>(type: "int", nullable: false),
                    DateLastCompleted = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.MissionId);
                    table.ForeignKey(
                        name: "FK_Missions_MissionMaps_MissionMapId",
                        column: x => x.MissionMapId,
                        principalTable: "MissionMaps",
                        principalColumn: "MissionMapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrayerFollowUp",
                columns: table => new
                {
                    PrayerFollowUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrayerRequestId = table.Column<int>(type: "int", nullable: false),
                    FollowUpText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrayerFollowUp", x => x.PrayerFollowUpId);
                    table.ForeignKey(
                        name: "FK_PrayerFollowUp_PrayerRequest_PrayerRequestId",
                        column: x => x.PrayerRequestId,
                        principalTable: "PrayerRequest",
                        principalColumn: "PrayerRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MissionHistory",
                columns: table => new
                {
                    MissionHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "date", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "date", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionHistory", x => x.MissionHistoryId);
                    table.ForeignKey(
                        name: "FK_MissionHistory_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resident",
                columns: table => new
                {
                    ResidentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProtected = table.Column<bool>(type: "bit", nullable: false),
                    Apt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMember = table.Column<bool>(type: "bit", nullable: false),
                    MarkerColorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    MissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resident", x => x.ResidentId);
                    table.ForeignKey(
                        name: "FK_Resident_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "MissionId");
                });

            migrationBuilder.CreateTable(
                name: "Aquaintance",
                columns: table => new
                {
                    AquaintanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcquaintanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShortDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrayerNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestBusService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrayerHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResidentId = table.Column<int>(type: "int", nullable: false),
                    RequestPrayerRequestId = table.Column<int>(type: "int", nullable: false),
                    One = table.Column<bool>(type: "bit", nullable: false),
                    Two = table.Column<bool>(type: "bit", nullable: false),
                    Three = table.Column<bool>(type: "bit", nullable: false),
                    Four = table.Column<bool>(type: "bit", nullable: false),
                    Five = table.Column<bool>(type: "bit", nullable: false),
                    Six = table.Column<bool>(type: "bit", nullable: false),
                    Seven = table.Column<bool>(type: "bit", nullable: false),
                    Eight = table.Column<bool>(type: "bit", nullable: false),
                    Nine = table.Column<bool>(type: "bit", nullable: false),
                    Ten = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aquaintance", x => x.AquaintanceId);
                    table.ForeignKey(
                        name: "FK_Aquaintance_PrayerRequest_RequestPrayerRequestId",
                        column: x => x.RequestPrayerRequestId,
                        principalTable: "PrayerRequest",
                        principalColumn: "PrayerRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aquaintance_Resident_ResidentId",
                        column: x => x.ResidentId,
                        principalTable: "Resident",
                        principalColumn: "ResidentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aquaintance_RequestPrayerRequestId",
                table: "Aquaintance",
                column: "RequestPrayerRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Aquaintance_ResidentId",
                table: "Aquaintance",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_MapMarker_MissionMapId",
                table: "MapMarker",
                column: "MissionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionHistory_MissionId",
                table: "MissionHistory",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionMapId",
                table: "Missions",
                column: "MissionMapId");

            migrationBuilder.CreateIndex(
                name: "IX_PrayerFollowUp_PrayerRequestId",
                table: "PrayerFollowUp",
                column: "PrayerRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Resident_MissionId",
                table: "Resident",
                column: "MissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aquaintance");

            migrationBuilder.DropTable(
                name: "MapMarker");

            migrationBuilder.DropTable(
                name: "MissionHistory");

            migrationBuilder.DropTable(
                name: "PrayerFollowUp");

            migrationBuilder.DropTable(
                name: "Resident");

            migrationBuilder.DropTable(
                name: "PrayerRequest");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "MissionMaps");
        }
    }
}
