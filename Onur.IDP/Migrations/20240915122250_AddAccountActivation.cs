using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Onur.IDP.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountActivation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("483ae3d0-d691-4ac0-9d7c-d9f3589146be"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("648e16f3-30a4-4547-b7da-35628e9e74f8"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("657d1463-3803-4526-ad20-084e49a8dd03"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("7cdeb90d-0668-46ab-8a4d-84c59798b19c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("88ad1c93-a5fb-466a-820a-1d42f4e89744"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("8e169388-0897-448d-8019-63726bb9b907"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b047fe44-0377-446d-b01b-69041b830783"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("d1e45029-3457-48a7-a8ae-47151c7190c1"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "Users",
                type: "TEXT",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityCodeExpirationDate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("20992498-c9bf-48dc-99c5-bb14c0836c71"), "c599e640-3d66-4ae0-9ddd-1593f63a48cd", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("30ee9278-9bbe-4826-a76d-d4a2af049c00"), "4ab1a150-7b77-4c40-9c98-0ead68958f26", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("362c52af-8aee-4a12-bd06-a501e45379cf"), "9969f459-e1b3-4741-a81f-e2f0103975bb", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("5d29eedf-72a1-44c2-a1f0-55ad3ebdfc0c"), "4a022d88-f624-4753-8458-8c46901580eb", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("9575624f-8101-406f-9552-a61401d63623"), "f2be66c4-c3a9-4313-b8e4-719d5d72c24d", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("b777eb4a-bcba-4ffb-bc25-33f568c2367c"), "4d2fb1fd-9ce8-478b-a8c0-523b0835fbae", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("bff15ce9-8a66-4582-b962-fcaf93c9181a"), "db30bc73-d852-45da-9094-c0b7a92c6d69", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("d3a3d811-5410-42ce-803d-144f5da3e0c6"), "f0437b96-c01d-45ec-8fd1-93a6908afcc9", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                columns: new[] { "ConcurrencyStamp", "Email", "SecurityCode", "SecurityCodeExpirationDate" },
                values: new object[] { "c7c89d9f-d820-40f7-b82d-d5690ef087ad", "david@sp.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                columns: new[] { "ConcurrencyStamp", "Email", "SecurityCode", "SecurityCodeExpirationDate" },
                values: new object[] { "9d932227-68f1-472e-ad8a-6f9b573071de", "emma@sp.com", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("20992498-c9bf-48dc-99c5-bb14c0836c71"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("30ee9278-9bbe-4826-a76d-d4a2af049c00"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("362c52af-8aee-4a12-bd06-a501e45379cf"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("5d29eedf-72a1-44c2-a1f0-55ad3ebdfc0c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("9575624f-8101-406f-9552-a61401d63623"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b777eb4a-bcba-4ffb-bc25-33f568c2367c"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("bff15ce9-8a66-4582-b962-fcaf93c9181a"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("d3a3d811-5410-42ce-803d-144f5da3e0c6"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCodeExpirationDate",
                table: "Users");

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("483ae3d0-d691-4ac0-9d7c-d9f3589146be"), "c9f79d33-1498-4424-a5a4-a95b7600f179", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("648e16f3-30a4-4547-b7da-35628e9e74f8"), "c80f4c13-6b6a-40e7-9479-ce54303a9007", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("657d1463-3803-4526-ad20-084e49a8dd03"), "0a3bc699-5837-4746-86ef-e6cfc3ec6658", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("7cdeb90d-0668-46ab-8a4d-84c59798b19c"), "daf5a1a9-a4ac-40e9-a419-ac81a22e284d", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("88ad1c93-a5fb-466a-820a-1d42f4e89744"), "c9e609ff-d6fd-4c42-8eae-4ac056e3f3d4", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("8e169388-0897-448d-8019-63726bb9b907"), "fa0c66ae-0077-4f9b-8489-571e19f1b23d", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("b047fe44-0377-446d-b01b-69041b830783"), "91f8cab9-0a7a-4123-8715-fe7a41c27dfc", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("d1e45029-3457-48a7-a8ae-47151c7190c1"), "3f0da3a1-7ffb-402a-8daa-2aaaa4dd202f", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "3fbe0223-34df-46a1-b29d-62f1813addd6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "e1663fe9-8316-4182-843c-e12616a7ac87");
        }
    }
}
