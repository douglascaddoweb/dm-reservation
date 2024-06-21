using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createorderdeliverymantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderdeliveryman",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iddeliveryman = table.Column<int>(type: "integer", nullable: false),
                    idorder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.id);
                    table.ForeignKey("fk_order_iddeliveryman", x => x.iddeliveryman, "deliveryman", "id");
                    table.ForeignKey("fk_order_idorder", x => x.idorder, "order", "id");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderdeliveryman");
        }
    }
}
