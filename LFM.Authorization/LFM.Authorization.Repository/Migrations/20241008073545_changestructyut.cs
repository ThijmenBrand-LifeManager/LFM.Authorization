using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class changestructyut : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Roles_RoleId",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignments_Roles_RoleId",
                schema: "identity",
                table: "RoleAssignments",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Roles_RoleId",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignments_Roles_RoleId",
                schema: "identity",
                table: "RoleAssignments",
                column: "RoleId",
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
