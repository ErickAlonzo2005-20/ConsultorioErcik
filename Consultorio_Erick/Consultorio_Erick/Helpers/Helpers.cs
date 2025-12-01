using System;
using System.Windows.Forms;

namespace Consultorio_Erick.Helpers
{
    public static class Helpers
    {
       
        public static bool CampoVacio(string texto)
        {
            return string.IsNullOrWhiteSpace(texto);
        }

      
        public static void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

      
        public static bool Confirmar(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        

        public static string ObtenerEstado(DateTime fecha, int duracionMinutos)
        {
            DateTime fin = fecha.AddMinutes(duracionMinutos);
            DateTime ahora = DateTime.Now;

            if (ahora < fecha)
                return "Vigente";
            else if (ahora >= fecha && ahora < fin)
                return "En Proceso";
            else
                return "Finalizado";
        }

    


        public static int DiasRestantes(DateTime fecha)
        {
            return (fecha - DateTime.Now).Days;
        }
    }
}

