using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNS.API.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiltredWords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltredWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsWebsites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsWebsites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsWebsiteId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_NewsWebsites_NewsWebsiteId",
                        column: x => x.NewsWebsiteId,
                        principalTable: "NewsWebsites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchedNews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewsOneId = table.Column<int>(type: "int", nullable: false),
                    NewsTwoId = table.Column<int>(type: "int", nullable: false),
                    MatchingPercent = table.Column<int>(type: "int", nullable: false),
                    RowAddedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchedNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchedNews_News_NewsOneId",
                        column: x => x.NewsOneId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchedNews_News_NewsTwoId",
                        column: x => x.NewsTwoId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchedWords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchedNewsId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchedWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchedWords_MatchedNews_MatchedNewsId",
                        column: x => x.MatchedNewsId,
                        principalTable: "MatchedNews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchedNews_NewsOneId",
                table: "MatchedNews",
                column: "NewsOneId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchedNews_NewsTwoId",
                table: "MatchedNews",
                column: "NewsTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchedWords_MatchedNewsId",
                table: "MatchedWords",
                column: "MatchedNewsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsWebsiteId",
                table: "News",
                column: "NewsWebsiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiltredWords");

            migrationBuilder.DropTable(
                name: "MatchedWords");

            migrationBuilder.DropTable(
                name: "MatchedNews");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "NewsWebsites");
        }
    }
}
