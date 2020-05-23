using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Loja.Domain.Db
{
    public partial class dblojaContext : DbContext
    {
        public dblojaContext()
        {
        }

        public dblojaContext(DbContextOptions<dblojaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AberturaCaixa> AberturaCaixa { get; set; }
        public virtual DbSet<Caixa> Caixa { get; set; }
        public virtual DbSet<Contas> Contas { get; set; }
        public virtual DbSet<FechamentoCaixa> FechamentoCaixa { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<Lancamento> Lancamento { get; set; }
        public virtual DbSet<NivelUsuario> NivelUsuario { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<StatusCaixa> StatusCaixa { get; set; }
        public virtual DbSet<StatusFornecedor> StatusFornecedor { get; set; }
        public virtual DbSet<StatusItem> StatusItem { get; set; }
        public virtual DbSet<StatusPedido> StatusPedido { get; set; }
        public virtual DbSet<StatusUsuario> StatusUsuario { get; set; }
        public virtual DbSet<StatusVenda> StatusVenda { get; set; }
        public virtual DbSet<TipoLancamento> TipoLancamento { get; set; }
        public virtual DbSet<TipoPagamento> TipoPagamento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<VendasItem> VendasItem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;database=dbloja;user id=root;password=123456", x => x.ServerVersion("5.7.19-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AberturaCaixa>(entity =>
            {
                entity.HasKey(e => e.IdAberturaCaixa)
                    .HasName("PRIMARY");

                entity.ToTable("abertura_caixa");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("AberturaCaixaUsuario_idx");

                entity.Property(e => e.IdAberturaCaixa).HasColumnType("int(11)");

                entity.Property(e => e.DataAbertura).HasColumnType("datetime");

                entity.Property(e => e.DataMovimento).HasColumnType("date");

                entity.Property(e => e.NumCaixa).HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AberturaCaixa)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AberturaCaixaUsuario");
            });

            modelBuilder.Entity<Caixa>(entity =>
            {
                entity.HasKey(e => e.IdCaixa)
                    .HasName("PRIMARY");

                entity.ToTable("caixa");

                entity.HasIndex(e => e.AberturaId)
                    .HasName("CaixaAberturaCaixa_idx");

                entity.HasIndex(e => e.FechamentoId)
                    .HasName("CaixaFechamento_idx");

                entity.HasIndex(e => e.OperadorId)
                    .HasName("CaixaOperador_idx");

                entity.HasIndex(e => e.StatusCaixaId)
                    .HasName("CaixaStatusCaixa_idx");

                entity.Property(e => e.IdCaixa).HasColumnType("int(11)");

                entity.Property(e => e.AberturaId)
                    .HasColumnName("Abertura_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataAbertura).HasColumnType("datetime");

                entity.Property(e => e.DataFechamento).HasColumnType("datetime");

                entity.Property(e => e.DataMovimento).HasColumnType("date");

                entity.Property(e => e.FechamentoId)
                    .HasColumnName("Fechamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumCaixa).HasColumnType("int(11)");

                entity.Property(e => e.OperadorId)
                    .HasColumnName("Operador_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusCaixaId)
                    .HasColumnName("StatusCaixa_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalCredito).HasColumnType("decimal(10,2)");

                entity.Property(e => e.TotalDebito).HasColumnType("decimal(10,2)");

                entity.Property(e => e.TotalDinheiro).HasColumnType("decimal(10,2)");

                entity.Property(e => e.TotalVendas).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Abertura)
                    .WithMany(p => p.Caixa)
                    .HasForeignKey(d => d.AberturaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CaixaAberturaCaixa");

                entity.HasOne(d => d.Fechamento)
                    .WithMany(p => p.Caixa)
                    .HasForeignKey(d => d.FechamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CaixaFechamento");

                entity.HasOne(d => d.Operador)
                    .WithMany(p => p.Caixa)
                    .HasForeignKey(d => d.OperadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CaixaOperador");

                entity.HasOne(d => d.StatusCaixa)
                    .WithMany(p => p.Caixa)
                    .HasForeignKey(d => d.StatusCaixaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CaixaStatusCaixa");
            });

            modelBuilder.Entity<Contas>(entity =>
            {
                entity.HasKey(e => e.IdConta)
                    .HasName("PRIMARY");

                entity.ToTable("contas");

                entity.Property(e => e.IdConta).HasColumnType("int(11)");

                entity.Property(e => e.Agencia)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Banco)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Conta)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Saldo).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<FechamentoCaixa>(entity =>
            {
                entity.HasKey(e => e.IdFechamentoCaixa)
                    .HasName("PRIMARY");

                entity.ToTable("fechamento_caixa");

                entity.HasIndex(e => e.LancamentoId)
                    .HasName("FechamentoCaixaLancamento_idx");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("FechamentoCaixaUsuario_idx");

                entity.Property(e => e.IdFechamentoCaixa).HasColumnType("int(11)");

                entity.Property(e => e.DataFechamento).HasColumnType("datetime");

                entity.Property(e => e.DataMovimento).HasColumnType("date");

                entity.Property(e => e.LancamentoId)
                    .HasColumnName("Lancamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumCaixa).HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Lancamento)
                    .WithMany(p => p.FechamentoCaixa)
                    .HasForeignKey(d => d.LancamentoId)
                    .HasConstraintName("FechamentoCaixaLancamento");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.FechamentoCaixa)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FechamentoCaixaUsuario");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(e => e.IdFornecedor)
                    .HasName("PRIMARY");

                entity.ToTable("fornecedor");

                entity.HasIndex(e => e.Cnpj)
                    .HasName("CNPJ_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StatusFornecedorId)
                    .HasName("FornecedorStatusFornecedor_idx");

                entity.Property(e => e.IdFornecedor).HasColumnType("int(11)");

                entity.Property(e => e.Celular)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NomeFornecedor)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusFornecedorId)
                    .HasColumnName("StatusFornecedor_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Telefone)
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.StatusFornecedor)
                    .WithMany(p => p.Fornecedor)
                    .HasForeignKey(d => d.StatusFornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FornecedorStatusFornecedor");
            });

            modelBuilder.Entity<Lancamento>(entity =>
            {
                entity.HasKey(e => e.IdLancamento)
                    .HasName("PRIMARY");

                entity.ToTable("lancamento");

                entity.HasIndex(e => e.ContaId)
                    .HasName("LancamentoConta_idx");

                entity.HasIndex(e => e.FornecedorId)
                    .HasName("LancamentoFornecedor_idx");

                entity.HasIndex(e => e.TipoLancamentoId)
                    .HasName("LancamentoTipoLancamento_idx");

                entity.HasIndex(e => e.TipoPagamentoId)
                    .HasName("LancamentoTipoPagamento_idx");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("LancamentoUsuario_idx");

                entity.Property(e => e.IdLancamento).HasColumnType("int(11)");

                entity.Property(e => e.Baixado).HasColumnType("bit(1)");

                entity.Property(e => e.ContaId)
                    .HasColumnName("Conta_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataCancelamento).HasColumnType("datetime");

                entity.Property(e => e.DataLancamento).HasColumnType("datetime");

                entity.Property(e => e.DataPagamento).HasColumnType("datetime");

                entity.Property(e => e.DataVencimento).HasColumnType("date");

                entity.Property(e => e.DscLancamento)
                    .IsRequired()
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FornecedorId)
                    .HasColumnName("Fornecedor_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TipoLancamentoId)
                    .HasColumnName("TipoLancamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TipoPagamentoId)
                    .HasColumnName("TipoPagamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ValorLancamento).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Conta)
                    .WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.ContaId)
                    .HasConstraintName("LancamentoConta");

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LancamentoFornecedor");

                entity.HasOne(d => d.TipoLancamento)
                    .WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.TipoLancamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LancamentoTipoLancamento");

                entity.HasOne(d => d.TipoPagamento)
                    .WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.TipoPagamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LancamentoTipoPagamento");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Lancamento)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LancamentoUsuario");
            });

            modelBuilder.Entity<NivelUsuario>(entity =>
            {
                entity.HasKey(e => e.IdNivelUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("nivel_usuario");

                entity.Property(e => e.IdNivelUsuario).HasColumnType("int(11)");

                entity.Property(e => e.DscNivelUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PRIMARY");

                entity.ToTable("pedido");

                entity.HasIndex(e => e.FornecedorId)
                    .HasName("PedidoFornecedor_idx");

                entity.HasIndex(e => e.LancamentoId)
                    .HasName("PedidoLancamento_idx");

                entity.HasIndex(e => e.StatusPedidoId)
                    .HasName("PedidoStatusPedido_idx");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("PedidoUsuario_idx");

                entity.Property(e => e.IdPedido).HasColumnType("int(11)");

                entity.Property(e => e.DataPedido).HasColumnType("datetime");

                entity.Property(e => e.DescricaoPedido)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DscProdutos)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FornecedorId)
                    .HasColumnName("Fornecedor_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LancamentoId)
                    .HasColumnName("Lancamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusPedidoId)
                    .HasColumnName("StatusPedido_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PedidoFornecedor");

                entity.HasOne(d => d.Lancamento)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.LancamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PedidoLancamento");

                entity.HasOne(d => d.StatusPedido)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.StatusPedidoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PedidoStatusPedido");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PedidoUsuario");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PRIMARY");

                entity.ToTable("produto");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("ProdutoUsuario_idx");

                entity.Property(e => e.IdProduto).HasColumnType("int(11)");

                entity.Property(e => e.CodDeBarra)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DataAtualizacao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.DataValidade).HasColumnType("date");

                entity.Property(e => e.Entrada).HasColumnType("bit(1)");

                entity.Property(e => e.Estoque).HasColumnType("int(11)");

                entity.Property(e => e.EstoqueMinimo).HasColumnType("int(11)");

                entity.Property(e => e.NomeProduto)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PrecoCompra).HasColumnType("decimal(10,2)");

                entity.Property(e => e.PrecoVenda).HasColumnType("decimal(10,2)");

                entity.Property(e => e.Saida).HasColumnType("bit(1)");

                entity.Property(e => e.Unidade)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProdutoUsuario");
            });

            modelBuilder.Entity<StatusCaixa>(entity =>
            {
                entity.HasKey(e => e.IdStatusCaixa)
                    .HasName("PRIMARY");

                entity.ToTable("status_caixa");

                entity.Property(e => e.IdStatusCaixa).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusCaixa)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<StatusFornecedor>(entity =>
            {
                entity.HasKey(e => e.IdStatusFornecedor)
                    .HasName("PRIMARY");

                entity.ToTable("status_fornecedor");

                entity.Property(e => e.IdStatusFornecedor).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusFornecedor)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<StatusItem>(entity =>
            {
                entity.HasKey(e => e.IdStatusItem)
                    .HasName("PRIMARY");

                entity.ToTable("status_item");

                entity.Property(e => e.IdStatusItem).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusItem)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<StatusPedido>(entity =>
            {
                entity.HasKey(e => e.IdStatusPedido)
                    .HasName("PRIMARY");

                entity.ToTable("status_pedido");

                entity.Property(e => e.IdStatusPedido).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusPedido)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<StatusUsuario>(entity =>
            {
                entity.HasKey(e => e.IdStatusUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("status_usuario");

                entity.Property(e => e.IdStatusUsuario).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusUsuario)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<StatusVenda>(entity =>
            {
                entity.HasKey(e => e.IdStatusVenda)
                    .HasName("PRIMARY");

                entity.ToTable("status_venda");

                entity.Property(e => e.IdStatusVenda).HasColumnType("int(11)");

                entity.Property(e => e.DscStatusVenda)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TipoLancamento>(entity =>
            {
                entity.HasKey(e => e.IdTipoLancamento)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_lancamento");

                entity.Property(e => e.IdTipoLancamento).HasColumnType("int(11)");

                entity.Property(e => e.DscTipoLancamento)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<TipoPagamento>(entity =>
            {
                entity.HasKey(e => e.IdTipoPagamento)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_pagamento");

                entity.Property(e => e.IdTipoPagamento).HasColumnType("int(11)");

                entity.Property(e => e.DscTipoPagamento)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.Cpf)
                    .HasName("CpfUsuario_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NivelUsuarioId)
                    .HasName("UsuarioNivelUsuario_idx");

                entity.HasIndex(e => e.StatusUsuarioId)
                    .HasName("UsuarioStatusUsuario_idx");

                entity.Property(e => e.IdUsuario).HasColumnType("int(11)");

                entity.Property(e => e.Bairro)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Celular)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cidade)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Complemento)
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.DataExpiracao).HasColumnType("date");

                entity.Property(e => e.EmailUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NivelUsuarioId)
                    .HasColumnName("NivelUsuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NomeUsuario)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rg)
                    .IsRequired()
                    .HasColumnName("RG")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Rua)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasColumnType("varchar(256)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusUsuarioId)
                    .HasColumnName("StatusUsuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Telefone)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.NivelUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.NivelUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioNivelUsuario");

                entity.HasOne(d => d.StatusUsuario)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.StatusUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioStatusUsuario");
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.IdVenda)
                    .HasName("PRIMARY");

                entity.ToTable("venda");

                entity.HasIndex(e => e.CaixaId)
                    .HasName("VendaCaixa_idx");

                entity.HasIndex(e => e.StatusVendaId)
                    .HasName("VendaStatusVenda_idx");

                entity.HasIndex(e => e.TipoPagamentoId)
                    .HasName("VendaTipoPagamento_idx");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("VendaUsuario_idx");

                entity.Property(e => e.IdVenda).HasColumnType("int(11)");

                entity.Property(e => e.CaixaId)
                    .HasColumnName("Caixa_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataCancelamento).HasColumnType("datetime");

                entity.Property(e => e.DataMovimento).HasColumnType("date");

                entity.Property(e => e.DataVenda).HasColumnType("datetime");

                entity.Property(e => e.NumCaixa).HasColumnType("int(11)");

                entity.Property(e => e.StatusVendaId)
                    .HasColumnName("StatusVenda_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TipoPagamentoId)
                    .HasColumnName("TipoPagamento_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalCredito).HasColumnType("decimal(10,0)");

                entity.Property(e => e.TotalDebito).HasColumnType("decimal(10,0)");

                entity.Property(e => e.TotalDinheiro).HasColumnType("decimal(10,0)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ValorDesconto).HasColumnType("decimal(10,2)");

                entity.Property(e => e.ValorLiquido).HasColumnType("decimal(10,2)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Caixa)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.CaixaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendaCaixa");

                entity.HasOne(d => d.StatusVenda)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.StatusVendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendaStatusVenda");

                entity.HasOne(d => d.TipoPagamento)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.TipoPagamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendaTipoPagamento");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendaUsuario");
            });

            modelBuilder.Entity<VendasItem>(entity =>
            {
                entity.HasKey(e => e.IdVendasItem)
                    .HasName("PRIMARY");

                entity.ToTable("vendas_item");

                entity.HasIndex(e => e.CaixaId)
                    .HasName("VendasItemCaixa_idx");

                entity.HasIndex(e => e.ProdutoId)
                    .HasName("VendasItemProduto_idx");

                entity.HasIndex(e => e.StatusItemId)
                    .HasName("VendasItemStatusItem_idx");

                entity.HasIndex(e => e.StatusVendaId)
                    .HasName("VendasItemStatusVenda_idx");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("VendasItemUsuario_idx");

                entity.HasIndex(e => e.VendaId)
                    .HasName("VendasItemVenda_idx");

                entity.Property(e => e.IdVendasItem).HasColumnType("int(11)");

                entity.Property(e => e.CaixaId)
                    .HasColumnName("Caixa_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataCancelamento).HasColumnType("datetime");

                entity.Property(e => e.DataMovimento).HasColumnType("date");

                entity.Property(e => e.DtVendaItem).HasColumnType("datetime");

                entity.Property(e => e.NumCaixa).HasColumnType("int(11)");

                entity.Property(e => e.PrecoProduto).HasColumnType("decimal(10,2)");

                entity.Property(e => e.PrecoTotal).HasColumnType("decimal(10,2)");

                entity.Property(e => e.ProdutoId)
                    .HasColumnName("Produto_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QtdProduto).HasColumnType("int(11)");

                entity.Property(e => e.StatusItemId)
                    .HasColumnName("StatusItem_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StatusVendaId)
                    .HasColumnName("StatusVenda_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("Usuario_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.VendaId)
                    .HasColumnName("Venda_Id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Caixa)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.CaixaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemCaixa");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemProduto");

                entity.HasOne(d => d.StatusItem)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.StatusItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemStatusItem");

                entity.HasOne(d => d.StatusVenda)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.StatusVendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemStatusVenda");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemUsuario");

                entity.HasOne(d => d.Venda)
                    .WithMany(p => p.VendasItem)
                    .HasForeignKey(d => d.VendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VendasItemVenda");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
