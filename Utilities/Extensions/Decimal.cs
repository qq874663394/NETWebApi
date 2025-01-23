
namespace WebApi.Utilities.Extensions
{
    public static class Decimal
    {
        /// <summary>
        /// 保留指定小数位数
        /// </summary>
        /// <returns></returns>
        public static string ToStringByDigit(this decimal value, int digit)
        {
            switch (digit)
            {
                case 1:
                    return value.ToString("#0.0");
                case 2:
                    return value.ToString("#0.00");
                case 3:
                    return value.ToString("#0.000");
                case 4:
                    return value.ToString("#0.0000");
                case 5:
                    return value.ToString("#0.00000");
                case 6:
                    return value.ToString("#0.000000");
                case 7:
                    return value.ToString("#0.0000000");
                case 8:
                    return value.ToString("#0.00000000");
                case 9:
                    return value.ToString("#0.000000000");
                case 10:
                    return value.ToString("#0.0000000000");
                default:
                    return value.ToString("#0");
            }
        }
    }
}
