
using apibronco.bronco.com.br.Entity;
using static apibronco.bronco.com.br.Repository.LogRepository;

namespace apibronco.bronco.com.br.Interfaces
{
    public interface ILogRepository : IRepository<LogInfo>
    {
        public IList<LogInfo> ObterTodosByFilter(LogFilter filter);
    }
}
