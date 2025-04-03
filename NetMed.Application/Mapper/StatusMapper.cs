using NetMed.Application.Dtos.Status;
using NetMed.Domain.Entities;
using System.Data;


namespace NetMed.Application.Mapper
{
    public static class StatusMapper
    {
        public static StatusDto ToDto(Status status)
        {
            return new StatusDto
            {
                id = status.Id,
                StatusName = status.StatusName,
            };
        }

        public static Status ToEntity(SaveStatusDto dto)
        {
            return new Status
            {
                StatusName = dto.StatusName,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static Status ToEntity(UpdateStatusDto dto)
        {
            return new Status
            {
                Id = dto.id,
                StatusName = dto.StatusName,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static Status ToEntity(DeleteStatusDto dto)
        {
            return new Status
            {
                Id = dto.StatusId,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static void UpdateEntity(this Status status, UpdateStatusDto dto)
        {
            status.StatusName = dto.StatusName;
            status.UpdatedAt = DateTime.UtcNow;
        }

        public static List<StatusDto> ToDtoList(IEnumerable<Status> statuses)
        {
            return statuses.Select(n => ToDto(n)).ToList();

        }
    }
}