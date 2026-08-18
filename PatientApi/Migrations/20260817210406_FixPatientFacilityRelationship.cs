using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApi.Migrations
{
    /// <inheritdoc />
    public partial class FixPatientFacilityRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Facility",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Patients",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients");

            migrationBuilder.AlterColumn<int>(
                name: "FacilityId",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Facility",
                table: "Patients",
                type: "varchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "FacilityId");
        }
    }
}
