using System;
using System.Linq;
using System.Windows.Forms;
using Consultorio_Erick.BD_Consultorio;

namespace Consultorio_Erick
{
    public partial class FormDentistas : Form
    {
        Consultorio_Denta_ERICKEntities db = new Consultorio_Denta_ERICKEntities();
        int idSeleccionado = 0;

        public FormDentistas()
        {
            InitializeComponent();
            Mostrar();
        }



        private void dgvDentistas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                idSeleccionado = Convert.ToInt32(dgvDentistas.Rows[e.RowIndex].Cells[0].Value);
                var d = db.Dentistas.Find(idSeleccionado);

                txtNombre.Text = d.Nombre;
                txtTelefono.Text = d.Telefono;
                txtEspecialidad.Text = d.Especialidad;
            }
        }

       

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var d = db.Dentistas.Find(idSeleccionado);

            db.Dentistas.Remove(d);
            db.SaveChanges();
            Mostrar();
            Limpiar();
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEspecialidad.Clear();
        }

        private void btnMostrar_Click_1(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void Mostrar()
        {
            dgvDentistas.DataSource = db.Dentistas.ToList();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Dentista d = new Dentista()
            {
                Nombre = txtNombre.Text,
                Telefono = txtTelefono.Text,
                Especialidad = txtEspecialidad.Text
            };

            db.Dentistas.Add(d);
            db.SaveChanges();
            Mostrar();
            Limpiar();
        }

        private void btnActualizar_Click_1(object sender, EventArgs e)
        {
            var d = db.Dentistas.Find(idSeleccionado);

            d.Nombre = txtNombre.Text;
            d.Telefono = txtTelefono.Text;
            d.Especialidad = txtEspecialidad.Text;

            db.SaveChanges();
            Mostrar();
            Limpiar();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            var d = db.Dentistas.Find(idSeleccionado);

            db.Dentistas.Remove(d);
            db.SaveChanges();
            Mostrar();
            Limpiar();
        }
    }
}


