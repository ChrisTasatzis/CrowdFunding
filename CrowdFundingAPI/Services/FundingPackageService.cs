using CrowdFunding.Models;
using CrowdFundingAPI.DTO;
using Microsoft.EntityFrameworkCore;

namespace CrowdFundingAPI.Services
{
    public class FundingPackageService : IFundingPackage
    {
        private readonly CFContext _cfContext;
        public FundingPackageService (CFContext context)
        {
            _cfContext = context;        
        }
        public async Task<FundingPackageDto> AddFundingPackage(FundingPackageDto dto)
        {
            Project? project = await _cfContext.Projects.SingleOrDefaultAsync(p => p.Id == dto.ProjectId);
            if (project == null) return null; 

            FundingPackage fundingpackage = new FundingPackage()
            {
                Name = dto.Name,
                Project = project,
                Price = dto.Price
            };

            _cfContext.FundingPackages.Add(fundingpackage);
            _cfContext.SaveChanges();
            return new FundingPackageDto()
            {
                Id = fundingpackage.Id,
                Name = fundingpackage.Name,
                ProjectName = fundingpackage.Project.Name,
                Price = fundingpackage.Price
            };

        }

        public async Task<bool> Delete(int fundingpackageId)
        {
            FundingPackage fundingpackage = _cfContext.FundingPackages.SingleOrDefault(p => p.Id == fundingpackageId);
            if (fundingpackage == null) return false;
            _cfContext.FundingPackages.Remove(fundingpackage);
            await _cfContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<FundingPackageDto>> GetAllPackages()
        {
            var fundingPackages = await _cfContext.FundingPackages.ToListAsync();

            List<FundingPackageDto> result = new List<FundingPackageDto>();
            foreach (var fundingpackage in fundingPackages)
            {
                result.Add(new FundingPackageDto ()
              { 
                Id = fundingpackage.Id,
                Name = fundingpackage.Name,
                ProjectName = fundingpackage.Project.Name,
                Price = fundingpackage.Price
            });
            }
            return result;
        }

        public async Task<FundingPackageDto?> GetFundingPackage(int fundingpackageId)
        {
            var fundingpackage = await _cfContext.FundingPackages
                .Include(f => f.Project)
                .SingleOrDefaultAsync(f => f.Id == fundingpackageId);
            if (fundingpackage == null) return null;

            return new FundingPackageDto
            {
                Id = fundingpackage.Id,
                Name = fundingpackage.Name,
                ProjectName = fundingpackage.Project.Name,
                Price = fundingpackage.Price,
            };
        }

        public async Task <List<FundingPackageDto>> Search(string name, string project)
        {
            IQueryable <FundingPackage> results = _cfContext.FundingPackages.Include(p => p.Project); 
            if (name is not null) results = results. Where (p => p.Name == name);
            List<FundingPackage> fundingPackages = results.ToList();
            List<FundingPackageDto?> fundingpackageDtos = new List<FundingPackageDto?>();
            foreach (var fundingpackage in fundingPackages)
            {
                fundingpackageDtos.Add(new FundingPackageDto()
                {
                    Id = fundingpackage.Id,
                    Name = fundingpackage.Name,
                    ProjectName = fundingpackage.Project.Name,
                    Price = fundingpackage.Price,
                }) ;

           }
            return fundingpackageDtos;
        }

        public async Task<FundingPackageDto> Update(int fundingpackageId, FundingPackageDto dto)
        {
            FundingPackage? fundingpackage = await _cfContext.FundingPackages.SingleOrDefaultAsync(p => p.Id == fundingpackageId);
                if (fundingpackage is null) return null;
                if (dto.Name != null) fundingpackage.Name= dto.Name;                
            if (dto.Price != null) fundingpackage.Price = dto.Price;

            await _cfContext.SaveChangesAsync();
            return new FundingPackageDto()
            {
                Id = fundingpackage.Id,
                Name = fundingpackage.Name,
                ProjectName = fundingpackage.Project.Name,
                Price = fundingpackage.Price
            };
        }
           
      }
    }

