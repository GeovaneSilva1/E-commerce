using LojaVirtual.CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.CatalogoAPI.Context
{
    public class AppDbContextCatalogoApi: DbContext
    {   public AppDbContextCatalogoApi(DbContextOptions<AppDbContextCatalogoApi> options) : base(options)
        {

        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<ImagemProduto> ImagensProdutos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entidades
            #region Produto
            modelBuilder.Entity<Produto>().HasKey(p => p.Handle);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .Property(p => p.SKU)
                .HasMaxLength(100);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(12, 2);
            
            modelBuilder.Entity<Produto>()
                .Property(p => p.PercentualDescontoAvista)
                .HasPrecision(12, 2);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Estoque)
                .IsRequired();

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Marca)
                .WithMany()
                .HasForeignKey(p => p.MarcaId);
            #endregion Produto

            #region Categoria
            modelBuilder.Entity<Categoria>().HasKey(c => c.Handle);
            
            modelBuilder.Entity<Categoria>()
                .Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();
            #endregion Categoria

            #region Marca
            modelBuilder.Entity<Marca>().HasKey(m => m.Handle);
            modelBuilder.Entity<Marca>()
                .Property(m => m.Nome)
                .HasMaxLength(100)
                .IsRequired();
            #endregion Marca

            #region ImagemProduto
            modelBuilder.Entity<ImagemProduto>().HasKey(ip => ip.Handle);
            modelBuilder.Entity<ImagemProduto>()
                .Property(ip => ip.Url)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<ImagemProduto>()
                .HasOne(ip => ip.Produto)
                .WithMany()
                .HasForeignKey(ip => ip.ProdutoId);
            #endregion ImagemProduto
            #endregion Entidades

            #region Relacionamentos
            #region Produto e Categoria
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion Produto e Categoria

            #region Produto e Marca
            modelBuilder.Entity<Marca>()
                .HasMany(m => m.Produtos)
                .WithOne(p => p.Marca)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion Produto e Marca

            #region ImagemProduto e Produto
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ImagemProdutos)
                .WithOne(ip => ip.Produto)
                .HasForeignKey(ip => ip.ProdutoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion ImagemProduto e Produto
            #endregion Relacionamentos
        }
    }
}
