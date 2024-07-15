using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
//using apibronco.bronco.com.br.Repository.Azuredb;
using apibronco.bronco.com.br.Repository.Mongodb;

namespace apibronco.bronco.com.br.Repository
{
    public class Cliente_SeguradoRepository : DapperRepository<Cliente_Segurado>, ISeguradoRepository
    {
        IConfiguration _config;
        ISeguradoRepository _repository;
        public Cliente_SeguradoRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDCliente_Segurado(_config);
            else
                throw new NotImplementedException();
            // _repository = new AZCliente_Segurado(_config);
        }

        public override void Alterar(Cliente_Segurado entidade)
        {
            _repository.Alterar(entidade);
        }

        public override void Cadastrar(Cliente_Segurado entidade)
        {
            _repository.Cadastrar(entidade);
        }

        public override void Deletar(Cliente_Segurado entidade)
        {
            _repository.Deletar(entidade);
        }

        public override bool IsUnique(Cliente_Segurado entidade)
        {
            throw new NotImplementedException();
        }

        public override Cliente_Segurado ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override Cliente_Segurado ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorCodigo(codigo);
        }


        public override IList<Cliente_Segurado> ObterTodos()
        {
            return _repository.ObterTodos();
        }
    }
}
