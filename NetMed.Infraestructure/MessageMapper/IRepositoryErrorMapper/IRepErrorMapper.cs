namespace NetMed.Infrastructure.Mapper.IRepositoryErrorMapper
{
    public interface IRepErrorMapper
    {
        public Dictionary<string, string> ErrorDoctorsRepositoryMessages { get; set; }
        public Dictionary<string, string> ErrorPatientsRepositoryMessages { get; set; }
        public Dictionary<string, string> ErrorUsersRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessDoctorsRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessPatientsRepositoryMessages { get; set; }
        public Dictionary<string, string> SuccessUsersRepositoryMessages { get; set; }

        public Dictionary<string, string> DataISNullErrorGlogal { get; set; }

        public Dictionary<string, string> SaveEntityErrorMessage { get; set; }

        public Dictionary<string, string> UpdateEntityErrorMessage { get; set; }

        public Dictionary<string, string> GetEntityByIdErrorMessage { get; set; }

        public Dictionary<string, string> GetAllEntitiesErrorMessage { get; set; }
    }
}
