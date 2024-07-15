using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository;

namespace apibronco.bronco.com.br.Services
{
    public class IntegrationService
    {
        IProdutoRepository _produtoRepository;
        IGrupoRamoRepository _grupoRamoRepository;
        ICoberturaRepository _coberturaRepository;
        public IntegrationService(
            IProdutoRepository produtoRepository, 
            IGrupoRamoRepository grupoRamoRepository, 
            ICoberturaRepository coberturaRepository)
        {
            _produtoRepository = produtoRepository;
            _grupoRamoRepository = grupoRamoRepository;
            _coberturaRepository = coberturaRepository;
        }


        public  List<IntegrationGrupoRamoDTO> ListarRamoDTO()
        {
            var list = _grupoRamoRepository.ObterTodos();
            List<IntegrationGrupoRamoDTO> integrationGrupoRamoDTOs = new List<IntegrationGrupoRamoDTO>();
            foreach (GrupoRamo item in list)
            {
                integrationGrupoRamoDTOs.Add(new IntegrationGrupoRamoDTO
                {
                    Descricao_Ramo = item.Descricao_Ramo,
                    Codigo_Ramo = item.Codigo_Ramo,
                    Descricao_Grupo = item.Descricao_Grupo,
                    Codigo_Grupo = item.Codigo_Grupo,
                    Codigo_Grupo_SUSEP = item.Codigo_Grupo_SUSEP,
                    Codigo_Ramo_SUSEP = item.Codigo_Ramo_SUSEP,
                });
            }
            return integrationGrupoRamoDTOs;
        }

        public  List<IntegrationCoberturaDTO> ListarCoberturaDTO()
        {
            var list = _coberturaRepository.ObterTodos();
            var listRamo = _grupoRamoRepository.ObterTodos();
            
            List<IntegrationCoberturaDTO> integrationCoberturaDTOs = new List<IntegrationCoberturaDTO>();
            foreach (Cobertura item in list)
            {
                var ramo = listRamo.Where(x => x.Id == item.Id_Ramo).FirstOrDefault();

                integrationCoberturaDTOs.Add(new IntegrationCoberturaDTO
                {
                    //Codigo_Grupo_Ramo = item.Codigo_Grupo_Ramo,
                    Codigo_Identificador = item.Codigo_Identificador,
                    Codigo_Moeda = item.Codigo_Moeda,
                    Codigo_Susep = item.Codigo_Susep,
                    Descricao = item.Descricao,
                    Valor_LMI = item.Valor_LMI,
                    Valor_DanoMaximo = item.Valor_DanoMaximo,
                    Valor_Premio = item.Valor_Premio,
                    Valor_Is = item.Valor_Is,
                    Valor_Add_Fraq = item.Valor_Add_Fraq,
                    Valor_IOF = item.Valor_IOF,
                    Valor_Comiss = item.Valor_Comiss,
                    Valor_Cosseg_Cedido = item.Valor_Cosseg_Cedido,
                    Valor_Custo_Emiss = item.Valor_Custo_Emiss,
                    Comentario = item.Comentario,
                    Codigo_Grupo_Ramo = ramo.Codigo_Ramo
                });
            }
            
            return integrationCoberturaDTOs;
        }
    }
}
