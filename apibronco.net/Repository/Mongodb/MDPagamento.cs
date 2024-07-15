using apibronco.bronco.com.br.Entity;
using apibronco.bronco.com.br.Interfaces;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using Amazon.Auth.AccessControlPolicy;

namespace apibronco.bronco.com.br.Repository.Mongodb
{
    public class MDPagamento : Repository.MongodbBaseRepository<Pagamento>, IPagamentoRepository
    {
        public MDPagamento(IConfiguration configuration) : base(configuration) { 
            
        }
        public override void Alterar(Pagamento entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento");
            var filter = Builders<Pagamento>.Filter.Eq(e => e.Id, entidade.Id);

            var old = _collection.Find(filter).First();
            var oldId = old.Id;
            _collection.ReplaceOne(filter, entidade);
        }

        public override void Cadastrar(Pagamento entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento"); 
            _collection.InsertOne(entidade);
        }

        public override void Deletar(Pagamento entidade)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento"); 
            var filter = Builders<Pagamento>.Filter.Eq(e => e.Id, entidade.Id);
            _collection.DeleteOne(filter);
        }

        public override Pagamento ObterPorId(string  id)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento"); 
            var filter = Builders<Pagamento>.Filter.Eq(e => e.Id, id);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Pagamento>();
        }

        public override IList<Pagamento> ObterTodos()
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento"); ;
            var allDocs = _collection.Find(Builders<Pagamento>.Filter.Empty).ToList();
            return allDocs;
        }

        public override bool IsUnique(Pagamento entidade)
        {
            IList<Pagamento> pagamentos = ObterTodos();
            var findProd = pagamentos.Where(x => x.Codigo_Identificador == entidade.Codigo_Identificador).FirstOrDefault();
            if (findProd != null)
                return false;
            else
                return true;
        }

        public override Pagamento ObterPorCodigo(string codigo)
        {
            var client = new MongoClient(ConnectionString);
            IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento");
            var filter = Builders<Pagamento>.Filter.Eq(e => e.Codigo_Identificador, codigo);
            var allDocs = _collection.Find(filter).ToList();
            return allDocs.FirstOrDefault<Pagamento>();
        }

        


        //public Pagamento ObterPorCodigoInterno(string codigo_interno)
        //{
        //    var client = new MongoClient(ConnectionString);
        //    IMongoCollection<Pagamento> _collection = client.GetDatabase(DbName).GetCollection<Pagamento>("Pagamento");
        //    var filter = Builders<Pagamento>.Filter.Eq(e => e.Codigo_Interno, codigo_interno);
        //    var allDocs = _collection.Find(filter).ToList();

        //    if (allDocs.Count == 0)
        //        throw new Exception("codigo interno não encontrado");

        //    return allDocs.FirstOrDefault<Pagamento>();
        //}
    }
}
