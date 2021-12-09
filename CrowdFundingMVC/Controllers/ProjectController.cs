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
        private readonly IPhotoService _photoService;
        private readonly IVideoService _videoService;

        public ProjectController(
            IProjectService projectService,
            UserManager<User> userManager,
            ILogger<ProjectController> logger,
            IHostEnvironment hostEnvironment,
            IPostService postService,
            IFundingPackageService fundingPackageService,
            IPhotoService photoService,
            IVideoService videoService
            )
        {
            _projectService = projectService;
            _userManager = userManager;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
            _postService = postService;
            _fundingPackageService = fundingPackageService;
            _photoService = photoService;
            _videoService = videoService;
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
            if (await authorizeCreator(id) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            return View(new AddPostViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel model)
        {
            if (await authorizeCreator(model.ProjectId) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            if (ModelState.IsValid)
            {
                var res = _postService.CreatePost(new Post()
                {
                    Text = model.Text
                }, model.ProjectId);

                if (res.StatusCode == 0)
                    return RedirectToAction("Details", "Project", new { id = model.ProjectId });

                ModelState.AddModelError(string.Empty, res.Description);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> AddFundingPackage(int id)
        {
            if (await authorizeCreator(id) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            return View(new AddFPViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFundingPackage(AddFPViewModel model)
        {
            if (await authorizeCreator(model.ProjectId) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            if (ModelState.IsValid && model.Name != null && model.Reward != null)
            {
                var res = _fundingPackageService.CreateFundingPackage(new FundingPackage()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Reward
                }, model.ProjectId);

                if (res.StatusCode == 0)
                    return RedirectToAction("Details", "Project", new { id = model.ProjectId });

                ModelState.AddModelError(string.Empty, res.Description);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BackProject(DetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var projectId = model.ProjectId;
                var fundingPackageId = model.FundingPackageId;

                var res = _projectService.BackProject(projectId, user.Id, fundingPackageId);

                if (res.StatusCode == 0)
                    return RedirectToAction("Details", "Project", new { id = model.ProjectId });

                ModelState.AddModelError(string.Empty, res.Description);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
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
            if (ModelState.IsValid && model.Name != null && model.Description != null && model.Category != null && model.Thumbnail != null)
            {

                // Create file paths for saving
                var uniqueFileName = getUniqueFileName(model.Thumbnail.FileName);
                var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "wwwroot", "images");
                var thumbnail = "/images/" + uniqueFileName;
                var filePath = Path.Combine(uploads, uniqueFileName);

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
                    if (filePath != null)
                    {
                        model.Thumbnail.CopyTo(new FileStream(filePath, FileMode.Create));
                        _logger.LogInformation("Project Created");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save image.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        [HttpGet("Category/{cat:int}/{page:int}")]
        public IActionResult Category(int cat, int page)
        {

            var projects = _projectService.ReadProject((Category)cat, 6, page).Data;
            var pages = _projectService.GetNumberOfPages((Category)cat, 6).Data;

            return View(new CategoryViewModel()
            {
                Projects = projects,
                Category = cat,
                Pages = pages
            });
        }


        [HttpGet("Category/{page:int}")]
        public IActionResult CategoryAll(int page)
        {

            var projects = _projectService.ReadProject(6, page).Data;
            var pages = _projectService.GetNumberOfPages(6).Data;

            return View(new CategoryViewModel()
            {
                Projects = projects,
                Pages = pages
            });
        }


        [HttpGet]
        public ActionResult Search(string name, int page)
        {
            var projects = _projectService.ReadProject(name, 6, page).Data;
            var pages = _projectService.GetNumberOfPages(name, 6).Data;

            return View(new SearchViewModel()
            {
                Projects = projects,
                SearchTerm = name,
                Pages = pages
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddPhoto(int id)
        {
            if (await authorizeCreator(id) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            return View(new AddPhotoViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPhoto(AddPhotoViewModel model)
        {
            if (await authorizeCreator(model.ProjectId) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            if (ModelState.IsValid && model.Photo != null)
            {
                var uniqueFileName = getUniqueFileName(model.Photo.FileName);
                var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "wwwroot", "images");
                var photoUri = "/images/" + uniqueFileName;
                var filePath = Path.Combine(uploads, uniqueFileName);


                var photo = new Photo()
                {
                    URI = photoUri,
                };

                var result = _photoService.CreatePhoto(photo, model.ProjectId);

                if (result.StatusCode == 0)
                {
                    if (filePath != null)
                    {
                        model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                        _logger.LogInformation("Photo Saved");
                        return RedirectToAction("Details", "Project", new { id = model.ProjectId });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save image.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddVideo(int id)
        {
            if (await authorizeCreator(id) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            return View(new AddVideoViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoViewModel model)
        {
            if (await authorizeCreator(model.ProjectId) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            if (ModelState.IsValid && model.URL != null)
            {
                var url = "https://www.youtube.com/embed/" + model.URL.Substring(model.URL.Length - 11);

                var video = new Video()
                {
                    URL = url,
                };

                var result = _videoService.CreateVideo(video, model.ProjectId);

                if (result.StatusCode == 0)
                {
                    _logger.LogInformation("Video Saved");
                    return RedirectToAction("Details", "Project", new { id = model.ProjectId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (await authorizeCreator(id) == false) return StatusCode(StatusCodes.Status401Unauthorized);

            _projectService.DeleteProject(id);

            return RedirectToAction("index", "home");
        }

        private string getUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        private async Task<bool> authorizeCreator(int projectId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var project = _projectService.ReadProjectComplete(projectId);

            if (project.Data == null || user == null) return false;

            if (user != project.Data.ProjectCreator) return false;

            return true;
        }
    }
}
