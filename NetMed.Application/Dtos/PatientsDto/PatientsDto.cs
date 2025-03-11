

using System.ComponentModel.DataAnnotations;

namespace NetMed.Application.Dtos.PatientsDto
{
    public class PatientsDto : DtoBase
    {

       
        public DateOnly DateOfBirth { get; set; }
    
        public char Gender { get; set; }
      
        public  string? EmergencyContactName { get; set; }
     
        public  string? EmergencyContactPhone { get; set; }
        public char BloodType { get; set; }
       
        public string? Allergies { get; set; }
      
        public int InsuranceProviderID { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }






    }
}
