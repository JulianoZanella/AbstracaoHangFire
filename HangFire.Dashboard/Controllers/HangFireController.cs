using HangFire.Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace HangFire.Dashboard.Controllers
{
    public class HangFireController : Controller
    {
        public IActionResult Index()
        {
            JobsService.Rodar();
            return Redirect(Url.Content("~/hangfire"));
        }
    }
}
