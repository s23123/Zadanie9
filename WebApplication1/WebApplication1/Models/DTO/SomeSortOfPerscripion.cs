using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models.DTO
{
    public class SomeSortOfPerscripion
    {
        public string DoctorName { get; set; }
        public string DoctorLastName { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }

        public virtual ICollection<SomeSortOfMedicament> Medicament { get; set; }
    }
}
