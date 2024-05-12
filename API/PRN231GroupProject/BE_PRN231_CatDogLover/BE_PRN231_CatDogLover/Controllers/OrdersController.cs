using Microsoft.AspNetCore.Mvc;

namespace BE_PRN231_CatDogLover.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
