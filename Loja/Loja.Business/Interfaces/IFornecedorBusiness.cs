﻿using Loja.Repository.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Business.Interfaces
{
   public interface IFornecedorBusiness
    {
        Task<List<FornecedorDto>> ObterFornecedoresAtivos();
        Task<bool> Create(FornecedorDto pFornecedor);
        Task<FornecedorDto> ObterFornecedorPorId(int pId);
        Task<bool> Delete(int pIdFornecedor);
        Task<bool> Update(FornecedorDto pFornecedor);
    }
}
