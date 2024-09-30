using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataTierWebServer.Migrations
{
    /// <inheritdoc />
    public partial class InitailCreate : Migration
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
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<uint>(type: "INTEGER", nullable: false),
                    Balance = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AcctNo);
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
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserHistories",
                columns: table => new
                {
                    Transaction = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccountId = table.Column<uint>(type: "INTEGER", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AcctNo", "Address", "Age", "Balance", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 2135u, "388 Amanda Wright St.", 58u, 34686, "Sandra.Evans781@gmail.com", "Sandra", "Evans" },
                    { 3937u, "7 William Lewis St.", 74u, 90378, "Rebecca.Thompson413@gmail.com", "Rebecca", "Thompson" },
                    { 5181u, "57 Paul Lee St.", 61u, 93063, "Gary.Ruiz653@gmail.com", "Gary", "Ruiz" },
                    { 5291u, "511 Kimberly Price St.", 91u, 24207, "Sharon.Nelson345@gmail.com", "Sharon", "Nelson" },
                    { 5441u, "205 Jeffrey Martin St.", 39u, 11708, "Paul.Watson481@gmail.com", "Paul", "Watson" },
                    { 6390u, "83 Steven Lewis St.", 31u, 21411, "Brian.Edwards381@gmail.com", "Brian", "Edwards" },
                    { 6680u, "240 Mark Ramos St.", 69u, 61961, "Donald.Thompson989@gmail.com", "Donald", "Thompson" },
                    { 7524u, "220 Emma Campbell St.", 93u, 50349, "Matthew.Ward780@gmail.com", "Matthew", "Ward" },
                    { 7791u, "972 Samuel Phillips St.", 21u, 59523, "Christine.Robinson746@gmail.com", "Christine", "Robinson" },
                    { 8839u, "703 Stephen Walker St.", 106u, 28671, "Raymond.Ward918@gmail.com", "Raymond", "Ward" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Address", "Age", "Email", "FName", "LName", "Password", "PhoneNumber", "ProfilePictureUrl", "Username" },
                values: new object[,]
                {
                    { 1, "972 Samuel Phillips St.", 21u, "Christine.Robinson746@gmail.com", "Christine", "Robinson", "xSaxuJoHbv2i", "+9616801591", null, "Christine.Robinson129" },
                    { 2, "205 Jeffrey Martin St.", 39u, "Paul.Watson481@gmail.com", "Paul", "Watson", "VhA0jBMtEj30", "+9612108257", null, "Paul.Watson677" },
                    { 3, "83 Steven Lewis St.", 31u, "Brian.Edwards381@gmail.com", "Brian", "Edwards", "5dm706TEem6l", "+9619954485", null, "Brian.Edwards208" },
                    { 4, "703 Stephen Walker St.", 106u, "Raymond.Ward918@gmail.com", "Raymond", "Ward", "oXLWhyRQze0D", "+9619016293", null, "Raymond.Ward613" },
                    { 5, "7 William Lewis St.", 74u, "Rebecca.Thompson413@gmail.com", "Rebecca", "Thompson", "4idqNemvKokn", "+9616432720", null, "Rebecca.Thompson266" },
                    { 6, "220 Emma Campbell St.", 93u, "Matthew.Ward780@gmail.com", "Matthew", "Ward", "u3QPp8aoRqNX", "+9615284552", null, "Matthew.Ward511" },
                    { 7, "57 Paul Lee St.", 61u, "Gary.Ruiz653@gmail.com", "Gary", "Ruiz", "c8s9ys23O1eb", "+9618706389", null, "Gary.Ruiz793" },
                    { 8, "511 Kimberly Price St.", 91u, "Sharon.Nelson345@gmail.com", "Sharon", "Nelson", "ZbvJkh49jtIh", "+9619698518", null, "Sharon.Nelson636" },
                    { 9, "388 Amanda Wright St.", 58u, "Sandra.Evans781@gmail.com", "Sandra", "Evans", "2CzTbn7Bsy0i", "+9617062497", null, "Sandra.Evans707" },
                    { 10, "240 Mark Ramos St.", 69u, "Donald.Thompson989@gmail.com", "Donald", "Thompson", "TPIIbKdnt8cE", "+9616922122", null, "Donald.Thompson333" }
                });

            migrationBuilder.InsertData(
                table: "UserHistories",
                columns: new[] { "Transaction", "AccountId", "HistoryString" },
                values: new object[,]
                {
                    { 1, 2135u, "Balance updated by -202 on 29/09/2024 9:18:09 PM.  | Old Balance: 35054 ----- New Balance: 34852" },
                    { 2, 6680u, "Balance updated by -362 on 29/09/2024 9:18:09 PM.  | Old Balance: 62645 ----- New Balance: 62283" },
                    { 3, 6680u, "Balance updated by -322 on 29/09/2024 9:18:09 PM.  | Old Balance: 62283 ----- New Balance: 61961" },
                    { 4, 7524u, "Balance updated by 615 on 29/09/2024 9:18:09 PM.  | Old Balance: 49734 ----- New Balance: 50349" },
                    { 5, 8839u, "Balance updated by 287 on 29/09/2024 9:18:09 PM.  | Old Balance: 28384 ----- New Balance: 28671" },
                    { 6, 2135u, "Balance updated by -166 on 29/09/2024 9:18:09 PM.  | Old Balance: 34852 ----- New Balance: 34686" },
                    { 7, 3937u, "Balance updated by 894 on 29/09/2024 9:18:09 PM.  | Old Balance: 89484 ----- New Balance: 90378" },
                    { 8, 5181u, "Balance updated by 494 on 29/09/2024 9:18:09 PM.  | Old Balance: 92569 ----- New Balance: 93063" },
                    { 9, 7791u, "Balance updated by 170 on 29/09/2024 9:18:09 PM.  | Old Balance: 59353 ----- New Balance: 59523" },
                    { 10, 5441u, "Balance updated by -836 on 29/09/2024 9:18:09 PM.  | Old Balance: 12544 ----- New Balance: 11708" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHistories_AccountId",
                table: "UserHistories",
                column: "AccountId");
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
