using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebApi.Domain.AggregateRoots;
using WebApi.Domain.Entities;
using WebApi.Domain.Enum;
using WebApi.Domain.Interface.IAggregateRoots;
using WebApi.Domain.Interface.IRepositories;
using WebApi.Domain.Interface.IServices;
using WebApi.Domain.Interface.ISpecifications;
using WebApi.Domain.Specifications;
using WebApi.Filters;

namespace WebApi.Controllers._Shared
{
    [EnableCors("AllowSpecificOrigins")]

    [Route("api/[controller]/[action]")]
    [ApiController]
    /// <summary>
    /// 控制器基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseController<TEntity> : ControllerBase
            where TEntity : class, IEntity, IAggregateRoot
    {
        /// <summary>
        /// 服务层
        /// </summary>
        protected readonly IServices<TEntity> _services;

        /// <summary>
        /// 日志
        /// </summary>
        protected readonly ILogger<BaseController<TEntity>> _logger;


        /// <summary>
        /// 构造函数，用于依赖注入
        /// </summary>
        /// <param name="services">服务层，包含工作单元和仓储</param>
        public BaseController(IServices<T_User> services)
        {
            this.services = services;
        }

        /// <summary>
        /// 构造函数，用于依赖注入
        /// </summary>
        /// <param name="services">服务层，包含工作单元和仓储</param>
        /// <param name="logger">日志</param>
        public BaseController(IServices<TEntity> services, ILogger<BaseController<TEntity>> logger)
        {
            _services = services;
            _logger = logger;

            // 如果上传目录不存在，则自动创建
            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
                _logger.LogInformation($"上传目录已创建: {_uploadFolder}");
            }
        }

        #region 文件上传相关
        //上传限制大小 10MB
        private const long MaxFileSize = 10 * 1024 * 1024;
        //上传限制文件格式
        private static readonly string[] AllowedExtensions = { ".jpg", ".png", ".pdf", ".docx" };
        //上传文件夹
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private IServices<T_User> services;


        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="file">文件数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadSingleFile([FromForm] IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("未选择文件。");
            }

            var validationResult = ValidateFile(file);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            try
            {
                var fileName = await SaveFileAsync(file);
                return Ok(new { FileName = fileName, FilePath = Path.Combine(_uploadFolder, fileName) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "文件上传失败");
                return StatusCode(500, "文件上传失败，请稍后重试。");
            }
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="files">文件数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadMultipleFiles([FromForm] List<IFormFile> files)
        {
            if (files == null || !files.Any())
            {
                return BadRequest("未选择任何文件。");
            }

            var uploadedFiles = new List<object>();
            foreach (var file in files)
            {
                var validationResult = ValidateFile(file);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning($"文件 {file.FileName} 验证失败: {validationResult.Message}");
                    continue;
                }

                try
                {
                    var fileName = await SaveFileAsync(file);
                    uploadedFiles.Add(new { FileName = fileName, FilePath = Path.Combine(_uploadFolder, fileName) });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"文件 {file.FileName} 上传失败。");
                }
            }

            if (!uploadedFiles.Any())
            {
                return BadRequest("所有文件上传均失败，请检查后重试。");
            }

            return Ok(uploadedFiles);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteFile([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest("文件名不能为空。");
            }

            var filePath = Path.Combine(_uploadFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("文件不存在。");
            }

            try
            {
                await DeleteFileAsync(filePath);
                _logger.LogInformation($"文件已删除: {fileName}");
                return Ok($"文件 {fileName} 已成功删除。");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"删除文件 {fileName} 失败。");
                return StatusCode(500, "文件删除失败，请稍后重试。");
            }
        }

        /// <summary>
        /// 校验文件
        /// </summary>
        /// <param name="file">文件数据</param>
        /// <returns></returns>
        private (bool IsValid, string Message) ValidateFile(IFormFile file)
        {
            if (file.Length > MaxFileSize)
            {
                return (false, $"文件大小不能超过 {MaxFileSize / (1024 * 1024)}MB。");
            }

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(fileExtension))
            {
                return (false, $"不支持的文件类型，仅支持: {string.Join(", ", AllowedExtensions)}。");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// 异步保存文件到上传目录
        /// </summary>
        /// <param name="file">文件数据</param>
        /// <returns></returns>
        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_uploadFolder, fileName);

            // 异步写入文件流
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _logger.LogInformation($"文件已保存: {fileName}");
            return fileName;
        }

        /// <summary>
        /// 异步删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        private async Task DeleteFileAsync(string filePath)
        {
            // 异步删除文件
            await Task.Run(() => System.IO.File.Delete(filePath));
        }
        #endregion


        #region CURD通用函数
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="code">主键</param>
        /// <returns></returns>
        [HttpGet]
        public virtual async Task<ActionResult<TEntity>> GetById([FromRoute] Guid code)
        {
            // 通过主键 (Guid) 查询数据
            var entity = await _services.Repository.GetByKeyAsync(code);
            if (entity == null)
            {
                return NotFound(); // 如果没有找到数据，返回 404
            }

            return Ok(entity); // 返回查询结果
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
            entity.CreateTime = DateTime.Now;
            entity.CreateUserCode = Guid.NewGuid();
            return Ok(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="code">标识符</param>
        /// <param name="entity">修改的实体</param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<IActionResult> Update(Guid code, TEntity entity)
        {
            if (!code.Equals(entity.Code))
            {
                return BadRequest();
            }
            var entities = await _services.Repository.GetByKeyAsync(code);
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
        /// <param name="code">标识符</param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteFlag(Guid code)
        {
            var entities = await _services.Repository.GetByKeyAsync(code);
            _services.Repository.Remove(entities);
            return Ok();
        }
        #endregion
    }
}
