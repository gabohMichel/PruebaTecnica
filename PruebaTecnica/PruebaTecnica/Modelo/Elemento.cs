using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.Modelo
{
    class Elemento
    {
        public string text { get; set; }
        public string value { get; set; }
        public Elemento(string text,string value)
        {
            this.text = text;
            this.value = value;
        }
    }
}
