using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interface.IRepositories;
using Domain.Interface.IRepositories.WebApiDB;
using Domain.Interface.IServices;
using Domain.Interface.IServices.WebApiDB;

namespace Domain.Services.WebApiDB
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
