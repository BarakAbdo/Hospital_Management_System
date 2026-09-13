using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class hfhfh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoles_Permissions_PermissionsId",
                table: "PermissionRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoles_Roles_RolesId",
                table: "PermissionRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoles_PermissionsId",
                table: "PermissionRoles");

            migrationBuilder.DropColumn(
                name: "PermissionsId",
                table: "PermissionRoles");

            migrationBuilder.RenameColumn(
                name: "PemissionsId",
                table: "PermissionRoles",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "PermissionRoles",
                newName: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoles_PermissionId",
                table: "PermissionRoles",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoles_Permissions_PermissionId",
                table: "PermissionRoles",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoles_Roles_RoleId",
                table: "PermissionRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoles_Permissions_PermissionId",
                table: "PermissionRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRoles_Roles_RoleId",
                table: "PermissionRoles");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRoles_PermissionId",
                table: "PermissionRoles");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "PermissionRoles",
                newName: "PemissionsId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "PermissionRoles",
                newName: "RolesId");

            migrationBuilder.AddColumn<int>(
                name: "PermissionsId",
                table: "PermissionRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRoles_PermissionsId",
                table: "PermissionRoles",
                column: "PermissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoles_Permissions_PermissionsId",
                table: "PermissionRoles",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRoles_Roles_RolesId",
                table: "PermissionRoles",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
