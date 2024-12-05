using System;
using System.IO;
using System.Windows.Forms;
using static player_stats_1._2.basket_stats;

namespace player_stats_1._2
{
    public partial class basket_stats : Form
    {
        private Estadisticas_basket estadisticas_basket;

        public basket_stats()
        {
            InitializeComponent();

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        // mostrar estadisticas
        private void button3_Click(object sender, EventArgs e)
        {
            if (estadisticas_basket != null)
            {
                resultados_basket basket_result = new resultados_basket(estadisticas_basket);
                basket_result.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Primero debes calcular las estadísticas antes de mostrarlas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // le damos sus ordenes al btn de calcular 
        private void btncalcular_datos_Click(object sender, EventArgs e)
        {
            try
            {
                // datos del jugador 
                string nombre = txtnombre_jugador.Text;
                string nombre_equipo = txtequipo.Text;
                string posicion_jugador = comboBox1.Text;

                int numero_jugador = int.Parse(txtnumero_jugador.Text);
                int minutos_jugados = int.Parse(txtminutos_jugados.Text);
                int partidos_jugados = int.Parse(txtpartidos_jugados.Text);

                // tiros del jugador
                int tiros_anotados = int.Parse(txttiros_anotados.Text);
                int tiros_intentados = int.Parse(txttiros_intentados.Text);

                int tiros_libres_anotados = int.Parse(txttiros_libres_anotados.Text);
                int tiros_libres_intentados = int.Parse(txttiros_libres_intentados.Text);

                int tiros_3_anotados = int.Parse(txttiros_3_anotados.Text);
                int tiros_3_intentados = int.Parse(txttiros_3_intentados.Text);

                // otras estadisticas
                int rebotes_ofensivos = int.Parse(txtrebotes_ofensivos.Text);
                int rebotes_defensivos = int.Parse(txtrebotes_defensivos.Text);

                int asistencias = int.Parse(txtasistencias.Text);
                int robos = int.Parse(txtrobos.Text);

                int bloqueos = int.Parse(txtbloqueos.Text);
                int perdidas = int.Parse(txtperdidas.Text);

                // calculo de estadisticas del jugador 

                // iniciamos con el porcentaje de tiros 
                double porcentaje_de_tiros = (tiros_intentados > 0) ? (double)tiros_anotados / (tiros_anotados + tiros_intentados) * 100 : 0;

                // porcentaje de los tiros de 3 
                double porcentaje_tiros_de_3 = (tiros_3_intentados > 0) ? (double)tiros_3_anotados / (tiros_3_anotados + tiros_3_intentados) * 100 : 0;

                // porcentaje de tiros libres
                double porcentaje_tiros_libres = (tiros_libres_intentados > 0) ? (double)tiros_libres_anotados / (tiros_libres_anotados + tiros_libres_intentados) * 100 : 0;

                // total de rebotes
                int rebotes_totales_jugador = rebotes_defensivos + rebotes_ofensivos;

                // porcentaje de rebotes
                double porcentaje_de_rebotes = (rebotes_totales_jugador > 0) ? (double)rebotes_totales_jugador / (rebotes_defensivos + rebotes_ofensivos) * 100 : 0;

                // Calcular puntos totales anotados
                int puntos_totales_anotados = (tiros_3_anotados * 3) + (tiros_anotados * 2) + tiros_libres_anotados;

                // Calcular total de tiros intentados
                int total_tiros_intentados = tiros_intentados + tiros_3_intentados + tiros_libres_intentados;

                // Calcular eficiencia de tiro
                double eficiencia_de_tiro = (total_tiros_intentados > 0)
                    ? ((double)puntos_totales_anotados / total_tiros_intentados) * 100
                    : 0; // Evitar división por cero

                // Valoración de eficiencia del jugador
                double valoracion_de_eficiencia_general = (minutos_jugados > 0)? ((puntos_totales_anotados + rebotes_totales_jugador + asistencias + bloqueos + robos - perdidas) / minutos_jugados) * 100: 0;

                estadisticas_basket = new Estadisticas_basket(nombre, nombre_equipo, numero_jugador, posicion_jugador, porcentaje_de_tiros, porcentaje_tiros_de_3, porcentaje_tiros_libres, rebotes_totales_jugador, porcentaje_de_rebotes, puntos_totales_anotados, total_tiros_intentados, eficiencia_de_tiro, valoracion_de_eficiencia_general);

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

        // clase para almacenar las estadisticas de basket 
        public class Estadisticas_basket
        {
            public string Nombre { get; set; }
            public string Nombre_equipo { get; set; }
            public int Numero_jugador { get; set; }
            public string Posicion_jugador { get; set; }

            public double Porcentaje_tiro_calculado { get; set; }
            public double Porcentaje_tiro3_calculado { get; set; }
            public double Porcentaje_tiro_libre_calculado { get; set; }
            public int Rebotes_totales_jugador { get; set; }
            public double Porcentaje_rebotes { get; set; }
            public int Puntos_totales_anotados { get; set; }
            public int Total_tiros_intentados { get; set; }
            public double Porcentaje_eficiencia_de_tiro { get; set; }
            public double Porcentaje_de_eficiencia_general { get; set; }

            public Estadisticas_basket(string nombre, string nombre_equipo, int numero_jugador, string posicion_jugador, double porcentaje_tiro_calculado, double porcentaje_tiro3_calculado, double porcentaje_tiro_libre_calculado,
                                        int rebotes_totales_jugador, double porcentaje_rebotes_totales_calculado,
                                        int puntos_totales_anotados, int total_tiros_intentados,
                                        double porcentaje_eficacia_de_tiro, double porcentaje_de_eficiencia_general)
            {
                Nombre = nombre;
                Nombre_equipo = nombre_equipo;
                Numero_jugador = numero_jugador;
                Posicion_jugador = posicion_jugador;
                Porcentaje_tiro_calculado = porcentaje_tiro_calculado;
                Porcentaje_tiro3_calculado = porcentaje_tiro3_calculado;
                Porcentaje_tiro_libre_calculado = porcentaje_tiro_libre_calculado;

                Rebotes_totales_jugador = rebotes_totales_jugador;
                Porcentaje_rebotes = porcentaje_rebotes_totales_calculado;

                Puntos_totales_anotados = puntos_totales_anotados;
                Total_tiros_intentados = total_tiros_intentados;

                Porcentaje_eficiencia_de_tiro = porcentaje_eficacia_de_tiro;
                Porcentaje_de_eficiencia_general = porcentaje_de_eficiencia_general;
            }
        }


        // btn para salir del programa con un messagebox de confirmacion 
        private void btnsalir_programa_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Confirmar salida", MessageBoxButtons.YesNo);
            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        
        // btn para guardar en archivo los resultados del jugador 
        private void btnguardar_archivo_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo para confirmar si se desea guardar
            DialogResult resultado = MessageBox.Show("¿Desea guardar los resultados en un archivo?", "Guardar Resultados", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                // Llamar a la función para guardar los resultados
                guardar_resultados_archivo();
            }
        }

        // funcion para que el programa guarde los resultados 
        private void guardar_resultados_archivo ()
        {

            // verificacion de estadisticas 
            if (estadisticas_basket == null)
            {
                MessageBox.Show("No hay estadísticas disponibles para guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // esto sera lo que ira en el archivo 
            string contenido = $"Nombre: {estadisticas_basket.Nombre}\n" +
                       $"Nombre del equipo: {estadisticas_basket.Nombre_equipo}\n" +
                       $"Numero del jugador: {estadisticas_basket.Numero_jugador}\n" +
                       $"Posición del jugador: {estadisticas_basket.Posicion_jugador}\n" +
                       $"Porcentaje de Tiro: {estadisticas_basket.Porcentaje_tiro_calculado:F2}%\n" +
                       $"Porcentaje de Tiro de 3: {estadisticas_basket.Porcentaje_tiro3_calculado:F2}%\n" +
                       $"Puntos Totales Anotados: {estadisticas_basket.Puntos_totales_anotados}\n" +
                       $"Rebotes Totales: {estadisticas_basket.Rebotes_totales_jugador}\n" +
                       $"Eficiencia de Tiro: {estadisticas_basket.Porcentaje_eficiencia_de_tiro:F2}%\n";

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


        // un poco del menu strip y un mensaje de instrucciones 
        private void usoDelProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar un cuadro de diálogo con instrucciones sobre cómo usar el programa
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

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensaje = "Acerca de nuestro programa(Proyecto):\n\n" +
                                "Bienvenidos a Player Stats Baloncesto, un proyecto creado por estudiantes de la Universidad Americana UAM " +
                                "de la facultad de Ingeniería y Arquitectura, de la clase Metodología de la Programación Estructurada. " +
                                "Con el fin de darles a los usuarios un programa donde puedan agregar las estadísticas de un jugador de baloncesto y calcularlas.\n\n" +
                                "Disfruta del programa.\n";

            // Muestra el mensaje en un MessageBox
            MessageBox.Show(mensaje, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // guardar en archivo desde el menu strip 
        private void guardarEnArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea guardar los resultados en un archivo?", "Guardar Resultados", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                // Llamar a la función para guardar los resultados
                guardar_resultados_archivo();
            }
        }


        // salir del programa desde menu strip 
        private void salirDelProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void basket_stats_Load(object sender, EventArgs e)
        {

        }
    }
}
