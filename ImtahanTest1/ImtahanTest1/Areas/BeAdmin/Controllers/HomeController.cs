using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImtahanTest1.Areas.BeAdmin.Controllers
{
	[Area("BeAdmin")]
	[Authorize(Roles ="Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
