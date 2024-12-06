using System;
using System.IO;
using System.Windows.Forms;
using static player_stats_1._2.basket_stats;

namespace player_stats_1._2
{
    public partial class futbol_stats : Form
    {
        private EstadisticasFutbol estadisticasFutbol;

        public futbol_stats()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        private void btncalcular_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = txtnombre_jugador.Text;
                string nombre_equipo = txtnombre_equipo.Text;
                string posicion_jugador = txtposicion_jugador.Text;

                int partidos_jugados = int.Parse(txtpartidos_jugados.Text);
                int goles_anotados = int.Parse(txtgoles_anotados.Text);
                int numero_jugador = int.Parse(txtnumero_jugador.Text);
                int asistencias = int.Parse(txtasistencias.Text);

                int faltas_cometidas = int.Parse(txtfaltas_cometidas.Text);
                int tarjetas_amarillas = int.Parse(txttarjetas_amarillas.Text);
                int tarjetas_rojas = int.Parse(txttarjetas_rojas.Text);
                int minutos_jugados = int.Parse(txtminutos_jugados.Text);

                // Calcular promedios
                double promedio_goles = partidos_jugados > 0 ? (double)goles_anotados / partidos_jugados : 0;
                double promedio_asistencias = partidos_jugados > 0 ? (double)asistencias / partidos_jugados : 0;
                double promedio_faltas = partidos_jugados > 0 ? (double)faltas_cometidas / partidos_jugados : 0;

                // Calcular porcentajes de tarjetas
                double porcentaje_tarjetasAmarillas = partidos_jugados > 0 ? (double)tarjetas_amarillas / partidos_jugados * 100 : 0;
                double porcentaje_tarjetasRojas = partidos_jugados > 0 ? (double)tarjetas_rojas / partidos_jugados * 100 : 0;

                // Cálculo de valoración general
                double valoracion = (goles_anotados * 4) + (asistencias * 3) - faltas_cometidas - (tarjetas_amarillas * 2) - (tarjetas_rojas * 5) + (minutos_jugados * 0.1);

                estadisticasFutbol = new EstadisticasFutbol(nombre, nombre_equipo, posicion_jugador, partidos_jugados, goles_anotados, numero_jugador, asistencias,
                    faltas_cometidas, tarjetas_amarillas, tarjetas_rojas, minutos_jugados, promedio_goles, promedio_asistencias, promedio_faltas,
                    porcentaje_tarjetasAmarillas, porcentaje_tarjetasRojas, valoracion);

                MessageBox.Show("Resultados calculados. Ahora puedes mostrarlos o guardarlos en archivo.", "Cálculo Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Por favor, asegúrate de que todos los campos numéricos estén llenos correctamente.", "Error en la Entrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnmostrar_datos_Click(object sender, EventArgs e)
        {
            if (estadisticasFutbol != null)
            {
                resultados_futbol futbol_result = new resultados_futbol(estadisticasFutbol);
                futbol_result.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Primero debes calcular las estadísticas antes de mostrarlas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public class EstadisticasFutbol
        {
            public string Nombre { get; set; }
            public string Nombre_equipo { get; set; }
            public string Posicion { get; set; }
            public int Partidos_jugados { get; set; }
            public int Goles_anotados { get; set; }
            public int Numero_jugador { get; set; }
            public int Asistencias { get; set; }

            public int Faltas_cometidas { get; set; }
            public int Tarjetas_amarillas { get; set; }
            public int Tarjetas_rojas { get; set; }
            public int Minutos_jugados { get; set; }

            public double Promedio_goles { get; set; }
            public double Promedio_asistencias { get; set; }
            public double Promedio_faltas { get; set; }

            public double Porcentaje_tarjetasAmarillas { get; set; }
            public double Porcentaje_tarjetasRojas { get; set; }

            public double Valoracion { get; set; }

            public EstadisticasFutbol(string nombre, string nombre_equipo, string posicion, int partidos_jugados, int goles_anotados, int numero_jugador,
                int asistencias, int faltas_cometidas, int tarjetas_amarillas, int tarjetas_rojas, int minutos_jugados,
                double promedio_goles, double promedio_asistencias, double promedio_faltas,
                double porcentaje_tarjetasAmarillas, double porcentaje_tarjetasRojas, double valoracion)
            {
                Nombre = nombre;
                Nombre_equipo = nombre_equipo;
                Posicion = posicion;
                Partidos_jugados = partidos_jugados;
                Goles_anotados = goles_anotados;
                Numero_jugador = numero_jugador;
                Asistencias = asistencias;

                Faltas_cometidas = faltas_cometidas;
                Tarjetas_amarillas = tarjetas_amarillas;
                Tarjetas_rojas = tarjetas_rojas;
                Minutos_jugados = minutos_jugados;

                Promedio_goles = promedio_goles;
                Promedio_asistencias = promedio_asistencias;
                Promedio_faltas = promedio_faltas;

                Porcentaje_tarjetasAmarillas = porcentaje_tarjetasAmarillas;
                Porcentaje_tarjetasRojas = porcentaje_tarjetasRojas;

                Valoracion = valoracion;
            }
        }

        private void btnsalir_del_programa_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void btnguardar_en_archivo_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo para confirmar si se desea guardar
            DialogResult resultado = MessageBox.Show("¿Desea guardar los resultados en un archivo?", "Guardar Resultados", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                // Llamar a la función para guardar los resultados
                guardar_resultados_archivo();
            }
        }

        private void guardar_resultados_archivo()
        {

            // verificacion de estadisticas 
            if (estadisticasFutbol == null)
            {
                MessageBox.Show("No hay estadísticas disponibles para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // esto sera lo que ira en el archivo 
            string contenido = $"Nombre: {estadisticasFutbol.Nombre}\n" +
            $"Nombre del equipo: {estadisticasFutbol.Nombre_equipo}\n" +
                       $"Numero del jugador: {estadisticasFutbol.Numero_jugador}\n" +
                       $"Posición del jugador: {estadisticasFutbol.Posicion}\n" +
                       $"Porcentaje de goles: {estadisticasFutbol.Promedio_goles:F2}%\n" +
                       $"Promedio de asistencias: {estadisticasFutbol.Promedio_asistencias:F2}%\n" +
                       $"Promedio de faltas: {estadisticasFutbol.Promedio_faltas}\n" +
                       $"Porcentaje en tarjetas amarillas: {estadisticasFutbol.Porcentaje_tarjetasAmarillas}\n" +
                       $"Porcentaje en tarjetas rojas: {estadisticasFutbol.Porcentaje_tarjetasRojas}\n" +
                       $"Valoracion: {estadisticasFutbol.Valoracion:F2}%\n";

            // el guardado del archivo, dandole el formato y preguntandonos en que lugar lo guardaremos 
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                saveFileDialog.Title = "Guardar Resultados";
                saveFileDialog.FileName = "ResultadosJugador.txt"; // Nombre por defecto


                // guardado de resultados 
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Guardar el contenido en el archivo seleccionado
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            writer.WriteLine(contenido);
                        }
                        MessageBox.Show("Resultados guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al guardar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void usoDelPrgoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instrucciones:\n\n" +
                "Bienvenidos a Player Stats Baloncesto, acá podras agregar datos y estadisticas de un jugador" +
                "y calcular para obtener los datos, !RECUERDA SIEMPRE DARLE EL BOTON DE CALCULAR LUEGO DE INGRESAR LOS DATOS!, una vez calculado podras acceder a las otras acciones del programa\n\n" +
                "1. Ingrese los datos del jugador.\n" +
                "2. Haga clic en 'Calcular' para obtener las estadísticas.\n" +
                "3. Haga clic en 'Guardar Resultados' para guardar los datos.\n" +
                "4. Puede ingresar nuevos jugadores haciendo clic en 'Nuevo Jugador'.\n",
                "Cómo usar el programa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void guardarEnArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea guardar los resultados en un archivo?", "Guardar Resultados", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                // Llamar a la función para guardar los resultados
                guardar_resultados_archivo();
            }
        }

        private void salirEnProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void acercaDeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string mensaje = "Acerca de nuestro programa(Proyecto):\n\n" +
                    "Bienvenidos a Player Stats Baloncesto, un proyecto creado por estudiantes de la Universidad Americana UAM " +
                    "de la facultad de Ingeniería y Arquitectura, de la clase Metodología de la Programación Estructurada. " +
                    "Con el fin de darles a los usuarios un programa donde puedan agregar las estadísticas de un jugador de baloncesto y calcularlas.\n\n" +
                    "Disfruta del programa.\n";

            // Muestra el mensaje en un MessageBox
            MessageBox.Show(mensaje, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
