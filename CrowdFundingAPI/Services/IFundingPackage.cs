using CrowdFunding.Models;
using CrowdFundingAPI.DTO;

namespace CrowdFundingAPI.Services
{
    public interface IFundingPackage
    { 
        public Task <FundingPackageDto> GetFundingPackage (int fundingpackageId);
        public Task<List<FundingPackageDto>> GetAllPackages();
        public Task<FundingPackageDto> AddFundingPackage(FundingPackageDto dto);
        public Task<List<FundingPackageDto>> Search(string name, string project);
        public Task <FundingPackageDto> Update(int fundingpackageId, FundingPackageDto dto);
        public Task<bool> Delete (int fundingpackageId);
    }
}
