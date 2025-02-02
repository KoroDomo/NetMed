
namespace NetMed.Domain.Base
{
    public abstract class AvailabilityEntity : BaseEntity<int>
    {
        protected AvailabilityEntity() 
        {
            this.CreatedAt = DateTime.Now;
        }
        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

}
