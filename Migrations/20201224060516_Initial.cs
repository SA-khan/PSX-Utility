using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSXDataFetchingApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClosingMarketSummary",
                columns: table => new
                {
                    ClosingMarketSummaryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Open = table.Column<string>(type: "TEXT", nullable: true),
                    High = table.Column<string>(type: "TEXT", nullable: true),
                    Low = table.Column<string>(type: "TEXT", nullable: true),
                    Closing = table.Column<string>(type: "TEXT", nullable: true),
                    Volume = table.Column<string>(type: "TEXT", nullable: true),
                    Ldcp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingMarketSummary", x => x.ClosingMarketSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "CurrentMarketSummary",
                columns: table => new
                {
                    CurrentMarketSummaryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Category = table.Column<long>(type: "INTEGER", nullable: false),
                    Current = table.Column<string>(type: "TEXT", nullable: true),
                    Ldcp = table.Column<string>(type: "TEXT", nullable: true),
                    Open = table.Column<string>(type: "TEXT", nullable: true),
                    High = table.Column<string>(type: "TEXT", nullable: true),
                    Low = table.Column<string>(type: "TEXT", nullable: true),
                    Change = table.Column<double>(type: "REAL", nullable: false),
                    Volume = table.Column<string>(type: "TEXT", nullable: true),
                    Closing = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentMarketSummary", x => x.CurrentMarketSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "FundInfo",
                columns: table => new
                {
                    FundInfoId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundInfo", x => x.FundInfoId);
                });

            migrationBuilder.CreateTable(
                name: "FundwiseBucketMarketSummary",
                columns: table => new
                {
                    FundwiseBucketMarketSummaryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    ReadingStatus = table.Column<string>(type: "TEXT", nullable: true),
                    FundId = table.Column<long>(type: "INTEGER", nullable: false),
                    ShareName = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<string>(type: "TEXT", nullable: true),
                    AveragePrice = table.Column<string>(type: "TEXT", nullable: true),
                    BookCost = table.Column<string>(type: "TEXT", nullable: true),
                    MarketPrice = table.Column<string>(type: "TEXT", nullable: true),
                    MarketValue = table.Column<string>(type: "TEXT", nullable: true),
                    AppDep = table.Column<string>(type: "TEXT", nullable: true),
                    ClosingPercentage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundwiseBucketMarketSummary", x => x.FundwiseBucketMarketSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "FundwiseMarketSummary",
                columns: table => new
                {
                    FundwiseMarketSummaryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    FundId = table.Column<string>(type: "TEXT", nullable: true),
                    ShareName = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<string>(type: "TEXT", nullable: true),
                    AveragePrice = table.Column<string>(type: "TEXT", nullable: true),
                    BookCost = table.Column<string>(type: "TEXT", nullable: true),
                    MarketPrice = table.Column<string>(type: "TEXT", nullable: true),
                    MarketValue = table.Column<string>(type: "TEXT", nullable: true),
                    AppDep = table.Column<string>(type: "TEXT", nullable: true),
                    ClosingPercentage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundwiseMarketSummary", x => x.FundwiseMarketSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "MufapMarketSummary",
                columns: table => new
                {
                    MufapMarketSummaryId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidityDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Rating = table.Column<string>(type: "TEXT", nullable: true),
                    Nav = table.Column<string>(type: "TEXT", nullable: true),
                    Ytd = table.Column<string>(type: "TEXT", nullable: true),
                    Mtd = table.Column<string>(type: "TEXT", nullable: true),
                    _1Day = table.Column<string>(type: "TEXT", nullable: true),
                    _15Days = table.Column<string>(type: "TEXT", nullable: true),
                    _30Days = table.Column<string>(type: "TEXT", nullable: true),
                    _90Days = table.Column<string>(type: "TEXT", nullable: true),
                    _180Days = table.Column<string>(type: "TEXT", nullable: true),
                    _270Days = table.Column<string>(type: "TEXT", nullable: true),
                    _365Days = table.Column<string>(type: "TEXT", nullable: true),
                    Ter = table.Column<string>(type: "TEXT", nullable: true),
                    Mf = table.Column<string>(type: "TEXT", nullable: true),
                    Sandm = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MufapMarketSummary", x => x.MufapMarketSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "Pkfrv",
                columns: table => new
                {
                    PkfrvId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FloatingRateBond = table.Column<string>(type: "TEXT", nullable: true),
                    IssuanceDate = table.Column<string>(type: "TEXT", nullable: true),
                    MaturityDate = table.Column<string>(type: "TEXT", nullable: true),
                    CouponFrequency = table.Column<string>(type: "TEXT", nullable: true),
                    BMA = table.Column<string>(type: "TEXT", nullable: true),
                    CANDM = table.Column<string>(type: "TEXT", nullable: true),
                    CMKA = table.Column<string>(type: "TEXT", nullable: true),
                    IONE = table.Column<string>(type: "TEXT", nullable: true),
                    JSCM = table.Column<string>(type: "TEXT", nullable: true),
                    MCPL = table.Column<string>(type: "TEXT", nullable: true),
                    SCPL = table.Column<string>(type: "TEXT", nullable: true),
                    VCPL = table.Column<string>(type: "TEXT", nullable: true),
                    FMA = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pkfrv", x => x.PkfrvId);
                });

            migrationBuilder.CreateTable(
                name: "Pkrv",
                columns: table => new
                {
                    PkrvId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tenor = table.Column<string>(type: "TEXT", nullable: true),
                    MidRate = table.Column<string>(type: "TEXT", nullable: true),
                    Change = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pkrv", x => x.PkrvId);
                });

            migrationBuilder.CreateTable(
                name: "ScripInfo",
                columns: table => new
                {
                    ScripInfoId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<long>(type: "INTEGER", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: false),
                    CategoryName = table.Column<string>(type: "TEXT", nullable: true),
                    Code = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScripInfo", x => x.ScripInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Table = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Fetching_Date_Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fetching = table.Column<bool>(type: "INTEGER", nullable: false),
                    Fetching_Date_End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Clear = table.Column<bool>(type: "INTEGER", nullable: false),
                    Dump = table.Column<bool>(type: "INTEGER", nullable: false),
                    Comment = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 1L, 0L, null, 0L, "IBL HealthCare Limited.", 0L, "IBLHL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 2L, 0L, null, 0L, "Macter International Limited.", 0L, "MACTER" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 3L, 0L, null, 0L, "The Searle Company Ltd.", 0L, "SEARL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 4L, 0L, null, 0L, "Wyeth Pakistan Limited.", 0L, "WYETH" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 5L, 0L, null, 0L, "Altern Energy Ltd.", 0L, "ALTN" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 6L, 0L, null, 0L, "Engro Powergen Qadirpur Ltd.", 0L, "EPQL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 7L, 0L, null, 0L, "Hub Power Company Limited.", 0L, "HUBC" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 8L, 0L, null, 0L, "K-Electric Limited.", 0L, "KEL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 9L, 0L, null, 0L, "Oil & Gas Development Company Ltd.", 0L, "OGDC" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 10L, 0L, null, 0L, "Lucky Cement Limited.", 0L, "LUCK" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 11L, 0L, null, 0L, "AGRITECH LIMITED", 0L, "AGL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 12L, 0L, null, 0L, "Allied Bank Ltd.", 0L, "ABL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 13L, 0L, null, 0L, "FAUJI FERTILIZER BIN QASIM", 0L, "FFBL" });

            migrationBuilder.InsertData(
                table: "ScripInfo",
                columns: new[] { "ScripInfoId", "CategoryId", "CategoryName", "Code", "Name", "Number", "Symbol" },
                values: new object[] { 14L, 0L, null, 0L, "Zephyr Textile Limited.", 0L, "ZTL" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosingMarketSummary");

            migrationBuilder.DropTable(
                name: "CurrentMarketSummary");

            migrationBuilder.DropTable(
                name: "FundInfo");

            migrationBuilder.DropTable(
                name: "FundwiseBucketMarketSummary");

            migrationBuilder.DropTable(
                name: "FundwiseMarketSummary");

            migrationBuilder.DropTable(
                name: "MufapMarketSummary");

            migrationBuilder.DropTable(
                name: "Pkfrv");

            migrationBuilder.DropTable(
                name: "Pkrv");

            migrationBuilder.DropTable(
                name: "ScripInfo");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
