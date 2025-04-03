using NetMed.Application.Dtos.Roles;
using NetMed.Domain.Entities;
using System.Data;

namespace NetMed.Application.Mapper
{
    public static class RolesMapper
    {
        public static RolesDto ToDto(Roles role)
        {
            return new RolesDto
            {
                id = role.Id,
                RoleName = role.RoleName,
            };
        }

        public static Roles ToEntity(SaveRolesDto dto)
        {
            return new Roles
            {
                RoleName = dto.RoleName,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static Roles ToEntity(UpdateRolesDto dto)
        {
            return new Roles
            {
                Id = dto.id,
                RoleName = dto.RoleName,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static Roles ToEntity(DeleteRolesDto dto)
        {
            return new Roles
            {
                Id = dto.RolesId,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static void UpdateEntity(this Roles role, UpdateRolesDto dto)
        {
            role.RoleName = dto.RoleName;
            role.UpdatedAt = DateTime.UtcNow;
        }

        public static List<RolesDto> ToDtoList(IEnumerable<Roles> roles)
        {
            return roles.Select(n => ToDto(n)).ToList();
        }
    }
}