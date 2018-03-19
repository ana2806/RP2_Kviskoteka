using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Kviskoteka
{    
    public partial class Form1 : Form
    {
        List<Tuple<String, List<Tuple<String, String>>>> lista;
        int brojac = 0;
        int igracBodovi=0, comp1Bodovi=0, comp2Bodovi = 0;
        int comp1Prob = 70;
        int comp2Prob = 60;
        int bodoviZaKviska = 7;

        int KviskoIgrac=0,KviskoComp1=0, KviskoComp2 = 0;

        //ovo je za buttone dole lijevo, da mozemo testirati igre, na kraju treba maknut
        
        Form2 form2=new Form2(0,0,0,0,0,0);
        Form3 form3=new Form3(0,0,0,0,0,0);

        public Form1()
        {
            InitializeComponent();

        }
        
        private static Random rng = new Random();

        public List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return (list);
        }

        //usage
        //List lista=Shuffle(lista)
        int provjera()
        {
            int k = 0;

            if (radioButton1.Checked == true)
            {
                k = 0;
            }
            else if (radioButton2.Checked == true)
            {
                k = 1;
            }
            else if (radioButton3.Checked == true)
            {
                k = 2;
            }

            //ovo je dobro, ne diraj
            if (lista[brojac-1].Item2[k].Item2 == "T") return (1);
            else return (0);
        }
        void kraj()
        {
            button1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            textBox1.Visible = false;

            button2.Visible = false;

            button3.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            label1.Text = "Igrac-> bodovi: " + igracBodovi.ToString() + ", kvisko: "+KviskoIgrac.ToString();
            label2.Text = "Comp1-> bodovi: " + comp1Bodovi.ToString() + ", kvisko: "+KviskoComp1.ToString();
            label3.Text = "Comp2-> bodovi: " + comp2Bodovi.ToString() + ", kvisko: "+KviskoComp2.ToString();

        }

        int simulirajComp(int prob)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            if (randomNumber < prob)
                return (1);
            else return (0);            
        }

        private void button4_Click(object sender, EventArgs e)
        {

            form3.Show();
            this.Hide();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 otvori = new Form2(igracBodovi, comp1Bodovi, comp2Bodovi, KviskoIgrac, KviskoComp1, KviskoComp2);
            otvori.Show();
            button3.Visible = false;
            button2.Visible = true;
            brojac = 0;
            lista.Clear();
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            //resetiraj sve vrijednosti
            igracBodovi = 0;
            comp1Bodovi = 0;
            comp2Bodovi = 0;
            KviskoIgrac = 0;
            KviskoComp1 = 0;
            KviskoComp2 = 0;
            //sakrij ovu formu
            this.Hide();
            //otvori formu 2, za novu igru
            //posalji kviska i ostalo ak treba
            //zatvori ovu formu
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //provjera odgovora i dodatak bodova
            if (provjera() == 1)
            {
                igracBodovi++;
                AutoClosingMessageBox.Show("Točan odgovor! :D", "Caption", 1000);
            }
            else AutoClosingMessageBox.Show("Netočan odgovor :'(", "Caption", 1000);

            //provjera odgovora za comp1 i comp2

            comp1Bodovi += simulirajComp(comp1Prob);
            comp2Bodovi += simulirajComp(comp2Prob);

            //uzme listu iz button2
            if (brojac < lista.Count)
            {
                var pitOdg = lista[brojac++];

                String pitanje = pitOdg.Item1;
                Debug.WriteLine(pitanje);
                textBox1.Text = pitanje;
                //odgovor je lista
                var odgovor = pitOdg.Item2;
                radioButton1.Text = odgovor[0].Item1;
                radioButton2.Text = odgovor[1].Item1;
                radioButton3.Text = odgovor[2].Item1;
                Debug.WriteLine(odgovor[0].Item1 + " " + odgovor[0].Item2);
            }
            else
            {
                //poruka o gotovoj igri
                //Debug.WriteLine(igracBodovi);
                KviskoIgrac = provjeriKviska(igracBodovi);
                KviskoComp1 = provjeriKviska(comp1Bodovi);
                KviskoComp2 = provjeriKviska(comp2Bodovi);
                kraj();
            }

        }

        int provjeriKviska(int bodovi)
        {
            if (bodovi >= bodoviZaKviska) return (1);
            else return (0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Datoteka probica = new Datoteka("C:\\Users\\Ana\\source\\repos\\Kviskoteka\\Kviskoteka\\pitanja.txt");
            lista = probica.readABCpitalica();
            lista = Shuffle<Tuple<String, List<Tuple<String, String>>>>(lista);

            button1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            textBox1.Visible = true;

            button2.Visible = false;

            //postavljanje prvog pitanja
            var pitOdg = lista[brojac++];
            String pitanje = pitOdg.Item1;
            Debug.WriteLine(pitanje);
            textBox1.Text = pitanje;
            //odgovor je lista
            var odgovor = pitOdg.Item2;
            radioButton1.Text = odgovor[0].Item1;
            radioButton2.Text = odgovor[1].Item1;
            radioButton3.Text = odgovor[2].Item1;
            Debug.WriteLine(odgovor[0].Item1 + " " + odgovor[0].Item2);


        }
    }
}
