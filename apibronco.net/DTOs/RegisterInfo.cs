using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.DTOs
{
    /// <summary>
    /// Informações trafegadas para registro de usuario 
    /// </summary>
    public class RegisterInfo
    {
        public string nome { get; set; }
        public string sobre_nome { get; set; }
        public string nome_social { get; set; }
        public bool flag_possui_nome_social { get; set; }

        public char genero{ get; set; }

        public string rg { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public string profissao { get; set; }
        public int option_renda { get; set; } 
        public string senha { get; set; }

        public DateTime data_nascimento { get; set; }
    }

    //public class AlterarUsuarioDTO
    //{
    //    public string Nome;
    //    public string Senha;
    //    public string Email;
    //    public EnumTipoAcesso TipoAcesso;
    //}
}


//     {id: 'email', message: 'Email precisa ser preenchido e estar no formato correto', type:'valide_email', isValid:false},
//     {id: 'phone', message: 'Telefone precisa ser preenchido', isValid:false},
//     {id: 'profissao', message: 'Profissão precisa ser preenchido', isValid:false},
//     {id: 'renda', message: 'Renda precisa ser preenchido', type:'valide_number', isValid:false},
//     {id: 'endereco_cep', message: 'CEP precisa ser preenchido', type:'valide_cep', isValid:false},
//     {id: 'endereco_rua', message: 'Endereço precisa ser preenchido', isValid:false},
//     {id: 'endereco_numero', message: 'Número precisa ser preenchido', type:'valide_number', isValid:false},
//     //{id: 'endereco_complemento', message: 'Complemento precisa ser preenchido'},
//     {id: 'endereco_cidade', message: 'Cidade precisa ser preenchido', isValid:false},
//     {id: 'endereco_bairro', message: 'Bairro precisa ser preenchido', isValid:false}
//   ]
// });