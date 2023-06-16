using hospital_back.Enums;

namespace hospital_back.Dto
{
    public class PatientResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal CPF { get; set; }
        public decimal CellPhone { get; set; }
        public RoleType Role { get; set; }

    }
}