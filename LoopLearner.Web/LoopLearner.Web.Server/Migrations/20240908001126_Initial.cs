using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoopLearner.Web.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotePositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StringNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    FretNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Artist = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BPM = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedByUserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Songs_Users_LastModifiedByUserId",
                        column: x => x.LastModifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InstrumentParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstrumentName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentParts_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tab",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    InstrumentPartId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tab_InstrumentParts_InstrumentPartId",
                        column: x => x.InstrumentPartId,
                        principalTable: "InstrumentParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RootNote = table.Column<string>(type: "TEXT", nullable: false),
                    ChordType = table.Column<string>(type: "TEXT", nullable: false),
                    ChordExtension = table.Column<string>(type: "TEXT", nullable: false),
                    TabId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chords_Tab_TabId",
                        column: x => x.TabId,
                        principalTable: "Tab",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NoteName = table.Column<string>(type: "TEXT", nullable: false),
                    NotePositionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TabId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_NotePositions_NotePositionId",
                        column: x => x.NotePositionId,
                        principalTable: "NotePositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Tab_TabId",
                        column: x => x.TabId,
                        principalTable: "Tab",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChordNote",
                columns: table => new
                {
                    ChordId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NoteId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChordNote", x => new { x.ChordId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_ChordNote_Chords_ChordId",
                        column: x => x.ChordId,
                        principalTable: "Chords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChordNote_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("03280f59-2df0-41b3-8d4d-115c84aea6c6"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAED1GbI9JFt6JFhkrHwaR/cXyJCrT3Fs07kMR7v8oeew2ej0jfidWF8/cN2MaW6YrnA==", "aalexander" },
                    { new Guid("164186cd-a55a-489f-990f-0f306a54e9bc"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEGkCoFQWyy40sDUoXWUBdmJdjdpUIEACRlBEw2rS+oEeKIZ0JNoAzMc9okpT+j3oeg==", "ldavinci" },
                    { new Guid("16ba4d12-8187-40b1-a366-42eea5399a06"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAEB1kRcuT11E8k+ivEgTHC0wATctqOTzxTqYqwR7CnqddQgvIYkj8ZEsmHq39Xhe8DA==", "ccleopatra" },
                    { new Guid("2c84c8fb-1565-48f0-966b-f961f59b8cdb"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEOIl62jkF6sh2nnsTmYtTUQ03HqPvTVI7P+Jg1xM+kwEDLOXYrkUKDY0h0ANbroBAg==", "ntesla" },
                    { new Guid("32b86004-c72d-42b9-9037-0bc3ca3df34d"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAEOF1DgPSj5J/z9yceOfXRHsH7/rf1i9mnIJl66feELVhz/JIMUbEHyaWMZKIRaFkdg==", "mcurie" },
                    { new Guid("59fec155-df3d-450a-b7b1-fed722a36e58"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAEFM3UboO36XEQCYexfK1L08dbn/5bl9cSPxN5QvO6PsP7ATydN++8AaHnLaw2JdWUA==", "wshakespeare" },
                    { new Guid("79bab614-7901-4bb0-8c83-612985175bd7"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAEEfW9z+/mabX822Xu9oLVe5pbh4fSQUvJi95ZPEyjWWaCbtLHLOFyw6dPILygQUI9w==", "alincoln" },
                    { new Guid("7e94c2a8-8da4-4951-b709-b950486bfa3e"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAEBOghTtNNea5lnOXAexhL40ZHFjCZAM94Fj60T7mBl4a3gEo3KX9G9I749fzqpEzTw==", "wgenghis" },
                    { new Guid("e0ccbcce-77ab-43b0-9ce6-415a4ee9fe96"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAEEXABvgGx6J/JHw1d/k31FFgZKyF4McA1YO6graSNN2mWSbp/42PkqVx/0BB7tL0VQ==", "aeinstein" },
                    { new Guid("e912d3fe-602e-4d6d-9912-1a71818cc401"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAENx2RXiFov8+B6xy4asTZ0ndXuoOsuxmAMLUb3wrq7Yhv0eNCm0JjzBpUdPtlUJxHg==", "jcaesar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChordNote_NoteId",
                table: "ChordNote",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chords_TabId",
                table: "Chords",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentParts_SongId",
                table: "InstrumentParts",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NotePositionId",
                table: "Notes",
                column: "NotePositionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TabId",
                table: "Notes",
                column: "TabId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_CreatedByUserId",
                table: "Songs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_LastModifiedByUserId",
                table: "Songs",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_InstrumentPartId",
                table: "Tab",
                column: "InstrumentPartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChordNote");

            migrationBuilder.DropTable(
                name: "Chords");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "NotePositions");

            migrationBuilder.DropTable(
                name: "Tab");

            migrationBuilder.DropTable(
                name: "InstrumentParts");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
