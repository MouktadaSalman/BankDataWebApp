using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Accounts",
                newName: "AccountName");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 2135u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 3937u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5181u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5291u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5441u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6390u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6680u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7524u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7791u,
                column: "AccountName",
                value: "Savings Account");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 8839u,
                column: "AccountName",
                value: "Savings Account");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountName",
                table: "Accounts",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<uint>(
                name: "Age",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 2135u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "388 Amanda Wright St.", 58u, "Sandra.Evans781@gmail.com", "Sandra", "Evans" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 3937u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "7 William Lewis St.", 74u, "Rebecca.Thompson413@gmail.com", "Rebecca", "Thompson" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5181u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "57 Paul Lee St.", 61u, "Gary.Ruiz653@gmail.com", "Gary", "Ruiz" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5291u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "511 Kimberly Price St.", 91u, "Sharon.Nelson345@gmail.com", "Sharon", "Nelson" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 5441u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "205 Jeffrey Martin St.", 39u, "Paul.Watson481@gmail.com", "Paul", "Watson" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6390u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "83 Steven Lewis St.", 31u, "Brian.Edwards381@gmail.com", "Brian", "Edwards" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 6680u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "240 Mark Ramos St.", 69u, "Donald.Thompson989@gmail.com", "Donald", "Thompson" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7524u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "220 Emma Campbell St.", 93u, "Matthew.Ward780@gmail.com", "Matthew", "Ward" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 7791u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "972 Samuel Phillips St.", 21u, "Christine.Robinson746@gmail.com", "Christine", "Robinson" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AcctNo",
                keyValue: 8839u,
                columns: new[] { "Address", "Age", "Email", "FirstName", "LastName" },
                values: new object[] { "703 Stephen Walker St.", 106u, "Raymond.Ward918@gmail.com", "Raymond", "Ward" });

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 1,
                column: "HistoryString",
                value: "Balance updated by -202 on 29/09/2024 9:18:09 PM.  | Old Balance: 35054 ----- New Balance: 34852");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 2,
                column: "HistoryString",
                value: "Balance updated by -362 on 29/09/2024 9:18:09 PM.  | Old Balance: 62645 ----- New Balance: 62283");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 3,
                column: "HistoryString",
                value: "Balance updated by -322 on 29/09/2024 9:18:09 PM.  | Old Balance: 62283 ----- New Balance: 61961");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 4,
                column: "HistoryString",
                value: "Balance updated by 615 on 29/09/2024 9:18:09 PM.  | Old Balance: 49734 ----- New Balance: 50349");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 5,
                column: "HistoryString",
                value: "Balance updated by 287 on 29/09/2024 9:18:09 PM.  | Old Balance: 28384 ----- New Balance: 28671");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 6,
                column: "HistoryString",
                value: "Balance updated by -166 on 29/09/2024 9:18:09 PM.  | Old Balance: 34852 ----- New Balance: 34686");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 7,
                column: "HistoryString",
                value: "Balance updated by 894 on 29/09/2024 9:18:09 PM.  | Old Balance: 89484 ----- New Balance: 90378");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 8,
                column: "HistoryString",
                value: "Balance updated by 494 on 29/09/2024 9:18:09 PM.  | Old Balance: 92569 ----- New Balance: 93063");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 9,
                column: "HistoryString",
                value: "Balance updated by 170 on 29/09/2024 9:18:09 PM.  | Old Balance: 59353 ----- New Balance: 59523");

            migrationBuilder.UpdateData(
                table: "UserHistories",
                keyColumn: "Transaction",
                keyValue: 10,
                column: "HistoryString",
                value: "Balance updated by -836 on 29/09/2024 9:18:09 PM.  | Old Balance: 12544 ----- New Balance: 11708");
        }
    }
}
