
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Entity;
using MongoDB.Driver;
using Amazon.Auth.AccessControlPolicy;
using MongoDB.Driver.Core.Configuration;
using System.Xml.Linq;

namespace apibronco.bronco.com.br.Repository
{
    public class MongodbGenericRepository : IGenericListRepository
    {
        private readonly string? _connectionString;
        private readonly string? _dbName;
        //private readonly string? _entidadeNome;
        protected string ConnectionString => _connectionString; 
        protected string DbName => _dbName;
        //protected string EntidadeNome => _dbName;


        public MongodbGenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:MongoDB");
            _dbName = configuration.GetValue<string>("ConnectionStrings:MongoDbName");
            //_entidadeNome = entidadeNome;
        }
      
        public List<CondicaoPagto> ObterCondicaoPagtos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<CondicaoPagto> _collection = client.GetDatabase(DbName).GetCollection<CondicaoPagto>("CondicaoPagto");
            //var filter = Builders<Proposta>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(Builders<CondicaoPagto>.Filter.Empty).ToList();
            return allDocs;
        }

        public List<GrupoRamo> ObterGrupoRamos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo");
            //var filter = Builders<Proposta>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(Builders<GrupoRamo>.Filter.Empty).ToList();
            return allDocs;
        }

        public List<Cobertura> ObterCoberturas()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("Cobertura");
            //var filter = Builders<Proposta>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(Builders<Cobertura>.Filter.Empty).ToList();
            return allDocs;
        }

        public string TestConnection()
        {
            var client = new MongoClient(ConnectionString);
            var db = client.GetDatabase(DbName);
            return "";
        }



    }
}
