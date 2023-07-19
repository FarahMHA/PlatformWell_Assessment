namespace PlatformWell_Assessment.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string? UniqueName { get; set; } = null!;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Well> Wells { get; set; } = new List<Well>();
    }
}
