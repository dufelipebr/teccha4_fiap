using apibronco.bronco.com.br.DTOs;
using System.Security.Cryptography.X509Certificates;

namespace apibronco.bronco.com.br.Entity
{
    public class Corretor
    {
        public string Codigo_Corretor { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CNPJ { get; set; }

        public string Razao_Social_Empresa { get; set; }

    }
}
