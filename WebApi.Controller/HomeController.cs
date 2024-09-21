using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    /// <summary>
    /// 主页跳转
    /// </summary>
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Redirect($"~/swagger/ui/index.html");
        }
    }
}
