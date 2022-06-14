using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Prescription_Medicament
    {
       
        
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
       
        public int Dose { get; set; }
        
        public string Details { get; set; }

        public virtual Medicament Medicament { get; set; }
        public virtual Prescription Prescription { get; set; }
        
       
       
    }
}
