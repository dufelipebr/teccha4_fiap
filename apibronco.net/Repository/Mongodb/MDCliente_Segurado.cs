using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDCliente_Segurado : MongodbBaseRepository<Cliente_Segurado>, ISeguradoRepository
    {
        public MDCliente_Segurado(IConfiguration configuration) : base(configuration) { 
            
        }
        public override void Alterar(Cliente_Segurado entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado");
            var filter = Builders<Cliente_Segurado>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Cliente_Segurado entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Cliente_Segurado entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado"); 
            var filter = Builders<Cliente_Segurado>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override bool IsUnique(Cliente_Segurado entidade)
        {
            throw new NotImplementedException();
        }

        public override Cliente_Segurado ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado"); 
            var filter = Builders<Cliente_Segurado>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Cliente_Segurado>();
        }

        public override Cliente_Segurado ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado");
            var filter = Builders<Cliente_Segurado>.Filter.Eq(e => e.Email, codigo);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Cliente_Segurado>();
        }

        public override IList<Cliente_Segurado> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Cliente_Segurado> _collection = client.GetDatabase(DbName).GetCollection<Cliente_Segurado>("Cliente_Segurado");
            var allDocs = _collection.Find(Builders<Cliente_Segurado>.Filter.Empty).ToList();
            return allDocs;
        }


        //public Cliente_Segurado ObterPorNomeCliente_SeguradoESenha(string email, string senha)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
