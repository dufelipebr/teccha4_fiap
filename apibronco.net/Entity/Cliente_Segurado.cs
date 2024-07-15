using apibronco.bronco.com.br.DTOs;
using apibronco.bronco.com.br.Interfaces;

namespace apibronco.bronco.com.br.Entity
{
    public class Cliente_Segurado : Entidade, IEntidade
    {
        #region constructors
        public Cliente_Segurado(RegisterInfo info) 
        {
            this.Nome = info.nome;
            this.Sobrenome = info.sobre_nome;
            this.Flag_Possui_Nome_social = info.flag_possui_nome_social;
            
            if (Flag_Possui_Nome_social)
                this.Nome_Social = info.nome_social;

            this.Renda = info.option_renda;
            this.Data_Nascimento = info.data_nascimento;
            this.Genero = info.genero;
            this.RG = info.rg;
            this.Celular = info.telefone;
            this.CPF_CNPJ = info.cpf;
            this.Tipo_Segurado = GetTipoSegurado_Padrao();
            this.Email = info.email;
            this.Profissao = info.profissao;
            //this.Endereco_Segurado = endSegurado;

            IsValid();
        }
        #endregion

        #region properties

        public string Identificador_Usuario { get; set; }
        public string Nome { get; set; }

        public string Sobrenome { get; set; }
        
        public bool Flag_Possui_Nome_social { get; set; }

        public string? Nome_Social { get; set; }
        public char Genero { get; set; }

        public DateTime Nascimento { get; set; }

        public string Email { get; set; }
        
        public string Profissao{ get; set; }

        public int Renda { get; set; }

        public string CPF_CNPJ{ get; set; }
        public char Tipo_Segurado { get; set; } // J - Juridica P- Fisica 
        public string RG{ get; set; }

        public DateTime Data_Nascimento { get; set; }
        public Endereco Endereco_Segurado { get; set; }
        //public string Telefone_Comercial { get; set; }

        //public string Telefone_Residencial { get; set; }

        public string Celular { get; set; }

        //public string Telefones { get
        //    {
        //        return $"Celular:{Celular} / Res:{Telefone_Residencial} / Com:{Telefone_Comercial}";
        //    } 
        //}

        public string Telefones
        {
            get
            {
                return $"Tel. Contato:{Celular}";
            }
        }
        #endregion

        #region methods
        // retorna pessoa fisica como padrão, atualmente somente pessoa fisica.
        public char GetTipoSegurado_Padrao()
        {
            return 'F';

        }

        public bool IsValid()
        {
            //AssertionConcern.AssertArgumentRange((double)Codigo_Conta, 1, 1000, "Codigo Conta precisa ser preenchido de 0-1000.");
            //AssertionConcern.AssertArgumentRange((double)Digito_Conta, 0, 99, "Digito Conta precisa ser preenchido de 0-99.");
            //AssertionConcern.AssertArgumentLength(Codigo_Conta, 50, "Codigo do Investimento precisa ter no maximo 10 caracteres.");

            AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome precisa ser preenchido.");
            
            AssertionConcern.AssertArgumentNotEmpty(Sobrenome, "SobreNome precisa ser preenchido.");
            
            if (Flag_Possui_Nome_social)
                AssertionConcern.AssertArgumentNotEmpty(Nome_Social, "Nome Social precisa ser preenchido.");

            AssertionConcern.AssertArgumentNotEmpty(RG, "RG precisa ser preenchido.");

            AssertionConcern.AssertArgumentNotEmpty(Profissao, "Profissão precisa ser preenchido.");

            AssertionConcern.AssertArgumentTrue(Renda > 0, "Renda não pode ser 0.");

            AssertionConcern.AssertArgumentNotEmpty(Celular, "Telefone precisa ser preenchido.");

            AssertionConcern.AssertArgumentNotEmpty(Email, "E-mail precisa ser preenchido.");
            AssertionConcern.AssertArgumentMatches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", Email, "E-mail invalido!");

            AssertionConcern.AssertArgumentNotEmpty(CPF_CNPJ, "CPF precisa ser preenchido.");
            AssertionConcern.AssertArgumentMatches(@"^\d{3}.?\d{3}.?\d{3}-?\d{2}$", CPF_CNPJ, "CPF invalido!");

            AssertionConcern.AssertArgumentTrue(Data_Nascimento > new DateTime(1900,1,1) && Data_Nascimento < new DateTime(2024, 1, 1), "Data de Nascimento precisa ser valida.");


            //AssertionConcern.AssertArgumentNotEquals(TipoAcesso, 0, "Tipo Acesso precisa ser preenchido");

            //AssertionConcern.AssertArgumentRange((double)Saldo_Carteira, 0.1, 10, "Taxa ADM precisa estar entre 0.1 e 10.");
            //AssertionConcern.AssertArgumentRange((double)Saldo_Carteira, 0, 1000000, "Saldo Carteira precisa ser maior que 0 e menor que 1.000.00,00");
            return true;
        }
        #endregion
    }
}
