using NetMed.WebApplicationRefactor.Persistence.Interfaces;
using WebApplicationRefactor.Application.Contracts;
using WebApplicationRefactor.Models;
using WebApplicationRefactor.Models.Doctors;

namespace WebApplicationRefactor.Application.Services
{
    public class DoctorServices : IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorServices(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<OperationResult> Add(DoctorsApiModel dto)
        {
           return await _doctorRepository.Add(dto);
        }

        public Task<OperationResult> Delete(DoctorsApiModel dto)
        {
            return _doctorRepository.Delete(dto);
        }

        public Task<OperationResult> GetAllData()
        {
            return _doctorRepository.GetAllData();
        }

        public Task<OperationResult> GetById(int id)
        {
            return _doctorRepository.GetById(id);
        }

        public Task<OperationResult> Update(DoctorsApiModel dto)
        {
            return _doctorRepository.Update(dto);
        }
    }
}
