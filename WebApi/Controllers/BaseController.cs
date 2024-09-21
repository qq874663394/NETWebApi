using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.ISpecifications;
using WebApi.Domain.Specifications;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [EnableCors("AllowSpecificOrigins")]
    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseController<TEntity> : ControllerBase
            where TEntity : class, IEntity, IAggregateRoot
    {
        protected readonly IServices<TEntity> _services;

        /// <summary>
        /// 构造函数，用于依赖注入
        /// </summary>
        /// <param name="services">服务层，包含工作单元和仓储</param>
        public BaseController(IServices<TEntity> services)
        {
            _services = services;
        }
        /// <summary>
        /// 查询全部数据并分页
        /// </summary>
        /// <param name="columnNames">动态查询列名</param>
        /// <param name="columnValue">列值</param>
        /// <param name="sortPredicate">排序字段列名</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>

        [HeadersResultFilter("Access-Control-Expose-Headers", "TotalPages,TotalRecords")]
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll(
            [FromQuery] string[] columnNames,
            [FromQuery] string columnValue,
            [FromQuery] string[] sortPredicate,
            [FromQuery] int pageIndex,
            [FromQuery] int pageSize)
        {
            // 生成条件表达式
            ISpecification<TEntity> _specification = SpecExprExtensions.BuildSpecification<TEntity>(columnNames, columnValue);

            //生成排序表达式
            var _sortPredicate = SpecExprExtensions.GetExpression<TEntity>(sortPredicate);
            //获取数据
            var data = await _services.Repository.GetAllAsync(_specification, _sortPredicate, SortOrder.Descending, pageIndex, pageSize);

            Response.Headers.Add("TotalPages", data.TotalPages.ToString());
            Response.Headers.Add("TotalRecords", data.TotalRecords.ToString());
            return Ok(data);
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create(TEntity entity)
        {
            _services.Repository.Add(entity);
            return Ok(entity);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Id">标识符</param>
        /// <param name="entity">修改的实体</param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<IActionResult> Update(string Id, TEntity entity)
        {
            if (!Id.Equals(entity.Code))
            {
                return BadRequest();
            }
            var entities = await _services.Repository.GetByKeyAsync(Id);
            entities = entity;
            entities.ModifyTime = DateTime.Now;
            //当前用户ID
            entities.ModifyUserCode = Guid.NewGuid();
            _services.Repository.Update(entities);
            return Ok(entities);
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="Id">标识符</param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteFlag(string Id)
        {
            var entities = await _services.Repository.GetByKeyAsync(Id);
            _services.Repository.Remove(entities);
            return Ok();
        }
    }
}
