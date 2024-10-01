using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAccountAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "AccountAcctNo",
                table: "UserProfiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 2135u,
                column: "UserId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 3937u,
                column: "UserId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5181u,
                column: "UserId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5291u,
                column: "UserId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5441u,
                column: "UserId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6390u,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6680u,
                column: "UserId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7524u,
                column: "UserId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7791u,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 8839u,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 1,
                column: "HistoryString",
                value: "Balance updated by -202 on 30/09/2024 11:12:45 PM.  | Old Balance: 35054 ----- New Balance: 34852");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 2,
                column: "HistoryString",
                value: "Balance updated by -362 on 30/09/2024 11:12:45 PM.  | Old Balance: 62645 ----- New Balance: 62283");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 3,
                column: "HistoryString",
                value: "Balance updated by -322 on 30/09/2024 11:12:45 PM.  | Old Balance: 62283 ----- New Balance: 61961");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 4,
                column: "HistoryString",
                value: "Balance updated by 615 on 30/09/2024 11:12:45 PM.  | Old Balance: 49734 ----- New Balance: 50349");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 5,
                column: "HistoryString",
                value: "Balance updated by 287 on 30/09/2024 11:12:45 PM.  | Old Balance: 28384 ----- New Balance: 28671");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 6,
                column: "HistoryString",
                value: "Balance updated by -166 on 30/09/2024 11:12:45 PM.  | Old Balance: 34852 ----- New Balance: 34686");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 7,
                column: "HistoryString",
                value: "Balance updated by 894 on 30/09/2024 11:12:45 PM.  | Old Balance: 89484 ----- New Balance: 90378");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 8,
                column: "HistoryString",
                value: "Balance updated by 494 on 30/09/2024 11:12:45 PM.  | Old Balance: 92569 ----- New Balance: 93063");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 9,
                column: "HistoryString",
                value: "Balance updated by 170 on 30/09/2024 11:12:45 PM.  | Old Balance: 59353 ----- New Balance: 59523");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 10,
                column: "HistoryString",
                value: "Balance updated by -836 on 30/09/2024 11:12:45 PM.  | Old Balance: 12544 ----- New Balance: 11708");

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 2,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 3,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 4,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 5,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 6,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 7,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 8,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 9,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: 10,
                column: "AccountAcctNo",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountAcctNo",
                table: "UserProfiles",
                column: "AccountAcctNo");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Accounts_AccountAcctNo",
                table: "UserProfiles",
                column: "AccountAcctNo",
                principalTable: "Accounts",
                principalColumn: "AcctNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Accounts_AccountAcctNo",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AccountAcctNo",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "AccountAcctNo",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 1,
                column: "HistoryString",
                value: "Balance updated by -202 on 30/09/2024 10:48:29 PM.  | Old Balance: 35054 ----- New Balance: 34852");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 2,
                column: "HistoryString",
                value: "Balance updated by -362 on 30/09/2024 10:48:29 PM.  | Old Balance: 62645 ----- New Balance: 62283");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 3,
                column: "HistoryString",
                value: "Balance updated by -322 on 30/09/2024 10:48:29 PM.  | Old Balance: 62283 ----- New Balance: 61961");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 4,
                column: "HistoryString",
                value: "Balance updated by 615 on 30/09/2024 10:48:29 PM.  | Old Balance: 49734 ----- New Balance: 50349");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 5,
                column: "HistoryString",
                value: "Balance updated by 287 on 30/09/2024 10:48:29 PM.  | Old Balance: 28384 ----- New Balance: 28671");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 6,
                column: "HistoryString",
                value: "Balance updated by -166 on 30/09/2024 10:48:29 PM.  | Old Balance: 34852 ----- New Balance: 34686");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 7,
                column: "HistoryString",
                value: "Balance updated by 894 on 30/09/2024 10:48:29 PM.  | Old Balance: 89484 ----- New Balance: 90378");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 8,
                column: "HistoryString",
                value: "Balance updated by 494 on 30/09/2024 10:48:29 PM.  | Old Balance: 92569 ----- New Balance: 93063");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 9,
                column: "HistoryString",
                value: "Balance updated by 170 on 30/09/2024 10:48:29 PM.  | Old Balance: 59353 ----- New Balance: 59523");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 10,
                column: "HistoryString",
                value: "Balance updated by -836 on 30/09/2024 10:48:29 PM.  | Old Balance: 12544 ----- New Balance: 11708");
        }
    }
}
