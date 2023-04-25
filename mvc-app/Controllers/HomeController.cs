using Microsoft.AspNetCore.Mvc;
namespace westcoast_education.web.Controllers;

public class HomeController : Controller
{
    // Action method...
    public IActionResult Index()
    {
        // Returnerar ett ViewResult...
        return View("Index");
    }
}
