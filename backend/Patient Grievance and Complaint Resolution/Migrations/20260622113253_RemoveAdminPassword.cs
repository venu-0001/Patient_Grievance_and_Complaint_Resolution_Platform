using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient_Grievance_and_Complaint_Resolution.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                schema: "admin",
                table: "ADMIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "password",
                schema: "admin",
                table: "ADMIN",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
