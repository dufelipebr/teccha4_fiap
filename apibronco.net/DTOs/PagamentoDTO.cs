using apibronco.bronco.com.br.Entity;
using System.ComponentModel.DataAnnotations;

namespace apibronco.bronco.com.br.DTOs
{
    public class PagamentoDTO
    {
        public Cartao? Cartao_Info { get; set; }


        public string Codigo_Condicao_Pagto { get; set; } // Boleto - B, D - Debito, C- Credito, P - PIX

        public decimal Valor_Pagamento { get; set; }


        public int Parcelas { get; set; }


        public string Reference { get; set; }
        //public Parcelas_Pagamento[] Parcelas { get; set; }


    }

    public class Cartao
    {
        [Required]
        public string CC_Nome { get; set; }
        [Required]
        public string CC_Numero { get; set; }
        [Required]
        public string CC_CVV { get; set; }
        [Required]
        public string CC_Expira { get; set; }

        public bool ValidarCartao() 
        {
            AssertionConcern.AssertArgumentLength(CC_Nome, 40, "CC_Nome must have max 40 digits");
            AssertionConcern.AssertArgumentLength(CC_Numero, 16, "CC_Numero must have max 16 digits");
            AssertionConcern.AssertArgumentLength(CC_CVV, 3, "CC_CVV must have max 3 digits");
            AssertionConcern.AssertArgumentLength(CC_Expira, 5, "CC_Expira must have max 5 digits");
            AssertionConcern.AssertArgumentMatches(@"\d{16}", CC_Numero, "CC_Numero invalid format.");
            AssertionConcern.AssertArgumentMatches(@"\d{3}", CC_CVV, "CC_CVV invalid format.");
            AssertionConcern.AssertArgumentMatches(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$", CC_Expira, "CC_Expira invalid format.");

            return true;
        }
    }

}
