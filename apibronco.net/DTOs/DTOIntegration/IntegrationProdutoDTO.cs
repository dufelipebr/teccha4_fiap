using apibronco.bronco.com.br.Entity;
using System.Collections.Generic;

namespace apibronco.bronco.com.br.DTOs.DTOIntegration
{
    public class IntegrationProdutoDTO
    {
        #region properties
        public string Identificador { get; set; }
        //public Grupo_Ramo Ramo { get; set; }
        public string Identicador_Ramo { get; set; }
        public string Produto_Descricao { get; set; }
        public string Comentario_Contratacao { get; set; }

        public string Comentario_Produto { get; set; }
        public decimal Preco_Produto { get; set; }
        public string Moeda { get; set; }
        public string[] Coberturas_IDs { get; set; } // codigos das coberturas atreladas ao Produto
        public QuestionarioRisco[] Questionario_Riscos { get; set; }

        #endregion
    }
}
