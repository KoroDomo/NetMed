
namespace NetMed.Application.Dtos.AvailabilityModes
{
    public class RemoveAvailabilityModesDto : DtoBase //Record es lo recomendable
    {
        public int Id { get; set; }
        public bool Removed { get; set; }
    }
}
