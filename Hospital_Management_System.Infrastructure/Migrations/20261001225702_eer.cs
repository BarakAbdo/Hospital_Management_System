using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentFiles_Departments_DepartmentId",
                table: "DepartmentFiles");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentFiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentFiles_Departments_DepartmentId",
                table: "DepartmentFiles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentFiles_Departments_DepartmentId",
                table: "DepartmentFiles");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "DepartmentFiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentFiles_Departments_DepartmentId",
                table: "DepartmentFiles",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
