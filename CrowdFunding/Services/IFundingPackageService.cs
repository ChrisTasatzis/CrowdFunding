using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IFundingPackageService
    {
        Response<FundingPackage> CreateFundingPackage(FundingPackage fundingpackage, int projectId); 
        Response<bool> DeleteFundingPackage (int fundingpackageId);
        Response<FundingPackage> UpdateFundingPackage (int fundingpackageId, FundingPackage fundingpackage);
        public List<FundingPackage> ReadAllPackages(int projectId, int pageSize, int pageNumber); 
    }
}
