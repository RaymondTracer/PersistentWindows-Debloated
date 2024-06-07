﻿using System;
using System.Windows.Forms;

using PersistentWindows.Common.WinApiBridge;

namespace PersistentWindows.Common
{
    public partial class LayoutProfile : Form
    {
        public char snapshot_name;

        public LayoutProfile()
        {
            User32.SetThreadDpiAwarenessContextSafe();
            InitializeComponent();
        }

        private void ProfileName_TextChanged(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            snapshot_name = Char.ToLower(str[0]); 
            Close();
        }


        private void LayoutProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
