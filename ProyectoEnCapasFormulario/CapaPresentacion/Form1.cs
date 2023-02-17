using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        ListaProducto lproducto = new ListaProducto();
        public Form1()
        {
            InitializeComponent();
            lproducto.conectarDB();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(textBoxCodigo.Text);
            string nombre = textBoxNombre.Text;
            double precio = double.Parse(textBoxPrecio.Text);
            lproducto.insertarRegistro(codigo,nombre,precio);
            DataTable dt = lproducto.cargarRegistros();
            dataGridViewRegistros.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = lproducto.cargarRegistros();
            dataGridViewRegistros.DataSource = dt;
            //lproducto.cargarRegistros(dataGridViewRegistros);
        }

        private void dataGridViewRegistros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxCodigo.Text = dataGridViewRegistros.CurrentRow.Cells[0].Value.ToString();
                textBoxNombre.Text = dataGridViewRegistros.CurrentRow.Cells[1].Value.ToString();
                textBoxPrecio.Text = dataGridViewRegistros.CurrentRow.Cells[2].Value.ToString();
            }
            catch(Exception ex)
            {
                Console.WriteLine("No se pueden reflejar los cambios en los TEXTBOX:"+ex.ToString());
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxCodigo.Text = "";
            textBoxNombre.Text = "";
            textBoxPrecio.Text = "";
        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            int codigo = int.Parse(textBoxCodigo.Text);
            string nombre = textBoxNombre.Text;
            double precio = double.Parse(textBoxPrecio.Text);
            lproducto.actualizarRegistro(codigo,nombre,precio);
            DataTable dt = lproducto.cargarRegistros();
            dataGridViewRegistros.DataSource = dt;
        }

        private void buttonSalir_Click(object sender, EventArgs e)
        {
            string message = "Salir del formulario";
            string caption = "Salir";
            MessageBoxButtons btnsalir = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(this, message, caption, btnsalir);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
            
            lproducto.desconectarDB();
        }
    }
}
