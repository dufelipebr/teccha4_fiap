using apibronco.bronco.com.br.DTOs;

namespace apibronco.bronco.com.br.Entity
{
    public class Usuario : Entidade
    {
        enum TipoAcesso { OnlineCorretor, BackOffice }

        #region construtores
        public Usuario(RegisterInfo cad)
        {
            this.Nome = cad.nome;
            this.Senha = cad.senha;
            this.Email = cad.email;
            this.TipoPermissao = EnumTipoAcesso.CorretorOnline;
            this.TipoLogin = "Corretor_Online";
        }
        //public Usuario(AlterarUsuarioDTO cad)
        //{
        //    this.Nome = cad.Nome;
        //    this.Senha = cad.Senha;
        //    this.TipoPermissao = cad.TipoAcesso;
        //    this.Email = cad.Email;
        //}

        //public Usuario()
        //{
        //}

        //public Usuario(string _id, String _nome) {
        //    this.Id = _id;
        //    this.Nome = _nome;
        //}
        #endregion

        #region properties 
        public string Email { get; set; }
        public string Nome { get; set; }

        public string Senha { get; set; }

        public string TipoLogin { get; set; } // CORRETOR_ONLINE

        public EnumTipoAcesso TipoPermissao { get; set; }
        #endregion

        #region methods
        public bool IsValid()
        {
            return true;
        }
        #endregion

    }
}
