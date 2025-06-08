using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CondicaoPagamento> CondicaoPagamentos { get; set; }
        public DbSet<TabelaPreco> TabelaPrecos { get; set; }
        public DbSet<PrecoProdutoCliente> PrecoProdutoClientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<VendaRelatorio> VendaRelatorios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var mySqlConnection = "Server=localhost;Port=3306;Database=HavanDB;Uid=root;Pwd=masterkey";
            optionsBuilder.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
        }
           
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaRelatorio>().HasNoKey();

            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Itens)
                .WithOne()
                .HasForeignKey(vi => vi.VendaId);

            modelBuilder.Entity<TabelaPreco>()
                .HasMany(tp => tp.PrecoProdutoClientes)
                .WithOne()
                .HasForeignKey(vi => vi.TabelaPrecoId);


            modelBuilder.Entity<Produto>()
                .HasMany(p => p.VendaItems)
                .WithOne()
                .HasForeignKey(vi => vi.ProdutoId);
            
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.PrecoProdutoClientes)
                .WithOne()
                .HasForeignKey(ppc => ppc.ProdutoId);

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.Notificacoes)
                .WithOne()
                .HasForeignKey(n => n.ProdutoId);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Vendas)
                .WithOne()
                .HasForeignKey(v => v.ClienteId);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.PrecoProdutoClientes)
                .WithOne()
                .HasForeignKey(ppc => ppc.ClienteId);

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Notificacoes)
                .WithOne()
                .HasForeignKey(n => n.ClienteId); 

            modelBuilder.Entity<CondicaoPagamento>()
                .HasMany(cp => cp.Vendas)
                .WithOne()
                .HasForeignKey(v => v.CondicaoPagamentoId);

            modelBuilder.Entity<TabelaPreco>()
                .HasMany(tb => tb.Produtos)
                .WithOne()
                .HasForeignKey(p => p.TabelaPrecoId);
        }

    }
}
