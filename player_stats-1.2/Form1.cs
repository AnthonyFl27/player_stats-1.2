﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace player_stats_1._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            basket_stats basket_stats = new basket_stats();
            basket_stats.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            futbol_stats futbol_Stats = new futbol_stats();
            futbol_Stats.Show();
            this.Hide();
        }
    }
}
