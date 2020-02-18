using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Critza.Bibliotecas;

namespace PruebaTecnica.Controlador
{
    class OpCurp
    {
        public Curp critza;

        public OpCurp()
        {
            critza = new Curp();
        }

        public string generarCurp(string nombre, string primerApellido, string segundoApellido,char sexo, string fechaNacimiento, string estadoNacimiento)
        {
            string curp;
            try
            {
                curp = critza.ObtenCurp(nombre, primerApellido, segundoApellido, sexo, fechaNacimiento, estadoNacimiento);
            }
            catch
            {
                curp = "";
            }

            return curp;
        }

        public string verificarCampos(string nombre,string primerApellido,string segundoApellido,string estadoNacimiento,string sexo)
        {
            string validador = "";
            validador = validarTextoLargo(nombre, "Nombre") + validarTextoLargo(primerApellido, "Primer Apellido") + validarTextoLargo(primerApellido, "Segundo Apellido") + validarTextoVacio(estadoNacimiento, "Estado de Nacimiento") + validarTextoVacio(sexo, "Sexo");
            return validador;
        }

        private string validarTextoLargo(string texto, string nombreCampo)
        {
            string resultado = "";
            if (texto == "")
            {
                resultado = ", Ingrese valores al campo " + nombreCampo;
            }
            else if(texto.Length < 2)
            {
                resultado =  ", Debe tener al menos 2 caracteres en el campo " + nombreCampo;
            }

            return resultado;
        }

        private string validarTextoVacio(string texto, string nombreCampo)
        {
            string resultado = "";
            if (texto == "")
            {
                resultado = ", Ingrese valores al campo " + nombreCampo;
            }

            return resultado;
        }
    }
}
