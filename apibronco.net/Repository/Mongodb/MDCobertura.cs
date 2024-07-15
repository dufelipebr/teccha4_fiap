using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;
using System.Text.Json;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDCobertura : Repository.MongodbBaseRepository<Cobertura>, ICoberturaRepository
    {
        public MDCobertura(IConfiguration configuration) : base(configuration) { 
            
        }
        public override void Alterar(Cobertura entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura");
            var filter = Builders<Cobertura>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Cobertura entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Cobertura entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura"); 
            var filter = Builders<Cobertura>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override bool IsUnique(Cobertura entidade)
        {
            IList<Cobertura> list = ObterTodos();
            var find = list.Where(x => x.Codigo_Identificador == entidade.Codigo_Identificador).FirstOrDefault();
            if (find != null)
                return false;
            else
                return true;
        }

        public override Cobertura ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura");
            var filter = Builders<Cobertura>.Filter.Eq(e => e.Codigo_Identificador, codigo) & Builders<Cobertura>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Cobertura>();
        }

        public override Cobertura ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura"); 
            var filter = Builders<Cobertura>.Filter.Eq(e => e.Id, id) & Builders<Cobertura>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Cobertura>();
        }

        public override IList<Cobertura> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cobertura> _collection = client.GetDatabase(DbName).GetCollection<Cobertura>("cobertura");
            var allDocs = _collection.Find(Builders<Cobertura>.Filter.Eq(e => e.Id_Status, 1)).ToList();
            return allDocs;
        }


    }
}
