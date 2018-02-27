using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _Othello
{
    public partial class ChooseSide : Form
    {
        Form form1 = new Form();

        public ChooseSide()
        {
            InitializeComponent();
        }

        public void New()
        {
            Form form1 = new Form();
        }

        private void Pieces()
        {
            Image black = _Othello.Properties.Resources.black;
            Image white = _Othello.Properties.Resources.white;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            form1.Dispose();
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            form1.Dispose();
        }
    }
}
