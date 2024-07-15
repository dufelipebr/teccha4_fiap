using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Interfaces;
using System.Net;

namespace apibronco.bronco.com.br.Entity
{
    public class Cobertura : Entidade, IEntidade
    {
        public Cobertura(IntegrationCoberturaDTO dto) 
        { 
            this.Codigo_Susep = dto.Codigo_Susep;
            //this.Codigo_Grupo_Ramo = dto.Codigo_Grupo_Ramo;
            this.Codigo_Identificador = dto.Codigo_Identificador;
            this.Codigo_Moeda = dto.Codigo_Moeda;
            this.Codigo_Susep = dto.Codigo_Susep;
            this.Valor_LMI = dto.Valor_LMI;
            this.Valor_IOF = dto.Valor_IOF;
            this.Valor_Custo_Emiss = dto.Valor_Custo_Emiss;
            this.Valor_Is = dto.Valor_Is;
            this.Valor_Comiss = dto.Valor_Comiss;
            this.Valor_Premio = dto.Valor_Premio;
            this.Descricao = dto.Descricao;
            this.Comentario = dto.Comentario;

            IsValid();
        }


        //public TipoCobertura Tipo_Cobertura { get; set; } //  96001- Básica-Incêndio, raio, explosão, implosão e fumaça
        public string Codigo_Susep { get; set; }
        public string Codigo_Identificador { get; set; }
        public string Descricao { get; set; }

        public string Comentario { get; set; }
        /*        Básica-Incêndio, raio, explosão, implosão e fumaça
Vendaval, furacão, ciclone, tornardo, granizo, impacto de veículos e queda de aeronaves
Danos elétricos
Equipamentos eletrônicos*/

        //public int Codigo_Sequencial { get; set; }
        public decimal Valor_DanoMaximo { get; set; }

        public Decimal Valor_Premio { get; set; }
        public Decimal Valor_IOF { get; set; }
        public Decimal Valor_Custo_Emiss { get; set; }
        public Decimal Valor_Add_Fraq { get; set; }
        public Decimal Valor_Cosseg_Cedido { get; set; }
        public Decimal Valor_LMI { get; set; }
        public Decimal Valor_Is { get; set; }
        public Decimal Valor_Comiss { get; set; }

        public Decimal Valor_Total { 
            get
            {
                return Valor_Premio + Valor_IOF + Valor_Custo_Emiss + Valor_Cosseg_Cedido + Valor_Is + Valor_Comiss;
            } 
        }

        public String Codigo_Moeda { get; set; }

        //public String Codigo_Grupo_Ramo { get; set; }

        public String Id_Ramo { get; set; }

        //public String Codigo_Cobertura_SUSEP { get; set; }


        public bool IsValid()
        {
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Susep, "Codigo_Susep precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Identificador, "Codigo_Identificador precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Descricao, "Descricao precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Comentario, "Comentario precisa ser preenchido.");
            //AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo_Ramo, "Codigo_Grupo_Ramo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Moeda, "Codigo_Grupo_Ramo precisa ser preenchido.");
            AssertionConcern.AssertArgumentRange((double) Valor_Premio , 0.1, 1000000, "Valor_Premio precisa ser preenchido.");

            return true;
        }
    }
}
