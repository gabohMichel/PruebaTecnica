using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.Modelo;

namespace PruebaTecnica.Controlador
{
    class OpTelefono
    {
        public void actualizarTelefonos(string telefonos,string idTelefonos,int idPersona)
        {
            string[] arrayTelefonos = telefonos.Split(',');
            string[] arrayTelefonosId = idTelefonos.Split(',');
            int telefonosConsulta = 0;
            int telefonosArray = 0;
            List<Telefono> listaTelefonos;

            try
            {
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    listaTelefonos = dbo.Telefono.Where(tel => tel.IdFkPersona == idPersona).ToList();
                }
                telefonosConsulta = listaTelefonos.Count;
                telefonosArray = arrayTelefonos.Length;

                if (telefonosConsulta == telefonosArray)
                {
                    
                }
                else if(telefonosConsulta < telefonosArray)
                {

                }
                else if (telefonosConsulta > telefonosArray)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Telefono> agregarTelefonos(string telefonoString)
        {
            List<Telefono> telefonos = new List<Telefono>();
            string[] telefonosTmp = telefonoString.Split(',');

            Array.ForEach(telefonosTmp, telefono => { telefonos.Add(new Telefono() { varNumeroTelefonico = telefono }); });

            return telefonos;
        }
    }
}
