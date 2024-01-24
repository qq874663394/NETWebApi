using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.Entities.WebApiDB;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Interface.IUnitOfWorks.WebApiDB;
using WebApi.Domain.Model;
using WebApi.Repositories.WebApiDB;
using WebApi.Utilities.ConstValue;

namespace WebApi.Domain.Services.WebApiDB
{
    public class RelevanceService : IRelevanceService
    {
        IRelevanceUnitOfWork _unitOfWork;
        public IRelevanceUnitOfWork UnitOfWork => _unitOfWork;
        Lazy<ILogger> _localLogger;

        public RelevanceService(IRelevanceUnitOfWork? unitOfWork,
            ILoggerFactory loggerFactory)
        {
            _localLogger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(GetType()));
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 添加关联
        /// <para>比如给用户分配资源，那么firstId就是用户ID，secIds就是资源ID列表</para>
        /// </summary>
        /// <param name="type">关联的类型，如Define.USERRESOURCE</param>
        public void Assign(AssignReq request)
        {
            Assign(request.type, request.secIds.ToLookup(u => request.firstId));
        }

        /// <summary>
        /// 添加关联，需要人工删除以前的关联
        /// </summary>
        /// <param name="key"></param>
        /// <param name="idMaps"></param>
        public void Assign(string key, ILookup<string, string> idMaps)
        {
            UnitOfWork.Relevance.BatchAdd((from sameVals in idMaps
                                           from value in sameVals
                                           select new T_Relevance
                                           {
                                               Key = key,
                                               FirstId = sameVals.Key,
                                               SecondId = value,
                                               OperateTime = DateTime.Now
                                           }).ToArray());
        }

        /// <summary>
        /// 取消关联
        /// </summary>
        /// <param name="type">关联的类型，如Define.USERRESOURCE</param>
        /// <param name="firstId">The first identifier.</param>
        /// <param name="secIds">The sec ids.</param>
        public void UnAssign(AssignReq req)
        {
            if (req.secIds == null || req.secIds.Length == 0)
            {
                DeleteBy(req.type, req.firstId);
            }
            else
            {
                DeleteBy(req.type, req.secIds.ToLookup(u => req.firstId));
            }
        }

        /// <summary>
        /// 删除关联
        /// </summary>
        /// <param name="key">关联标识</param>
        /// <param name="idMaps">关联的&lt;firstId, secondId&gt;数组</param>
        private void DeleteBy(string key, ILookup<string, string> idMaps)
        {
            foreach (var sameVals in idMaps)
            {
                foreach (var value in sameVals)
                {
                    try
                    {
                        UnitOfWork.Relevance.Delete<T_Relevance>(u => u.Key == key && u.FirstId == sameVals.Key && u.SecondId == value);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        public void DeleteBy(string key, params string[] firstIds)
        {
            UnitOfWork.Relevance.Delete<T_Relevance>(u => firstIds.Contains(u.FirstId) && u.Key == key);
        }


        /// <summary>
        /// 根据关联表的一个键获取另外键的值
        /// </summary>
        /// <param name="key">映射标识</param>
        /// <param name="returnSecondIds">返回的是否为映射表的第二列，如果不是则返回第一列</param>
        /// <param name="ids">已知的ID列表</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> Get(string key, bool returnSecondIds, params string[] ids)
        {
            if (returnSecondIds)
            {
                return UnitOfWork.Relevance.Find(u => u.Key == key
                                            && ids.Contains(u.FirstId)).Select(u => u.SecondId).ToList();
            }
            else
            {
                return UnitOfWork.Relevance.Find(u => u.Key == key
                                            && ids.Contains(u.SecondId)).Select(u => u.FirstId).ToList();
            }
        }

        /// <summary>
        /// 根据key ,firstId,secondId获取thirdId
        /// </summary>
        /// <param name="key"></param>
        /// <param name="firstId"></param>
        /// <param name="secondId"></param>
        /// <returns></returns>
        public List<string> Get(string key, string firstId, string secondId)
        {
            return UnitOfWork.Relevance.Find(u => u.Key == key && u.FirstId == firstId && u.SecondId == secondId)
                .Select(u => u.ThirdId).ToList();
        }

        /// <summary>
        /// 分配数据字段权限
        /// </summary>
        /// <param name="request"></param>
        public void AssignData(AssignDataReq request)
        {
            if (!request.Properties.Any())
            {
                return;
            }

            var relevances = new List<T_Relevance>();
            foreach (var requestProperty in request.Properties)
            {
                relevances.Add(new T_Relevance
                {
                    Key = Define.ROLEDATAPROPERTY,
                    FirstId = request.RoleId,
                    SecondId = request.ModuleCode,
                    ThirdId = requestProperty,
                    OperateTime = DateTime.Now
                });
            }

            UnitOfWork.Relevance.BatchAdd(relevances.ToArray());
        }

        /// <summary>
        /// 取消数据字段分配
        /// </summary>
        /// <param name="request"></param>
        public void UnAssignData(AssignDataReq request)
        {
            if (request.Properties == null || request.Properties.Length == 0)
            {
                if (string.IsNullOrEmpty(request.ModuleCode)) //模块为空，直接把角色的所有授权删除
                {
                    DeleteBy(Define.ROLEDATAPROPERTY, request.RoleId);
                }
                else //把角色的某一个模块权限全部删除
                {
                    DeleteBy(Define.ROLEDATAPROPERTY, new[] { request.ModuleCode }.ToLookup(u => request.RoleId));
                }
            }
            else //按具体的id删除
            {
                foreach (var property in request.Properties)
                {
                    UnitOfWork.Relevance.Delete<T_Relevance>(u => u.Key == Define.ROLEDATAPROPERTY
                                                    && u.FirstId == request.RoleId
                                                    && u.SecondId == request.ModuleCode
                                                    && u.ThirdId == property);
                }
            }
        }

        /// <summary>
        /// 为角色分配用户，需要统一提交，会删除以前该角色的所有用户
        /// </summary>
        /// <param name="request"></param>
        public void AssignRoleUsers(AssignRoleUsers request)
        {
            UnitOfWork.Relevance.ExecuteWithTransaction(() =>
            {
                //删除以前的所有用户
                UnitOfWork.Relevance.Delete<T_Relevance>(u => u.SecondId == request.RoleId && u.Key == Define.USERROLE);
                //批量分配用户角色
                UnitOfWork.Relevance.BatchAdd((from firstId in request.UserIds
                                   select new T_Relevance
                                   {
                                       Key = Define.USERROLE,
                                       FirstId = firstId,
                                       SecondId = request.RoleId,
                                       OperateTime = DateTime.Now
                                   }).ToArray());
            });
        }

        /// <summary>
        /// 为部门分配用户，需要统一提交，会删除以前该部门的所有用户
        /// </summary>
        /// <param name="request"></param>
        public void AssignOrgUsers(AssignOrgUsers request)
        {
            UnitOfWork.Relevance.ExecuteWithTransaction(() =>
            {
                //删除以前的所有用户
                UnitOfWork.Relevance.Delete<T_Relevance>(u => u.SecondId == request.OrgId && u.Key == Define.USERORG);
                //批量分配用户角色
                UnitOfWork.Relevance.BatchAdd((from firstId in request.UserIds
                                   select new T_Relevance
                                   {
                                       Key = Define.USERORG,
                                       FirstId = firstId,
                                       SecondId = request.OrgId,
                                       OperateTime = DateTime.Now
                                   }).ToArray());
            });
        }
    }
}
