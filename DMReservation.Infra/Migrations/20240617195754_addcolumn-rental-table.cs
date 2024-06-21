using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnrentaltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(name: "dateforecastfinish", "rental", type: "timestamp", nullable: false);
            migrationBuilder.AlterColumn<DateTime>(name: "datefinish", table: "rental", nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "dateforecastfinish", table: "rental");
        }
    }
}
