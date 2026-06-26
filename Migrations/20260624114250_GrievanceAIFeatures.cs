using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient_Grievance_and_Complaint_Resolution.Migrations
{
    /// <inheritdoc />
    public partial class GrievanceAIFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "estimated_time_saved_hrs",
                schema: "grievance",
                table: "GRIEVANCES",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "grievance_summary",
                schema: "grievance",
                table: "GRIEVANCES",
                type: "varchar(1000)",
                unicode: false,
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matched_grievance_number",
                schema: "grievance",
                table: "GRIEVANCES",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estimated_time_saved_hrs",
                schema: "grievance",
                table: "GRIEVANCES");

            migrationBuilder.DropColumn(
                name: "grievance_summary",
                schema: "grievance",
                table: "GRIEVANCES");

            migrationBuilder.DropColumn(
                name: "matched_grievance_number",
                schema: "grievance",
                table: "GRIEVANCES");
        }
    }
}
