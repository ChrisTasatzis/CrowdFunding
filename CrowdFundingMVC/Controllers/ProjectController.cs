using CrowdFunding.Models;
using CrowdFunding.Services;
using CrowdFundingMVC.Models.ProjectView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProjectController> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ProjectController(
            IProjectService projectService,
            UserManager<User> userManager,
            ILogger<ProjectController> logger,
            IHostEnvironment hostEnvironment
            )
        {
            _projectService = projectService;
            _userManager = userManager;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(int id)
        {
            var result = _projectService.ReadProject(id);

            if (result.StatusCode == 0)
                return View(result.Data);
            else
                return NotFound();
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(model);
                var img = model.Thumbnail;
                string filePath = null;
                string thumbnail = null;
                if (img != null)
                {
                    var uniqueFileName = getUniqueFileName(img.FileName);
                    var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "wwwroot", "images");
                    thumbnail = "/images/" + uniqueFileName;
                    filePath = Path.Combine(uploads, uniqueFileName);
                }

                var project = new Project()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Category = (Category)Int32.Parse(model.Category),
                    Goal = model.Goal,
                    Thumbnail = thumbnail
                };


                var user = await _userManager.GetUserAsync(HttpContext.User);
                var result = _projectService.CreateProject(project, user.Id);


                if (result.StatusCode == 0)
                {
                    if (filePath != null) img.CopyTo(new FileStream(filePath, FileMode.Create));
                    _logger.LogInformation("Project Created");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Description);
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }


        private string getUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

    }
}
