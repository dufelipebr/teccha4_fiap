using System.ComponentModel.DataAnnotations;

namespace apibronco.bronco.com.br.DTOs.DTOIntegration
{
    public class IntegrationCoberturaDTO
    {
        [Required]
        public string Codigo_Susep { get; set; }
        [Required]
        public string Codigo_Identificador { get; set; }
        [Required]
        public string Descricao { get; set; }

        [Required]
        public string Comentario { get; set; }

        public decimal Valor_DanoMaximo { get; set; }

        public decimal Valor_Premio { get; set; }
        public decimal Valor_IOF { get; set; }
        public decimal Valor_Custo_Emiss { get; set; }
        public decimal Valor_Add_Fraq { get; set; }
        public decimal Valor_Cosseg_Cedido { get; set; }
        public decimal Valor_LMI { get; set; }
        public decimal Valor_Is { get; set; }
        public decimal Valor_Comiss { get; set; }
        [Required]
        public string Codigo_Moeda { get; set; }
        [Required]
        public String Codigo_Grupo_Ramo { get; set; }

    }
}
