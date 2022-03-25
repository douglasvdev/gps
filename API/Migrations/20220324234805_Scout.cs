using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Scout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtPartida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JogadorId = table.Column<int>(type: "int", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false),
                    ParametroId = table.Column<int>(type: "int", nullable: false),
                    Gol = table.Column<int>(type: "int", nullable: true),
                    Assistencia = table.Column<int>(type: "int", nullable: true),
                    ObsScout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inativo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scouts_Jogadores_JogadorId",
                        column: x => x.JogadorId,
                        principalTable: "Jogadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scouts_Parametros_ParametroId",
                        column: x => x.ParametroId,
                        principalTable: "Parametros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scouts_JogadorId",
                table: "Scouts",
                column: "JogadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scouts_ParametroId",
                table: "Scouts",
                column: "ParametroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scouts");
        }
    }
}
