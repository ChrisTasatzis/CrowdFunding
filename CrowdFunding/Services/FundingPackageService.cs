using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CrowdFunding.Services
{
    public class FundingPackageService : IFundingPackageService
    {
        private readonly CFContext _context;

        public FundingPackageService(CFContext context)
        {
            _context = context;
        }
        public Response<FundingPackage> CreateFundingPackage(FundingPackage fundingpackage, int projectId)

        {
            var project = _context.Projects.Find(projectId);
            if (projectId < 0)
            {
                return new Response<FundingPackage>()
                {
                    Data = null,
                    Description = "Project not found, please try again",
                    StatusCode = 50
                };
            }

            List<FundingPackage> fundingpackages = project.FundingPackages;
            fundingpackages.Add(fundingpackage);
            if (_context.SaveChanges() == 1) ;
            return new Response<FundingPackage>()
            {
                Data = fundingpackage,
                Description = "The funding package was saved!",
                StatusCode = 0
            };
            return new Response<FundingPackage>()
            {
                Data = null,
                Description = "The funding package was not saved, please try again",
                StatusCode = 54
            };

        }


        public Response<bool> DeleteFundingPackage(int fundingpackageId)
        {
            var fundingpackage = _context.FundingPackages.Find(fundingpackageId);
            if (fundingpackage == null) return new Response<bool>() { Data = false, Description = "This funding package doesn't exist", StatusCode = 50 };

            _context.FundingPackages.Remove(fundingpackage);
            if (_context.SaveChanges() == 1)
            {
                return new Response<bool>() { Data = true, Description = "The Funding Package Succesfully Deleted", StatusCode = 0 };
            }
            else
            {
                return new Response<bool>() { Data = false, Description = "Could not save changes", StatusCode = 53 };

            }
        }

        public Response<FundingPackage> ReadFundingPackage(int fundingpackageId)
        {
            var fundingpackage = _context.FundingPackages.FirstOrDefault(f => f.Id == fundingpackageId);
            if (fundingpackage != null)
            {
                return new Response<FundingPackage>()
                {
                    Data = fundingpackage,
                    Description = "Here is the Funding Package!",
                    StatusCode = 0
                };
            }
            else
            {
                return new Response<FundingPackage>()
                {
                    Data = null,
                    Description = "The funding package was not found",
                    StatusCode = 50
                };
            }

        }

        public Response<FundingPackage> UpdateFundingPackage(int fundingpackageId, FundingPackage fundingPackage)
        {
            var fundingpackage = _context.FundingPackages.FirstOrDefault(f => f.Id == fundingpackageId);
            if (fundingpackage == null)
            {
                return new Response<FundingPackage>()
                {
                    Data = null,
                    Description = "The funding package was not found",
                    StatusCode = 50
                };
            }
            else
            fundingpackage.Name = fundingPackage.Name;
            fundingpackage.Price = fundingPackage.Price;
            fundingpackage.Description = fundingPackage.Description;

            _context.SaveChanges();
            return new Response<FundingPackage>()
            {
                Data = fundingpackage,
                Description = "The funding package was Succesfully Updated",
                StatusCode = 0
            };

        }

        public List<FundingPackage> ReadAllPackages(int projectId, int pageSize, int pageNumber)
        {
            var project = _context.Projects.Find(projectId);
            if (project == null) throw new KeyNotFoundException();
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 20) pageSize = 20;
            return project.FundingPackages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        }
    }
}
        