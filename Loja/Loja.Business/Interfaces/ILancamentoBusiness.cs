using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Business.Interfaces
{  
   public interface ILancamentoBusiness
    {
        Task<List<LancamentoDto>> ObterLancamentos();
        Task<bool> Create(LancamentoDto pLancamento);
        Task<LancamentoDto> ObterLancamentoPorId(int pId);
        Task<bool> Update(LancamentoDto pLancamento);
        Task<bool> Cancelar(int pId, int pIdUsuario);
        Task<List<ContasDto>> ObterContas();
    }
}
