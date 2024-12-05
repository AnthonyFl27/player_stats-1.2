using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static player_stats_1._2.basket_stats;

namespace player_stats_1._2
{
    public partial class resultados_basket : Form
    {
        public resultados_basket(Estadisticas_basket estadisticas)
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            txtnombre.Text = estadisticas.Nombre;
            txtnombre_equipo.Text = estadisticas.Nombre_equipo;
            txtnumero_jugador.Text = estadisticas.Numero_jugador.ToString();
            txtposicion_jugador.Text = estadisticas.Posicion_jugador;
            
            txttiro_calculado.Text = estadisticas.Porcentaje_tiro_calculado.ToString("F2") + "%";
            txttiro_3_calculado.Text = estadisticas.Porcentaje_tiro3_calculado.ToString("F2") + "%";
            txttiro_libre_calculado.Text = estadisticas.Porcentaje_tiro3_calculado.ToString("F2") + "%";

            txtrebotes_totales.Text = estadisticas.Rebotes_totales_jugador.ToString();

            txtpuntos_totales.Text = estadisticas.Puntos_totales_anotados.ToString();
            txttotal_tiros_intentados.Text = estadisticas.Total_tiros_intentados.ToString();

            txteficiencia_tiro.Text = estadisticas.Porcentaje_eficiencia_de_tiro.ToString() + "%";
            txteficiencia_general.Text = estadisticas.Porcentaje_de_eficiencia_general.ToString() + "%";
        }

        private void resultados_basket_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btningresar_nuevo_jugador_Click(object sender, EventArgs e)
        {
            this.Close();
            basket_stats nuevo_jugador_form = new basket_stats();
            nuevo_jugador_form.ShowDialog();
        }
    }
}
