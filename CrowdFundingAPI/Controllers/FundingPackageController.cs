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

        public async Task<ActionResult<FundingPackageDto>> GetFundingPackageById(int fundingpackageId) 
        {
            return await FundingPackageService.GetFundingPackage(fundingpackageId);
            
        }
    }
}
