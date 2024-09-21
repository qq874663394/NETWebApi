using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoots.TreeEntity;
using WebApi.Domain.Entities;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices;

namespace WebApi.Domain.Services
{
    public class Services<T> : IServices<T> where T : class, IEntity, IAggregateRoot
    {
        public IRepository<T> Repository { get; }


        #region 还是放到Services层实现吧...有时候服务层涉及到多个工作单元，
        //public IRepository<T> Repository => _repository;
        //public IUnitOfWork<T> UnitOfWork => _unitOfWork;

        //IUnitOfWork<T> _unitOfWork;
        //IRepository<T> _repository;

        //public Services(IUnitOfWork<T> unitOfWork, IRepository<T> repository)
        //{
        //    _unitOfWork = unitOfWork;
        //    _repository = repository;
        //} 
        #endregion
        // 实现通用服务接口
    }
}
