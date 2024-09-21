using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.IServices.WebApiDB;

namespace WebApi.Domain.Services.WebApiDB
{
    public class OrgService : IOrgServices
    {
        public IRepository<T_Org> Repository => _repository;
        public IUnitOfWork<T_Org> UnitOfWork => _unitOfWork;

        IOrgUnitOfWork _unitOfWork;
        IRepository<T_Org> _repository;

        public OrgService(IUnitOfWork<T_Org> unitOfWork, IRepository<T_Org> repository)
        {
            _unitOfWork = unitOfWork as IOrgUnitOfWork;
            _repository = repository;
        }
    }
}
