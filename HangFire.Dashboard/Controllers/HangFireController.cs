using Microsoft.AspNetCore.Mvc;

namespace HangFire.Dashboard.Controllers
{
    public class HangFireController : Controller
    {
        public IActionResult Index()
        {
            return Redirect(Url.Content("~/hangfire"));
        }
    }
}
