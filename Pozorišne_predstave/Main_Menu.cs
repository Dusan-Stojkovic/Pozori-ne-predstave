﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pozorišne_predstave
{
    public partial class PozoristeForm : Form
    {
        public PozoristeForm()
        {
            InitializeComponent();
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rezervacijeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RezervacijeForm f = new RezervacijeForm();
            f.Show();
            this.Hide();
        }

        private void poTrupamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PoTrupamaForm f = new PoTrupamaForm();
            f.Show();
            this.Hide();
        }
    }
}
