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
        public async Task<IActionResult> Search(string name, int page)
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
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(id).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            return View(new AddPhotoViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPhoto(AddPhotoViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(model.ProjectId).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            string filePath = null;
            string photoUri = null;

            if (model.Photo != null)
            {
                var uniqueFileName = getUniqueFileName(model.Photo.FileName);
                var uploads = Path.Combine(_hostEnvironment.ContentRootPath + "wwwroot", "images");
                photoUri = "/images/" + uniqueFileName;
                filePath = Path.Combine(uploads, uniqueFileName);
            }

            var photo = new Photo()
            {
                URI = photoUri,
            };

            var result = _photoService.CreatePhoto(photo, model.ProjectId);

            if (result.StatusCode == 0)
            {
                if (filePath != null) model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                _logger.LogInformation("Photo Saved");
                return RedirectToAction("Details", "Project", new { id = model.ProjectId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.Description);
                return View(model);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddVideo(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(id).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

            return View(new AddVideoViewModel()
            {
                ProjectId = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(model.ProjectId).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

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
                return View(model);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var creatorId = _projectService.ReadProjectComplete(id).Data.ProjectCreator;

            if (creatorId != user)
                return RedirectToAction("index", "home");

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

    }
}
