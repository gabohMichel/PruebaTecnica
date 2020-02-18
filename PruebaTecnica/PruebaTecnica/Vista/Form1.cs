using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PruebaTecnica.Vista;

namespace PruebaTecnica
{
    public partial class Form1 : Form
    {
        VistaForm1 vista;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vista = new VistaForm1(cbEstadoNacimiento,cbEstado,cbSexo,dgvDatos,dtFechaNacimiento);
        }

        private void btnDinamico_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoNacimiento = cbEstadoNacimiento.SelectedValue.ToString();
                string estado = cbEstado.SelectedValue.ToString();
                string genero = cbSexo.SelectedValue.ToString();
                string textoBoton = btnDinamico.Text;

                vista.procesoRegistroActualizar(lblIdPersona.Text, txtNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text, dtFechaNacimiento.Value, estadoNacimiento, txtTelefono.Text, genero, txtCalleNo.Text, estado, txtMunicipio.Text, txtColonia.Text, txtCurp.Text, textoBoton);

                vista.cargarInformacionPersona(dgvDatos);
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha seleccionado un valor valido para las listas de Estado de Nacimiento, Estado ó Sexo");
            }
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            vista.validarTextBox(txtNombre,errorProvider1,e);
        }

        private void txtPrimerApellido_Validating(object sender, CancelEventArgs e)
        {
            vista.validarTextBox(txtPrimerApellido, errorProvider1, e);
        }

        private void btnGenerarCurp_Click(object sender, EventArgs e)
        {
            string estadoNacimiento = cbEstadoNacimiento.SelectedValue.ToString();
            char genero = Convert.ToChar(cbSexo.SelectedValue.ToString());
            vista.generarCurp(txtNombre.Text, txtPrimerApellido.Text, txtSegundoApellido.Text,dtFechaNacimiento.Value, estadoNacimiento, genero,txtCurp);
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblIdPersona.Text = dgvDatos[0, e.RowIndex].Value.ToString();
                int idPersona = Convert.ToInt16(lblIdPersona.Text);
                btnDinamico.Text = "ACTUALIZAR";
                btnEliminar.Enabled = true;
                vista.llenarDatosPersonaFormulario(lblIdPersona, txtNombre, txtPrimerApellido, txtSegundoApellido, dtFechaNacimiento, cbEstadoNacimiento, cbSexo, txtTelefono, txtCalleNo, cbEstado, txtMunicipio, txtColonia, txtCurp);
            }
            catch (Exception)
            {
                MessageBox.Show("Error inesperado al obtener datos de la persona.");
            }
            
        }

        private void btnLimpiarForm_Click(object sender, EventArgs e)
        {
            btnDinamico.Text = "GUARDAR";
            btnEliminar.Enabled = false;
            vista.limpiarCamposFormulario(lblIdPersona,txtNombre,txtPrimerApellido,txtSegundoApellido,cbEstadoNacimiento,cbSexo,txtTelefono,txtCalleNo,cbEstado,txtMunicipio,txtColonia,txtCurp);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            vista.eliminarPersona(lblIdPersona);
            vista.cargarInformacionPersona(dgvDatos);
        }
    }
}
