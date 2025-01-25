using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.AggregateRoots;

using Domain.Interface.IAggregateRoots;
namespace Domain.Entities
{
    public partial class T_Menu : Entity, IAggregateRoot
    {
        public T_Menu()
        {
            T_Buttons = new HashSet<T_Button>();
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 父级菜单Code
        /// </summary>
        public Guid? ParentCode { get; set; }
        /// <summary>
        /// 菜单URL
        /// </summary>
        public string? URL { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// 菜单按钮列表
        /// </summary>
        public virtual ICollection<T_Button> T_Buttons { get; set; }
    }
}
