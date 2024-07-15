namespace apibronco.bronco.com.br.Entity
{
    public class Acordo : Entidade
    {
        public  CondicaoPagto CondicaoPagto { get; set; }
        //public string Codigo_Forma_Pag { get; set; } // Debito, Credito, Boleto
        public int Numero_Banco { get; set; } // Debito, Credito, Boleto
        public int Numero_Agencia { get; set; } // Debito, Credito, Boleto
        public int Numero_Agencia_Digito { get; set; } // Debito, Credito, Boleto
        public int NumeroConta { get; set; } // Debito, Credito, Boleto
        public int ContaDigito { get; set; } // Debito, Credito, Boleto
        public string TipoConta { get; set; } // Poupança, Corrente
        
        
    }
}
