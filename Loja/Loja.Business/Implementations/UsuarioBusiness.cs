using Loja.Business.Criptografia;
using Loja.Business.Interfaces;
using Loja.Repository.Dtos;
using Loja.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Business.Implementations
{
    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly IUsuarioRepository _repository;
        private Hash senhaHash;
        public UsuarioBusiness(IUsuarioRepository repository)
        {
            _repository = repository;
            senhaHash = new Hash(SHA512.Create());
        }
        public async Task<UsuarioDto> ValidarLogin(string Email, string Senha)
        {
            UsuarioDto model;
            try
            {
                model = await _repository.FindByEmailAsync(Email);
                if (!string.IsNullOrEmpty(model.NomeUsuario))
                {
                    model.Retorno = false;
                    if (senhaHash.VerificarSenha(Senha, model.Senha))
                        model.Retorno = true;
                }
                else
                {
                    model.Retorno = false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
    }
}
