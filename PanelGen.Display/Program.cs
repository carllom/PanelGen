﻿using System;
using System.Windows.Forms;

namespace PanelGen.Display
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PanelEditor());
            //Application.Run(new Form2());
        }
    }
}
