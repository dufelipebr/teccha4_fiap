using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository.Mongodb;
//using apibronco.bronco.com.br.Repository.Azuredb;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
//using static apibronco.bronco.com.br.Entity.Proposta;

namespace apibronco.bronco.com.br.Repository
{
    public class PropostaRepository : DapperRepository<Proposta>, IPropostaRepository
    {
        IConfiguration _config;
        IPropostaRepository _repository;
        public PropostaRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDProposta(_config);
            else
                throw new NotImplementedException();
            //_repository = new AZProposta(_config);
        }
        public override void Alterar(Proposta entidade)
        {
            _repository.Alterar(entidade);
        }

        public override void Cadastrar(Proposta entidade)
        {
            _repository.Cadastrar(entidade);
        }

        public override void Deletar(Proposta entidade)
        {
            _repository.Deletar(entidade);
        }

        public override Proposta ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override IList<Proposta> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        /// <summary>
        /// Date filter mongodb:  {"Data_Log":{$gte:ISODate('2024-07-07'),$lt:ISODate('2024-07-09')}} 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override Proposta ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorCodigo(codigo);
        }

        public override bool IsUnique(Proposta entidade)
        {
            throw new NotImplementedException();
        }
    }
}
