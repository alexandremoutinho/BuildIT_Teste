using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Models
{
    public class Saque
    {
        
        public int id { get; set; }
        
        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="Infome o {0} Correto")]
        public double Valor  { get; set; }
        
        public DateTime DataSaque { get; set; }


    }
}
