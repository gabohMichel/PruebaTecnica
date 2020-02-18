using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PruebaTecnica.Modelo;
using System.Data.Entity;

namespace PruebaTecnica.Controlador
{
    class OpPersona
    {
        public string registrarDatosPersona(Persona persona)
        {
            string resultadoOperacion = "";
            int exito = 0;
            try
            {
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    dbo.Persona.Add(persona);
                    dbo.Entry(persona).State = EntityState.Added;
                    exito = dbo.SaveChanges();

                    resultadoOperacion = exito > 0 ? "Exito al guardar datos" : "Error inesperado";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion = ex.Message;
            }
            
            return resultadoOperacion;
        }

        public string actualizarDatosPersona(Persona personaActualizar,Direccion direccionActualizar,int id)
        {
            Persona persona;
            Direccion direccion;
            OpDireccion opDireccion;
            string resultadoOperacion = "";
            int exito = 0;
            int idFkDireccion = 0;
            try
            {
                opDireccion = new OpDireccion();
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    persona = dbo.Persona.Find(id);
                    agregarValoresPersona(persona,personaActualizar);

                    idFkDireccion = persona.IdFkDireccion;
                    direccion = dbo.Direccion.Find(idFkDireccion);
                    opDireccion.agregarValoresDireccion(direccion,direccionActualizar);
                    persona.Direccion = direccion;
                    exito = dbo.SaveChanges();

                    resultadoOperacion = exito > 0 ? "Actualización exitosa de la persona, registros afectados: " + exito : "No se actualizó ningun registro";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion = ex.Message;
            }

            return resultadoOperacion;
        }

        public string eliminarPersona(int id)
        {
            Persona persona;
            Direccion direccion;
            OpDireccion opDireccion = new OpDireccion();
            string resultadoOperacion = "";
            int exito = 0;
            try
            {
                persona = obtenerDatosPersona(id);
                direccion = opDireccion.obtenerDireccionId(persona.IdFkDireccion);
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    var listaTelefonos = dbo.Telefono.Where(tel => tel.IdFkPersona == persona.Id);

                    dbo.Telefono.RemoveRange(listaTelefonos);
                    dbo.Direccion.Attach(direccion);
                    dbo.Persona.Attach(persona);

                    dbo.Entry(direccion).State = EntityState.Deleted;
                    dbo.Entry(persona).State = EntityState.Deleted;
                    exito = dbo.SaveChanges();

                    resultadoOperacion = exito > 0 ? "Éxito al eliminar persona" : "Error inesperado";
                }
            }
            catch (Exception ex)
            {
                resultadoOperacion = ex.Message;
            }

            return resultadoOperacion;
        }

        public Persona obtenerDatosPersona(int id)
        {
            Persona persona;
            try
            {
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    persona = dbo.Persona.Find(id);
                }
            }
            catch (Exception)
            {
                persona = new Persona();
            }

            return persona;
        }

        public List<Modelo.Persona> obtenerDatosPersonas()
        {
            List<Modelo.Persona> personas = new List<Modelo.Persona>();
            try
            {
                using (Modelo.PruebaTecnicaEntities dbo = new Modelo.PruebaTecnicaEntities())
                {
                    var listaPersonas = dbo.Persona;

                    personas = listaPersonas.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return personas;
        }

        public List<Modelo.Persona> obtenerDatosPersonasGrid()
        {
            List<Modelo.Persona> personas = new List<Modelo.Persona>();
            try
            {
                using (Modelo.PruebaTecnicaEntities dbo = new Modelo.PruebaTecnicaEntities())
                {
                    var listaPersonas = dbo.Persona;

                    personas = listaPersonas.ToList();
                }
            }
            catch (Exception)
            {

            }

            return personas;
        }

        public PersonaDatosCompletos obtenerDatosCompletos(int id)
        {
            PersonaDatosCompletos personaDatos;

            try
            {
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    var query = from per in dbo.Persona
                                join dir in dbo.Direccion on per.IdFkDireccion equals dir.Id
                                where per.Id == id
                                select new PersonaDatosCompletos()
                                {
                                    nombre = per.varNombre,
                                    primerApellido = per.varApellidoPaterno,
                                    segundoApellido = per.varApellidoMaterno,
                                    estadoNacimiento = per.varEstadoNacimiento,
                                    fechaNacimiento = per.dtFechaNacimiento.ToString(),
                                    sexo = per.cSexo,
                                    curp = per.varCurp,
                                    calleNo = dir.varCalleNo,
                                    estado = dir.varEstado,
                                    municipio = dir.varMunicipio,
                                    colonia = dir.varColonia
                                };
                    personaDatos = query.First<PersonaDatosCompletos>();
                }
            }
            catch(Exception)
            {
                personaDatos = new PersonaDatosCompletos();
            }
            return personaDatos;
        }

        public List<PersonaDatosParcial> obtenerDatosParciales()
        {
            List<PersonaDatosParcial> personaDatosLista;

            try
            {
                using (PruebaTecnicaEntities dbo = new PruebaTecnicaEntities())
                {
                    var query = from per in dbo.Persona
                                select new PersonaDatosParcial()
                                {
                                    id = per.Id.ToString(),
                                    nombre = per.varNombre,
                                    primerApellido = per.varApellidoPaterno,
                                    segundoApellido = per.varApellidoMaterno,
                                    estadoNacimiento = per.varEstadoNacimiento,
                                    fechaNacimiento = per.dtFechaNacimiento.ToString(),
                                    sexo = per.cSexo,
                                    curp = per.varCurp,
                                };
                    personaDatosLista = query.ToList();
                }
            }
            catch (Exception)
            {
                personaDatosLista = new List<PersonaDatosParcial>();
            }

            return personaDatosLista;
        }

        public void agregarValoresPersona(Persona persona,Persona personaActualizar)
        {
            persona.varNombre = personaActualizar.varNombre;
            persona.varApellidoPaterno = personaActualizar.varApellidoPaterno;
            persona.varApellidoMaterno = personaActualizar.varApellidoMaterno;
            persona.varCurp = personaActualizar.varCurp;
            persona.dtFechaNacimiento = personaActualizar.dtFechaNacimiento;
            persona.cSexo = personaActualizar.cSexo;
            persona.varEstadoNacimiento = personaActualizar.varEstadoNacimiento;
        }
    }
}
