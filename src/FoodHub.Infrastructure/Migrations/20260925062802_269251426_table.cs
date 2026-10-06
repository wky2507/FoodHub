using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _269251426_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RejectTime",
                table: "Orders",
                newName: "RejectedTime");

            migrationBuilder.RenameColumn(
                name: "OrderAcceptTime",
                table: "Orders",
                newName: "MealFinishedTime");

            migrationBuilder.RenameColumn(
                name: "MealFinishTime",
                table: "Orders",
                newName: "CompletedTime");

            migrationBuilder.RenameColumn(
                name: "CancelTime",
                table: "Orders",
                newName: "CanceledTime");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AcceptedTime",
                table: "Orders",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedTime",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "RejectedTime",
                table: "Orders",
                newName: "RejectTime");

            migrationBuilder.RenameColumn(
                name: "MealFinishedTime",
                table: "Orders",
                newName: "OrderAcceptTime");

            migrationBuilder.RenameColumn(
                name: "CompletedTime",
                table: "Orders",
                newName: "MealFinishTime");

            migrationBuilder.RenameColumn(
                name: "CanceledTime",
                table: "Orders",
                newName: "CancelTime");
        }
    }
}
