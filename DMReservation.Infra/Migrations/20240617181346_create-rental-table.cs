using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createrentaltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rental",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idmotorcyle = table.Column<int>(type: "integer", nullable: false),
                    idrentalplan = table.Column<short>(type: "smallint", nullable: false),
                    iddeliveryman = table.Column<int>(type: "integer", nullable: false),
                    datestart = table.Column<DateTime>(type: "timestamp", nullable: false),
                    datefinish = table.Column<DateTime>(type: "timestamp", nullable: false),
                    dateforecastfinish = table.Column<DateTime>(type: "timestamp", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    price = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    fine = table.Column<double>(type: "numeric(5,2)", nullable: false),
                    extraprice = table.Column<double>(type: "numeric(18,2)", nullable: false),
                    total = table.Column<double>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rental_id", x => x.id);

                    table.ForeignKey("fk_rental_idmotorcycle", x => x.idmotorcyle, "motorcycle", "id");
                    table.ForeignKey("fk_rental_idrentalplan", x => x.idrentalplan, "rentalplan", "id");
                    table.ForeignKey("fk_rental_iddeliveryman", x => x.iddeliveryman, "deliveryman", "id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "rental");
        }
    }
}
