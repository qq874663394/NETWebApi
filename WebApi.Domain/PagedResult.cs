using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApi.Domain
{
    /// <summary>
    /// 表示一个分页结果，包含分页信息以及当前页的数据集合。
    /// </summary>
    /// <typeparam name="T">数据集合中的元素类型</typeparam>
    public class PagedResult<T> : IEnumerable<T>, ICollection<T>
    {
        // 表示一个空的分页结果
        public static readonly PagedResult<T> EmptyPagedResult = new PagedResult<T>(0, 0, 0, 0, null);

        #region Public Properties

        // 总记录数
        public int TotalRecords { get; set; }

        // 总页数
        public int TotalPages { get; set; }

        // 每页的记录数
        public int PageSize { get; set; }

        // 页码
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置当前页码的记录集合
        /// </summary>
        public List<T> PageData { get; set; }

        #endregion

        #region Constructors

        // 默认构造函数
        public PagedResult()
        {
            this.PageData = new List<T>();
        }

        // 带参数的构造函数，用于初始化所有属性
        public PagedResult(int totalRecords, int totalPages, int pageSize, int pageIndex, List<T> data)
        {
            this.TotalPages = totalPages;
            this.TotalRecords = totalRecords;
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.PageData = data;
        }

        #endregion

        #region Overrides Object Members

        /// <summary>
        /// 确定指定的对象是否等于当前对象。
        /// </summary>
        public override bool Equals(object obj)
        {
            // 用于比较对象是否相等
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == (object)null)
                return false;
            var other = obj as PagedResult<T>;
            if (ReferenceEquals(other, (object)null))
                return false;
            return this.TotalPages == other.TotalPages &&
                this.TotalRecords == other.TotalRecords &&
                this.PageIndex == other.PageIndex &&
                this.PageSize == other.PageSize &&
                this.PageData == other.PageData;
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        public override int GetHashCode()
        {
            // 用于获取对象的哈希码
            return this.TotalPages.GetHashCode() ^
                this.TotalRecords.GetHashCode() ^
                this.PageIndex.GetHashCode() ^
                this.PageSize.GetHashCode();
        }

        /// <summary>
        /// 确定两个对象是否相等。
        /// </summary>
        public static bool operator ==(PagedResult<T> a, PagedResult<T> b)
        {
            // 用于判断两个对象是否相等
            if (ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
            return a.Equals(b);
        }

        /// <summary>
        /// 确定两个对象是否不相等。
        /// </summary>
        public static bool operator !=(PagedResult<T> a, PagedResult<T> b)
        {
            // 用于判断两个对象是否不相等
            return !(a == b);
        }

        #endregion

        #region IEnumerable Members

        // 获取枚举器，用于遍历数据集合
        public IEnumerator<T> GetEnumerator()
        {
            return PageData.GetEnumerator();
        }

        // 获取枚举器，用于遍历数据集合（非泛型版本）
        IEnumerator IEnumerable.GetEnumerator()
        {
            return PageData.GetEnumerator();
        }

        #endregion

        #region ICollection Members

        // 添加元素到集合
        public void Add(T item)
        {
            PageData.Add(item);
        }

        // 清空集合
        public void Clear()
        {
            PageData.Clear();
        }

        // 判断集合是否包含指定元素
        public bool Contains(T item)
        {
            return PageData.Contains(item);
        }

        // 将集合的元素复制到指定数组的指定索引处
        public void CopyTo(T[] array, int arrayIndex)
        {
            PageData.CopyTo(array, arrayIndex);
        }

        // 获取集合中的元素个数
        public int Count
        {
            get { return PageData.Count; }
        }

        // 获取集合是否为只读
        public bool IsReadOnly
        {
            get { return false; }
        }

        // 从集合中移除指定元素
        public bool Remove(T item)
        {
            return PageData.Remove(item);
        }

        #endregion
    }
}