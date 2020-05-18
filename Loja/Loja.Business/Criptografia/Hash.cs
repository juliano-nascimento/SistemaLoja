using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Business.Criptografia
{
   public class Hash
    {
        private HashAlgorithm _algoritimo;
        private StringBuilder sb;
        public Hash(HashAlgorithm algoritimo)
        {
            _algoritimo = algoritimo;
        }

        public string CriptografarSenha(string pSenha)
        {
            sb = new StringBuilder();
            var codigo = Encoding.UTF8.GetBytes(pSenha);
            var password = _algoritimo.ComputeHash(codigo);
            foreach(var caracter in password)
            {
                sb.Append(caracter.ToString("X2"));
            }
            return  sb.ToString();
        }

        public bool VerificarSenha(string pSenhaDigitada, string pSenhaCadastrada)
        {
            sb = new StringBuilder();
            if (string.IsNullOrEmpty(pSenhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha!");

            var password = _algoritimo.ComputeHash(Encoding.UTF8.GetBytes(pSenhaDigitada));
            foreach(var caracter in password)
            {
                sb.Append(caracter.ToString("X2"));
            }
            return sb.ToString() == pSenhaCadastrada;
        }
    }
}
