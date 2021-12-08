namespace CrowdFundingAPI.DTO
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string? URI { get; set; }
        public DateTime DateTime { get; set; }
        public string? ProjectName { get; set; }
        public int? ProjectId { get; set; }
    }
}
