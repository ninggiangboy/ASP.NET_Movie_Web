using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Group06_Project.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int Id)
        {
            _logger.LogInformation(Id.ToString());
            return View();
        }

        public IActionResult Feature()
        {
            return View();
        }

        public IActionResult Popular()
        {
            return View();
        }

        public IActionResult Newest()
        {
            return View();
        }

        public IActionResult Catalog()
        {
            return View();
        }

		public IActionResult Category()
		{
			return View();
		}
	}
}
