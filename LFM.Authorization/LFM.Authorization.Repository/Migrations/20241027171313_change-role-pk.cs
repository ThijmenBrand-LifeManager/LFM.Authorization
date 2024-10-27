using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class changerolepk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LfmRolePermission_Roles_RolesId",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Roles_RoleId",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RoleAssignments_RoleId",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LfmRolePermission",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_LfmRolePermission_RolesId",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "identity",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "identity",
                table: "RoleAssignments",
                newName: "RoleScope");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                schema: "identity",
                table: "LfmRolePermission",
                newName: "RolesScope");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "identity",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "identity",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RolesName",
                schema: "identity",
                table: "LfmRolePermission",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles",
                columns: new[] { "Name", "Scope" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LfmRolePermission",
                schema: "identity",
                table: "LfmRolePermission",
                columns: new[] { "PermissionsName", "RolesName", "RolesScope" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_RoleName_RoleScope",
                schema: "identity",
                table: "RoleAssignments",
                columns: new[] { "RoleName", "RoleScope" });

            migrationBuilder.CreateIndex(
                name: "IX_LfmRolePermission_RolesName_RolesScope",
                schema: "identity",
                table: "LfmRolePermission",
                columns: new[] { "RolesName", "RolesScope" });

            migrationBuilder.AddForeignKey(
                name: "FK_LfmRolePermission_Roles_RolesName_RolesScope",
                schema: "identity",
                table: "LfmRolePermission",
                columns: new[] { "RolesName", "RolesScope" },
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumns: new[] { "Name", "Scope" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAssignments_Roles_RoleName_RoleScope",
                schema: "identity",
                table: "RoleAssignments",
                columns: new[] { "RoleName", "RoleScope" },
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumns: new[] { "Name", "Scope" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LfmRolePermission_Roles_RolesName_RolesScope",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAssignments_Roles_RoleName_RoleScope",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RoleAssignments_RoleName_RoleScope",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LfmRolePermission",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropIndex(
                name: "IX_LfmRolePermission_RolesName_RolesScope",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.DropColumn(
                name: "RoleName",
                schema: "identity",
                table: "RoleAssignments");

            migrationBuilder.DropColumn(
                name: "RolesName",
                schema: "identity",
                table: "LfmRolePermission");

            migrationBuilder.RenameColumn(
                name: "RoleScope",
                schema: "identity",
                table: "RoleAssignments",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "RolesScope",
                schema: "identity",
                table: "LfmRolePermission",
                newName: "RolesId");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "identity",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "identity",
                table: "Roles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "identity",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Scope",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "identity",
                table: "RoleAssignments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text")
                .Annotation("Relational:ColumnOrder", 1)
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "identity",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LfmRolePermission",
                schema: "identity",
                table: "LfmRolePermission",
                columns: new[] { "PermissionsName", "RolesId" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_RoleId",
                schema: "identity",
                table: "RoleAssignments",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LfmRolePermission_RolesId",
                schema: "identity",
                table: "LfmRolePermission",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_LfmRolePermission_Roles_RolesId",
                schema: "identity",
                table: "LfmRolePermission",
                column: "RolesId",
                principalSchema: "identity",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
