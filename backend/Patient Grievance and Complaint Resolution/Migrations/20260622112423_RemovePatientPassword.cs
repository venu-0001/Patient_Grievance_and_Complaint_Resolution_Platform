using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient_Grievance_and_Complaint_Resolution.Migrations
{
    /// <inheritdoc />
    public partial class RemovePatientPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passwordhash",
                schema: "core",
                table: "PATIENTS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Passwordhash",
                schema: "core",
                table: "PATIENTS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
