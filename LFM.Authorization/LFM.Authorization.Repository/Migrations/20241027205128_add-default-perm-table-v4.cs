using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class adddefaultpermtablev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleName1",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleScope1",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_RoleName1_RoleScope1",
                schema: "identity",
                table: "RoleAssignments",
                columns: new[] { "RoleName1", "RoleScope1" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignments_Roles_RoleName1_RoleScope1",
                schema: "identity",
                table: "RoleAssignments",
                columns: new[] { "RoleName1", "RoleScope1" },
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumns: new[] { "Name", "Scope" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Roles_RoleName1_RoleScope1",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RoleAssignments_RoleName1_RoleScope1",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropColumn(
                name: "RoleName1",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropColumn(
                name: "RoleScope1",
                schema: "identity",
                table: "RoleAssignments");
        }
    }
}
