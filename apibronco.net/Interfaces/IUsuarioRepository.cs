using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario ObterPorNomeUsuarioESenha(string email, string senha);
    }
}
