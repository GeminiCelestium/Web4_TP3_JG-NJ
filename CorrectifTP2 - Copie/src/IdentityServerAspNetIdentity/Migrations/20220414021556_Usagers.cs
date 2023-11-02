using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServerAspNetIdentity.Migrations
{
    public partial class Usagers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "653bb917-ac53-464e-9e41-1be6fa6cf9e1", "682d6b0d-4dc8-4ff6-a9f2-d712a7c3c439", "manager", "MANAGER" },
                    { "b4a17d23-2b27-4a35-950a-d97382cb90f4", "a0f76f34-ed1d-4414-931d-ce47944b6c17", "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3", 0, "a3ea8183-42b1-4291-91eb-2dc0a4d0f8a9", "admin@cegeplimoilou.ca", false, "admin", "admin", false, null, "ADMIN@CEGEPLIMOILOU.CA", "ADMIN@CEGEPLIMOILOU.CA", "AQAAAAEAACcQAAAAEK7ccFhmMlq4beXZFHubx8pvvVRWwnlzg9Ek+T41AschddUSSQaRcFc6PsDuhmvtPg==", null, false, "6affda33-9457-41af-b242-a723ff464975", false, "admin@cegeplimoilou.ca" },
                    { "420b58eb-032f-4f21-8460-a1e3adeba461", 0, "1210a03a-b2da-4cd0-b4d0-59dbde7f43fb", "manager@cegeplimoilou.ca", false, "manager", "manager", false, null, "MANAGER@CEGEPLIMOILOU.CA", "MANAGER@CEGEPLIMOILOU.CA", "AQAAAAEAACcQAAAAED7X91sK/TX8DVoPKPFK4M3dmTDHFxodej6LKbRdO2Gtalxpfg/y7zXsb3bdumrm7w==", null, false, "3eef334e-8392-49ed-a8c8-231c2d149ba4", false, "manager@cegeplimoilou.ca" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "653bb917-ac53-464e-9e41-1be6fa6cf9e1", "420b58eb-032f-4f21-8460-a1e3adeba461" },
                    { "b4a17d23-2b27-4a35-950a-d97382cb90f4", "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "653bb917-ac53-464e-9e41-1be6fa6cf9e1", "420b58eb-032f-4f21-8460-a1e3adeba461" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b4a17d23-2b27-4a35-950a-d97382cb90f4", "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "653bb917-ac53-464e-9e41-1be6fa6cf9e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4a17d23-2b27-4a35-950a-d97382cb90f4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "420b58eb-032f-4f21-8460-a1e3adeba461");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3");
        }
    }
}
