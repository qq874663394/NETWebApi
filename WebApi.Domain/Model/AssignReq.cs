using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Domain.AggregateRoot;
using WebApi.Domain.Interface.IAggregateRoots;

namespace WebApi.Domain.Model
{
    public partial class AssignReq
    {
        /// <summary>
        /// 分配的关键字，比如：UserRole
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 比如给用户分配角色，那么firstId就是用户ID，secIds就是角色ID列表
        /// </summary>
        public string firstId { get; set; }
        public string[] secIds { get; set; }
    }
}
