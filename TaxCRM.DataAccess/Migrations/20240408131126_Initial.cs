using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCRM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entrepreneurs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrepreneurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntrepreneurProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaxPayerNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Country = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    EntrepreneurId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntrepreneurProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntrepreneurProfiles_Entrepreneurs_EntrepreneurId",
                        column: x => x.EntrepreneurId,
                        principalTable: "Entrepreneurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value_Amount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Value_Currency = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntrepreneurProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incomes_EntrepreneurProfiles_EntrepreneurProfileId",
                        column: x => x.EntrepreneurProfileId,
                        principalTable: "EntrepreneurProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntrepreneurProfiles_EntrepreneurId",
                table: "EntrepreneurProfiles",
                column: "EntrepreneurId");

            migrationBuilder.CreateIndex(
                name: "IX_Incomes_EntrepreneurProfileId",
                table: "Incomes",
                column: "EntrepreneurProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incomes");

            migrationBuilder.DropTable(
                name: "EntrepreneurProfiles");

            migrationBuilder.DropTable(
                name: "Entrepreneurs");
        }
    }
}
