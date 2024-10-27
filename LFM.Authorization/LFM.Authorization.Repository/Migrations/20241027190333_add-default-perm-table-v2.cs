using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class adddefaultpermtablev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefaultRolePermissions",
                schema: "identity",
                columns: table => new
                {
                    Role = table.Column<string>(type: "text", nullable: false),
                    PermissionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultRolePermissions", x => new { x.Role, x.PermissionName });
                    table.ForeignKey(
                        name: "FK_DefaultRolePermissions_Permissions_PermissionName",
                        column: x => x.PermissionName,
                        principalSchema: "identity",
                        principalTable: "Permissions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefaultRolePermissions_PermissionName",
                schema: "identity",
                table: "DefaultRolePermissions",
                column: "PermissionName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultRolePermissions",
                schema: "identity");
        }
    }
}
