using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjadaTraineesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisorIdToTrainees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Supervisor",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.AddColumn<string>(
                name: "SupervisorId",
                schema: "HR",
                table: "Trainees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_SupervisorId",
                schema: "HR",
                table: "Trainees",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_AspNetUsers_SupervisorId",
                schema: "HR",
                table: "Trainees",
                column: "SupervisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_AspNetUsers_SupervisorId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Trainees_SupervisorId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.AddColumn<string>(
                name: "Supervisor",
                schema: "HR",
                table: "Trainees",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");
        }
    }
}
