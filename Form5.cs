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
    public partial class Form5 : Form
    {
        int ukIgrac, ukComp1, ukComp2;
        string pobjednik;

        private void Form5_Shown(object sender, EventArgs e)
        {
            label2.Text = "Igrac-> bodovi: " + ukIgrac.ToString(); 
            label3.Text = "Comp1-> bodovi: " + ukComp1.ToString();
            label4.Text = "Comp2-> bodovi: " + ukComp2.ToString();

            if (ukIgrac > ukComp1 && ukIgrac > ukComp2) pobjednik = "igrac";
            else if (ukComp1 > ukIgrac && ukComp1 > ukComp2) pobjednik = "comp 1";
            else if (ukComp2 > ukIgrac && ukComp2 > ukComp1) pobjednik = "comp 2";
            else pobjednik = "nitko";

            label5.Text = "Pobjedio je ... " + pobjednik + "!";
        }

        public Form5(int ukIgrac,int ukComp1,int ukComp2)
        {
            InitializeComponent();
            this.ukIgrac = ukIgrac;
            this.ukComp1 = ukComp1;
            this.ukComp2 = ukComp2;
            
        }
    }
}
