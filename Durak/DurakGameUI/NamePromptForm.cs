/**
 * NamePromptForm.cs - Form that prompts for the player's name
 * 
 * This form is used to get the player's name.
 * 
 * @author  Anastasiia Kononirenko
 * @author  Harry Palmer
 * @author  Andrew Rocha
 * @author  Natan Colavite Dellagiustina
 * @since   2020-04-11
 * @see     Beginning Visual C# 2012 Programming By Karli Watson, et al.
 */

using System;
using System.Windows.Forms;

namespace DurakGameUI
{
    public partial class frmNamePrompt : Form
    {
        /// <summary>
        /// Player's name
        /// </summary>
        public String PlayerName = String.Empty;

        /// <summary>
        /// frmNamePrompt default constructor
        /// </summary>
        public frmNamePrompt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fires when a user clicks the submit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            PlayerName = txtPlayerName.Text;    // get the player's name
            this.Close();                       // close the form
        }

        /// <summary>
        /// Fires when a user presses enter, after entering thier name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            // If a user pressed enter, click the Submit button
            if (e.KeyCode == Keys.Enter)
                btnSubmit_Click(this, new EventArgs());
        }
    }
}
