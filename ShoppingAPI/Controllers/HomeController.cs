using System.Web.Mvc;

namespace ShoppingAPI.Controllers
{
    /// <summary>
    /// This class is responsible for the home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets the action result.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}