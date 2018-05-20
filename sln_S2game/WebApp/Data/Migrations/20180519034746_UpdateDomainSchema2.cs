using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebApp.Data.Migrations
{
    public partial class UpdateDomainSchema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Produtoras_ProdutoraId",
                table: "Jogos");

            migrationBuilder.DropTable(
                name: "Produtoras");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_ProdutoraId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "ProdutoraId",
                table: "Jogos");

            migrationBuilder.AddColumn<string>(
                name: "Produtora",
                table: "Jogos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Produtora",
                table: "Jogos");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoraId",
                table: "Jogos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Produtoras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtoras", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_ProdutoraId",
                table: "Jogos",
                column: "ProdutoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Produtoras_ProdutoraId",
                table: "Jogos",
                column: "ProdutoraId",
                principalTable: "Produtoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
