/**
 * MainForm.cs - The MainForm of the DurakGameUI project
 * 
 * This project is used to play the Durak card game.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-03-25
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DurakGameUI
{
    public partial class frmDurak : Form
    {
        public frmDurak()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

    }
}