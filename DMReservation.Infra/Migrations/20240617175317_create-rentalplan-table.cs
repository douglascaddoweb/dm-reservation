using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createrentalplantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rentalplan",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    days = table.Column<short>(type: "smallint", nullable: false),
                    price = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    fine = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    extraprice = table.Column<double>(type: "numeric(18,2)", nullable: false)
                }, 
                constraints: table =>
                {
                    table.PrimaryKey("pk_rentalplan_id", x => x.id);
                });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "rentalplan");
        }
    }
}
