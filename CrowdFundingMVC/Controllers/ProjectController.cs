using CrowdFunding.Models;
using CrowdFunding.Services;
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
        public ProjectController(IProjectService projectService, UserManager<User> userManager, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _userManager = userManager;
            _logger = logger;
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var result = _projectService.CreateProject(project, user.Id);

                if (result.StatusCode == 0)
                {
                    _logger.LogInformation("Project Created");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Description);
                    return View(project);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(project);

        }
    }
}
