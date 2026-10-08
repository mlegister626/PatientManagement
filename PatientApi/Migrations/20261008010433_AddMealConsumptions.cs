using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApi.Migrations
{
    /// <inheritdoc />
    public partial class AddMealConsumptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealConsumptions",
                columns: table => new
                {
                    MealConsumptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeliveredMealId = table.Column<int>(type: "int", nullable: false),
                    CaloriesEaten = table.Column<int>(type: "int", nullable: false),
                    DateEaten = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealConsumptions", x => x.MealConsumptionId);
                    table.ForeignKey(
                        name: "FK_MealConsumptions_MealDeliveries_DeliveredMealId",
                        column: x => x.DeliveredMealId,
                        principalTable: "MealDeliveries",
                        principalColumn: "MealDeliveryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MealConsumptions_DeliveredMealId",
                table: "MealConsumptions",
                column: "DeliveredMealId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealConsumptions");
        }
    }
}
