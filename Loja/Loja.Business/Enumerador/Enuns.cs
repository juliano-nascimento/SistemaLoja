namespace Loja.Business.Enumerador
{
    public class Enuns
    {
        public enum StatusFornecedor
        {
            Ativo = 1,
            Inativo = 2,
            Suspenso = 3,
            Cancelado = 4
        }

        public enum Baixado
        {
            Nao = 0,
            Sim = 1
        }

        public enum TipoPagamento
        {
            Boleto = 1,
            Credito = 2,
            Debito = 3
        }

        public enum TipoLancamento
        {
            Recebimento = 1,
            Pagamento = 2,
            Devolucao = 3,
            FechamentoCaixa = 4
        }

        public enum StatusPedido
        {
            Pendente = 1,
            Realizado = 2,
            Enviado = 3,
            Recebido = 4,
            Conferido = 5
        }
    }
}
