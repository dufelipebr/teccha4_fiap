using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository;
using Microsoft.AspNetCore.Identity.Data;

namespace apibronco.bronco.com.br.Services
{
    public class PropostaService
    {
        //protected List<CondicaoPagto> _validCondicoes;
        //protected List<GrupoRamo> _validRamos;
        //protected List<Cobertura> _validCoberturas;
        IProdutoRepository _produtoRepository;
        IGrupoRamoRepository _grupoRamoRepository;
        ICoberturaRepository _coberturaRepository;
        IUsuarioRepository _usuarioRepository;
        public PropostaService(IProdutoRepository produtoRepository, IGrupoRamoRepository grupoRamoRepository, ICoberturaRepository coberturaRepository, IUsuarioRepository usuarioRepository)
        {
            _produtoRepository = produtoRepository;
            _grupoRamoRepository = grupoRamoRepository;
            _coberturaRepository = coberturaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Proposta CriarProposta(IntegrationPropostaDTO propostaDTO) 
        {
            Proposta p = new Proposta(propostaDTO);

            p.Id = String.Empty;
            p.CreatedOn = DateTime.Now;
            p.LastUpdateOn = DateTime.MinValue;
            p.Id_Object_Type = "PROPO";
            p.Id_Status = (int) EnStatusProposta.Aberto; // Active
          

            p.Data_Emissao = DateTime.Now;
            p.Data_Fim_Vigencia = DateTime.Now.AddYears(1);
            p.Data_Inicio_Vigencia = DateTime.Now;

            p.Id_Corretor = "0";
            p.Id_Ramo_Principal = "0";

            try {
                p.Id_Segurado = _usuarioRepository.ObterPorCodigo(propostaDTO.Codigo_Segurado).Id;
                //p.Id_Segurado = _usersList.Where(e => e.Email == propostaDTO.Codigo_Segurado).FirstOrDefault().Id;
            }
            catch{
                throw new ArgumentException($"Problema para obter o Codigo_Segurado {propostaDTO.Codigo_Segurado}");
            }

            try {
                p.Id_Produto = _produtoRepository.ObterPorCodigo(propostaDTO.Codigo_Produto).Id;
                //p.Id_Produto = _produtoList.Where(e => e.Identificador == propostaDTO.Codigo_Produto).FirstOrDefault().Id;
            }
            catch{
                throw new ArgumentException($"Problema para obter o Codigo_Produto {propostaDTO.Codigo_Produto}");
            }

            


            //var ramo = _grupoRamoRepository.ObterPorCodigo(infoRequest.Identicador_Ramo);
            //if (ramo == null)
            //    throw new ArgumentException($"CriarProduto.Codigo_ramo não encontrado {infoRequest.Identicador_Ramo}");


            if (p.IsValid())
                ValidarProposta(p);

            return p;
            //_propostaRepository.Cadastrar(p);
        }

        


        public void AlterarProposta(Proposta dest, Proposta ori)
        {
            Proposta p = ori;

            p.Codigo_Empresa = dest.Codigo_Empresa;
            p.Codigo_Interno = dest.Codigo_Interno;
            //p.Codigo_Produto = dest.Codigo_Produto;
            p.Data_Assinatura_Proposta = dest.Data_Assinatura_Proposta;
            p.Data_Emissao = dest.Data_Emissao;
            p.Data_Fim_Vigencia = dest.Data_Fim_Vigencia;
            p.Data_Inicio_Vigencia =dest.Data_Inicio_Vigencia;
            //p.Codigo_Condicao_Pagto = dest.Codigo_Condicao_Pagto;
            //p.Codigo_Grupo_Ramo = dest.Codigo_Grupo_Ramo;
            //p.Status_Proposta = dest.Status_Proposta;
            p.UF_Risco_Principal = dest.UF_Risco_Principal;
            //p.Coberturas = dest.Coberturas;
            p.LastUpdateOn = DateTime.Now;// Setar Com Data Atual 


            if (p.Id == null || p.Id.Trim() == "")
                throw new Exception("Id não informado");

            if (p.IsValid())
                ValidarProposta(p);

        }

        private bool ValidarProposta(Proposta p) 
        {
            if (p.Id_Segurado == null  || p.Id_Segurado == "")
                throw new Exception("Segurado não informado");

            if (p.Id_Produto == null || p.Id_Produto == "")
                throw new Exception("Produto não informado");

            //if (p.Codigo_Grupo_Ramo == String.Empty)
            //    throw new Exception("Ramo não informado");

            if (p.Moeda != "BRL")
                throw new Exception("BRL deve ser informado para moeda");

            //if (p.Coberturas == null || p.Coberturas.Length == 0)
            //    throw new Exception("Coberturas devem ser informadas");

            //if (p.Codigo_Empresa == null)
            //    throw new Exception("Codigo da Empresa deve ser informado");

            //if (p.Pagamento == null)
            //    throw new Exception("Forma de pagamento deve ser informado");

            //if (p.Cobertura_Seguro == null)
            //    throw new Exception("Precisa informar Cobertura");

            //var cond = _validCondicoes.FirstOrDefault(x => x.Codigo == p.Codigo_Condicao_Pagto);
            //if (cond == null)
            //    throw new Exception("Codigo_Condicao_Pagamento invalida");

            //var ramos = _validRamos.FirstOrDefault(x => x.Codigo_Ramo == p.Codigo_Grupo_Ramo);
            //if (ramos == null)
            //    throw new Exception("CriarProposta.erro8: Id_Ramo invalida");


            //foreach (var cob in p.Coberturas)
            //    if (_validCoberturas.FirstOrDefault(x => x.Codigo == cob.Codigo) == null)
            //        throw new Exception("CriarProposta.erro8: Cobertura invalida");

            return true;
        }

        //public List<IntegrationReturnPropostaDTO> ConvertToDTO(List<Proposta> propostas)
        //{
          

        //}
    

     
    }
}
