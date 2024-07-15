using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using apibronco.bronco.com.br.Repository.Mongodb;

namespace apibronco.bronco.com.br.Repository
{
    public class LogRepository : DapperRepository<LogInfo>, ILogRepository
    {
        IConfiguration _config;
        ILogRepository _repository;

        public LogRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = (ILogRepository) new MDLogInfo(_config);
            else
                throw new NotImplementedException("interface para sql server do LogInfo não implementada");
                //_repository = new AZProposta(_config);
        }

        public override void Alterar(LogInfo entidade)
        {
            _repository.Alterar(entidade);
        }

        public override void Cadastrar(LogInfo entidade)
        {
            _repository.Cadastrar(entidade);
        }

        public override void Deletar(LogInfo entidade)
        {
            _repository.Deletar(entidade);
        }

        public override bool IsUnique(LogInfo entidade)
        {
            throw new NotImplementedException();
        }

        public override LogInfo ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override LogInfo ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorId(codigo);
        }

        public override IList<LogInfo> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IList<LogInfo> ObterTodosByFilter(LogFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
