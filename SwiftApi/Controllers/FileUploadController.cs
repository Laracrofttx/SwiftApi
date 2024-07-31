using Microsoft.AspNetCore.Mvc;

namespace SwiftApi.Controllers
{
	public class FileUploadController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
