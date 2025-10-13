using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrationService.Migrations
{
    public partial class Adds_User_Aggregate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferrerUserId = table.Column<long>(type: "bigint", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MobileNumberWithoutCountryCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NormalizedEmailAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsEmailAddressVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsMobileNumberVerified = table.Column<bool>(type: "bit", nullable: false),
                    LastChangedPasswordDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Locale = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PictureUrl = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    Location_AddressId = table.Column<long>(type: "bigint", nullable: true),
                    Location_Latitude = table.Column<decimal>(type: "decimal(18,15)", nullable: true),
                    Location_Longitude = table.Column<decimal>(type: "decimal(18,15)", nullable: true),
                    Location_Area_Id = table.Column<int>(type: "int", nullable: true),
                    Location_Area_Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    AverageRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RatingCount = table.Column<int>(type: "int", nullable: true),
                    Creator_Id = table.Column<long>(type: "bigint", nullable: true),
                    Creator_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RegistrationClientAppId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PasswordHash = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_User_ReferrerUserId",
                        column: x => x.ReferrerUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Other" },
                    { 4, "Prefer not to disclose" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderId",
                table: "User",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MobileNumber",
                table: "User",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_MobileNumberWithoutCountryCode",
                table: "User",
                column: "MobileNumberWithoutCountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_User_NormalizedEmailAddress",
                table: "User",
                column: "NormalizedEmailAddress",
                unique: true,
                filter: "[NormalizedEmailAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_ReferrerUserId",
                table: "User",
                column: "ReferrerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Gender");
        }
    }
}
