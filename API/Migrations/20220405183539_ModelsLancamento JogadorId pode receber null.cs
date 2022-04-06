using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class ModelsLancamentoJogadorIdpoderecebernull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancamentos_Jogadores_JogadorId",
                table: "Lancamentos");

            migrationBuilder.AlterColumn<int>(
                name: "JogadorId",
                table: "Lancamentos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamentos_Jogadores_JogadorId",
                table: "Lancamentos",
                column: "JogadorId",
                principalTable: "Jogadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lancamentos_Jogadores_JogadorId",
                table: "Lancamentos");

            migrationBuilder.AlterColumn<int>(
                name: "JogadorId",
                table: "Lancamentos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lancamentos_Jogadores_JogadorId",
                table: "Lancamentos",
                column: "JogadorId",
                principalTable: "Jogadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
