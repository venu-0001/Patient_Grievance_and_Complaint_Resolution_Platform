using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Patient_Grievance_and_Complaint_Resolution.Migrations
{
    /// <inheritdoc />
    public partial class AddUserandRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "core",
                table: "PATIENTS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "grievance",
                table: "INVESTIGATORS",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "admin",
                table: "ADMIN",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_USERS_ROLE_ID",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PATIENTS_UserId",
                schema: "core",
                table: "PATIENTS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_INVESTIGATORS_UserId",
                schema: "grievance",
                table: "INVESTIGATORS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ADMIN_UserId",
                schema: "admin",
                table: "ADMIN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ADMIN_USER_ID",
                schema: "admin",
                table: "ADMIN",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_INVESTIGATOR_USER_ID",
                schema: "grievance",
                table: "INVESTIGATORS",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PATIENT_USER_ID",
                schema: "core",
                table: "PATIENTS",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADMIN_USER_ID",
                schema: "admin",
                table: "ADMIN");

            migrationBuilder.DropForeignKey(
                name: "FK_INVESTIGATOR_USER_ID",
                schema: "grievance",
                table: "INVESTIGATORS");

            migrationBuilder.DropForeignKey(
                name: "FK_PATIENT_USER_ID",
                schema: "core",
                table: "PATIENTS");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_PATIENTS_UserId",
                schema: "core",
                table: "PATIENTS");

            migrationBuilder.DropIndex(
                name: "IX_INVESTIGATORS_UserId",
                schema: "grievance",
                table: "INVESTIGATORS");

            migrationBuilder.DropIndex(
                name: "IX_ADMIN_UserId",
                schema: "admin",
                table: "ADMIN");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "core",
                table: "PATIENTS");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "grievance",
                table: "INVESTIGATORS");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "admin",
                table: "ADMIN");
        }
    }
}
