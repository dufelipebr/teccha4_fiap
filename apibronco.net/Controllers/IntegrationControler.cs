using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using apibronco.bronco.com.br.Services;
using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Repository.Mongodb;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json.Converters;
using Microsoft.Azure.Amqp.Framing;
using MongoDB.Driver;

namespace apibronco.bronco.com.br.Controllers
{
    [ApiController]
    [Route("integration_interface")]
    public class IntegrationControler : ControllerBase
    {
        //private IPagamentoRepository _pagamentoRepository;
        private IPropostaRepository _propostaRepository;
        private IProdutoRepository _produtoRepository;
        private ICoberturaRepository _coberturaRepository;
        private IGenericListRepository _genericRepository;
        private IGrupoRamoRepository _grupoRamoRepository;
        private IUsuarioRepository _usuarioRepository;
        private IPagamentoRepository _pagamentoRepository;
        private readonly ILogger<IntegrationControler> _logger;


        public IntegrationControler(  
            IPropostaRepository repository, 
            IGenericListRepository genericListRepository, 
            //IPagamentoRepository pagamentoRepository, 
            IProdutoRepository produtoRepository, 
            ICoberturaRepository coberturaRepository,
            IGrupoRamoRepository grupoRamoRepository,
            IUsuarioRepository usuarioRepository,
            IPagamentoRepository pagamentoRepository,
            ILogger<IntegrationControler> logger)
        {
            _propostaRepository = repository;
            _logger = logger;
            _genericRepository = genericListRepository;
            //_pagamentoRepository = pagamentoRepository;
            _produtoRepository = produtoRepository;
            _coberturaRepository = coberturaRepository;
            _grupoRamoRepository = grupoRamoRepository;
            _usuarioRepository = usuarioRepository;
            _pagamentoRepository = pagamentoRepository;
            
        }


        /// <summary>
        /// checkar ws funcionando
        /// </summary>
        //[Authorize]
        [HttpGet("check_service")]
        //public IEnumerable<Proposta> GetPropostaList()
        public IActionResult Check_Service()
        {
            return Ok($"Ok:{_genericRepository.TestConnection()}");
        }

        

        #region generic entities - read only
        /// <summary>
        /// listar condicoes de pagamento
        /// </summary>
        /// <returns>IActionResult com array de investimentos IList/<Investimento/></returns>
        /// <remarks> Exemplo: GetCondicoesPagamento()</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">N�o autenticado</response>
        /// <response code="403">N�o autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("listar_condicoes_pagto")]
        //public IEnumerable<Proposta> GetFormaPagto()
        public IActionResult GetCondicoesPagamento()
        {
            _logger.Log(LogLevel.Information, "Iniciando GetCondicoesPagamento...");
            
            try
            {
                return Ok(_genericRepository.ObterCondicaoPagtos());
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar GetCondicoesPagamento : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

       
        #endregion

        #region Proposta

        /// <summary>
        /// listar investimentos cadastrados no sistema. acesso somente autorizado via token.
        /// </summary>
        /// <returns>IActionResult com array de investimentos IList/<Investimento/></returns>
        /// <remarks> Exemplo: GetInvestimentoList()</remarks>
        /// <response code="200">sucesso</response>
        /// <response code="401">N�o autenticado</response>
        /// <response code="403">N�o autorizado</response>
        /// <response code="501">Erro</response>
        //[Authorize]
        [HttpGet("listar_propostas")]
        //public IEnumerable<Proposta> GetPropostaList()
        public IActionResult listar_propostas()
        {
             //_logger.Log(LogLevel.Information, "Iniciando GetPropostaList...");
            
            try
            {
                IEnumerable<Proposta> list = _propostaRepository.ObterTodos();
                //List<IntegrationPropostaDTO>  returnList = PropostaService.ConvertToDTO(list);
                //return Ok(returnList.ToArray());

                List<IntegrationReturnPropostaDTO> dto = new List<IntegrationReturnPropostaDTO>();
                foreach (var item in list)
                {
                    Usuario segurado =  _usuarioRepository.ObterPorId(item.Id_Segurado);
                    //Pagamento pagamento = _pagamentoRepository.ObterPorCodigo(item.Codigo_Interno);
                    dto.Add(new IntegrationReturnPropostaDTO()
                    {
                        Codigo_Empresa = item.Codigo_Empresa,
                        Codigo_Interno = item.Codigo_Interno,
                        Codigo_Produto = item.Id_Produto,
                        Cobertura_Total = item.Cobertura_Total,
                        Premio_Total = item.Premio_Total,
                        Segurado = segurado, 
                        //Condicao_Pagto = pagamento,
                        Endereco_Faturamento = item.Endereco_Faturamento,
                        Questionario_Risco = item.Questionario_Risco
                    });
                }

                return Ok(dto.ToArray());

            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar _propostaRepository.GetPropostaList() : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("criar_proposta")]
        public IActionResult CreateProposta(IntegrationPropostaDTO propostaDTO)
        {
            _logger.Log(LogLevel.Information, "Iniciando CreateProposta...");

            try
            {
                //List<CondicaoPagto> condicoes  = _genericRepository.ObterCondicaoPagtos();
                //List<GrupoRamo> ramos = _genericRepository.ObterGrupoRamos();
                //List<Cobertura> coberturas = _genericRepository.ObterCoberturas();
                //prop.
                PropostaService service = new PropostaService(_produtoRepository, _grupoRamoRepository, _coberturaRepository, _usuarioRepository);
                Proposta p = service.CriarProposta(propostaDTO);
                _propostaRepository.Cadastrar(p);

                

                Pagamento pag = new Pagamento(propostaDTO.Condicao_Pagto);
                pag.Reference = p.Codigo_Interno;
                _pagamentoRepository.Cadastrar(pag);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar CreateProposta() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        //[Authorize]
        [HttpPut("editar_proposta")]
        public IActionResult AlterProposta(Proposta prop)
        {
            _logger.Log(LogLevel.Information, "Iniciando AlterProposta...");

            try
            {
                //   Proposta p = new Proposta(prop);
                //List<CondicaoPagto> condicoes = _genericRepository.ObterCondicaoPagtos();
                //List<GrupoRamo> ramos = _genericRepository.ObterGrupoRamos();
                //List<Cobertura> coberturas = _genericRepository.ObterCoberturas();

                //Proposta original = _propostaRepository.ObterPorId(prop.Id);

                //if (original == null)
                //    throw new Exception("Não encontrado original");

                //PropostaService service = new PropostaService(condicoes, ramos, coberturas);
                //service.AlterarProposta(prop, original);
                //_propostaRepository.Alterar(prop);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar AlterProposta : {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }

        //[Authorize]
        [HttpDelete("delete_proposta/{proposta_codigo_interno}")]
        public IActionResult DeleteProposta(string proposta_codigo_interno)
        {
            _logger.Log(LogLevel.Information, "Iniciando DeleteProposta...");

            try
            {
                var p = _propostaRepository.ObterPorCodigo(proposta_codigo_interno);
                if (p == null)
                    throw new Exception("proposta_codigo_interno não encontrado");

                _propostaRepository.Deletar(p);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar DeleteProposta(): {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }
        #endregion

        #region Produto
        /// <summary>
        /// listar_produtos
        /// </summary>
        /// <returns>Array de Produtos</returns>
        //[Authorize]
        [HttpGet("listar_produtos")]
        /////https://www2.susep.gov.br/safe/scripts/bnweb/bnmapi.exe?router=upload/8548
        public IActionResult listar_produtos()
        {
            _logger.Log(LogLevel.Information, "Iniciando listar_produtos...");

            try
            {
                ProdutoService prodService = new ProdutoService(_produtoRepository, _grupoRamoRepository, _coberturaRepository);
                return Ok(prodService.ListarProdutosDTO());
                //List<IntegrationProdutoDTO> list = IntegrationService.ConvertProdutoToIntegrationDTO(_produtoRepository.ObterTodos());

            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar listar_produtos : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("criar_produto")]
        public IActionResult criar_produto(IntegrationProdutoDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando criar_produto...");

            try
            {
                ProdutoService prodService = new ProdutoService(_produtoRepository, _grupoRamoRepository, _coberturaRepository);
                prodService.SalvarProduto(infoRequest, true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar criar_produto() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        /// <summary>
        /// Permite atualização de dados do Produto
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        [HttpPut("editar_produto")]
        public IActionResult editar_produto(IntegrationProdutoDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando editar_Produto...");

            try
            {
                ProdutoService prodService = new ProdutoService(_produtoRepository, _grupoRamoRepository, _coberturaRepository);
                prodService.SalvarProduto(infoRequest, false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar editar_produto : {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }

        /// <summary>
        /// Permite Cancelamento produto 
        /// </summary>
        /// <param name="codigo_produto">Codigo da produto</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("cancelar_produto/{codigo_produto}")]
        public IActionResult cancelar_produto(string codigo_produto)
        {
            _logger.Log(LogLevel.Information, "Iniciando cancelar_produto...");

            try
            {
                var ent = _produtoRepository.ObterPorCodigo(codigo_produto);
                //GrupoRamo gr = new GrupoRamo() { Codigo_Ramo = codigo_ramo};
                if (ent == null)
                    throw new Exception("codigo_produto não encontrado");

                //_grupoRamoRepository.Deletar(grupoRamo);
                ent.Identificador = "CANCELADO#" + ent.Identificador;
                ent.Id_Status = 0;
                ent.LastUpdateOn = DateTime.Now;

                _produtoRepository.Alterar(ent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar cancelar_produto(): {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }
        #endregion

        #region Cobertura

        /// <summary>
        /// listar coberturas
        /// </summary>
        /// <returns>Array de Coberturas</returns>
        //[Authorize]
        [HttpGet("listar_coberturas")]
        /////https://www2.susep.gov.br/safe/scripts/bnweb/bnmapi.exe?router=upload/8548
        public IActionResult listar_coberturas()
        {
            _logger.Log(LogLevel.Information, "Iniciando listar_coberturas...");

            try
            {
                IntegrationService itService = new IntegrationService(_produtoRepository, _grupoRamoRepository, _coberturaRepository);
                return Ok(itService.ListarCoberturaDTO());
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar listar_coberturas : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Criação de coberturas
        /// </summary>
        //[Authorize]
        [HttpPost("criar_cobertura")]
        public IActionResult criar_cobertura(IntegrationCoberturaDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando criar_cobertura...");

            try
            {
                Cobertura cobertura = new Cobertura(infoRequest);
                cobertura.Id_Object_Type = "COBER";
                cobertura.Id_Status = 1;
                cobertura.CreatedOn = DateTime.Now;

                var ramo = _grupoRamoRepository.ObterPorCodigo(infoRequest.Codigo_Grupo_Ramo);
                if (ramo == null)
                    throw new ArgumentException("criar_cobertura.Codigo_Grupo_Ramo não encontrado.");

                cobertura.Id_Ramo = ramo.Id;

                _coberturaRepository.Cadastrar(cobertura);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar criar_cobertura() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        //[Authorize]
        [HttpPut("editar_cobertura")]
        public IActionResult editar_cobertura(IntegrationCoberturaDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando editar_cobertura...");

            try
            {
                Cobertura original = _coberturaRepository.ObterPorCodigo(infoRequest.Codigo_Identificador);

                if (original == null)
                    throw new Exception("Cobertura não encontrado original");

                original.Comentario = infoRequest.Comentario;
                //original.Codigo_Moeda = infoRequest.Codigo_Moeda;
                original.Descricao = infoRequest.Descricao;
                original.Codigo_Susep = infoRequest.Codigo_Susep;
                original.Valor_Add_Fraq = infoRequest.Valor_Add_Fraq;
                original.Valor_Comiss = infoRequest.Valor_Comiss;
                original.Valor_Cosseg_Cedido = infoRequest.Valor_Cosseg_Cedido;
                original.Valor_Custo_Emiss = infoRequest.Valor_Custo_Emiss;
                original.Valor_DanoMaximo = infoRequest.Valor_DanoMaximo;
                original.Valor_Is = infoRequest.Valor_Is;
                original.Valor_LMI = infoRequest.Valor_LMI;
                original.Valor_Premio = infoRequest.Valor_Premio;

                _coberturaRepository.Alterar(original);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar AlterProposta : {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }

        /// <summary>
        /// Permite Cancelamento da cobertura 
        /// </summary>
        /// <param name="codigo_cobertura">Codigo da Cobertura</param>
        /// <returns></returns>
        //[Authorize]
        [HttpDelete("cancelar_cobertura/{codigo_cobertura}")]
        public IActionResult cancelar_cobertura(string codigo_cobertura)
        {
            _logger.Log(LogLevel.Information, "Iniciando cancelar_cobertura...");

            try
            {
                var cob = _coberturaRepository.ObterPorCodigo(codigo_cobertura);
                //GrupoRamo gr = new GrupoRamo() { Codigo_Ramo = codigo_ramo};
                if (cob == null)
                    throw new Exception("codigo_cobertura não encontrado");

                //_grupoRamoRepository.Deletar(grupoRamo);
                cob.Codigo_Identificador = "CANCELADO#" + cob.Codigo_Identificador;
                cob.Id_Status = 0;
                cob.LastUpdateOn = DateTime.Now;

                _coberturaRepository.Alterar(cob);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar cancelar_cobertura(): {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }

        #endregion

        #region GrupoRamo

        /// <summary>
        /// listar ramos
        /// </summary>
        /// <returns>Array de GrupoRamo</returns>
        //[Authorize]
        [HttpGet("listar_ramos")]
        /////https://www2.susep.gov.br/safe/scripts/bnweb/bnmapi.exe?router=upload/8548
        public IActionResult listar_ramos()
        {
            _logger.Log(LogLevel.Information, "Iniciando listar_ramos...");

            try
            {
                IntegrationService itService = new IntegrationService(_produtoRepository, _grupoRamoRepository, _coberturaRepository);
                return Ok(itService.ListarRamoDTO());
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar listar_ramos : {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("criar_ramo")]
        public IActionResult criar_ramo(IntegrationGrupoRamoDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando criar_grupo_ramo...");

            try
            {
                GrupoRamo grupoRamo = new GrupoRamo(infoRequest);
                grupoRamo.Id_Object_Type = "GRAMO";
                grupoRamo.Id_Status = 1;// Status Ativo
                grupoRamo.CreatedOn = DateTime.Now;

                _grupoRamoRepository.Cadastrar(grupoRamo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar criar_ramo() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        /// <summary>
        /// Permite atualização dos campos Descricao_Grupo, Descricao_Ramo.
        /// </summary>
        /// <param name="infoRequest"></param>
        /// <returns></returns>
        //[Authorize]
        [HttpPut("editar_ramo")]
        public IActionResult editar_ramo(IntegrationGrupoRamoDTO infoRequest)
        {
            _logger.Log(LogLevel.Information, "Iniciando editar_ramo...");

            try
            {
                GrupoRamo original = _grupoRamoRepository.ObterPorCodigo(infoRequest.Codigo_Ramo);

                if (original == null)
                    throw new Exception("editar_ramo.Ramo não encontrado original");

                original.LastUpdateOn = DateTime.Now;
                original.Descricao_Grupo = infoRequest.Descricao_Grupo;
                original.Descricao_Ramo = infoRequest.Descricao_Grupo;
                //original. = infoRequest.Comentario;
                if (original.IsValid())
                    _grupoRamoRepository.Alterar(original);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar editar_ramo() : {ex.Message}");
                return BadRequest(ex.Message);
            }
            return Ok("ok");
        }

        //[Authorize]
        [HttpDelete("cancelar_ramo/{codigo_ramo}")]
        public IActionResult cancelar_ramo(string codigo_ramo)
        {
            _logger.Log(LogLevel.Information, "Iniciando cancelar_ramo...");

            try
            {
                var grupoRamo =  _grupoRamoRepository.ObterPorCodigo(codigo_ramo);
                //GrupoRamo gr = new GrupoRamo() { Codigo_Ramo = codigo_ramo};
                if (grupoRamo == null)
                    throw new Exception("codigo_ramo não encontrado");

                //_grupoRamoRepository.Deletar(grupoRamo);
                grupoRamo.Codigo_Ramo = "CANCELADO#" + grupoRamo.Codigo_Ramo;
                grupoRamo.Id_Status = 0;
                grupoRamo.LastUpdateOn = DateTime.Now;

                _grupoRamoRepository.Alterar(grupoRamo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"falha ao executar cancelar_ramo(): {ex.Message}");
                return BadRequest();
            }
            return Ok("ok");
        }


        #endregion

    }
}

