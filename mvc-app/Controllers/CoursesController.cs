using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using westcoast_education.web.Models;
using westcoast_education.web.ViewModels;

namespace westcoast_education.web.Controllers
{
    [Route("courses")]
    public class CoursesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _httpClient;
        public CoursesController(IConfiguration config, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUrl = _config.GetSection("apiSettings:baseUrl").Value;
            _options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        }

        public async Task<IActionResult> Index()
        {
            // Skapa en in instans av http-klienten
            using var client = _httpClient.CreateClient();

            // Hämta datat ifrån api:et...
            var response = await client.GetAsync($"{_baseUrl}/courses/listall");

            if (!response.IsSuccessStatusCode) return Content("Obs något gick fel...");

            var json = await response.Content.ReadAsStringAsync();

            var courses = JsonSerializer.Deserialize<IList<CourseListViewModel>>(json, _options);

            return View("Index", courses);
        }

        [Route("details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses/course/{id}");

            if (!response.IsSuccessStatusCode) return Content("Obs något gick fel...");

            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<CourseDetailsViewModel>(json, _options);

            return View("Details", course);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var model = new CoursePostViewModel();
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/teachers/listall");

            if(!response.IsSuccessStatusCode) return StatusCode(500, "Something went wrong whn trying to fetch data from API");

            var json = await response.Content.ReadAsStringAsync();
            var teachers = JsonSerializer.Deserialize<IList<TeacherListViewModel>>(json, _options);

            return View(model);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CoursePostViewModel model)
        {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var coursePost = new CoursePostViewModel
        {
            Title = model.Title,
            CourseNumber = model.CourseNumber,
            Duration = model.Duration,
            StartDate = model.StartDate,
            TeacherId = model.TeacherId ?? null
        };

        using var client = _httpClient.CreateClient();

        var content = new StringContent(JsonSerializer.Serialize(coursePost), Encoding.UTF8, "application/json");

        var response = await client.PostAsync($"{_baseUrl}/courses/create", content);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Error = "An error occurred while trying to create the course.";
            return View(model);
        }

        return RedirectToAction("Index");
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new CourseEditViewModel();
            using var client = _httpClient.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/courses/course/{id}");

            if(!response.IsSuccessStatusCode) return StatusCode(500, "Something went wrong whn trying to fetch data from API");

            var json = await response.Content.ReadAsStringAsync();
            var course = JsonSerializer.Deserialize<CourseEditViewModel>(json, _options);

            return View(course);
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit(CourseEditViewModel model)
        {
            // var teachersList = new List<SelectListItem>();

            // using var client = _httpClient.CreateClient();

            // var response = await client.GetAsync($"{_baseUrl}/teachers/listall");
            // if (!response.IsSuccessStatusCode) return Content("Obs något gick fel...");

            // var json = await response.Content.ReadAsStringAsync();

            // var teachers = JsonSerializer.Deserialize<List<CourseSettings>>(json, _options);

            // foreach (var teacher in teachers)
            // {
            //     teachersList.Add(new SelectListItem{Value = teacher.FirstName, Text = teacher.FirstName});
            // }
            // var course = new CourseEditViewModel();
            // course.Teachers = teachersList;

            // return View("Edit", course);

            return View();
        }
    }
}




// public async Task<IActionResult> Patch(CoursePostViewModel model)
// {
// if (!ModelState.IsValid)
// {
//     return View(model);
// }

// var courseUpdate = new CoursePostViewModel
// {
//     Title = model.Title,
//     CourseNumber = model.CourseNumber,
//     Duration = model.Duration,
//     StartDate = model.StartDate,
//     TeacherId = model.TeacherId ?? null
// };

// using var client = _httpClient.CreateClient();

// var content = new StringContent(JsonSerializer.Serialize(courseUpdate), Encoding.UTF8, "application/json");

// var response = await client.PostAsync($"{_baseUrl}/courses/create", content);

// if (!response.IsSuccessStatusCode)
// {
//     ViewBag.Error = "An error occurred while trying to create the course.";
//     return View(model);
// }

// return RedirectToAction("Index");
// }
