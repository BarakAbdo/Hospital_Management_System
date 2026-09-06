using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class allp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorName",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientName",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientName",
                table: "Appointments",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "DoctorName",
                table: "Appointments",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientName",
                table: "Appointments",
                newName: "IX_Appointments_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorName",
                table: "Appointments",
                newName: "IX_Appointments_DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Appointments",
                newName: "PatientName");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Appointments",
                newName: "DoctorName");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                newName: "IX_Appointments_PatientName");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                newName: "IX_Appointments_DoctorName");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorName",
                table: "Appointments",
                column: "DoctorName",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientName",
                table: "Appointments",
                column: "PatientName",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
