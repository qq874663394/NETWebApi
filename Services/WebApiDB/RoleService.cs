using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.IServices.WebApiDB;

namespace WebApi.Domain.Services.WebApiDB
{
    public class RoleService : IRoleServices
    {
        public IRepository<T_Role> Repository => _repository;
        public IUnitOfWork<T_Role> UnitOfWork => _unitOfWork;

        IRoleUnitOfWork _unitOfWork;
        IRepository<T_Role> _repository;

        public RoleService(IUnitOfWork<T_Role> unitOfWork, IRepository<T_Role> repository)
        {
            _unitOfWork = unitOfWork as IRoleUnitOfWork;
            _repository = repository;
        }
    }
}
