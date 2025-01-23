using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Utilities.ConstValue
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public class DataBaseType
    {
        public const string TENANT_ID = "tenantId";

        public const string DBTYPE_SQLSERVER = "SqlServer";    //sql server
        public const string DBTYPE_MYSQL = "MySql";    //mysql
        public const string DBTYPE_PostgreSQL = "PostgreSQL";    //PostgreSQL
        public const string DBTYPE_ORACLE = "Oracle";    //oracle
    }
}
