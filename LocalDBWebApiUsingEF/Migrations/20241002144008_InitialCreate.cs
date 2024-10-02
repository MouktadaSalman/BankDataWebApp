using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AcctNo = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AcctNo);
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                columns: table => new
                {
                    Transaction = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sender = table.Column<uint>(type: "INTEGER", nullable: false),
                    HistoryString = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistories", x => x.Transaction);
                    table.ForeignKey(
                        name: "FK_UserHistories_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AcctNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
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
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    AccountAcctNo = table.Column<uint>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Accounts_AccountAcctNo",
                        column: x => x.AccountAcctNo,
                        principalTable: "Accounts",
                        principalColumn: "AcctNo");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AcctNo", "AccountName", "Balance", "UserId" },
                values: new object[,]
                {
                    { 2135u, "Savings Account", 35018, 9 },
                    { 3937u, "Savings Account", 89484, 5 },
                    { 5181u, "Savings Account", 92034, 7 },
                    { 5291u, "Savings Account", 23418, 8 },
                    { 5441u, "Savings Account", 13292, 2 },
                    { 6390u, "Savings Account", 20644, 3 },
                    { 6680u, "Savings Account", 62609, 10 },
                    { 7524u, "Savings Account", 49734, 6 },
                    { 7791u, "Savings Account", 59353, 1 },
                    { 8839u, "Savings Account", 29277, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "AccountAcctNo", "Address", "Age", "Email", "FName", "LName", "Password", "PhoneNumber", "ProfilePictureUrl", "Username" },
                values: new object[,]
                {
                    { 1, null, "972 Samuel Phillips St.", 21u, "Christine.Robinson746@gmail.com", "Christine", "Robinson", "xSaxuJoHbv2i", "+9616801591", null, "Christine.Robinson129" },
                    { 2, null, "205 Jeffrey Martin St.", 39u, "Paul.Watson481@gmail.com", "Paul", "Watson", "VhA0jBMtEj30", "+9612108257", null, "Paul.Watson677" },
                    { 3, null, "83 Steven Lewis St.", 31u, "Brian.Edwards381@gmail.com", "Brian", "Edwards", "5dm706TEem6l", "+9619954485", null, "Brian.Edwards208" },
                    { 4, null, "703 Stephen Walker St.", 106u, "Raymond.Ward918@gmail.com", "Raymond", "Ward", "oXLWhyRQze0D", "+9619016293", null, "Raymond.Ward613" },
                    { 5, null, "7 William Lewis St.", 74u, "Rebecca.Thompson413@gmail.com", "Rebecca", "Thompson", "4idqNemvKokn", "+9616432720", null, "Rebecca.Thompson266" },
                    { 6, null, "220 Emma Campbell St.", 93u, "Matthew.Ward780@gmail.com", "Matthew", "Ward", "u3QPp8aoRqNX", "+9615284552", null, "Matthew.Ward511" },
                    { 7, null, "57 Paul Lee St.", 61u, "Gary.Ruiz653@gmail.com", "Gary", "Ruiz", "c8s9ys23O1eb", "+9618706389", null, "Gary.Ruiz793" },
                    { 8, null, "511 Kimberly Price St.", 91u, "Sharon.Nelson345@gmail.com", "Sharon", "Nelson", "ZbvJkh49jtIh", "+9619698518", null, "Sharon.Nelson636" },
                    { 9, null, "388 Amanda Wright St.", 58u, "Sandra.Evans781@gmail.com", "Sandra", "Evans", "2CzTbn7Bsy0i", "+9617062497", null, "Sandra.Evans707" },
                    { 10, null, "240 Mark Ramos St.", 69u, "Donald.Thompson989@gmail.com", "Donald", "Thompson", "TPIIbKdnt8cE", "+9616922122", null, "Donald.Thompson333" }
                });

            migrationBuilder.InsertData(
                table: "UserHistories",
                columns: new[] { "Transaction", "AccountId", "Amount", "DateTime", "HistoryString", "Sender", "Type" },
                values: new object[,]
                {
                    { 1, 2135u, -202.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2150), "Account ID: 2135 --- Withdrawn: $202.00 --- Date and Time: October 02, 2024 22:40 PM", 2135u, "Withdraw" },
                    { 2, 8839u, 893.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2232), "Account ID: 8839 --- Received: $893.00 --- Date and Time: October 02, 2024 22:40 PM", 2135u, "Receive" },
                    { 3, 5181u, -41.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2243), "Account ID: 5181 --- Deposited: $41.00 --- Date and Time: October 02, 2024 22:40 PM", 5181u, "Deposit" },
                    { 4, 2135u, 166.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2250), "Account ID: 2135 --- Received: $166.00 --- Date and Time: October 02, 2024 22:40 PM", 3937u, "Receive" },
                    { 5, 5181u, -494.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2256), "Account ID: 5181 --- Sent: $494.00 --- Date and Time: October 02, 2024 22:40 PM", 7791u, "Send" },
                    { 6, 5441u, 836.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2263), "Account ID: 5441 --- Received: $836.00 --- Date and Time: October 02, 2024 22:40 PM", 7524u, "Receive" },
                    { 7, 5441u, -88.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2268), "Account ID: 5441 --- Sent: $88.00 --- Date and Time: October 02, 2024 22:40 PM", 6390u, "Send" },
                    { 8, 6390u, -767.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2274), "Account ID: 6390 --- Deposited: $767.00 --- Date and Time: October 02, 2024 22:40 PM", 6390u, "Deposit" },
                    { 9, 5291u, -789.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2279), "Account ID: 5291 --- Deposited: $789.00 --- Date and Time: October 02, 2024 22:40 PM", 5291u, "Deposit" },
                    { 10, 6680u, -36.0, new DateTime(2024, 10, 2, 22, 40, 8, 687, DateTimeKind.Local).AddTicks(2284), "Account ID: 6680 --- Sent: $36.00 --- Date and Time: October 02, 2024 22:40 PM", 6390u, "Send" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_AccountId",
                table: "UserHistories",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AccountAcctNo",
                table: "UserProfiles",
                column: "AccountAcctNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserHistories");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
