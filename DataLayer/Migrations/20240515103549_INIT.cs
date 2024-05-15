using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DTbl_Calander",
                columns: table => new
                {
                    HolidayCalendarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublicHoliday = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_Calander", x => x.HolidayCalendarID);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeftDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveApply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveApplyEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LeaveApplyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppliedFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppliedToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveApply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplies_Employees",
                        column: x => x.EmployeeId,
                        principalTable: "DTbl_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveApplies_LeaveTypes",
                        column: x => x.LeaveTypeId,
                        principalTable: "DTbl_LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveDaysApplied = table.Column<int>(type: "int", nullable: false),
                    RemainingLeave = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AllocatedThisYear = table.Column<int>(type: "int", nullable: false),
                    UsedThisYear = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveApplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DTbl_LeaveBalances_DTbl_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "DTbl_Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveApplies_LeaveBalance",
                        column: x => x.LeaveApplyId,
                        principalTable: "DTbl_LeaveApply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveApply_EmployeeId",
                table: "DTbl_LeaveApply",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveApply_LeaveTypeId",
                table: "DTbl_LeaveApply",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveBalances_EmployeeId",
                table: "DTbl_LeaveBalances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveBalances_LeaveApplyId",
                table: "DTbl_LeaveBalances",
                column: "LeaveApplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DTbl_Calander");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveBalances");

            migrationBuilder.DropTable(
                name: "DTbl_User");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveApply");

            migrationBuilder.DropTable(
                name: "DTbl_Employee");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveTypes");
        }
    }
}
