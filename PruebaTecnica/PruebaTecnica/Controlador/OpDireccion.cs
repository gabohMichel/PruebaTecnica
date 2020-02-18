using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.Modelo;

namespace PruebaTecnica.Controlador
{
    class OpDireccion
    {
        public Direccion obtenerDireccionId(int id)
        {
			Direccion direccion;
			try
			{
				using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
				{
					direccion = dbo.Direccion.Find(id);
				}
			}
			catch (Exception)
			{
				direccion = new Direccion();
			}

			return direccion;
        }

		public void agregarValoresDireccion(Direccion direccion, Direccion direccionActualizar)
		{
			direccion.varCalleNo = direccionActualizar.varCalleNo;
			direccion.varEstado = direccionActualizar.varEstado;
			direccion.varMunicipio = direccionActualizar.varMunicipio;
			direccion.varColonia = direccionActualizar.varColonia;
		}

	}
}
