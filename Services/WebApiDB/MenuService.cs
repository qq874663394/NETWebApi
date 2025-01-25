using Domain.Entities;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;
using Domain.Interface.IServices;
using Domain.Interface.IServices.WebApiDB;

namespace Domain.Services.WebApiDB
{
    public class MenuService : IMenuServices
    {
        public IMenuUnitOfWork UnitOfWork => _unitOfWork;

        public IRepository<T_Menu> Repository => _repository;


        IMenuUnitOfWork _unitOfWork;
        IRepository<T_Menu> _repository;

        public MenuService(IUnitOfWork<T_Menu> unitOfWork, IRepository<T_Menu> repository)
        {
            _unitOfWork = unitOfWork as IMenuUnitOfWork;
            _repository = repository;
        }
    }
}
