namespace WebApi.Utilities.ConstValue
{
    /// <summary>
    /// 接口错误类型
    /// </summary>
    public class API_ERROR_TYPE
    {
        /// <summary>
        /// 异常
        /// </summary>
        public const string EXCEPTION = "EXCEPTION";
        /// <summary>
        /// 请求错误
        /// </summary>
        public const string BAD_REQUEST = "BAD_REQUEST";
        /// <summary>
        /// 页面不存在
        /// </summary>
        public const string NOT_FOUND = "NOT_FOUND";
        /// <summary>
        /// 参数错误
        /// </summary>
        public const string PARAM_ERROR = "PARAM_ERROR";
        public const string CONSUME_CHECK_FAIL = "CONSUME_CHECK_FAIL";
        public const string CONSUME_FAIL = "CONSUME_FAIL";
        public const string REFUND_FAIL = "REFUND_FAIL";
        public const string CONDITION_DISSATISFY = "CONDITION_DISSATISFY";
        public const string INVALID_SIGN = "INVALID_SIGN";
        public const string INVALID_TOKEN = "INVALID_TOKEN";
        public const string INVALID_MAC = "INVALID_MAC";
        public const string AUTHENTICATION_FAIL = "AUTHENTICATION_FAIL";
        public const string SQLDB_COMMIT_FAIL = "SQLDB_COMMIT_FAIL";
        public const string REMOTE_OPEN_DOOR_FAIL = "REMOTE_OPEN_DOOR_FAIL";
        public const string RECORD_IS_EXISTS = "RECORD_IS_EXISTS";
        public const string RECORD_IS_DEALED = "RECORD_IS_DEALED";
        public const string OBJECT_NOT_EXISTS = "OBJECT_NOT_EXISTS";
        public const string CARD_NOT_EXISTS = "CARD_NOT_EXISTS";
        public const string CARD_BAGS_NOT_EXISTS = "CARD_BAGS_NOT_EXISTS";
        public const string CARD_STATUS_NOT_NORMAL = "CARD_STATUS_NOT_NORMAL";
        public const string CREDIT_BACK_ID_NOT_EXISTS = "CREDIT_BACK_ID_NOT_EXISTS";
        public const string DEAL_NOT_EXISTS = "DEAL_NOT_EXISTS";
        public const string OPERATION_TOO_FREQUENT = "OPERATION_TOO_FREQUENT";
    }
}