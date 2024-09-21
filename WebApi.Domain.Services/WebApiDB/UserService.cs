using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IRepositories.WebApiDB;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.IServices.WebApiDB;

namespace WebApi.Domain.Services.WebApiDB
{
    public class UserService : IUserServices
    {
        public IRepository<T_User> Repository => _repository;
        public IUserUnitOfWork UnitOfWork => _unitOfWork;

        IUserUnitOfWork _unitOfWork;
        IRepository<T_User> _repository;

        public UserService(IUserUnitOfWork unitOfWork, IRepository<T_User> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }
    }
}
