using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminsTableWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Accounts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FName = table.Column<string>(type: "TEXT", nullable: true),
                    LName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<uint>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Address", "Age", "Email", "FName", "LName", "Password", "PhoneNumber", "ProfilePictureUrl", "Username" },
                values: new object[,]
                {
                    { 1, "456 Donald Thompson St.", 50u, "John.Sanders407@admin.bank.dc.au", "John", "Sanders", "bmlICFkEH1Dn", "+9615545281", null, "John.Sanders235" },
                    { 2, "318 Kevin Flores St.", 77u, "Larry.Hughes562@admin.bank.dc.au", "Larry", "Hughes", "WkJJ0M0hd8lD", "+9611171385", null, "Larry.Hughes543" },
                    { 3, "52 Gary Cooper St.", 51u, "Larry.Collins941@admin.bank.dc.au", "Larry", "Collins", "1sXNmcL137xC", "+9612551733", null, "Larry.Collins969" }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "AccountName",
                table: "Accounts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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
        }
    }
}
