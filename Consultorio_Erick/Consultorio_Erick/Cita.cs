using Consultorio_Erick.BD_Consultorio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Consultorio_Erick
{
    public partial class FormCita : Form
    {
        Consultorio_Denta_ERICKEntities db = new Consultorio_Denta_ERICKEntities(); // <--- cambia si tu contexto se llama diferente
        int citaSeleccionadaID = 0;

        public FormCita()
        {
            InitializeComponent();
        }

        private void FormCita_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarControles();
            CargarCitas();
        }

        private void CargarCombos()
        {
            cbPaciente.DataSource = db.Pacientes
                .Select(p => new
                {
                    p.PacienteID,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                })
                .ToList();
            cbPaciente.DisplayMember = "NombreCompleto";
            cbPaciente.ValueMember = "PacienteID";

            cbDentista.DataSource = db.Dentistas.ToList();
            cbDentista.DisplayMember = "Nombre";
            cbDentista.ValueMember = "DentistaID";

            cbMotivo.DataSource = db.Motivoes.ToList();
            cbMotivo.DisplayMember = "Descripcion";
            cbMotivo.ValueMember = "MotivoID";
        }

        private void ConfigurarControles()
        {
            dtpFecha.Format = DateTimePickerFormat.Custom;
            dtpFecha.CustomFormat = "yyyy-MM-dd HH:mm";
            dtpFecha.ShowUpDown = true;

            numDuracion.Minimum = 15;
            numDuracion.Maximum = 240;
            numDuracion.Value = 30;
            numDuracion.Increment = 15;
        }

        private void CargarCitas()
        {
            var lista = db.Citas
                .Select(c => new
                {
                    c.CitaID,
                    Paciente = c.Paciente.Nombre + " " + c.Paciente.Apellido,
                    Dentista = c.Dentista.Nombre,
                    Motivo = c.Motivo.Descripcion,
                    c.Fecha,
                    c.DuracionMinutos
                })
                .ToList();

            dgvCitas.DataSource = lista;
        }

        private bool ValidarHorario()
        {
            DateTime fechaSeleccionada = dtpFecha.Value;
            int duracion = (int)numDuracion.Value;
            int dentistaID = (int)cbDentista.SelectedValue;

            var choque = db.Citas.FirstOrDefault(c =>
                c.DentistaID == dentistaID &&
                c.CitaID != citaSeleccionadaID &&
                c.Fecha <= fechaSeleccionada.AddMinutes(duracion) &&
                fechaSeleccionada <= c.Fecha.AddMinutes(c.DuracionMinutos)
            );

            return choque != null;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarHorario())
            {
                MessageBox.Show("Este dentista ya tiene una cita en ese horario.");
                return;
            }

            var nueva = new Cita
            {
                PacienteID = (int)cbPaciente.SelectedValue,
                DentistaID = (int)cbDentista.SelectedValue,
                MotivoID = (int)cbMotivo.SelectedValue,
                Fecha = dtpFecha.Value,
                DuracionMinutos = (int)numDuracion.Value
            };

            db.Citas.Add(nueva);
            db.SaveChanges();
            MessageBox.Show("Cita agregada exitosamente");
            CargarCitas();
        }

        private void dgvCitas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                citaSeleccionadaID = Convert.ToInt32(dgvCitas.Rows[e.RowIndex].Cells["CitaID"].Value);

                var cita = db.Citas.Find(citaSeleccionadaID);

                cbPaciente.SelectedValue = cita.PacienteID;
                cbDentista.SelectedValue = cita.DentistaID;
                cbMotivo.SelectedValue = cita.MotivoID;
                dtpFecha.Value = cita.Fecha;
                numDuracion.Value = cita.DuracionMinutos;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (citaSeleccionadaID == 0)
            {
                MessageBox.Show("Selecciona una cita para editar");
                return;
            }

            if (ValidarHorario())
            {
                MessageBox.Show("Conflicto de horario con otra cita");
                return;
            }

            var cita = db.Citas.Find(citaSeleccionadaID);

            cita.PacienteID = (int)cbPaciente.SelectedValue;
            cita.DentistaID = (int)cbDentista.SelectedValue;
            cita.MotivoID = (int)cbMotivo.SelectedValue;
            cita.Fecha = dtpFecha.Value;
            cita.DuracionMinutos = (int)numDuracion.Value;

            db.SaveChanges();
            MessageBox.Show("Actualizado correctamente");
            CargarCitas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (citaSeleccionadaID == 0)
            {
                MessageBox.Show("Selecciona una cita para eliminar");
                return;
            }

            if (MessageBox.Show("¿Eliminar la cita?",
                "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var cita = db.Citas.Find(citaSeleccionadaID);
                db.Citas.Remove(cita);
                db.SaveChanges();
                MessageBox.Show("Eliminado exitosamente");
                CargarCitas();
                citaSeleccionadaID = 0;
            }
        }


        private void btnMostrar_Click_1(object sender, EventArgs e)
        {
            CargarCitas();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (ValidarHorario())
            {
                MessageBox.Show("Este dentista ya tiene una cita en ese horario.");
                return;
            }

            var nueva = new Cita
            {
                PacienteID = (int)cbPaciente.SelectedValue,
                DentistaID = (int)cbDentista.SelectedValue,
                MotivoID = (int)cbMotivo.SelectedValue,
                Fecha = dtpFecha.Value,
                DuracionMinutos = (int)numDuracion.Value
            };

            db.Citas.Add(nueva);
            db.SaveChanges();
            MessageBox.Show("Cita agregada exitosamente");
            CargarCitas();
        }
    }
}
