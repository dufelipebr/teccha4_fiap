using apibronco.bronco.com.br.DTOs.DTOIntegration;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository.Mongodb;
using Microsoft.AspNetCore.Identity.Data;

namespace apibronco.bronco.com.br.Entity
{
    public class Produto : Entidade, IEntidade
    {
        public Produto(IntegrationProdutoDTO dto) 
        {
            Identificador = dto.Identificador;
            //Identicador_Ramo = dto.Identicador_Ramo;
            Produto_Descricao = dto.Produto_Descricao;
            Comentario_Contratacao = dto.Comentario_Contratacao;
            Comentario_Produto = dto.Comentario_Produto;
            Preco_Produto = dto.Preco_Produto;
            Moeda = dto.Moeda;
            //Coberturas = dto.Coberturas;
            //Questionario_Riscos = dto.Questionario_Riscos;

            //List<QuestionarioRisco> riscos = new List<QuestionarioRisco>();
            //foreach (IntegrationQuestionarioRiscoDTO qr in dto.Questionario_Riscos)
            //{
            //    riscos.Add(new QuestionarioRisco(qr));
            //}

            this.Questionario_Riscos = dto.Questionario_Riscos;

            //List<Cobertura> coberturas = new List<Cobertura>();
            //foreach (IntegrationCoberturaDTO cob in dto.Coberturas)
            //{
            //    coberturas.Add(new Cobertura(cob));
            //}
            //this.Coberturas = coberturas.ToArray(); 

            IsValid();
        }
        #region properties
        public string Identificador { get; set; }
        //public Grupo_Ramo Ramo { get; set; }
        //public string Identicador_Ramo { get; set; }

        public string Ramo_Id { get; set; }
        public string Produto_Descricao { get; set; }
        public string Comentario_Contratacao { get; set; }
        public string Comentario_Produto { get; set; }

        public decimal Preco_Produto { get; set; }
        public string Moeda { get; set; }
        public string[] Cobertura_Ids { get; set; }
        public QuestionarioRisco[] Questionario_Riscos { get; set; }

        #endregion

        #region methods
        public bool IsValid()
        {
            AssertionConcern.AssertArgumentNotEmpty(Identificador, "Identificador precisa ser preenchido.");
            //AssertionConcern.AssertArgumentNotEmpty(Identicador_Ramo, "Identicador_Ramo precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Produto_Descricao, "Produto_Descricao precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Comentario_Produto, "Comentario_Produto precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Comentario_Contratacao, "Comentario_Contratacao precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotEmpty(Moeda, "Produto_Descricao precisa ser preenchido.");
            //AssertionConcern.AssertArgumentNotNull(Coberturas, "Coberturas precisa ser preenchido.");
            AssertionConcern.AssertArgumentNotNull(Questionario_Riscos, "Questionario_Riscos precisa ser preenchido.");

            return true;
        }
        #endregion
    }
}
