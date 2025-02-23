
namespace NetMed.Domain.Base
{
    public abstract class AvailabilityEntity : BaseEntity<int>
    {
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
