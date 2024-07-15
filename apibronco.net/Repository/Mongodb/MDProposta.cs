using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDProposta : Repository.MongodbBaseRepository<Proposta>, IPropostaRepository
    {
        public MDProposta(IConfiguration configuration) : base(configuration) { 
            
        }

        public override void Alterar(Proposta entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta");
            var filter = Builders<Proposta>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Proposta entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Proposta entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta"); 
            var filter = Builders<Proposta>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override Proposta ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta"); 
            var filter = Builders<Proposta>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Proposta>();
        }
       
        public override IList<Proposta> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta"); ;
            var allDocs = _collection.Find(Builders<Proposta>.Filter.Empty).ToList();
            return allDocs;
        }

        ////public Proposta ObterPorCodigo(string codigo_interno)
        ////{
        ////    var client = new MongoClient(ConnectionString);
        ////    IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta");
        ////    var filter = Builders<Proposta>.Filter.Eq(e => e.Codigo_Interno, codigo_interno);
        ////    var allDocs = _collection.Find(filter).ToList();

        ////    if (allDocs.Count == 0)
        ////        throw new Exception("codigo interno não encontrado");

        ////    return allDocs.FirstOrDefault<Proposta>();
        ////}

        public override bool IsUnique(Proposta entidade)
        {
            throw new NotImplementedException();
        }

        public override Proposta ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Proposta> _collection = client.GetDatabase(DbName).GetCollection<Proposta>("proposta");
            var filter = Builders<Proposta>.Filter.Eq(e => e.Codigo_Interno, codigo);
            var allDocs = _collection.Find(filter).ToList();

            if (allDocs.Count == 0)
                throw new Exception("codigo interno não encontrado");

            return allDocs.FirstOrDefault<Proposta>();
        }
    }
}
