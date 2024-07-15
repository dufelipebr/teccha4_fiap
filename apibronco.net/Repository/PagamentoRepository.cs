using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Repository.Mongodb;
//using apibronco.bronco.com.br.Repository.Azuredb;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography.Xml;
//using static apibronco.bronco.com.br.Entity.Pagamento;

namespace apibronco.bronco.com.br.Repository
{
    public class PagamentoRepository : DapperRepository<Pagamento>, IPagamentoRepository
    {
        IConfiguration _config;
        IPagamentoRepository _repository;
        public PagamentoRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDPagamento(_config);
            else
                throw new NotImplementedException();
        }
        public override void Alterar(Pagamento entidade)
        {
            _repository.Alterar(entidade);
        }

        public override void Cadastrar(Pagamento entidade)
        {
            _repository.Cadastrar(entidade);
        }

        public override void Deletar(Pagamento entidade)
        {
            _repository.Deletar(entidade);
        }

        public override bool IsUnique(Pagamento entidade)
        {
            throw new NotImplementedException();
        }

        public override Pagamento ObterPorCodigo(string id)
        {
            throw new NotImplementedException();
        }

        public override Pagamento ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override IList<Pagamento> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        //public Pagamento ObterPorCodigoInterno(string id)
        //{
        //    return _repository.ObterPorCodigoInterno(id);
        //}
    }
}
