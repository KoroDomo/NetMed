
using NetMed.Domain.Entities;
using NetMed.Model.Models;


namespace NetMed.Persistence.Repositories
{
    public static class NetworkTypeExtensions
    {
        public static IQueryable<NetworkTypeModel> MapToNetworkTypeModel(
            this IQueryable<NetworkType> query)
        {
            return query.Select(nt => new NetworkTypeModel()
            {
                Id = nt.Id,
                Name = nt.Name,
                Description = nt.Description,
                CreatedAt = nt.CreatedAt,
                UpdatedAt = nt.UpdatedAt,
                IsActive = nt.IsActive
            });
        }
    }

}