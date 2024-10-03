using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class FixConflictWithNewUserHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 1,
                column: "HistoryString",
                value: "Balance updated by -202 on 3/10/2024 9:54:58 AM.  | Old Balance: 35054 ----- New Balance: 34852");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 2,
                column: "HistoryString",
                value: "Balance updated by -362 on 3/10/2024 9:54:58 AM.  | Old Balance: 62645 ----- New Balance: 62283");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 3,
                column: "HistoryString",
                value: "Balance updated by -322 on 3/10/2024 9:54:58 AM.  | Old Balance: 62283 ----- New Balance: 61961");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 4,
                column: "HistoryString",
                value: "Balance updated by 615 on 3/10/2024 9:54:58 AM.  | Old Balance: 49734 ----- New Balance: 50349");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 5,
                column: "HistoryString",
                value: "Balance updated by 287 on 3/10/2024 9:54:58 AM.  | Old Balance: 28384 ----- New Balance: 28671");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 6,
                column: "HistoryString",
                value: "Balance updated by -166 on 3/10/2024 9:54:58 AM.  | Old Balance: 34852 ----- New Balance: 34686");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 7,
                column: "HistoryString",
                value: "Balance updated by 894 on 3/10/2024 9:54:58 AM.  | Old Balance: 89484 ----- New Balance: 90378");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 8,
                column: "HistoryString",
                value: "Balance updated by 494 on 3/10/2024 9:54:58 AM.  | Old Balance: 92569 ----- New Balance: 93063");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 9,
                column: "HistoryString",
                value: "Balance updated by 170 on 3/10/2024 9:54:58 AM.  | Old Balance: 59353 ----- New Balance: 59523");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 10,
                column: "HistoryString",
                value: "Balance updated by -836 on 3/10/2024 9:54:58 AM.  | Old Balance: 12544 ----- New Balance: 11708");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 1,
                column: "HistoryString",
                value: "Balance updated by -202 on 2/10/2024 11:13:40 PM.  | Old Balance: 35054 ----- New Balance: 34852");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 2,
                column: "HistoryString",
                value: "Balance updated by -362 on 2/10/2024 11:13:40 PM.  | Old Balance: 62645 ----- New Balance: 62283");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 3,
                column: "HistoryString",
                value: "Balance updated by -322 on 2/10/2024 11:13:40 PM.  | Old Balance: 62283 ----- New Balance: 61961");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 4,
                column: "HistoryString",
                value: "Balance updated by 615 on 2/10/2024 11:13:40 PM.  | Old Balance: 49734 ----- New Balance: 50349");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 5,
                column: "HistoryString",
                value: "Balance updated by 287 on 2/10/2024 11:13:40 PM.  | Old Balance: 28384 ----- New Balance: 28671");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 6,
                column: "HistoryString",
                value: "Balance updated by -166 on 2/10/2024 11:13:40 PM.  | Old Balance: 34852 ----- New Balance: 34686");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 7,
                column: "HistoryString",
                value: "Balance updated by 894 on 2/10/2024 11:13:40 PM.  | Old Balance: 89484 ----- New Balance: 90378");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 8,
                column: "HistoryString",
                value: "Balance updated by 494 on 2/10/2024 11:13:40 PM.  | Old Balance: 92569 ----- New Balance: 93063");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 9,
                column: "HistoryString",
                value: "Balance updated by 170 on 2/10/2024 11:13:40 PM.  | Old Balance: 59353 ----- New Balance: 59523");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 10,
                column: "HistoryString",
                value: "Balance updated by -836 on 2/10/2024 11:13:40 PM.  | Old Balance: 12544 ----- New Balance: 11708");
        }
    }
}
