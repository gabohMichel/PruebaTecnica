using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PruebaTecnica.Controlador;
using PruebaTecnica.Modelo;
using Critza.Bibliotecas;
using System.ComponentModel;

namespace PruebaTecnica.Vista
{
    class VistaForm1
    {
        OpPersona opPersona;
        OpCurp opCurp;
        OpTelefono opTelefono;
        OpDireccion opDireccion;

        public VistaForm1()
        {
            inicializarAplicacionBasica();
        }

        public VistaForm1(ComboBox cbEstados,ComboBox cbEstadoNacimiento, ComboBox cbSexo, DataGridView dgvDatos,DateTimePicker dtFechaNacimiento)
        {
            try
            {
                inicializarAplicacionBasica();
                inicializarAplicacionInformacion(cbEstados, cbEstadoNacimiento, cbSexo, dgvDatos);
                dtFechaNacimiento.MaxDate = DateTime.Now;
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Se cerrara la aplicación ya que uno de sus componente no está funcionando de manera correcta.");
                throw;
            }
        }

        public void inicializarAplicacionBasica()
        {
            opPersona = new OpPersona();
            opCurp = new OpCurp();
            opTelefono = new OpTelefono();
            opDireccion = new OpDireccion();
        }

        public void inicializarAplicacionInformacion(ComboBox cbEstados, ComboBox cbEstadoNacimiento,ComboBox cbSexo, DataGridView dgvDatos)
        {
            cargarEstadosRepublica(cbEstados, cbEstadoNacimiento);
            cargarSexo(cbSexo);
            cargarInformacionPersona(dgvDatos);
        }

        public void cargarEstadosRepublica(ComboBox cbEstados, ComboBox cbEstadoNacimiento)
        {
            List<Curp.Estado> estados = opCurp.critza.estados;
            List<Elemento> elementosComboBox1 = new List<Elemento>();
            List<Elemento> elementosComboBox2 = new List<Elemento>();

            estados.ForEach(estado => elementosComboBox1.Add(new Elemento(estado.Nombre,estado.EstadoID)));
            estados.ForEach(estado => elementosComboBox2.Add(new Elemento(estado.Nombre, estado.EstadoID)));

            llenarComboBox(cbEstados, elementosComboBox1);
            llenarComboBox(cbEstadoNacimiento, elementosComboBox2);
        }

        public void cargarSexo(ComboBox cbSexo)
        {
            List<Elemento> elementosComboBox = new List<Elemento>();

            elementosComboBox.Add(new Elemento("SELECCIONAR...","0"));
            elementosComboBox.Add(new Elemento("HOMBRE","H"));
            elementosComboBox.Add(new Elemento("MUJER","M"));

            llenarComboBox(cbSexo, elementosComboBox);
        }

        public void llenarComboBox(ComboBox comboBox,List<Elemento> valores)
        {
            comboBox.DataSource = valores;
            comboBox.ValueMember = "value";
            comboBox.DisplayMember = "text";
            comboBox.Refresh();
            comboBox.SelectedIndex = 0;
        }

        private string registrarDatosPersona(string nombre, string primerApellido
                                          , string segundoApellido, DateTime fechaNacimiento
                                          , string estadoNacimiento, string telefono
                                          , string sexo, string calleNo
                                          , string estado, string municipio
                                          , string colonia, string curp)
        {
            Persona persona = new Persona();
            Direccion direccion = new Direccion();

            persona.varNombre = nombre;
            persona.varApellidoPaterno = primerApellido;
            persona.varApellidoMaterno = segundoApellido;
            persona.dtFechaNacimiento = fechaNacimiento;
            persona.varEstadoNacimiento = estadoNacimiento;
            persona.Telefono = opTelefono.agregarTelefonos(telefono);
            persona.cSexo = sexo;
            persona.varCurp = curp;

            direccion.varCalleNo = calleNo;
            direccion.varEstado = estado;
            direccion.varMunicipio = municipio;
            direccion.varColonia = colonia;
            persona.Direccion = direccion;

            return opPersona.registrarDatosPersona(persona);
        }

        private string actualizarDatosPersona(int id,string nombre, string primerApellido
                                          , string segundoApellido, DateTime fechaNacimiento
                                          , string estadoNacimiento, string telefono
                                          , string sexo, string calleNo
                                          , string estado, string municipio
                                          , string colonia, string curp)
        {
            Persona persona = new Persona();
            Direccion direccion = new Direccion();

            persona.Id = id;
            persona.varNombre = nombre;
            persona.varApellidoPaterno = primerApellido;
            persona.varApellidoMaterno = segundoApellido;
            persona.dtFechaNacimiento = fechaNacimiento;
            persona.varEstadoNacimiento = estadoNacimiento;
            persona.Telefono = opTelefono.agregarTelefonos(telefono);
            persona.cSexo = sexo;
            persona.varCurp = curp;

            direccion.varCalleNo = calleNo;
            direccion.varEstado = estado;
            direccion.varMunicipio = municipio;
            direccion.varColonia = colonia;

            return opPersona.actualizarDatosPersona(persona, direccion, id);
        }

        public void procesoRegistroActualizar(string id, string nombre, string primerApellido
                                          , string segundoApellido, DateTime fechaNacimiento
                                          , string estadoNacimiento, string telefono
                                          , string sexo, string calleNo
                                          , string estado, string municipio
                                          , string colonia, string curp,string textoBoton)
        {
            string respuesta = "";
            int idPersona = 0;
            try
            {
                if (textoBoton == "GUARDAR")
                {
                    respuesta = registrarDatosPersona(nombre, primerApellido, segundoApellido, fechaNacimiento, estadoNacimiento, telefono, sexo, calleNo, estado, municipio, colonia, curp);
                }
                else if (textoBoton == "ACTUALIZAR")
                {
                    idPersona = Convert.ToInt16(id);
                    respuesta = actualizarDatosPersona(idPersona, nombre, primerApellido, segundoApellido, fechaNacimiento, estadoNacimiento, telefono, sexo, calleNo, estado, municipio, colonia, curp);
                }

                MessageBox.Show(respuesta);
            }
            catch (Exception)
            {
                MessageBox.Show("Error Inesperado");
            }
            
        }

        public void eliminarPersona(Label lblIdPersona)
        {
            int idPersona = 0;
            string resultadoOperacion = "";
            try
            {
                idPersona = Convert.ToInt16(lblIdPersona.Text);
                resultadoOperacion = opPersona.eliminarPersona(idPersona);
            }
            catch (Exception ex)
            {
                resultadoOperacion = ex.Message;
            }
            MessageBox.Show(resultadoOperacion);
        }

        public void cargarInformacionPersona(DataGridView dgvDatos)
        {
            dgvDatos.DataSource = opPersona.obtenerDatosParciales();
        }

        public void generarCurp(string nombre, string primerApellido, string segundoApellido, DateTime fechaNacimiento, string estadoNacimiento, char sexo,TextBox txtCurp)
        {
            string validaciones = opCurp.verificarCampos(nombre, primerApellido, segundoApellido, estadoNacimiento, sexo.ToString());
            if (validaciones == "")
            {
                string curp = opCurp.generarCurp(nombre,primerApellido,segundoApellido,Convert.ToChar(sexo),fechaNacimiento.ToString(),estadoNacimiento);
                txtCurp.Text = curp;
            }
            else
            {
                MessageBox.Show(validaciones);
            }
        }

        public void validarTextBox(TextBox textBox, ErrorProvider error, CancelEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox.Text) || string.IsNullOrWhiteSpace(textBox.Text))
                {
                    e.Cancel = true;
                    error.SetError(textBox, "Es necesario llenar el campo");
                }
                else if (textBox.Text.Length < 2)
                {
                    e.Cancel = true;
                    error.SetError(textBox, "Debe tener más longitud los caracteres");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "!Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void llenarDatosPersonaFormulario(Label id, TextBox txtNombre, TextBox txtPrimerApellido, TextBox txtSegundoApellido, DateTimePicker dtFechaNacimiento, ComboBox cbEstadoNacimiento, ComboBox cbSexo, TextBox txtTelefono, TextBox txtCalleNo, ComboBox cbEstado,TextBox txtMunicipio, TextBox txtColonia, TextBox txtCurp)
        {
            PersonaDatosCompletos persona;
            int idPersona = 0;

            try
            {
                idPersona = Convert.ToInt16(id.Text);
                persona = opPersona.obtenerDatosCompletos(idPersona);

                txtNombre.Text = persona.nombre;
                txtPrimerApellido.Text = persona.primerApellido;
                txtSegundoApellido.Text = persona.segundoApellido;
                dtFechaNacimiento.Value = Convert.ToDateTime(persona.fechaNacimiento);
                cbEstadoNacimiento.SelectedValue = persona.estadoNacimiento;
                cbSexo.SelectedValue = persona.sexo;
                //txtTelefono.Text = persona.Telefono;
                txtCalleNo.Text = persona.calleNo;
                cbEstado.SelectedValue = persona.estado;
                txtMunicipio.Text = persona.municipio;
                txtColonia.Text = persona.colonia;
                txtCurp.Text = persona.curp;
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado.");
            }
            
        }

        public void limpiarCamposFormulario(Label lblIdPersona, TextBox txtNombre, TextBox txtPrimerApellido, TextBox txtSegundoApellido, ComboBox cbEstadoNacimiento, ComboBox cbSexo, TextBox txtTelefono, TextBox txtCalleNo, ComboBox cbEstado, TextBox txtMunicipio, TextBox txtColonia, TextBox txtCurp)
        {
            lblIdPersona.Text = "idPersona";
            txtNombre.Text = txtPrimerApellido.Text = txtSegundoApellido.Text = txtTelefono.Text = txtCalleNo.Text = txtMunicipio.Text = txtColonia.Text = txtCurp.Text = "";
            cbEstadoNacimiento.SelectedIndex = cbSexo.SelectedIndex = cbEstado.SelectedIndex = 0;
        }

    }
}
