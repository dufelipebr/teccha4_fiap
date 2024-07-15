using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Entity;
using System.ComponentModel.DataAnnotations;

namespace apibronco.bronco.com.br.DTOs
{
    public class IntegrationReturnPropostaDTO    
    {
        //public StatusProposta StatusProposta { get; set; }
        //public string Status_Proposta { get; set; }

        //public DateTime Data_Criacao_Proposta { get; set; }
        
        public string Codigo_Interno { get; set; }
        
        public string Codigo_Produto { get; set; } // codigo produto do seguro por exemplo VIDA01 
        
        public Corretor Corretor { get; set; }
        //public Pagamento Pagamento { get; set; }

        public Usuario Segurado { get; set; }
        
        //public string Codigo_Segurado { get; set; }
        
        //public Pagamento? Condicao_Pagto { get; set; }

        public QuestionarioRisco[] Questionario_Risco { get; set; }

        
        public decimal? Cobertura_Total { get; set; }
        
        public decimal? Premio_Total { get; set; }
        
        public Endereco Endereco_Faturamento { get; set; }
        
        
        public string Codigo_Empresa { get; set; }

    }
}
