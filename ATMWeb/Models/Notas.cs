using ATMWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Models
{
    public class Notas
    {
        public int NotasId { get; set; }
        public string Nota { get; set; }
        public double Valor { get; set; }
        public int Qtde { get; set; }
        public string ImgNota { get; set; }


    }


}
