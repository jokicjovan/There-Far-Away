using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Three_Far_Away.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_credentials", x => x.id);
                    table.ForeignKey(
                        name: "FK_credentials_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "arrangements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JourneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arrangements", x => x.id);
                    table.ForeignKey(
                        name: "FK_arrangements_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "attractions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JourneyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JourneyId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JourneyId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attractions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "journeys",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    transportation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journeys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: false),
                    latitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                    table.ForeignKey(
                        name: "FK_locations_journeys_id",
                        column: x => x.id,
                        principalTable: "journeys",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_arrangements_JourneyId",
                table: "arrangements",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_arrangements_UserId",
                table: "arrangements",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_attractions_JourneyId",
                table: "attractions",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_attractions_JourneyId1",
                table: "attractions",
                column: "JourneyId1");

            migrationBuilder.CreateIndex(
                name: "IX_attractions_JourneyId2",
                table: "attractions",
                column: "JourneyId2");

            migrationBuilder.CreateIndex(
                name: "IX_attractions_LocationId",
                table: "attractions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_credentials_UserId",
                table: "credentials",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_journeys_EndLocationId",
                table: "journeys",
                column: "EndLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_arrangements_journeys_JourneyId",
                table: "arrangements",
                column: "JourneyId",
                principalTable: "journeys",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_attractions_journeys_JourneyId",
                table: "attractions",
                column: "JourneyId",
                principalTable: "journeys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_attractions_journeys_JourneyId1",
                table: "attractions",
                column: "JourneyId1",
                principalTable: "journeys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_attractions_journeys_JourneyId2",
                table: "attractions",
                column: "JourneyId2",
                principalTable: "journeys",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_attractions_locations_LocationId",
                table: "attractions",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_journeys_locations_EndLocationId",
                table: "journeys",
                column: "EndLocationId",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_locations_journeys_id",
                table: "locations");

            migrationBuilder.DropTable(
                name: "arrangements");

            migrationBuilder.DropTable(
                name: "attractions");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "journeys");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
