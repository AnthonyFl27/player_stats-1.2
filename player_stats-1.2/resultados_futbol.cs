using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static player_stats_1._2.futbol_stats;

namespace player_stats_1._2
{
    public partial class resultados_futbol : Form
    {
        public resultados_futbol(EstadisticasFutbol estadisticas)
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            txtnombre_jugador.Text = estadisticas.Nombre;
            txtnombre_equipo.Text = estadisticas.Nombre_equipo;
            txtposicion.Text = estadisticas.Posicion;
            txtnumero.Text = estadisticas.Numero_jugador.ToString();
            
            txtpromedio_goles.Text = estadisticas.Promedio_goles.ToString("F2") + "%";
            txtpromedio_asistencias.Text = estadisticas.Promedio_asistencias.ToString("F2") + "%";
            txtpromedio_faltas.Text = estadisticas.Promedio_faltas.ToString("F2") + "%";

            txtporcentaje_tarjetasamarillas.Text = estadisticas.Porcentaje_tarjetasAmarillas.ToString("F2") + "%";
            txtporcentaje_tarjetasrojas.Text = estadisticas.Porcentaje_tarjetasRojas.ToString("F2") + "%";

            txtvaloracion.Text = estadisticas.Valoracion.ToString("F2") + "%";
        }

        private void resultados_futbol_Load(object sender, EventArgs e)
        {

        }

        private void btningresar_nuevo_jugador_Click(object sender, EventArgs e)
        {
            this.Close();
            futbol_stats nuevo_jugador_form = new futbol_stats();
            nuevo_jugador_form.ShowDialog();
        }

        private void btnsalir_del_programa_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
