
using Loja.Domain.Db;
using Loja.Repository.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loja.Repository.Interfaces
{
    public interface ILancamentoRepository
    {
        Task<bool> Add(Lancamento pLancamento);
        Task<List<LancamentoDto>> FindIdLancamento(int pUsuarioId, int pTipoLancamentoId);
        void DesfazerLancamento(int pId);
    }
}
