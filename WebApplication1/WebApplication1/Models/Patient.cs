using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Patient
    {

        public int IdPatient { get; set; }

        public string FristName { get; set; }

        public string LastName { get; set; }
        public DateTime BrithDate { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
