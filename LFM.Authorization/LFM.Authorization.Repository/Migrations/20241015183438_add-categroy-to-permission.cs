using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LFM.Authorization.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addcategroytopermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "identity",
                table: "Permissions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "identity",
                table: "Permissions");
        }
    }
}
