using NetMed.Domain.Entities;
using NetMed.Model.Models;

namespace NetMed.Persistence.Repositories
{
    public static class InsuranceProviderRepositoryMapper
    {
        public static IQueryable<InsuranceProviderModel> MapToInsuranceProviderModel(
            this IQueryable<InsuranceProviders> query)
        {
            return query.Select(ip => new InsuranceProviderModel()
            {
                Id = ip.Id,
                Name = ip.Name,
                ContactNumber = ip.ContactNumber,
                Email = ip.Email,
                Website = ip.Website,
                Address = ip.Address,
                City = ip.City,
                State = ip.State,
                Country = ip.Country,
                ZipCode = ip.ZipCode,
                IsActive = ip.IsActive,
                CoverageDetails = ip.CoverageDetails,
                IsPreferred = ip.IsPreferred,
                NetworkTypeID = ip.NetworkTypeID,
                AcceptedRegions = ip.AcceptedRegions,
                UpdatedAt = ip.UpdatedAt,
                CustomerSupportContact = ip.CustomerSupportContact,
                MaxCoverageAmount = ip.MaxCoverageAmount

            });
        }
    }

}