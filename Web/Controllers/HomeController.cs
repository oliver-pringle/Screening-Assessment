using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private const string MemCacheKey = "IntegerValue";

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            int integerValue = GetCacheValue();


			return View(model: integerValue);
        }

        [HttpPost]
        public IActionResult IncrementNumber()
        {
            int integerValue = IncrementCacheValue();
            return Json( new { value = integerValue });
		}

		public IActionResult NumberOfClicks()
		{
			int integerValue = GetCacheValue();

			return View(model: integerValue);
		}

		public IActionResult Privacy()
        {
            return View();
        }


        private int GetCacheValue()
        {
			int integerValue = _memoryCache.TryGetValue(MemCacheKey, out int intCacheValue) ? intCacheValue : 0;
			return integerValue;
		}

		private int IncrementCacheValue()
        {
            int integerValue = GetCacheValue();
            integerValue += 1;
			_memoryCache.Set(MemCacheKey, integerValue);
            return integerValue;
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
