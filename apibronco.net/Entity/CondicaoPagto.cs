namespace apibronco.bronco.com.br.Entity
{
    public class CondicaoPagto : Entidade // 1-DebitoEmConta, 2-Boleto, 3-Credito 
    {
        public CondicaoPagto(string id, string descricao, string codigo, int max_parcelamento)
        {
            this.Descricao = descricao;
            this.Codigo = codigo;
            this.Max_Parcelamento = max_parcelamento;
        }

        public string Id { get; set; }
        public string Descricao {get; set; }
        public string Codigo { get; set; }        
        public int Max_Parcelamento { get; set; }

        public CondicaoPagto[] ListaCondicoesValidas
        {
            get
            {
                return new CondicaoPagto[] { new CondicaoPagto("2", "Boleto", "2", 1) };
            }
        }

    }
}
