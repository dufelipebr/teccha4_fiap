using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDGrupoRamo : Repository.MongodbBaseRepository<GrupoRamo>, IGrupoRamoRepository
    {
        public MDGrupoRamo(IConfiguration configuration) : base(configuration) { 
            
        }
        public override void Alterar(GrupoRamo entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo");
            var filter = Builders<GrupoRamo>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(GrupoRamo entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(GrupoRamo entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo"); 
            var filter = Builders<GrupoRamo>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override bool IsUnique(GrupoRamo entidade)
        {
            throw new NotImplementedException();
        }

        public override GrupoRamo ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo"); 
            var filter = Builders<GrupoRamo>.Filter.Eq(e => e.Id, id) & Builders<GrupoRamo>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();

            return allDocs.FirstOrDefault<GrupoRamo>();
        }

        public override GrupoRamo ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo");
            var filter = Builders<GrupoRamo>.Filter.Eq(e => e.Codigo_Ramo, codigo) & Builders<GrupoRamo>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();

            return allDocs.FirstOrDefault<GrupoRamo>();
        }

        public override IList<GrupoRamo> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<GrupoRamo> _collection = client.GetDatabase(DbName).GetCollection<GrupoRamo>("GrupoRamo");

            string FilterJSON = "{'Id_Status':1}";
            //string JsonString = JsonSerializer.Serialize(FilterJSON);
            var allDocs = _collection.Find(FilterJSON).ToList(); ;

            //var allDocs = _collection.Find(Builders<GrupoRamo>.Filter.Empty).ToList();
            return allDocs;
        }


        //public GrupoRamo ObterPorNomeGrupoRamoESenha(string email, string senha)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
