using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Medicament
    {
        
        public int IdMedicament { get; set; }
       
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Type { get; set; }
        public virtual ICollection<Prescription_Medicament> Prescriptions { get; set; }
    }
}
