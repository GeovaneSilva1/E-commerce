using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LojaVirtual.CatalogoAPI.Migrations
{
    /// <inheritdoc />
    public partial class SqlServerCatalogoApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categorias",
                columns: table => new
                {
                    Handle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.Handle);
                });

            migrationBuilder.CreateTable(
                name: "marcas",
                columns: table => new
                {
                    Handle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marcas", x => x.Handle);
                });

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    Handle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Estoque = table.Column<long>(type: "bigint", nullable: false),
                    CategoriaId = table.Column<long>(type: "bigint", nullable: false),
                    MarcaId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.Handle);
                    table.ForeignKey(
                        name: "FK_produtos_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "Handle",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_produtos_marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "marcas",
                        principalColumn: "Handle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "imagensprodutos",
                columns: table => new
                {
                    Handle = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProdutoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_imagensprodutos", x => x.Handle);
                    table.ForeignKey(
                        name: "FK_imagensprodutos_produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produtos",
                        principalColumn: "Handle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_imagensprodutos_ProdutoId",
                table: "imagensprodutos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_CategoriaId",
                table: "produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_produtos_MarcaId",
                table: "produtos",
                column: "MarcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "imagensprodutos");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "categorias");

            migrationBuilder.DropTable(
                name: "marcas");
        }
    }
}
