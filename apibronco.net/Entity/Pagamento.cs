using apibronco.bronco.com.br.DTOs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace apibronco.bronco.com.br.Entity
{
    public enum Pagamento_Status{ Aberto =1, Pago = 2, Cancelado = 3 }
    public class Pagamento : Entidade
    {
        public Pagamento(PagamentoDTO pagDTO)
        {
            this.Codigo_Identificador = $"Proposta:{pagDTO.Reference}";
            this.Codigo_Condicao_Pagto = pagDTO.Codigo_Condicao_Pagto;
            this.Parcelas = pagDTO.Parcelas;
            this.Total_Pagto = pagDTO.Valor_Pagamento;
            this.Reference = pagDTO.Reference;
            //this.Data_Pagamento = DateTime.Now;
            this.Data_Vencimento = DateTime.Now.AddDays(3);
            this.Data_Processamento = DateTime.Now;
            this.Id_Object_Type = "PAGMT";
            this.Id_Status = (int) Pagamento_Status.Aberto;
            //this.Id = String.Empty;
            this.CreatedOn = DateTime.Now;

            if (pagDTO.Cartao_Info != null)
            {
                this.Cartao_Info = new Cartao(pagDTO.Cartao_Info);
            }


            List<Parcelas_Pagamento> parcelas = new List<Parcelas_Pagamento>();
            int parcelaNumero = 1;
            for (int i =1; i <= Parcelas; i++)
            {
                Parcelas_Pagamento p = new Parcelas_Pagamento() {
                    //Pagamento = this,
                    Parcela = parcelaNumero, 
                    Valor_Pagamento = this.Total_Pagto/Parcelas,
                    Data_Pagamento = null,
                    Data_Processamento = DateTime.Now,
                    Data_Vencimento = this.Data_Vencimento.AddMonths(i),
                    Reference = this.Reference
                };
                parcelas.Add(p);
            }
            this.Parcelas_Pagamento = parcelas.ToArray();
            
            IsValid();
        }
        public string Codigo_Condicao_Pagto { get; set; }

        public string Codigo_Identificador { get; set; }

        public string Reference { get; set; }

        public decimal Total_Pagto { get; set; }

        public DateTime? Data_Pagamento { get; set; }

        public DateTime Data_Vencimento { get; set; }

        public DateTime Data_Processamento { get; set; }

        public Cartao? Cartao_Info { get; set; }

        public string Descricao
        {
            get
            {
                return $"Pagamento referente a cobrança de Premio{Reference} ";
            }
        }

        public Parcelas_Pagamento[] Parcelas_Pagamento { get; set; }

        public int Parcelas { get; set; }

        public void IsValid()
        {
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Identificador, "Codigo_Identificador must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Condicao_Pagto,  "Codigo_Condicao_Pagto must not be empty");
            AssertionConcern.AssertArgumentNotEmpty(Reference, "Reference must not be empty");
            AssertionConcern.AssertArgumentRange((double) Total_Pagto, 0.1, 10000000, "Total_Pagto must not be greater than 0");
            if (Data_Pagamento != null)
                AssertionConcern.AssertArgumentTrue(Data_Pagamento > new DateTime(2024, 1, 1), "Data_Pagamento must be greater than 2024-01-01");

            AssertionConcern.AssertArgumentTrue(Data_Processamento > new DateTime(2024, 1, 1), "Data_Processamento must be greater than 2024-01-01");
            AssertionConcern.AssertArgumentTrue(Data_Vencimento > new DateTime(2024, 1, 1), "Data_Vencimento must be greater than 2024-01-01");
        }
    }

    public class Parcelas_Pagamento
    {
        //public string Pagamento { get; set; }
        public int Parcela { get; set; }
        public decimal Valor_Pagamento { get; set; }
        
        public DateTime? Data_Pagamento { get; set; }

        public DateTime Data_Vencimento { get; set; }

        public DateTime Data_Processamento { get; set; }

        public String Reference { get; set; }

    }


    public class Cartao 
    {
        public Cartao(DTOs.Cartao pagDTO) 
        {
            this.CC_CVV = pagDTO.CC_CVV;
            this.CC_Expira = pagDTO.CC_Expira;
            this.CC_Nome = pagDTO.CC_Nome;
            this.CC_Numero = pagDTO.CC_Numero;

            IsValid();
        }

        [Required]
        public string CC_Nome { get; set; }
        [Required]
        public string CC_Numero { get; set; }
        [Required]
        public string CC_CVV { get; set; }
        [Required]
        public string CC_Expira { get; set; }

        public bool IsValid()
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
