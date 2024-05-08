using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DTbl_Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactDetails = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Position = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeftDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveTypes",
                columns: table => new
                {
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveTypes", x => x.LeaveTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateRemoved = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_Users", x => x.UserID);
                });

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
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_Calander", x => x.HolidayCalendarID);
                    table.ForeignKey(
                        name: "FK_DTbl_Calander_DTbl_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "DTbl_Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_DTbl_Calander_DTbl_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "DTbl_Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveApplies",
                columns: table => new
                {
                    LeaveApplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeID = table.Column<int>(type: "int", nullable: false),
                    LeaveBalanceID = table.Column<int>(type: "int", nullable: false),
                    LeaveApplyEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AppliedFromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppliedToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    LeaveBalanceID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveApplies", x => x.LeaveApplyID);
                    table.ForeignKey(
                        name: "FK_DTbl_LeaveApplies_DTbl_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "DTbl_Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DTbl_LeaveApplies_DTbl_LeaveTypes_LeaveTypeID",
                        column: x => x.LeaveTypeID,
                        principalTable: "DTbl_LeaveTypes",
                        principalColumn: "LeaveTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DTbl_LeaveBalances",
                columns: table => new
                {
                    LeaveBalanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    LeaveApplyID = table.Column<int>(type: "int", nullable: false),
                    LeaveDaysApplied = table.Column<int>(type: "int", nullable: false),
                    RemainingLeave = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    AllocatedThisYear = table.Column<int>(type: "int", nullable: false),
                    UsedThisYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DTbl_LeaveBalances", x => x.LeaveBalanceID);
                    table.ForeignKey(
                        name: "FK_DTbl_LeaveBalances_DTbl_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "DTbl_Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DTbl_LeaveBalances_DTbl_LeaveApplies_LeaveApplyID",
                        column: x => x.LeaveApplyID,
                        principalTable: "DTbl_LeaveApplies",
                        principalColumn: "LeaveApplyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_Calander_EmployeeID",
                table: "DTbl_Calander",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_Calander_UserID",
                table: "DTbl_Calander",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveApplies_EmployeeID",
                table: "DTbl_LeaveApplies",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveApplies_LeaveBalanceID1",
                table: "DTbl_LeaveApplies",
                column: "LeaveBalanceID1");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveApplies_LeaveTypeID",
                table: "DTbl_LeaveApplies",
                column: "LeaveTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveBalances_EmployeeID",
                table: "DTbl_LeaveBalances",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_DTbl_LeaveBalances_LeaveApplyID",
                table: "DTbl_LeaveBalances",
                column: "LeaveApplyID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DTbl_LeaveApplies_DTbl_LeaveBalances_LeaveBalanceID1",
                table: "DTbl_LeaveApplies",
                column: "LeaveBalanceID1",
                principalTable: "DTbl_LeaveBalances",
                principalColumn: "LeaveBalanceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DTbl_LeaveApplies_DTbl_Employee_EmployeeID",
                table: "DTbl_LeaveApplies");

            migrationBuilder.DropForeignKey(
                name: "FK_DTbl_LeaveBalances_DTbl_Employee_EmployeeID",
                table: "DTbl_LeaveBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_DTbl_LeaveApplies_DTbl_LeaveBalances_LeaveBalanceID1",
                table: "DTbl_LeaveApplies");

            migrationBuilder.DropTable(
                name: "DTbl_Calander");

            migrationBuilder.DropTable(
                name: "DTbl_Users");

            migrationBuilder.DropTable(
                name: "DTbl_Employee");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveBalances");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveApplies");

            migrationBuilder.DropTable(
                name: "DTbl_LeaveTypes");
        }
    }
}
