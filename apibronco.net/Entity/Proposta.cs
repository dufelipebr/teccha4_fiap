using apibronco.bronco.com.br.DTOs;
using System.ComponentModel.DataAnnotations;

namespace apibronco.bronco.com.br.Entity
{
    public class Proposta : Entidade
    {
        IntegrationPropostaDTO _propostaDTO; 
        public Proposta(IntegrationPropostaDTO dto)
        {
           // this.Codigo_Produto = dto.Codigo_Produto;
            //this.Codigo_Usuario = dto.Codigo_Corretor;
            this.UF_Risco_Principal = dto.Endereco_Faturamento.UF;
            this.Endereco_Faturamento = dto.Endereco_Faturamento;
            this.Status_Proposta = EnStatusProposta.Aberto;
            this.Codigo_Interno = dto.Codigo_Interno;
            //this.Codigo_Produto = dto.Codigo_Produto;
            this.Codigo_Empresa = dto.Codigo_Empresa;
        
            this.Endereco_Faturamento = dto.Endereco_Faturamento;
            this.Premio_Total = dto.Premio_Total;
            this.Cobertura_Total = dto.Cobertura_Total;
            this.Questionario_Risco = dto.Questionario_Risco;
            this.Moeda = "BRL";


            //if (dto.Condicao_Pagto.Codigo_Condicao_Pagto == "D" || dto.Condicao_Pagto.Codigo_Condicao_Pagto == "C")
            //    this.Pagamento = new Cartao(dto.Condicao_Pagto);
            //else
            //    this.Pagamento = new Pagamento(dto.Condicao_Pagto);

            //this.Pagamento.Reference = $"Proposta:{Codigo_Interno}";

            IsValid();
        }
        //public StatusProposta StatusProposta { get; set; }

        public string Id_Corretor { get; set; }

        public string Id_Segurado { get; set; }

        public string Id_Produto { get; set; }

        public string Id_Ramo_Principal { get; set; }

        public EnStatusProposta Status_Proposta { get; set; }
        public string Codigo_Interno { get; set; }

        //public string Codigo_Produto { get; set; } // codigo produto do seguro por exemplo VIDA01 
        public Cobertura Cobertura_Seguro { get; set; }

        public string Codigo_Empresa { get; set; }

        //public Cliente_Segurado Segurado { get; set; }

        //public Pagamento Pagamento { get; set; }

        public DateTime Data_Emissao { get; set; }

        public DateTime Data_Inicio_Vigencia { get; set; }

        public DateTime Data_Fim_Vigencia { get; set; }

        public DateTime? Data_Assinatura_Proposta { get; set; }

        public string Moeda { get; set; } // Default BRL 

        public decimal? Cobertura_Total { get; set; }

        public decimal? Premio_Total { get; set; }

        public string UF_Risco_Principal { get; set; } // provalmente vai seguir endereço do segurado

        public Endereco Endereco_Faturamento { get; set; }

        public QuestionarioRisco[] Questionario_Risco { get; set; }


        public bool IsValid()
        {
            AssertionConcern.AssertArgumentLength(Codigo_Interno, 50, "Proposta.Codigo_Interno must have max 50 digits");
            AssertionConcern.AssertArgumentLength(Codigo_Empresa, 10, "Proposta.Codigo_Empresa must have max 10 digits");
            AssertionConcern.AssertArgumentLength(Moeda, 3, "Proposta.Moeda must have max 3 digits");
            AssertionConcern.AssertArgumentLength(UF_Risco_Principal, 2, "Proposta.UF_Risco_Principal must have max 2 digits");

            AssertionConcern.AssertArgumentNotEmpty(UF_Risco_Principal, "Proposta.UF_Risco_Principal is empty");
            //AssertionConcern.AssertArgumentNotEmpty(Id_Corretor, "Codigo_Produto is empty");
            AssertionConcern.AssertArgumentNotEmpty(Moeda, "Proposta.Moeda is empty");
            //AssertionConcern.AssertArgumentNotEmpty(Codigo_Condicao_Pagto, "Codigo_Condicao_Pagto is empty");
            //AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo_Ramo, "Codigo_Grupo_Ramo is empty");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Empresa, "Proposta.Codigo_Grupo_Ramo is empty");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Interno, "Proposta.Codigo_Interno is empty");

            AssertionConcern.AssertArgumentTrue(StatusProposta.IsValid(Status_Proposta), "Proposta.Status_Proposta invalid");

            //if (this.Cobertura_Seguro == null)
            //    throw new ArgumentException("Cobertura da proposta não pode ser nula");

            //if (this.Pagamento == null)
            //    throw new ArgumentException("Proposta.Pagamento da proposta não pode ser nula");





            //AssertionConcern.AssertArgument(Codigo_Empresa, 10, "Codigo_Empresa must have max 50 digits");

            return true;
            
        }

    }
}
