using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createnotifyordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notifyorder",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddeliveryman = table.Column<int>(type: "integer", nullable: false),
                    idorder = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifyorder_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifyorder_iddeliveryman",
                        column: x => x.iddeliveryman,
                        principalTable: "deliveryman",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_notifyorder_idorder",
                        column: x => x.idorder,
                        principalTable: "order",
                        principalColumn: "id");
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifyorder");
        }
    }
}
