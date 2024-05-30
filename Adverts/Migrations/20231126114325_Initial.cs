using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adverts.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_advertisers",
                columns: table => new
                {
                    adv_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adv_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_organisationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_deliveryAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_postalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_billingAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    adv_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_advertisers", x => x.adv_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ads",
                columns: table => new
                {
                    ad_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ad_headline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ad_advertismentPrice = table.Column<double>(type: "float", nullable: false),
                    ad_productPrice = table.Column<double>(type: "float", nullable: false),
                    ad_advertisers_id = table.Column<int>(type: "int", nullable: true),
                    ad_Advertisersadv_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ads", x => x.ad_id);
                    table.ForeignKey(
                        name: "FK_tbl_ads_tbl_advertisers_ad_Advertisersadv_id",
                        column: x => x.ad_Advertisersadv_id,
                        principalTable: "tbl_advertisers",
                        principalColumn: "adv_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ads_ad_Advertisersadv_id",
                table: "tbl_ads",
                column: "ad_Advertisersadv_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_advertisers_adv_name",
                table: "tbl_advertisers",
                column: "adv_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ads");

            migrationBuilder.DropTable(
                name: "tbl_advertisers");
        }
    }
}
