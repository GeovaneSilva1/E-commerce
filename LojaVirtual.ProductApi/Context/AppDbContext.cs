using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Context
{
    public class AppDbContext : DbContext
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
            var mysqlServerConnection = "Server=localhost;Database=HavanDB;UId=sa;Password=masterkey;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(mysqlServerConnection);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaRelatorio>().HasNoKey();

            //tabelaprecos
            modelBuilder.Entity<TabelaPreco>().HasKey(tp => tp.Id);

            modelBuilder.Entity<TabelaPreco>().
                Property(tp => tp.Descricao).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<TabelaPreco>().
                Property(tp => tp.DataInicio).
                HasColumnType("date").
                IsRequired();

            modelBuilder.Entity<TabelaPreco>().
                Property(tp => tp.DataFim).
                HasColumnType("date").
                IsRequired();

            //produtos
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);

            modelBuilder.Entity<Produto>().
                Property(p => p.SKU).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<Produto>().
                Property(p => p.Descricao).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<Produto>().
                Property(p => p.Preco).
                HasPrecision(12, 2);

            //clientes
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);

            modelBuilder.Entity<Cliente>().
                Property(c => c.CNPJ).
                HasMaxLength(18).
                IsRequired();
            
            modelBuilder.Entity<Cliente>().
                Property(c => c.RazaoSocial).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<Cliente>().
                Property(c => c.Email).
                HasMaxLength(50).
                IsRequired();

            //vendas
            modelBuilder.Entity<Venda>().HasKey(v => v.Id);

            modelBuilder.Entity<Venda>().
                Property(v => v.Data).
                HasColumnType("date").
                IsRequired();

            //condicaoPagamento
            modelBuilder.Entity<CondicaoPagamento>().HasKey(cp => cp.Id);

            modelBuilder.Entity<CondicaoPagamento>().
                Property(cp => cp.Descricao).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<CondicaoPagamento>().
                Property(cp => cp.Dias).
                IsRequired();

            //PrecoProdutoClientes
            modelBuilder.Entity<PrecoProdutoCliente>().HasKey(ppc => ppc.Id);

            modelBuilder.Entity<PrecoProdutoCliente>().
                Property(ppc => ppc.Valor).
                HasPrecision(12, 2);

            //vendaitens
            modelBuilder.Entity<VendaItem>().HasKey(vi => vi.Id);

            modelBuilder.Entity<VendaItem>().
                Property(vi => vi.Quantidade).
                IsRequired();

            modelBuilder.Entity<VendaItem>().
                Property(vi => vi.Valor).
                HasPrecision(12,2).
                IsRequired();

            //notificacoes
            modelBuilder.Entity<Notificacao>().HasKey(n => n.Id);

            modelBuilder.Entity<Notificacao>().
                Property(n => n.Mensagem).
                HasMaxLength(150).
                IsRequired();

            modelBuilder.Entity<Notificacao>().
                Property(n => n.DataEnvio).
                HasColumnType("date").
                IsRequired();

            modelBuilder.Entity<Notificacao>().
                Property(n => n.Status).
                HasMaxLength(100).
                IsRequired();

            //Relacionamento vendaitens-venda-produto
            modelBuilder.Entity<Venda>().
                HasMany(v => v.VendaItem).
                WithOne(vi => vi.Venda).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Produto>().
                HasMany(p => p.VendaItem).
                WithOne(vi => vi.Produto).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            //Relacionamento Vendas-cliente-condicaopagamento
            modelBuilder.Entity<Cliente>().
                HasMany(c => c.Vendas).
                WithOne(v => v.Cliente)
                .IsRequired().
                OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<CondicaoPagamento>().
                HasMany(cp => cp.Vendas).
                WithOne(v => v.CondicaoPagamento)
                .IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            //Relacionamento Produto-TabelaPreco
            modelBuilder.Entity<TabelaPreco>().
                HasMany(tp => tp.Produtos).
                WithOne(p => p.TabelaPreco).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            //Relacionamento PrecoProdutoCliente-produto-cliente
            modelBuilder.Entity<Produto>().
                HasMany(p => p.PrecoProdutoClientes).
                WithOne(ppc => ppc.Produto).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>().
                HasMany(c => c.PrecoProdutoClientes).
                WithOne(ppc => ppc.Cliente).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

            //Relacionamento notificacoes-cliente-produto
            modelBuilder.Entity<Cliente>().
                HasMany(c => c.Notificacoes).
                WithOne(n => n.Cliente).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Produto>().
                HasMany(p => p.Notificacoes).
                WithOne(n => n.Produto).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);
        }

    }
}
