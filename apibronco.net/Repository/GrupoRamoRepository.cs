using apibronco.bronco.com.br.Repository;
using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using System.Collections.Generic;
using System.Data.SqlClient;
//using apibronco.bronco.com.br.Repository.Azuredb;
using apibronco.bronco.com.br.Repository.Mongodb;

namespace apibronco.bronco.com.br.Repository
{
    public class GrupoRamoRepository : DapperRepository<GrupoRamo>, IGrupoRamoRepository
    {
        IConfiguration _config;
        IGrupoRamoRepository _repository;
        public GrupoRamoRepository(IConfiguration configuration) : base(configuration)
        {
            _config = configuration;
            if (TypeConnection == ConnectionType.Mongodb)
                _repository = new MDGrupoRamo(_config);
            else
                throw new NotImplementedException();
            //_repository = new AZProposta(_config);
        }

        public override void Alterar(GrupoRamo entidade)
        {
            if (entidade.IsValid())
                _repository.Alterar(entidade);
        }

        public override void Cadastrar(GrupoRamo entidade)
        {
            if (entidade.IsValid() && IsUnique(entidade))
                _repository.Cadastrar(entidade);
        }

        public override void Deletar(GrupoRamo entidade)
        {
            if (entidade.IsValid())
                _repository.Deletar(entidade);
        }

        public override bool IsUnique(GrupoRamo entidade)
        {
            IEnumerable<GrupoRamo> listFind = ObterTodos().Where(a => a.Codigo_Ramo == entidade.Codigo_Ramo);
            if (listFind.Count() > 0)
                throw new ArgumentException("UNIQUEKEY: Codigo_Ramo já existente.");

            return true;
        }

        public override GrupoRamo ObterPorId(string id)
        {
            return _repository.ObterPorId(id);
        }

        public override GrupoRamo ObterPorCodigo(string codigo)
        {
            return _repository.ObterPorCodigo(codigo);
        }

        public override IList<GrupoRamo> ObterTodos()
        {
            return _repository.ObterTodos();
        }
    }
}
