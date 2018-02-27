using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/*
 *Othello
 *Team 3
 *CSC 478B
 *Author Jake Crawford 
 */



namespace Othello
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Board());
        }
    }
}
