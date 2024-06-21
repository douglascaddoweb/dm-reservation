using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createdeliverymantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveryman",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "varchar(100)", nullable: false),
                    cnpj = table.Column<string>(type: "varchar(20)", nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    cnh = table.Column<string>(type: "varchar(20)", nullable: false),
                    typecnh = table.Column<string>(type: "varchar(2)", nullable: false),
                    image = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveryman_id", x => x.id);
                    table.UniqueConstraint("uk_deliveryman_cnpj", x => x.cnpj);
                    table.UniqueConstraint("uk_deliveryman_cnh", x => x.cnh);
                });
        }
        
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliveryman");
        }
    }
}
