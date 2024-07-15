using apibronco.bronco.com.br.Interfaces;

namespace apibronco.bronco.com.br.Entity
{
    public class Endereco 
    {
        public Endereco(string logradouro, string numero, string complemento, string bairro, string uF, string pais, string cEP)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            UF = uF;
            Pais = pais;
            CEP = cEP;
        }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        
        public string Bairro { get; set; }
        public string UF { get; set; } // dois digitos - SP, MG
        public string Pais { get; set; }

        public string CEP { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
