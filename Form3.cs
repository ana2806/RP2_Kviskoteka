using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kviskoteka
{
    public partial class Form3 : Form
    {
        Form5 form5;
        

        Dictionary<String, List<List<String>>> dict;
        String pogadanaOsoba;
        List<List<String>> pitanja;
        //kojoj osobi ce biti dodijeljeni koji odgovori (1,2,3 - 1 je od prave osobe)
        int a, b, c;
        string prava;
        int probB = 20;
        int probC = 10;

        int igracBodovi = 0, comp1Bodovi = 0, comp2Bodovi = 0;

        int ukIgrac,ukComp1,ukComp2;

        //ako je kvisko iskorišten, onda je kviskoUsed=2, inace 1
        int kviskoIgracUsed=1,kviskoComp1Used=1, kviskoComp2Used=1;
        //ima li igrac kviska
        int KviskoIgrac = 0;
        int KviskoComp1 = 0, KviskoComp2 = 0;

        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();
        // This integer variable keeps track of the remaining time.
        int timeLeft = 10;

        private void Form3_Shown(object sender, EventArgs e)
        {
            Datoteka datoteka = new Datoteka("C:\\Users\\Ana\\source\\repos\\Kviskoteka\\Kviskoteka\\osobe.txt");
            dict = datoteka.readOsobe();
            //keys je niz kljuceva
            string[] keys = new string[dict.Count];
            dict.Keys.CopyTo(keys, 0);

            Random random = new Random();
            int randomKey = random.Next(0, keys.Length);
            pogadanaOsoba = keys[randomKey];
            label1.Text = pogadanaOsoba;
            pitanja = dict[pogadanaOsoba];
            foreach (var pitanje in pitanja)
            {
                comboBox1.Items.Add(pitanje[0]);
            }

            //random pridruzivanje za a,b,c
            var nums = Enumerable.Range(1, 3).ToArray();
            var rnd = new Random();

            // Shuffle array
            for (int i = 0; i < nums.Length; ++i)
            {
                int randomIndex = rnd.Next(nums.Length);
                int temp = nums[randomIndex];
                nums[randomIndex] = nums[i];
                nums[i] = temp;
            }

            a = nums[0];
            if (a == 1) prava = "a";
            b = nums[1];
            if (b == 1) prava = "b";
            c = nums[2];
            if (c == 1) prava = "c";

            timer1.Start();
            timeLabel.Text = timeLeft + " seconds";

            if (KviskoIgrac == 0) button3.Enabled = false;
        }
        
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                button1.Enabled = false;
                comboBox2.Visible = true;
                button2.Visible = true;
            }
        }

        public Form3(int ukIgrac,int ukComp1,int ukComp2, int KviskoIgrac, int KviskoComp1, int KviskoComp2)
        {
            //Form3 igra3 = new Form3(ukIgrac, ukComp1, ukComp2, KI, KC1, KC2);
            InitializeComponent();
            this.ukIgrac = ukIgrac;
            this.ukComp1 = ukComp1;
            this.ukComp2 = ukComp2;
            this.KviskoComp1 = KviskoComp1;
            this.KviskoIgrac = KviskoIgrac;
            this.KviskoComp2 = KviskoComp2;
            //odredi hoce li compovi koristiti kviska
            if (Global.Comp1WinsGame3 > Global.Comp1WinsGame2) kviskoComp1Used = 2;
            if (Global.Comp2WinsGame3 > Global.Comp2WinsGame2) kviskoComp2Used = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form5=new Form5(ukIgrac,ukComp1,ukComp2);
            form5.Show();
            this.Hide();
        }

        void kraj()
        {

            button1.Visible = false; button2.Visible = false; button3.Visible = false;
            label1.Visible = false; label2.Visible = false; label3.Visible = false; label4.Visible = false;
            label5.Visible = false; label6.Visible = false; label7.Visible = false; label8.Visible = false;
            comboBox1.Visible = false; comboBox2.Visible = false;

            button4.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;

            label9.Text = "Igrac-> bodovi: " + igracBodovi.ToString();
            label10.Text = "Comp1-> bodovi: " + comp1Bodovi.ToString();
            label11.Text = "Comp2-> bodovi: " + comp2Bodovi.ToString();

            ukIgrac += igracBodovi;
            ukComp1 += comp1Bodovi;
            ukComp2 += comp2Bodovi;

        }

        //iskoristi kviska
        private void button3_Click(object sender, EventArgs e)
        {
            kviskoIgracUsed = 2;
            KviskoIgrac = 0;
            AutoClosingMessageBox.Show("Izabrali ste korištenje Kviska!", "Bravo", 1000);
            button3.Enabled = false;
        }

        int simulirajComp(int prob)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            if (randomNumber < prob)
                return (1);
            else return (0);
        }
        
        //postavljanje pitanja
        private void button1_Click_1(object sender, EventArgs e)
        {
            int index= comboBox1.SelectedIndex;
            if (index != -1)
            {
                if (prava == "a")
                {
                    label2.Text = pitanja[index][a];
                    if(simulirajComp(probB)==1)
                        label3.Text = pitanja[index][a];
                    else label3.Text = pitanja[index][b];
                    if (simulirajComp(probC) == 1)
                        label4.Text = pitanja[index][a];
                    else label4.Text = pitanja[index][c];
                }
                if (prava == "b")
                {
                    if (simulirajComp(probB) == 1)
                        label2.Text = pitanja[index][b];
                    else label2.Text = pitanja[index][a];
                    label3.Text= pitanja[index][b];
                    if (simulirajComp(probC) == 1)
                        label4.Text = pitanja[index][b];
                    else label4.Text = pitanja[index][c];
                }
                if (prava == "c")
                {
                    if (simulirajComp(probB) == 1)
                        label2.Text = pitanja[index][c];
                    else label2.Text = pitanja[index][a];
                    if (simulirajComp(probC) == 1)
                        label3.Text = pitanja[index][c];
                    else label3.Text = pitanja[index][b];
                    label4.Text = pitanja[index][c];
                }
            }
            else
                MessageBox.Show("Molimo, odaberite pitanje!");
            
        }
        
        //pogadanje osobe
        private void button2_Click(object sender, EventArgs e)
        {
            comp1Bodovi += 5 * simulirajComp(Global.Comp1WinsGame3)*kviskoComp1Used;
            comp2Bodovi += 5 * simulirajComp(Global.Comp2WinsGame3)*kviskoComp2Used;
            int index = comboBox2.SelectedIndex;
            if (index != -1)
            {
                if ((string)comboBox2.Items[index] == prava)
                {
                    AutoClosingMessageBox.Show("Točan odgovor! :D", "Caption", 1000);
                    igracBodovi+=5*kviskoIgracUsed;
                }
                else
                {
                    AutoClosingMessageBox.Show("Netocno :'(", "Caption", 1000);
                }
                //kraj() -> funkcija za prijelaz na kraj igre, pokazivanje rezultata igre
                // i prijelaz u nadformu gdje ce biti prikazani konacni rezultati
                kraj();
            }
            else
                MessageBox.Show("Molimo, odaberite odgovor!");
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }
    }
}
