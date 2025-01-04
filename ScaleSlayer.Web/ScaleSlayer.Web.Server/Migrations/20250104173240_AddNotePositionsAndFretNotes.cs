using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScaleSlayer.Web.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddNotePositionsAndFretNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotePositions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StringNumber = table.Column<int>(type: "integer", nullable: false),
                    FretNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotePositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    NotePositionId = table.Column<Guid>(type: "uuid", nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("014b06d9-d521-46d5-b9ca-a0c13af259b0"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAEFm2RudBYlc2fnF3E1Rh+1g4iav/4UR1o1dGyt+ZE+FNK2t/emhNsOdB58gclujudg==", "aalexander" },
                    { new Guid("04dc762f-2a9d-40ce-86db-0f11212e74e5"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEHLVBmhEkrrseBi/utLGj76AH40VjwSa7KvWh/vGQBPu3dVMy/IIMhyjzhOHyTsBuA==", "ntesla" },
                    { new Guid("1283d0c0-4772-4c6a-81cb-d02276e602be"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAEKkdaAgOGv1ytVKhpcoQN90PyqtYTOSFDx+T7SNDzYRGVFJchWq/PuHuFqK3D0Z9kw==", "mcurie" },
                    { new Guid("24e41bc9-f5a8-4c33-9a24-20093eaf0e93"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAECJBnhmuyaqh5RWA5Unn8uYvMmILirEn+j7SWi7978rKBsTDpRkPQbeVy1DXl15M+Q==", "ccleopatra" },
                    { new Guid("41828461-db19-41e7-8186-ee43f6416415"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAEA74e9UpRhlQsDlCNDItBrY8nKpMPXR9EGNtZnWi916/usaaxoMW6JJ+CBBFoYkXrA==", "wshakespeare" },
                    { new Guid("55c7e528-2ce9-469f-91da-905e2e28b625"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAEIvk0o3fyi9IYrgg5BCB9GHqSF34mX07Fb0uk9pEL683xuOvdnescw1Z1tQzmmAoyg==", "alincoln" },
                    { new Guid("84f1353d-31f1-4bc4-922f-73ad505358f5"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAECsVp4YCVud455hsgm+vy2xcwTRxU+olRccAskq+qErnDdyoPHAiHgXSvzXutK1aXw==", "aeinstein" },
                    { new Guid("a6041006-0234-4e91-86e9-5431c06467f0"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAEDT4DO0USMOK+4u4kzewVYPVFiIDy4/xGncBe6Un/osF0iiTGgI2GsaxtwlvgiEv5w==", "jcaesar" },
                    { new Guid("b2e83682-6ed8-491f-8aa2-d8180287d65b"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAENBU7nkOPg/DfUlHCALP9pi6Y7u7J5kM2dftGQ6zujEsSwd79dhZhQEcJGz77zL0uQ==", "wgenghis" },
                    { new Guid("ef2c643e-123f-436a-b0c8-b552a5ba2e9c"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEFWnl5wrNB1BgbXNb22P0lPiwigk4XU/nNCBcToWWrw486u0FcaKTpv/RtgIAwwEgA==", "ldavinci" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_NotePositionId",
                table: "Notes",
                column: "NotePositionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "NotePositions");
        }
    }
}
