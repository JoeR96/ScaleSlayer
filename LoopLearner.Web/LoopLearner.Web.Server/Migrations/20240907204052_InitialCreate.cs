using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoopLearner.Web.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Extension = table.Column<string>(type: "TEXT", nullable: false),
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
                    { new Guid("03015f65-81f3-4459-84e3-0bc7317a9af2"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEEzMse4yMlTpEO3b1nLo3ZPv4pi1A3AO2UhOKXsKhTpeFF+UrvtpTH61z5Al1MEQbQ==", "ldavinci" },
                    { new Guid("29352334-6241-4959-93fa-96af9fb93e22"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEOKQVZdLfAvr/ot90jEg/3pp3DXFjzTXfrKV5FkSeAV/khBehrK0TWdA0tt9u6uZcw==", "ntesla" },
                    { new Guid("700df619-b907-4473-8c39-c68606a27688"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAEM2F6fKnc7N2Ge5orlv5KBqD+dorYvRmZ2XZNqU4rGLdqzvGI4PV3QpponEedVj1VA==", "alincoln" },
                    { new Guid("a16341a2-bcf3-4f1d-b083-d1b74d0eb281"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAELRKJpPlWLe3XFiBOGGA0YluB4ZhFFiXllCoHhNZQQtr2WpAGWLgZMuPwiIMXAVdng==", "aeinstein" },
                    { new Guid("ab843922-d6b6-4a5d-a7f8-1190fec9c702"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAEEK8VvM/VDNjjgJlF/iyjTLfb9Qo5U0Bup8B/LKR0XtyWi3yJi3AaNsHU1dYSciERA==", "wshakespeare" },
                    { new Guid("c882368a-7d59-4c49-8ffb-a515d3d43780"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAELWD/i/uC62nMsby8zncrPcFqoyuh3U8NjyYgY2CigyG/b8XB70JnO/WYUztPDnr3Q==", "mcurie" },
                    { new Guid("cd694460-d9ba-4f99-888d-f7748f9a690f"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAEMie/CLh0MTOYbuFysOn8/n0oLhLN/Alg1hTKvXCvuwVG+VVlKoN/WD159dvLYPUzw==", "aalexander" },
                    { new Guid("dd4bf13a-50ab-4a9b-b931-5d232e0bdb77"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAEGjMBmI0euCu1+nMahnJ2YvsV5/Vp/pdsceElRsmPYn4pnzFfmXnl5EIAnzETGac2g==", "jcaesar" },
                    { new Guid("ee50e2a3-7524-4cc9-abb8-14965fb35bb9"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAEF4mn3gHuqYCm9UD0Flms1Nk6R3t5jjB1pkIqSpbPVGdJdMOVIIYzH2J2qpEsijksw==", "ccleopatra" },
                    { new Guid("ef353976-8fdf-4cba-a70d-ca00c27226ce"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAELWqwsm7mLI4BR/HM1XwJ1y07nxj4b/jCGMPHaVqrkGoBott1D9Dc2kXo1+ExhkfmA==", "wgenghis" }
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
