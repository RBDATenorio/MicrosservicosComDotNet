using System;
using System.Security.Cryptography;
using System.Text;

namespace NSE.Identidade.API.Models
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Senha { get;  private set; }
        public DateTime DataCadastro { get; private set; }
        //public ETipoUsuario TipoUsuario { get; private set; }
        public Usuario(string userName, string senha)
        {
            UserName = userName;
            Senha = Sha256_hash(senha);
            DataCadastro = DateTime.Now;
        }

        private static string Sha256_hash(string value)
        {
            var Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
