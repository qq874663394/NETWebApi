using WebApi.Domain.Entities.WebApiDB;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IUnitOfWorks.WebApiDB;

namespace WebApi.Repositories.WebApiDB
{
    public class T_RelevanceUnitOfWork : BaseUnitOfWork<T_Relevance>, IRelevanceUnitOfWork
    {
        public IWebApiRepository<T_Module> Module => _module;
        public IWebApiRepository<T_Relevance> Relevance => _relevance;

        IWebApiRepository<T_Module> _module;
        IWebApiRepository<T_Relevance> _relevance;
        public T_RelevanceUnitOfWork(WebApiRepositoryContext context) : base(context)
        {
            _module = new WebApiRepository<T_Module>(context);
            _relevance = new WebApiRepository<T_Relevance>(context);
        }
    }
}
