using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Interface.IServices.WebApiDB;
using WebApi.Domain.Interface.IUnitOfWorks.WebApiDB;
using WebApi.Domain.Model;
using WebApi.Utilities.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessObjsController : ControllerBase
    {
        private readonly IRelevanceUnitOfWork _app;
        public AccessObjsController(IRelevanceUnitOfWork app)
        {
            _app = app;
        }

        /// <summary>
        /// ��ӹ���
        /// <para>������û�������Դ����ôfirstId�����û�ID��secIds������ԴID�б�</para>
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response Assign(AssignReq request)
        {
            var result = new Response();
            try
            {
                _app.Assign(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        [HttpPost]
        public Response UnAssign(AssignReq request)
        {
            var result = new Response();
            try
            {
                _app.UnAssign(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// ��ɫ���������ֶ�Ȩ��
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response AssignDataProperty(AssignDataReq request)
        {
            var result = new Response();
            try
            {
                _app.AssignData(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        /// <summary>
        /// ȡ����ɫ�������ֶ�Ȩ��
        /// <para>���PropertiesΪ�գ���ѽ�ɫ��ĳһ��ģ��Ȩ��ȫ��ɾ��</para>
        /// <para>���moduleIdΪ�գ�ֱ�Ӱѽ�ɫ��������Ȩɾ��</para>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string lockobj = "lock";
        [HttpPost]
        public Response UnAssignDataProperty(AssignDataReq request)
        {
            var result = new Response();
            try
            {
                lock (lockobj)
                {
                    _app.UnAssignData(request);
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        /// <summary>
        /// ��ɫ�����û��������ύ���Ḳ��֮ǰ������
        /// </summary>
        [HttpPost]
        public Response AssignRoleUsers(AssignRoleUsers request)
        {
            var result = new Response();
            try
            {
                _app.AssignRoleUsers(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// ���ŷ����û��������ύ���Ḳ��֮ǰ������
        /// </summary>
        [HttpPost]
        public Response AssignOrgUsers(AssignOrgUsers request)
        {
            var result = new Response();
            try
            {
                _app.AssignOrgUsers(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
    }
}