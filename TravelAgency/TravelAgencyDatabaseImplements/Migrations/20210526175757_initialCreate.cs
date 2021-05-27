using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplements.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideName = table.Column<string>(nullable: false),
                    Cost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizators",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIO = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: false),
                    Fullname = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageInfos",
                columns: table => new
                {
                    MessageId = table.Column<string>(nullable: false),
                    OrganizatorId = table.Column<int>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    DateDelivery = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageInfos", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessageInfos_Organizators_OrganizatorId",
                        column: x => x.OrganizatorId,
                        principalTable: "Organizators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizatorId = table.Column<int>(nullable: false),
                    PlaceName = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Organizators_OrganizatorId",
                        column: x => x.OrganizatorId,
                        principalTable: "Organizators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripsGuides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideId = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripsGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripsGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripsGuides_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    DatePayment = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateGroup = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripsPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<int>(nullable: false),
                    TripId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripsPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripsPlaces_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripsPlaces_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GuidesExcursions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideId = table.Column<int>(nullable: false),
                    ExcursionId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuidesExcursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuidesExcursions_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuidesExcursions_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToursExcursions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(nullable: false),
                    ExcursionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToursExcursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToursExcursions_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToursExcursions_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsPlaces_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsPlaces_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToursGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToursGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToursGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToursGroups_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_UserId",
                table: "Excursions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UserId",
                table: "Groups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPlaces_GroupId",
                table: "GroupsPlaces",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsPlaces_PlaceId",
                table: "GroupsPlaces",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_GuidesExcursions_ExcursionId",
                table: "GuidesExcursions",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_GuidesExcursions_GuideId",
                table: "GuidesExcursions",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageInfos_OrganizatorId",
                table: "MessageInfos",
                column: "OrganizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_OrganizatorId",
                table: "Places",
                column: "OrganizatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ToursExcursions_ExcursionId",
                table: "ToursExcursions",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_ToursExcursions_TourId",
                table: "ToursExcursions",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_ToursGroups_GroupId",
                table: "ToursGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ToursGroups_TourId",
                table: "ToursGroups",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TripsGuides_GuideId",
                table: "TripsGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_TripsGuides_TripId",
                table: "TripsGuides",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripsPlaces_PlaceId",
                table: "TripsPlaces",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TripsPlaces_TripId",
                table: "TripsPlaces",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsPlaces");

            migrationBuilder.DropTable(
                name: "GuidesExcursions");

            migrationBuilder.DropTable(
                name: "MessageInfos");

            migrationBuilder.DropTable(
                name: "ToursExcursions");

            migrationBuilder.DropTable(
                name: "ToursGroups");

            migrationBuilder.DropTable(
                name: "TripsGuides");

            migrationBuilder.DropTable(
                name: "TripsPlaces");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizators");
        }
    }
}
