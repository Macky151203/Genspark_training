using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace firstapi.Migrations
{
    /// <inheritdoc />
    public partial class fluent_api : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appointments_doctors_DoctorId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_appointments_patients_PatientId",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_doctorSpecialities_doctors_DoctorId",
                table: "doctorSpecialities");

            migrationBuilder.DropForeignKey(
                name: "FK_doctorSpecialities_specialities_SpecialityId",
                table: "doctorSpecialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doctorSpecialities",
                table: "doctorSpecialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_appointments",
                table: "appointments");

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "doctorSpecialities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "doctorSpecialities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "appointments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_doctorSpecialities",
                table: "doctorSpecialities",
                column: "SerialNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppointmentNumber",
                table: "appointments",
                column: "AppointmentNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor",
                table: "appointments",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient",
                table: "appointments",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Speciality_Doctor",
                table: "doctorSpecialities",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Speciality_Spec",
                table: "doctorSpecialities",
                column: "SpecialityId",
                principalTable: "specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient",
                table: "appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Speciality_Doctor",
                table: "doctorSpecialities");

            migrationBuilder.DropForeignKey(
                name: "FK_Speciality_Spec",
                table: "doctorSpecialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_doctorSpecialities",
                table: "doctorSpecialities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppointmentNumber",
                table: "appointments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "doctorSpecialities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "doctorSpecialities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "appointments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_doctorSpecialities",
                table: "doctorSpecialities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_appointments",
                table: "appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_doctors_DoctorId",
                table: "appointments",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_appointments_patients_PatientId",
                table: "appointments",
                column: "PatientId",
                principalTable: "patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_doctorSpecialities_doctors_DoctorId",
                table: "doctorSpecialities",
                column: "DoctorId",
                principalTable: "doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_doctorSpecialities_specialities_SpecialityId",
                table: "doctorSpecialities",
                column: "SpecialityId",
                principalTable: "specialities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
