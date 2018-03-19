using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kviskoteka
{
    public partial class Form4 : Form
    {
        //ideja: ova forma ostaje otvorena, kao pocetni ekran
        //ostale forme imaju u sebi samo objekt za sljedecu, i na kraju igre se sakriju/zatvore
        //a nova forma se pokaze
        //zadnja forma (Form5) je samo prozor koji govori da je igra zavrsila i prikazuje bodove
        //kad se Form5 zatvori, ostaje samo pocetni prozor, moze se igrati opet, moze se zatvoriti
        Form1 form1 = new Form1();

        public Form4()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            form1.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
