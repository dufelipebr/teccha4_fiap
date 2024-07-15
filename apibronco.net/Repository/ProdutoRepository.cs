using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
//using apibronco.bronco.com.br.Repository.Azuredb;
using apibronco.bronco.com.br.Repository.Mongodb;

namespace apibronco.bronco.com.br.Repository
{
    public class ProdutoRepository : DapperRepository<Produto>, IProdutoRepository
    {
        IConfiguration _config;
        IProdutoRepository _repository;
        public ProdutoRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDProduto(_config);
            else
                throw new NotImplementedException();
            // _repository = new AZProduto(_config);
        }

        public override void Alterar(Produto entidade)
        {
            if (entidade.IsValid())
                _repository.Alterar(entidade);
        }

        public override void Cadastrar(Produto entidade)
        {
            if (entidade.IsValid() && IsUnique(entidade))
                _repository.Cadastrar(entidade);
        }

        public override void Deletar(Produto entidade)
        {
            _repository.Deletar(entidade);
        }

        public override bool IsUnique(Produto entidade)
        {
            IEnumerable<Produto> listFind = ObterTodos().Where(a => a.Identificador == entidade.Identificador);
            if (listFind.Count() > 0)
                throw new ArgumentException("UNIQUEKEY: Identificador do Produto já existente.");

            return true;
        }

        public override Produto ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override Produto ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorCodigo(codigo);
        }

        public override IList<Produto> ObterTodos()
        {
            return _repository.ObterTodos();
        }
    }
}
