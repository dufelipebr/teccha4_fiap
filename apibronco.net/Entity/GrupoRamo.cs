using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Interfaces;
using System.Linq.Expressions;

namespace apibronco.bronco.com.br.Entity
{
    public class GrupoRamo : Entidade, IEntidade
    {
        #region construtors
        public GrupoRamo() { }
        public GrupoRamo(IntegrationGrupoRamoDTO dto) { 
            this.Codigo_Grupo = dto.Codigo_Grupo;
            this.Codigo_Grupo_SUSEP = dto.Codigo_Grupo_SUSEP;
            this.Descricao_Grupo = dto.Descricao_Grupo;
            this.Codigo_Ramo = dto.Codigo_Ramo;
            this.Codigo_Ramo_SUSEP = dto.Codigo_Ramo_SUSEP;
            this.Descricao_Ramo = dto.Descricao_Ramo;
        }
        #endregion

        public string Codigo_Ramo { get; set; }

        public string Codigo_Ramo_SUSEP { get; set; }
        
        public string Descricao_Ramo { get; set; } // 

        public string Codigo_Grupo { get; set; }

        public string Codigo_Grupo_SUSEP { get; set; }

        public string Descricao_Grupo { get; set; }

        public bool IsValid()
        {
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Ramo, "Codigo_Ramo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Ramo_SUSEP, "Codigo_Ramo_SUSEP precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Descricao_Ramo, "Descricao_Ramo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo, "Codigo_Grupo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Codigo_Grupo_SUSEP, "Codigo_Grupo_SUSEP precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Descricao_Grupo, "Descricao_Grupo precisa ser preenchido.");

            return true;
        }
    }
}
