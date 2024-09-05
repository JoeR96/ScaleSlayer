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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("0d05e718-30f1-429e-af8a-21bcc20d7484"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAEIlXdCfPlEGjk3dAGxq9BWM3CYWlIlLeXTI/RmqzYk4lVYBSivrHoEcgQpU2HiQOdw==", "aeinstein" },
                    { new Guid("0e12a625-3b26-4928-98ae-cc37eeb552f1"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEIn2SZW/exQ1mU6cN71kbmcRsCwurwiKfbWJYwODXkRmvNvyWfPS491C3Z8iubVh3Q==", "ldavinci" },
                    { new Guid("6492ee0e-88a6-4f4d-b60b-681e4982d736"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAEJ1dq5AxzM47r0JA9TEtANBOFl7ez+KKylz7YepVhWH7zv1SsDsXMSjRl5gJFZjaAQ==", "aalexander" },
                    { new Guid("77a03a5c-cdcb-4f1f-afce-e4dc88b5832d"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAELu7pJN9YHyLktJRvRur2L0kp+MHRsvRrXAnJiYjAHN1tLbdEcBTKZ6Zgdh2tD05EA==", "jcaesar" },
                    { new Guid("82d7c2a7-a411-425d-af93-6b03ac870f60"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAEKit5WJksV+EgjWYTaELRjSUfJldUEb2jEfOdm51J4bXbVuRKzWYyTDCBXa0qrvLog==", "wgenghis" },
                    { new Guid("bfedcc71-2bc5-43ce-9eb9-42c40d343ece"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAED/OVJPyGLJr5dTYs7Rdmg28zTKX/bYKfRUtUfSGCP9Ml5SFOgAA375epOvxioShpA==", "ccleopatra" },
                    { new Guid("d3131eeb-ef3d-479f-818e-f3d211c29dfc"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAEASLSjfD6irf5upaXl9r1o5R7HOIo6VjedgQxEg63sVIqV7VhJ7/SVyX7azBlU59JQ==", "alincoln" },
                    { new Guid("e4be5a33-3e9c-489f-b1d5-657cced38d60"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAEH6qF2ixPcfwrOxqsI7e60p32IPR7XtYW96HOWZapPIfVX21+HLimzdmyXOb0u7OZA==", "wshakespeare" },
                    { new Guid("f3bd68b1-dc3b-4157-bde4-c5f191d73f11"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEEse5wsM/GeLYa96j3X4KDS7McJjKyg+cMbvLfXuUEXd6xeXATYF7lLkX5przQQKTQ==", "ntesla" },
                    { new Guid("fd918dab-5e99-4e62-8a54-85fb3a1cb609"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAEI9sCXYpc5HM2z6LnR9IhauJjBia/vxRbVXhsFo7athp28FD33K1bzIqtre11T5ezg==", "mcurie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
