using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.DTOs
{
    public class PropostaDTO    
    {
        //public StatusProposta StatusProposta { get; set; }
        public string Status_Proposta { get; set; }

        public DateTime Data_Criacao_Proposta { get; set; }
        public string Codigo_Interno { get; set; }
        public string Codigo_Produto { get; set; } // codigo produto do seguro por exemplo VIDA01 
        public string Codigo_Corretor { get; set; }
        //public Pagamento Pagamento { get; set; }

        //public Cliente_Segurado Segurado { get; set; }
        public string Nome_Segurado { get; set; }

        public decimal? Cobertura_Total { get; set; }
        public decimal? Premio_Total { get; set; }

        public QuestionarioRisco Questionario_Risco { get; set; }
        //public Ramo Ramo { get; set; }

        //public IList<Cobertura> Coberturas { get; set; } // lista de coberturas coberta na apolice/proposta
        //public string Codigo_Empresa { get; set; }

        //public string Codigo_Apolice { get; set; }


        //public string Segurado_Nome { get; set; }

        //public string Segurado_Nome_Social { get; set; }

        //public int Segurado_Genero { get; set; } // 1- Masc, 2-Fem, 3-Outros
        //public string Segurado_CPF_CNPJ { get; set; }
        //public char Segurado_Tipo { get; set; } // J - Juridica P- Fisica 
        //public string Segurado_RG { get; set; }

        //public string Segurado_Profissao { get; set; }

        //public decimal Segurado_Renda_Mensal { get; set; }

        //public DateTime Segurado_Data_Emissao_RG { get; set; }

        //public DateTime Segurado_Data_Nascimento { get; set; }
        //public string Segurado_Endereco_Logradouro { get; set; }
        //public string Segurado_Endereco_Numero { get; set; }
        //public string Segurado_Endereco_Complemento { get; set; }
        //public string Segurado_Endereco_Cep { get; set; }
        //public string Segurado_Endereco_Bairro{ get; set; }
        //public string Segurado_Endereco_UF { get; set; } // dois digitos - SP, MG
        //public string Segurado_Endereco_Pais { get; set; }

        //public string Segurado_Tel_Comercial{ get; set; }

        //public string Segurado_Tel_Residencial { get; set; }

        //public string Segurado_Tel_Celular { get; set; }  


        //public DateTime Data_Emissao { get; set; }

        //public DateTime Data_Inicio_Vigencia { get; set; }

        //public DateTime Data_Fim_Vigencia { get; set; }

        //public DateTime Data_Assinatura_Proposta { get; set; }

        //public string Moeda { get; set; } // Default BRL 

        //public int Id_Ramo_Principal { get; set; } // id do ramo principal susep -- fixo 


        //public string UF_Risco_Principal { get; set; } // provalmente vai seguir endereço do segurado

        //public decimal Valor_Premio { get; set; }

        //public decimal Valor_Total_Segurado { get; set; }

        /// <summary>
        /// public string Codigo_Interno_Susep { get; set; } // vai g
        /// </summary>

        //public decimal Valor_LMG { get; set; }





        public bool IsValid()
        {
            return true;
        }

    }
}
