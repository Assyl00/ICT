﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact_Book
{
    public partial class CreateContactForm : Form
    {
        public CreateContactForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CreateContactForm_Load(object sender, EventArgs e)
        {
            //nameTxt.Text = "";
            //phoneTxt.Text = "";
            //addressTxt.Text = "";
        }
    }
}
