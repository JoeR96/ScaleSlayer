using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoopLearner.Web.Server.Migrations
{
    /// <inheritdoc />
    public partial class updatenote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("03280f59-2df0-41b3-8d4d-115c84aea6c6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("164186cd-a55a-489f-990f-0f306a54e9bc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("16ba4d12-8187-40b1-a366-42eea5399a06"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2c84c8fb-1565-48f0-966b-f961f59b8cdb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("32b86004-c72d-42b9-9037-0bc3ca3df34d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59fec155-df3d-450a-b7b1-fed722a36e58"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("79bab614-7901-4bb0-8c83-612985175bd7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7e94c2a8-8da4-4951-b709-b950486bfa3e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e0ccbcce-77ab-43b0-9ce6-415a4ee9fe96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e912d3fe-602e-4d6d-9912-1a71818cc401"));

            migrationBuilder.RenameColumn(
                name: "NoteName",
                table: "Notes",
                newName: "Note");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("2ea77b24-787b-4ec5-8a7a-1233b7c6935d"), "cleopatra@example.com", "Cleopatra", "", "AQAAAAIAAYagAAAAEBPxnfKoQ2YA4BXc3Enk7M8PuucBY0pTZ0AJPXwpU+PK+sz950hJfX7mjASW6wSxqQ==", "ccleopatra" },
                    { new Guid("4a317b47-410c-4d9b-9155-5bcd606f4138"), "einstein.albert@example.com", "Albert", "Einstein", "AQAAAAIAAYagAAAAEP5eIk4PlTqJ2GP3wYNCkVbImmogutMbIySka4DI88V5Dr8Hrik771EjM40/Rt4DDg==", "aeinstein" },
                    { new Guid("60d3a9f7-d927-4662-b79d-6cfee3199690"), "caesar.julius@example.com", "Julius", "Caesar", "AQAAAAIAAYagAAAAEKNsYMMRBiW6kXnqC1myGoXHHpz42QOntH2KPJhTYYjsEsDQCtPjphpUJLqV630dhQ==", "jcaesar" },
                    { new Guid("6acaed02-6819-4621-8cab-780e80251f13"), "tesla.nikola@example.com", "Nikola", "Tesla", "AQAAAAIAAYagAAAAEFG1kqlPZcfc1VfklyUoA+9MjWlHMHUIH+3gD1MIbQ5/kqxl3Ft3gSZNOwJ4cyt5fA==", "ntesla" },
                    { new Guid("9dd17f36-4f05-4e4e-8dc4-7148f63868c9"), "curie.marie@example.com", "Marie", "Curie", "AQAAAAIAAYagAAAAEK9syNbBXARNCgIqHe5wH7FRLLaDKRo6ROs6oy7/B6Qvw+GnM1vjNVKQIa1xAutZww==", "mcurie" },
                    { new Guid("cd905263-fd06-4097-b6e7-8c5286d71722"), "genghis.khan@example.com", "Genghis", "Khan", "AQAAAAIAAYagAAAAEBorg17HcHsdYzcbn8ZcFTHdjWzVMW5afI04DkkPXm3n0uEqZ/2SmqTvT/BaF2TdAA==", "wgenghis" },
                    { new Guid("cdbf762b-5d6b-4e1e-8a46-c95857927838"), "lincoln.abraham@example.com", "Abraham", "Lincoln", "AQAAAAIAAYagAAAAECQzvtNEq0MDwof5HWHGKacXW0Dv+vqAHPFUBDqsmMDtFhyhvCSbIFYoHDzQrWk+Lw==", "alincoln" },
                    { new Guid("dd341f8b-5b8b-4bd5-8aaa-1c8ef3d6f649"), "alexander@example.com", "Alexander", "the Great", "AQAAAAIAAYagAAAAELJsT2IwGuS9fqzp03DmRm88Vk69O6iHO0GK613En98UzdVhOqzVomiACN8Zq9irrQ==", "aalexander" },
                    { new Guid("f133f2d3-2eb1-4e07-998c-fcfde273ed18"), "shakespeare.william@example.com", "William", "Shakespeare", "AQAAAAIAAYagAAAAEJYavB1fBfA2Bg8nbuR11WnHVSPtYolsPGqAAi1w3kc/jPioGpNrY+W+bKUnBKgcew==", "wshakespeare" },
                    { new Guid("fa4a4272-afeb-4ee6-911c-5a77ec8d9fd8"), "davinci.leonardo@example.com", "Leonardo", "da Vinci", "AQAAAAIAAYagAAAAEHCtSkIpDONKvWznJ6cSYPIluw6Ay5sbUOucGHrPYxLuYWbfP5A4WHW3GfaA++eLPA==", "ldavinci" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ea77b24-787b-4ec5-8a7a-1233b7c6935d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4a317b47-410c-4d9b-9155-5bcd606f4138"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("60d3a9f7-d927-4662-b79d-6cfee3199690"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6acaed02-6819-4621-8cab-780e80251f13"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9dd17f36-4f05-4e4e-8dc4-7148f63868c9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cd905263-fd06-4097-b6e7-8c5286d71722"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cdbf762b-5d6b-4e1e-8a46-c95857927838"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dd341f8b-5b8b-4bd5-8aaa-1c8ef3d6f649"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f133f2d3-2eb1-4e07-998c-fcfde273ed18"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fa4a4272-afeb-4ee6-911c-5a77ec8d9fd8"));

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Notes",
                newName: "NoteName");

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
        }
    }
}
