using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApi.Domain.AggregateRoot;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Entities.WebApiDB
{
    public partial class T_Relevance : Entity, IAggregateRoot
    {
        public override string Key { get => Key; set => Key = value; }

        public override string KeyName => "Id";
        [Key]
        public string Id { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        [Description("描述")]
        public string Description { get; set; }
        /// <summary>
        /// 映射标识
        /// </summary>
        [Description("映射标识")]
        public string ModelName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Description("状态")]
        public int Status { get; set; }
        /// <summary>
        /// 授权时间
        /// </summary>
        [Description("授权时间")]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 授权人
        /// </summary>
        [Description("授权人")]
        public string OperatorId { get; set; }
        /// <summary>
        /// 第一个表主键ID
        /// </summary>
        [Description("第一个表主键ID")]
        public string FirstId { get; set; }
        /// <summary>
        /// 第二个表主键ID
        /// </summary>
        [Description("第二个表主键ID")]
        public string SecondId { get; set; }
        /// <summary>
        /// 第三个主键
        /// </summary>
        [Description("第三个主键")]
        public string ThirdId { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        [Description("扩展信息")]
        public string ExtendInfo { get; set; }
    }
}
