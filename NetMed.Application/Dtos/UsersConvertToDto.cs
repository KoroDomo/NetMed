

using NetMed.Application.Dtos.UsersDto;

namespace NetMed.Application.Dtos
{
    public static class UsersConvertToDto
    {
        public static UserDto ConvertToDto(this Domain.Entities.Users entity)
        {
            return new UserDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Password = entity.Password,
                RoleID = entity.RoleID,
                CreatedAt = entity.CreatedAt,
                IsActive = entity.IsActive,
                UpdatedAt = entity.UpdatedAt ?? DateTime.MinValue
            };
        }
        public static List<UserDto> ConvertToDtoList(this IEnumerable<NetMed.Domain.Entities.Users> entities)
        {
            return entities.Select(x => x.ConvertToDto()).ToList();
        }
    }
}

