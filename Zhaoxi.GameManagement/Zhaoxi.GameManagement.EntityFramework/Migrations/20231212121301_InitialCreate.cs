using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Zhaoxi.GameManagement.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountType = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nickname = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Classes = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PlayerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Account", "AccountType", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("60ac4d21-2128-4121-ad9a-8600eb4f50b6"), "mw2021", "Free", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6126) },
                    { new Guid("b086fdbe-af78-408a-ac13-bc769c997116"), "dc2021", "Free", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6140) }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "Classes", "DateCreated", "Level", "Nickname", "PlayerId" },
                values: new object[,]
                {
                    { new Guid("20b5e46a-8219-4680-b20b-9da3bee9c8dc"), "Mage", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6779), 99, "Code Man", new Guid("60ac4d21-2128-4121-ad9a-8600eb4f50b6") },
                    { new Guid("3753d38f-7540-4c4e-8f40-acd340cedd2e"), "Death Knight", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6868), 90, "Batman", new Guid("b086fdbe-af78-408a-ac13-bc769c997116") },
                    { new Guid("98e12add-3637-4ca0-b29e-8641de0f1940"), "Warrior", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6864), 90, "Iron Man", new Guid("60ac4d21-2128-4121-ad9a-8600eb4f50b6") },
                    { new Guid("c4d04597-a30f-4b0f-8ee5-a947ca14e34f"), "Paladin", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6869), 99, "Superman", new Guid("b086fdbe-af78-408a-ac13-bc769c997116") },
                    { new Guid("c933fa97-f726-4b18-a033-4f62c004f67e"), "Druid", new DateTime(2023, 12, 12, 20, 13, 1, 354, DateTimeKind.Local).AddTicks(6866), 80, "Spider Man", new Guid("60ac4d21-2128-4121-ad9a-8600eb4f50b6") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Nickname",
                table: "Characters",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Account",
                table: "Players",
                column: "Account",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
