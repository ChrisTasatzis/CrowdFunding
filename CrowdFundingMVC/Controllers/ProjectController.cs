using CrowdFunding.Models;
using CrowdFunding.Services;
using CrowdFundingMVC.Models.ProjectController;
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
        private readonly IPostService _postService;
        private readonly IFundingPackageService _fundingPackageService;

        public ProjectController(
            IProjectService projectService,
            UserManager<User> userManager,
            ILogger<ProjectController> logger,
            IHostEnvironment hostEnvironment,
            IPostService postService,
            IFundingPackageService fundingPackageService
            )
        {
            _projectService = projectService;
            _userManager = userManager;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _postService = postService;
            _fundingPackageService = fundingPackageService;
        }

        public IActionResult Details(int id)
        {
            var result = _projectService.ReadProjectComplete(id);
            var result2 = _projectService.ReadTopBakcers(id, 5);

            if (result.StatusCode == 0 && result2.StatusCode == 0)
                return View(new DetailsViewModel()
                {
                    Project = result.Data,
                    BackerFunds = result2.Data,
                });
            else
                return NotFound();
        }

        [Authorize]
        public async Task<IActionResult> AddPost(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(id).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            return View(new AddPostViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(model.ProjectId).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            _postService.CreatePost(new Post()
            {
                Text = model.Text
            }, model.ProjectId);

            return RedirectToAction("Details", "Project", new { id = model.ProjectId });
        }

        [Authorize]
        public async Task<IActionResult> AddFundingPackage(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(id).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            return View(new AddFPViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFundingPackage(AddFPViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(model.ProjectId).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            _fundingPackageService.CreateFundingPackage(new FundingPackage()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Reward
            }, model.ProjectId);

            return RedirectToAction("Details", "Project", new { id = model.ProjectId });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BackProject(DetailsViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var projectId = model.ProjectId;
            var fundingPackageId = model.FundingPackageId;

            _projectService.BackProject(projectId, user.Id, fundingPackageId);



            return RedirectToAction("Details", "Project", new { id = model.ProjectId });
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
