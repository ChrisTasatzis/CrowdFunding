using CrowdFundingAPI.DTO;
using CrowdFundingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFundingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingPackageController : ControllerBase
    {
        
        public IFundingPackage FundingPackageService { get; set; }
        public FundingPackageController (IFundingPackage fundingPackageService)
        {
            FundingPackageService = fundingPackageService;
        }

        [HttpGet, Route("{fundingpackageId}")]
        public async Task<ActionResult<FundingPackageDto>> GetFundingPackageById([FromRoute] int fundingpackageId) 
        {
            var dto = await FundingPackageService.GetFundingPackage(fundingpackageId);

            if (dto is null) return BadRequest("The requested funding package does not exist");
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<FundingPackageDto>?>> GetAllPackages()
        {
            var dto = await FundingPackageService.GetAllPackages();

            if (dto is null) return BadRequest();
            return Ok(dto);
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult<List<FundingPackageDto?>>> SearchFundingPackages (string? name, string project)
        {
            var result = await FundingPackageService.Search(name, project);
            if (result is null || !result.Any()) return BadRequest("No funding packages that match the specified criteria were found");
            return result;
        }


        [HttpPost]
        public async Task<ActionResult<FundingPackageDto?>> PostFundingPackage(FundingPackageDto? dto) 
        {
            if (dto is null) return BadRequest("Please provide a valid funding package!");
            var result = await FundingPackageService.AddFundingPackage(dto);
            if (result is null) return BadRequest("The project does not exist. Please provide a valid project id");
            return Ok(result);
        }

        [HttpDelete, Route("{fundingpackageId}")]
        public async Task<ActionResult<bool>> DeleteFundingPackage(int fundingpackageId)
        {
            var result = await FundingPackageService.Delete(fundingpackageId);
            if (result) return Ok("The funding package was deleted!");
            return BadRequest();
        }

        [HttpPatch, Route("{fundingpackageId}")]
        public async Task<ActionResult<FundingPackageDto?>> UpdateFundingPackage(int fundingpackageId, FundingPackageDto dto)
        {
            var result = await FundingPackageService.Update(fundingpackageId, dto);
            if (result is null) return BadRequest("Invalid Operation, Funding Package Not Found");
            return Ok(result);
        }

    }
}
