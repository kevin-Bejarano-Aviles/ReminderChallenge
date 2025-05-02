using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReminderChallenge.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeExpirationInReminder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "expiration_type",
                table: "Reminder",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "expiration_type",
                table: "Reminder",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
