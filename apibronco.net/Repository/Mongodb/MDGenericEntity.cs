using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDGenericEntity : Repository.MongodbBaseRepository<Entidade>, IRepository<Entidade>
    {
        public MDGenericEntity(IConfiguration configuration) : base(configuration) { 
            
        }

        public override void Alterar(Entidade entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Entidade> _collection = client.GetDatabase(DbName).GetCollection<Entidade>(entidade.MongoDb_Entity_Name);
            var filter = Builders<Entidade>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Entidade entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Entidade> _collection = client.GetDatabase(DbName).GetCollection<Entidade>(entidade.MongoDb_Entity_Name); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Entidade entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Entidade> _collection = client.GetDatabase(DbName).GetCollection<Entidade>(entidade.MongoDb_Entity_Name); 
            var filter = Builders<Entidade>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override bool IsUnique(Entidade entidade)
        {
            throw new NotImplementedException();
        }

        public override Entidade ObterPorId(Entidade  filter)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Entidade> _collection = client.GetDatabase(DbName).GetCollection<Entidade>(filter.MongoDb_Entity_Name); 
            var filter = Builders<Entidade>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Entidade>();
        }

        public override IList<Entidade> ObterTodos(Entidade filter)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Entidade> _collection = client.GetDatabase(DbName).GetCollection<Entidade>(filter.MongoDb_Entity_Name);
            var allDocs = _collection.Find(Builders<Entidade>.Filter.Empty).ToList();
            return allDocs;
        }


        //public Entidade ObterPorNomeEntidadeESenha(string email, string senha)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
