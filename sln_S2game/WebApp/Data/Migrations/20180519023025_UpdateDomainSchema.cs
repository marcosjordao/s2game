using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class UpdateDomainSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_JogoCategorias_CategoriaId",
                table: "Jogos");

            migrationBuilder.DropTable(
                name: "JogoCategorias");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_CategoriaId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Jogos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Jogos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JogoCategorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoCategorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CategoriaId",
                table: "Jogos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_JogoCategorias_CategoriaId",
                table: "Jogos",
                column: "CategoriaId",
                principalTable: "JogoCategorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
