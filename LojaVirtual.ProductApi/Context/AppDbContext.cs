using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CondicaoPagamento> CondicaoPagamentos { get; set; }
        public DbSet<TabelaPreco> TabelaPrecos { get; set; }
        public DbSet<PrecoProdutoCliente> PrecoProdutoClientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<VendaRelatorio> VendaRelatorios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendaRelatorio>().HasNoKey();

            #region Entidades

            #region TabelaPreco
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
            #endregion TabelaPreco

            #region Produtos
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

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.TabelaPreco)
                .WithMany()
                .HasForeignKey(p => p.TabelaPrecoId);
            #endregion Produtos

            #region clientes
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
            #endregion clientes

            #region Vendas
            modelBuilder.Entity<Venda>().HasKey(v => v.Id);

            modelBuilder.Entity<Venda>().
                Property(v => v.Data).
                HasColumnType("date").
                IsRequired();

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteId);

            modelBuilder.Entity<Venda>()
                .HasOne(v => v.CondicaoPagamento)
                .WithMany()
                .HasForeignKey(v => v.CondicaoPagamentoId);
            #endregion Vendas

            #region CondicaoPagamentos
            modelBuilder.Entity<CondicaoPagamento>().HasKey(cp => cp.Id);

            modelBuilder.Entity<CondicaoPagamento>().
                Property(cp => cp.Descricao).
                HasMaxLength(100).
                IsRequired();

            modelBuilder.Entity<CondicaoPagamento>().
                Property(cp => cp.Dias).
                IsRequired();
            #endregion CondicaoPagamentos

            #region PrecoProdutoClientes
            modelBuilder.Entity<PrecoProdutoCliente>().HasKey(ppc => ppc.Id);

            modelBuilder.Entity<PrecoProdutoCliente>().
                Property(ppc => ppc.Valor).
                HasPrecision(12, 2);

            modelBuilder.Entity<PrecoProdutoCliente>()
                .HasOne(ppc => ppc.Produto)
                .WithMany()
                .HasForeignKey(ppc => ppc.ProdutoId);

            modelBuilder.Entity<PrecoProdutoCliente>()
                .HasOne(ppc => ppc.Cliente)
                .WithMany()
                .HasForeignKey(ppc => ppc.ClienteId);
            #endregion PrecoProdutoClientes

            #region VendaItens
            modelBuilder.Entity<VendaItem>().HasKey(vi => vi.Id);

            modelBuilder.Entity<VendaItem>().
                Property(vi => vi.Quantidade).
                IsRequired();

            modelBuilder.Entity<VendaItem>().
                Property(vi => vi.Valor).
                HasPrecision(12,2).
                IsRequired();

            modelBuilder.Entity<VendaItem>()
                .HasOne(vi => vi.Venda)
                .WithMany()
                .HasForeignKey(vi => vi.VendaId);

            modelBuilder.Entity<VendaItem>()
                .HasOne(vi => vi.Produto)
                .WithMany()
                .HasForeignKey(vi => vi.ProdutoId);
            #endregion VendaItens

            #region Notificacoes
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

            modelBuilder.Entity<Notificacao>()
                .HasOne(n => n.Cliente)
                .WithMany()
                .HasForeignKey(n => n.ClienteId);

            modelBuilder.Entity<Notificacao>()
                .HasOne(n => n.Produto)
                .WithMany()
                .HasForeignKey(n => n.ProdutoId);
            #endregion Notificacoes
            
            #endregion Entidades

            #region Relacionamentos
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
            #endregion Relacionamentos
        }

    }
}
