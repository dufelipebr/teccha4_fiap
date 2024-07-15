using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository;
using Microsoft.AspNetCore.Identity.Data;
using apibronco.bronco.com.br.DTOs.DTOIntegration;

namespace apibronco.bronco.com.br.Services
{
    public class ProdutoService
    {
        IProdutoRepository _produtoRepository;
        IGrupoRamoRepository _grupoRamoRepository;
        ICoberturaRepository _coberturaRepository;
        public ProdutoService(IProdutoRepository produtoRepository, IGrupoRamoRepository grupoRamoRepository, ICoberturaRepository coberturaRepository) 
        {
            _produtoRepository = produtoRepository;
            _grupoRamoRepository = grupoRamoRepository;
            _coberturaRepository = coberturaRepository;
        }

        public void SalvarProduto(IntegrationProdutoDTO infoRequest, bool criar)
        {
            Produto produto;

            if (criar) // criação do produto
            { 
                produto = new Produto(infoRequest);
                produto.Id_Object_Type = "PRODUC";
                produto.Id_Status = 1;// Status Ativo
                produto.CreatedOn = DateTime.Now;
            }
            else // // alteração do produto
            {
                produto = _produtoRepository.ObterPorCodigo(infoRequest.Identificador);

                if (produto == null)
                    throw new ArgumentException("Codigo Produto não encontrado original");

                produto.LastUpdateOn = DateTime.Now;
                produto.Produto_Descricao = infoRequest.Produto_Descricao;
                produto.Comentario_Contratacao = infoRequest.Comentario_Contratacao;
                produto.Comentario_Produto = infoRequest.Comentario_Produto;
                produto.Moeda = infoRequest.Moeda;
                produto.Preco_Produto = infoRequest.Preco_Produto;

                //List<QuestionarioRisco> riscos = new List<QuestionarioRisco>();
                //foreach (IntegrationQuestionarioRiscoDTO qr in infoRequest.Questionario_Riscos)
                //{
                //    riscos.Add(new QuestionarioRisco(qr));
                //}
                produto.Questionario_Riscos = infoRequest.Questionario_Riscos;
            }

            //checagem do Ramo
            var ramo = _grupoRamoRepository.ObterPorCodigo(infoRequest.Identicador_Ramo);
            if (ramo == null)
                throw new ArgumentException($"CriarProduto.Codigo_ramo não encontrado {infoRequest.Identicador_Ramo}");

            produto.Ramo_Id = ramo.Id;

            //checagem do Cobertura
            if (infoRequest.Coberturas_IDs.Length == 0)
                throw new ArgumentException($"Coberturas_IDs precisa ser preenchido");

            List<String> cobertura_ids = new List<string>();
            foreach(string cobertura_id in infoRequest.Coberturas_IDs)
            {
                Cobertura cob = _coberturaRepository.ObterPorCodigo(cobertura_id);
                if (cob== null)
                    throw new ArgumentException($"CriarProduto.Cobertura Invalida {cobertura_id}");

                cobertura_ids.Add(cob.Id);
            }
            produto.Cobertura_Ids = cobertura_ids.ToArray();

            //checagem do Questionario de Risco
            if (produto.Questionario_Riscos.Length == 0)
                throw new ArgumentException($"CriarProduto.Questionario_Riscos precisa ser preenchido");

            bool isValid = produto.IsValid(); 
            if (isValid && criar) _produtoRepository.Cadastrar(produto);
            if (isValid && !criar) _produtoRepository.Alterar(produto);

        }

        public List<IntegrationReturnProdutoDTO> ListarProdutosDTO()
        {
            IList<Produto> list = _produtoRepository.ObterTodos();
            List<IntegrationReturnProdutoDTO> produtosDTO = new List<IntegrationReturnProdutoDTO>();

            foreach (Produto produto in list)
            {
                //List<IntegrationCoberturaDTO> coberturas = new List<IntegrationCoberturaDTO>();
                List<string> includedFeatures = new List<string>();
                foreach (string cobertura_Id in produto.Cobertura_Ids)
                {
                    Cobertura cob = _coberturaRepository.ObterPorId(cobertura_Id);

                    if(cob != null)
                    {
                        includedFeatures.Add(cob.Descricao);
                        //coberturas.Add(new IntegrationCoberturaDTO()
                        //{
                        //    Codigo_Identificador = cob.Codigo_Identificador,
                        //    Codigo_Moeda = cob.Codigo_Moeda,
                        //    Codigo_Susep = cob.Codigo_Susep,
                        //    Valor_LMI = cob.Valor_LMI,
                        //    Valor_IOF = cob.Valor_IOF,
                        //    Valor_Custo_Emiss = cob.Valor_Custo_Emiss,
                        //    Valor_Is = cob.Valor_Is,
                        //    Valor_Comiss = cob.Valor_Comiss,
                        //    Valor_Premio = cob.Valor_Premio,
                        //    Descricao = cob.Descricao,
                        //    Comentario = cob.Comentario
                        //});
                    }

                }

                //List<IntegrationQuestionarioRiscoDTO> riscos = new List<IntegrationQuestionarioRiscoDTO>();
                //foreach (QuestionarioRisco info in produto.Questionario_Riscos)
                //{
                //    riscos.Add(new IntegrationQuestionarioRiscoDTO()
                //    {
                //        Numero = info.Numero,
                //        Identificador = info.Identificador,
                //        Pergunta = info.Pergunta,
                //        Tipo_Pergunta = info.Tipo_Pergunta,
                //    });
                //}

                var grupoRamo = _grupoRamoRepository.ObterPorId(produto.Ramo_Id);

                produtosDTO.Add(new IntegrationReturnProdutoDTO
                {
                    Identificador = produto.Identificador,
                    //GrupoRamo = grupoRamo,
                    //Identicador_Ramo = produto.Identicador_Ramo,
                    Produto_Descricao = produto.Produto_Descricao,
                    Comentario_Produto = produto.Comentario_Produto,
                    Comentario_Contratacao = produto.Comentario_Contratacao,
                    Preco_Produto = produto.Preco_Produto,
                    Moeda = produto.Moeda,
                    IncludedFeatures = includedFeatures.ToArray(),
                    Questionario_Riscos = produto.Questionario_Riscos
                });
                
            }

            return produtosDTO;
        }
    }
}
