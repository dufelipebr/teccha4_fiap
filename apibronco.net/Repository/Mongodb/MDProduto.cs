using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;
using System.Text.Json;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDProduto : Repository.MongodbBaseRepository<Produto>, IProdutoRepository
    {
        public MDProduto(IConfiguration configuration) : base(configuration) { 
            
        }
        public override void Alterar(Produto entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto");
            var filter = Builders<Produto>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Produto entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Produto entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto"); 
            var filter = Builders<Produto>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override bool IsUnique(Produto entidade)
        {
            IList<Produto> produtos = ObterTodos();
            var findProd = produtos.Where(x => x.Identificador == entidade.Identificador).FirstOrDefault();
            if (findProd != null)
                return false;
            else
                return true;
        }

        public override Produto ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto"); 
            var filter = Builders<Produto>.Filter.Eq(e => e.Id, id) & Builders<Produto>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Produto>();
        }

        public override Produto ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto");
            var filter = Builders<Produto>.Filter.Eq(e => e.Identificador, codigo) & Builders<Produto>.Filter.Eq(e => e.Id_Status, 1);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Produto>();
        }

        public override IList<Produto> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Produto> _collection = client.GetDatabase(DbName).GetCollection<Produto>("produto");
            var allDocs = _collection.Find(Builders<Produto>.Filter.Eq(e => e.Id_Status, 1)).ToList();
            return allDocs;
        }


    }
}
