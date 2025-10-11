using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P1_AP1_AngelGuerrero.Migrations
{
    /// <inheritdoc />
    public partial class Cambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasHuacalesDetalles_TiposHuacales_TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles");

            migrationBuilder.DropIndex(
                name: "IX_EntradasHuacalesDetalles_TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles");

            migrationBuilder.DropColumn(
                name: "TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EntradasHuacalesDetalles_TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles",
                column: "TipoHuacalTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasHuacalesDetalles_TiposHuacales_TipoHuacalTipoId",
                table: "EntradasHuacalesDetalles",
                column: "TipoHuacalTipoId",
                principalTable: "TiposHuacales",
                principalColumn: "TipoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
