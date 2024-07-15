
using apibronco.bronco.com.br.Interfaces;
using apibronco.bronco.com.br.Entity;

namespace apibronco.bronco.com.br.Repository
{
    public enum ConnectionType { Mongodb = 1, SqlServer = 2}
    public abstract class DapperRepository<T> : IRepository<T> where T : Entidade
    {
        private readonly string? _connectionString;
        protected string ConnectionString => _connectionString;

        private readonly ConnectionType _typeConnection;
        protected ConnectionType TypeConnection => _typeConnection;

        public DapperRepository(IConfiguration configuration)   
        {
            _connectionString = configuration.GetValue<string>("ConnectionStrings:AzureDB_ConnectionString");
            if (configuration.GetValue<string>("ConnectionType") == "Mongodb")
                _typeConnection = ConnectionType.Mongodb;
            else
                _typeConnection = ConnectionType.SqlServer;
        }
        public abstract void Alterar(T entidade);
        public abstract void Cadastrar(T entidade);
        public abstract void Deletar(T entidade);
        public abstract T ObterPorId(string id);
        public abstract T ObterPorCodigo(string id);
        public abstract bool IsUnique(T entidade);
        public abstract IList<T> ObterTodos();
        
    }
}
