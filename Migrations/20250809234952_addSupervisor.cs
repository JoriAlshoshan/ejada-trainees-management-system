using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EjadaTraineesManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_AspNetUsers_SupervisorId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Departments_DepartmentId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Universities_UniversityId",
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

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                schema: "HR",
                table: "Trainees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                schema: "HR",
                table: "Trainees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SupervisorIds",
                schema: "HR",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "TraineeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SupervisorTrainees",
                columns: table => new
                {
                    SupervisorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorTrainees", x => new { x.SupervisorId, x.TraineeId });
                    table.ForeignKey(
                        name: "FK_SupervisorTrainees_AspNetUsers_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupervisorTrainees_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalSchema: "HR",
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TraineeId",
                table: "AspNetUsers",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorTrainees_TraineeId",
                table: "SupervisorTrainees",
                column: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Trainees_TraineeId",
                table: "AspNetUsers",
                column: "TraineeId",
                principalSchema: "HR",
                principalTable: "Trainees",
                principalColumn: "TraineeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Departments_DepartmentId",
                schema: "HR",
                table: "Trainees",
                column: "DepartmentId",
                principalSchema: "HR",
                principalTable: "Departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Universities_UniversityId",
                schema: "HR",
                table: "Trainees",
                column: "UniversityId",
                principalSchema: "HR",
                principalTable: "Universities",
                principalColumn: "UniversityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Trainees_TraineeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Departments_DepartmentId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Universities_UniversityId",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropTable(
                name: "SupervisorTrainees");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TraineeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SupervisorIds",
                schema: "HR",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "TraineeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UniversityId",
                schema: "HR",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                schema: "HR",
                table: "Trainees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Departments_DepartmentId",
                schema: "HR",
                table: "Trainees",
                column: "DepartmentId",
                principalSchema: "HR",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Universities_UniversityId",
                schema: "HR",
                table: "Trainees",
                column: "UniversityId",
                principalSchema: "HR",
                principalTable: "Universities",
                principalColumn: "UniversityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
