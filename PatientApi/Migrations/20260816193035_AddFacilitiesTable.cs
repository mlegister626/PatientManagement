using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilitiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacilityId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FacilityId",
                table: "Patients",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "FacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Facilities_FacilityId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Patients_FacilityId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "Patients");
        }
    }
}
