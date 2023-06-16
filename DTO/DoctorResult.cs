using System.Collections.Generic;
using hospital_back.Enums;

namespace hospital_back.Dto
{
    public class DoctorResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PatientResult> Patients { get; set; }
        public RoleType Role { get; set; }
    }
}