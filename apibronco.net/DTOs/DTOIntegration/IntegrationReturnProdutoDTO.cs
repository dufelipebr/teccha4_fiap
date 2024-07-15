using apibronco.bronco.com.br.Entity;
using System.Collections.Generic;

namespace apibronco.bronco.com.br.DTOs.DTOIntegration
{
    public class IntegrationReturnProdutoDTO
    {
        private string[] _includedFeatures;

        #region properties
        public string Identificador { get; set; }
        //public Grupo_Ramo Ramo { get; set; }
        //public string Identicador_Ramo { get; set; }
        public string Produto_Descricao { get; set; }
        public string Comentario_Contratacao { get; set; }        
        public string Comentario_Produto { get; set; }
        public decimal Preco_Produto { get; set; }
        public string Moeda { get; set; }

        //public GrupoRamo GrupoRamo { get; set;  }
        //protected IntegrationCoberturaDTO[] Coberturas { get; set; }
        public QuestionarioRisco[] Questionario_Riscos { get; set; }
        
        public string[] IncludedFeatures { get; set;  }
        //{
        //    get {
        //        if (_includedFeatures == null)
        //        { 
        //            List<string> values = new List<string>();   
        //            foreach (IntegrationCoberturaDTO Cob in Coberturas) {
        //                values.Add(Cob.Descricao);
        //            }
        //            _includedFeatures =  values.ToArray();
        //        }
        //        return _includedFeatures;
        //    }
        //}
        #endregion
    }
}
