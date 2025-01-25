using Domain.Entities;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;
using Domain.Interface.IServices;
using Domain.Interface.IServices.WebApiDB;

namespace Domain.Services.WebApiDB
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
