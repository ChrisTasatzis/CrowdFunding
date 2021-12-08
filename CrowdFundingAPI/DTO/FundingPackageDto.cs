namespace CrowdFundingAPI.DTO
{
    public class FundingPackageDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string? ProjectName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int? ProjectId { get; set; }
    }
}
