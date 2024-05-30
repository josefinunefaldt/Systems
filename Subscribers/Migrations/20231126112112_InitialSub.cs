using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Subscribers.Migrations
{
    /// <inheritdoc />
    public partial class InitialSub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_subscribers",
                columns: table => new
                {
                    sub_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sub_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sub_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sub_deliveryAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sub_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sub_postalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sub_socialSecurityNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sub_subscriptionNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_subscribers", x => x.sub_id);
                });

            migrationBuilder.InsertData(
                table: "tbl_subscribers",
                columns: new[] { "sub_id", "sub_FirstName", "sub_LastName", "sub_PhoneNumber", "sub_deliveryAddress", "sub_postalCode", "sub_socialSecurityNumber", "sub_subscriptionNumber" },
                values: new object[,]
                {
                    { 1, "Maja", "Svensson", "0768234535", "fallrundan 11", "82830", "930412", 100 },
                    { 2, "Kalle", "Andersson", "0738234536", "dalrundan 27", "82832", "930418", 337 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_subscribers_sub_FirstName",
                table: "tbl_subscribers",
                column: "sub_FirstName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_subscribers");
        }
    }
}
