using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
//using apibronco.bronco.com.br.Repository.Azuredb;
using apibronco.bronco.com.br.Repository.Mongodb;

namespace apibronco.bronco.com.br.Repository
{
    public class CoberturaRepository : DapperRepository<Cobertura>, ICoberturaRepository
    {
        IConfiguration _config;
        IRepository<Cobertura> _repository;
        public CoberturaRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDCobertura(_config);
            else
                throw new NotImplementedException();
            //_repository = new AZProposta(_config);
        }

        public override void Alterar(Cobertura entidade)
        {
            if (entidade.IsValid())
                _repository.Alterar(entidade);
        }

        public override void Cadastrar(Cobertura entidade)
        {
            if (entidade.IsValid() && IsUnique(entidade))
                _repository.Cadastrar(entidade);
        }

        public override void Deletar(Cobertura entidade)
        {
            _repository.Deletar(entidade);
        }

        public override bool IsUnique(Cobertura entidade)
        {
            IEnumerable<Cobertura> listFind = ObterTodos().Where(a => a.Codigo_Identificador == entidade.Codigo_Identificador);
            if (listFind.Count() > 0)
                throw new ArgumentException("UNIQUEKEY: Codigo_Identificador já existente.");
            
            return true;
        }

        public override Cobertura ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override Cobertura ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorCodigo(codigo);
        }


        public override IList<Cobertura> ObterTodos()
        {
            return _repository.ObterTodos();
        }
    }
}
