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
using Microsoft.VisualBasic;

namespace Kviskoteka
{
    public partial class Form2 : Form
    {
        List<Tuple<String, List<String>, List<String>, List<String>, List<String>>> lista;
        List<int> neotvoreni = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        String RjesenjeZaIgru;
        List<int> stupciZaPogadjanje = new List<int> { 17, 18, 19, 20 };
        int koja_igra, gotovo = 0;
        String[,] tablica = new String[4, 4];
        String[] RjesenjaStupaca = new String[4];
        int id_buttona;
        int bodoviIgrac = 0, bodoviComp1 = 0, bodoviComp2 = 0, ukIgrac,ukComp1,ukComp2;
        int bodoviZaKviska = 15, KviskoIgrac = 0 ,KviskoComp1 = 0, KviskoComp2 = 0;
        int IgracPravoKviska, Comp1PravoKviska, Comp2PravoKviska;
        int koristiKviska = 0;
    
        public Form2(int a,int b, int c, int d, int e, int f)
        {
            InitializeComponent();
            //zbrajaj bodove iz prijašnje igre
            ukIgrac = a;
            ukComp1 = b;
            ukComp2 = c;
            //ako je igrac dobio kviska u prijašnjoj igri daj mu mogućnost da ga koristi
            IgracPravoKviska = d;
            if (IgracPravoKviska == 0)
                button22.Enabled = false;
            //comp1 ima veću vjerojatnost da pobjedi u 2. igri nego 3. pa koristi Kviska ako ga ima
            Comp1PravoKviska = e;
            //comp2 ne koristi Kviska jer ima veću vjerojatnost da pobjedi u 3. igri
            Comp2PravoKviska = f;
            odaberi_igru();
            rasporedi_stupce();
            printaj_tablicu();
            neotvoreni = Shuffle<int>(neotvoreni);
            printaj_neotvorene();
        }

        private static Random rng = new Random();
        public int max(int a,int b)
        {
            if (a >= b) return a;
            return b;
        }
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

        public void odaberi_igru()
        {
            Datoteka probica = new Datoteka("C:\\Users\\Ana\\source\\repos\\Kviskoteka\\Kviskoteka\\asocijacije.txt");
            lista = probica.readAsocijacije();

            Random rnd = new Random();
            int rand = rnd.Next(lista.Count); // izaberi naizmjenicno koju igru igramo npr. rand = 0 --> pojam, rand = 1 --> zivotinja 

            var prva = lista[rand];
            //pamti koja igra je  odabrana
            koja_igra = rand;
            //pamti konacno rjesenje igre
            RjesenjeZaIgru = prva.Item1;
            Debug.WriteLine(RjesenjeZaIgru);

        }

        public void rasporedi_stupce()
        {
            //napravi permutaciju stupca
            int[] numbers = new int[] { 0, 1, 2, 3 };
            Random rnd = new Random();
            int[] MyRandomNumbers = numbers.OrderBy(x => rnd.Next()).ToArray();

            var stupac = lista[koja_igra];   // uzmi podatke iz datoteke koji se odnose samo na zadanu igru
      
            for (int i = 0; i < 4; i++)
            {
                int s = MyRandomNumbers[i];
                if(s == 0)
                {
                    List<String> odg = new List<string>(stupac.Item2); // uzeli smo prvi stupac iz datoteke, kopiramo listu pojmova + rjesenje stupca
                    RjesenjaStupaca[i] = odg[4];    //rjesenje je na 5. mjestu u listi
                    odg.RemoveAt(4);                 //makni rjesenje iz liste, pamtimo samo pojmove
                    odg = Shuffle<String>(odg);      //shufflaj pojmove da ne idu po redu
                    for (int r = 0; r < 4; r++)
                    {
                        tablica[r, i] = odg[r];
                     }
                }
 
                else if(s == 1)
                {
                    List<String> odg = new List<string>(stupac.Item3); // uzeli smo drugi stupac iz datoteke, kopiramo listu pojmova + rjesenje stupca
                    RjesenjaStupaca[i] = odg[4];    //rjesenje je na 5. mjestu u listi
                    odg.RemoveAt(4);                 //makni rjesenje iz liste, pamtimo samo pojmove
                    odg = Shuffle<String>(odg);      //shufflaj pojmove da ne idu po redu
                    for (int r = 0; r < 4; r++)
                    {
                        tablica[r, i] = odg[r];
                    }
                }
                else if(s == 2)
                {
                    List<String> odg = new List<string>(stupac.Item4); // uzeli smo treci stupac iz datoteke, kopiramo listu pojmova + rjesenje stupca
                    RjesenjaStupaca[i] = odg[4];    //rjesenje je na 5. mjestu u listi
                    odg.RemoveAt(4);                 //makni rjesenje iz liste, pamtimo samo pojmove
                    odg = Shuffle<String>(odg);      //shufflaj pojmove da ne idu po redu
                    for (int r = 0; r < 4; r++)
                    {
                        tablica[r, i] = odg[r];
                    }
                }
                else if(s == 3)
                {
                    List<String> odg = new List<string>(stupac.Item5); // uzeli smo cetvrti stupac iz datoteke, kopiramo listu pojmova + rjesenje stupca
                    RjesenjaStupaca[i] = odg[4];    //rjesenje je na 5. mjestu u listi
                    odg.RemoveAt(4);                 //makni rjesenje iz liste, pamtimo samo pojmove
                    odg = Shuffle<String>(odg);      //shufflaj pojmove da ne idu po redu
                    for (int r = 0; r < 4; r++)
                    {
                        tablica[r, i] = odg[r];
                    }
                }
                    
            }
               
        }
        
        public void printaj_tablicu()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Debug.Write(tablica[i, j] + " " );
                }
                Debug.Write("\n");
                Debug.Write(RjesenjaStupaca[i]);
                Debug.Write("\n");
            }
        }

        public String provjeri_tablicu(int id)
        {
            int koji_u_stupcu;
            int koji_u_retku;
            // pomaknemo za jedan, npr. 4/4 = 1, mi zelimo da button4 bude u 0.retku
            // provjera da ne dodjemo do tablica[nesto, -1] u returnu
            if (id % 4 == 0)
            {
                koji_u_stupcu = id / 4 - 1;
                koji_u_retku = 4;
            }
            // npr. button 13 je na poziciji (3,0) , 13/4 = 3, 13%4 == 1
            else
            {
                koji_u_stupcu = id / 4;
                koji_u_retku = id % 4;
            }
               
            // s obzirom na id, vraca poziciju koja odgovara u tablici 
            return tablica[koji_u_stupcu, koji_u_retku - 1];

        }

        public void printaj_neotvorene()
        {
            foreach(int i in neotvoreni)
            {
                Debug.Write(i + " ");
            }
            Debug.Write("\n");
        }

        public void igracPogodio(int koji_stupac, String rjesenje)
        {
            bodoviIgrac = bodoviIgrac + 5;
            AutoClosingMessageBox.Show("Točan odgovor! :D\n Pogađajte dalje :)", "Caption", 1000);
            if(koji_stupac == 0)
            {
                String text = provjeri_tablicu(1);
                button1.Text = text;
                button1.Enabled = false;
                neotvoreni.Remove(1);
                text = provjeri_tablicu(5);
                button5.Text = text;
                button5.Enabled = false;
                neotvoreni.Remove(5);
                text = provjeri_tablicu(9);
                button9.Text = text;
                button9.Enabled = false;
                neotvoreni.Remove(9);
                text = provjeri_tablicu(13);
                button13.Text = text;
                button13.Enabled = false;
                neotvoreni.Remove(13);

                button17.Enabled = false;
                button17.Text = rjesenje;
                //izbaci stupac 1 , tj button17 = Rjesenje A -->pogodjen 
                stupciZaPogadjanje.Remove(17);
            }

            if (koji_stupac == 1)
            {
                String text = provjeri_tablicu(2);
                button2.Text = text;
                button2.Enabled = false;
                neotvoreni.Remove(2);
                text = provjeri_tablicu(6);
                button6.Text = text;
                button6.Enabled = false;
                neotvoreni.Remove(6);
                text = provjeri_tablicu(10);
                button10.Text = text;
                button10.Enabled = false;
                neotvoreni.Remove(10);
                text = provjeri_tablicu(14);
                button14.Text = text;
                button14.Enabled = false;
                neotvoreni.Remove(14);

                button18.Enabled = false;
                button18.Text = rjesenje;
                //izbaci stupac 2 , tj button18 = Rjesenje B -->pogodjen 
                stupciZaPogadjanje.Remove(18);
            }

            if (koji_stupac == 2)
            {
                String text = provjeri_tablicu(3);
                button3.Text = text;
                button3.Enabled = false;
                neotvoreni.Remove(3);
                text = provjeri_tablicu(7);
                button7.Text = text;
                button7.Enabled = false;
                neotvoreni.Remove(7);
                text = provjeri_tablicu(11);
                button11.Text = text;
                button11.Enabled = false;
                neotvoreni.Remove(11);
                text = provjeri_tablicu(15);
                button15.Text = text;
                button15.Enabled = false;
                neotvoreni.Remove(15);

                button19.Enabled = false;
                button19.Text = rjesenje;
                //izbaci stupac 1 , tj button19 = Rjesenje C -->pogodjen 
                stupciZaPogadjanje.Remove(19);
            }

            if (koji_stupac == 3)
            {
                String text = provjeri_tablicu(4);
                button4.Text = text;
                button4.Enabled = false;
                neotvoreni.Remove(4);
                text = provjeri_tablicu(8);
                button8.Text = text;
                button8.Enabled = false;
                neotvoreni.Remove(8);
                text = provjeri_tablicu(12);
                button12.Text = text;
                button12.Enabled = false;
                neotvoreni.Remove(12);
                text = provjeri_tablicu(16);
                button16.Text = text;
                button16.Enabled = false;
                neotvoreni.Remove(16);

                button20.Enabled = false;
                button20.Text = rjesenje;
                //izbaci stupac 4 , tj button20 = Rjesenje D -->pogodjen 
                stupciZaPogadjanje.Remove(20);
            }
            printaj_neotvorene();
            //Debug.WriteLine(stupciZaPogadjanje[0] + " ostalo ih je " + stupciZaPogadjanje.Count);
            panel1.Enabled = true;
        }
        public void Comp1Pogodio(int koji_stupac)
        {
            bodoviComp1 = bodoviComp1 + 5;
            //primamo brojeve 17,18,19 ili 20
            int n = koji_stupac - 16, kopija;
            kopija = n - 1;
            String text = " ";
            Console.WriteLine("n je " + n);
            while (n < 17)
            {
                text = provjeri_tablicu(n);
                panel1.Controls["button" + n.ToString()].Text = text;
                neotvoreni.Remove(n);
                panel1.Enabled = false;
                panel1.Controls["button" + n.ToString()].Enabled = false;
                n = n + 4;
            }
            // disable-aj button za rjesenje stupca 
            String rjesenjeStupca = RjesenjaStupaca[kopija];

            this.Controls["button" + koji_stupac.ToString()].Text = rjesenjeStupca;
            this.Controls["button" + koji_stupac.ToString()].Enabled = false;
            //izbaci stupac 
            stupciZaPogadjanje.Remove(koji_stupac);
            AutoClosingMessageBox.Show("Comp1 je pogodio rješenje stupca!", "Caption", 1000);
            System.Threading.Thread.Sleep(2000);
        }
        public void Comp2Pogodio(int koji_stupac)
        {
            bodoviComp2 = bodoviComp2 + 5;
            //primamo brojeve 17,18,19 ili 20
            int n = koji_stupac - 16, kopija;
            kopija = n - 1;
            String text = " ";
            Console.WriteLine("n je " + n);
            while (n < 17)
            {
                text = provjeri_tablicu(n);
                panel1.Controls["button" + n.ToString()].Text = text;
                neotvoreni.Remove(n);
                panel1.Enabled = false;
                panel1.Controls["button" + n.ToString()].Enabled = false;
                n = n + 4;
            }
            // disable-aj button za rjesenje stupca 
            String rjesenjeStupca = RjesenjaStupaca[kopija];

            this.Controls["button" + koji_stupac.ToString()].Text = rjesenjeStupca;
            this.Controls["button" + koji_stupac.ToString()].Enabled = false;
            //izbaci stupac 
            stupciZaPogadjanje.Remove(koji_stupac);
            AutoClosingMessageBox.Show("Comp2 je pogodio rješenje stupca!", "Caption", 1000);
            System.Threading.Thread.Sleep(2000);
        }
        public void Comp1PogodioStupac(int koji_stupac)
        {
            bodoviComp1 = bodoviComp1 + 5;
            // prima stupce 17,18,19 ili 20
            int n = koji_stupac % 4;
            if (n == 0)
                n = 3;
            else n = n - 1;
            // disable - aj button
            this.Controls["button" + koji_stupac.ToString()].Text = RjesenjaStupaca[n];
            this.Controls["button" + koji_stupac.ToString()].Enabled = false;
            stupciZaPogadjanje.Remove(koji_stupac);
            AutoClosingMessageBox.Show("Comp1 je pogodio rješenje stupca!", "Caption", 1000);
            System.Threading.Thread.Sleep(2000);

        }
        public void Comp2PogodioStupac(int koji_stupac)
        {
            bodoviComp2 = bodoviComp2 + 5;
            // prima stupce 17,18,19 ili 20
            int n = koji_stupac % 4;
            if (n == 0)
                n = 3;
            else n = n - 1;
            // disable - aj button
            this.Controls["button" + koji_stupac.ToString()].Text = RjesenjaStupaca[n];
            this.Controls["button" + koji_stupac.ToString()].Enabled = false;
            stupciZaPogadjanje.Remove(koji_stupac);
            AutoClosingMessageBox.Show("Comp2 je pogodio rješenje stupca!", "Caption", 1000);
            System.Threading.Thread.Sleep(2000);
        }
        public void Comp1Konacno()
        {
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            AutoClosingMessageBox.Show("Comp1 pogađa rješenje.." , "Pobjeda", 1000);
            if (randomNumber < Global.Comp1WinsGame2)
            {
                bodoviComp1 = bodoviComp1 + 20;
                KviskoComp1 = 1;
                if (bodoviIgrac >= bodoviZaKviska)
                    KviskoIgrac = 1;
                if (bodoviComp2 >= bodoviZaKviska)
                    KviskoComp2 = 1;
                //Comp1 je koristio kviska pa dobiva duple bodove
                if (Comp1PravoKviska == 1)
                {
                    bodoviComp1 = 2 * bodoviComp1;
                    Comp1PravoKviska = 0;
                }
                //zbroji ukupne bodove
                ukIgrac = ukIgrac + bodoviIgrac;
                ukComp1 = ukComp1 + bodoviComp1;
                ukComp2 = ukComp2 + bodoviComp2;
                // provjeri je li netko možda prije imao kviska pa ga nije iskoristio i to šalji u slijedeću igru
                int KI = max(KviskoIgrac, IgracPravoKviska);
                int KC1 = max(KviskoComp1, Comp1PravoKviska);
                int KC2 = max(KviskoComp2, Comp2PravoKviska);

                AutoClosingMessageBox.Show("Pobijedio je Comp1!\nKonačno rješenje bilo je : " + RjesenjeZaIgru +
                    "\nBodovi igraca: " + bodoviIgrac + " Kvisko: " + KviskoIgrac +
                    "\nBodovi Comp1: " + bodoviComp1 + " Kvisko: " + KviskoComp1 +
                    "\nBodovi Comp2: " + bodoviComp2 + " Kvisko:"  + KviskoComp2, "Pobjeda Comp1", 5000);
                gotovo = 1;
                Form3 igra3 = new Form3(ukIgrac,ukComp1,ukComp2,KI,KC1,KC2);
                igra3.Show();
                this.Close();
            }
            else
            {
                AutoClosingMessageBox.Show("Comp1 nije odgovorio točno!", "Obavijest", 1000);
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void Comp2Konacno()
        {
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            Debug.WriteLine("finale: " + randomNumber);
            AutoClosingMessageBox.Show("Comp2 pogađa rješenje..", "Pobjeda", 1000);
            if (randomNumber < Global.Comp2WinsGame2)
            {
                bodoviComp2 = bodoviComp2 + 20;
                KviskoComp2 = 1;
                if (bodoviIgrac >= bodoviZaKviska)
                    KviskoIgrac = 1;
                if (bodoviComp1 >= bodoviZaKviska)
                    KviskoComp1 = 1;

                //zbroji ukupne bodove
                ukIgrac = ukIgrac + bodoviIgrac;
                ukComp1 = ukComp1 + bodoviComp1;
                ukComp2 = ukComp2 + bodoviComp2;
                // provjeri je li netko možda prije imao kviska pa ga nije iskoristio i to šalji u slijedeću igru
                int KI = max(KviskoIgrac, IgracPravoKviska);
                int KC1 = max(KviskoComp1, Comp1PravoKviska);
                int KC2 = max(KviskoComp2, Comp2PravoKviska);

                AutoClosingMessageBox.Show("Pobijedio je Comp2!\nKonačno rješenje bilo je : " + RjesenjeZaIgru +
                    "\nBodovi igraca: " + bodoviIgrac + " Kvisko: " + KviskoIgrac +
                    "\nBodovi Comp1: " + bodoviComp1 + " Kvisko: " + KviskoComp1 +
                    "\nBodovi Comp2: " + bodoviComp2 + " Kvisko:" + KviskoComp2, "Pobjeda Comp2", 5000);
                gotovo = 1;
                Form3 igra3 = new Form3(ukIgrac,ukComp1,ukComp2,KI,KC1,KC2);
                igra3.Show();
                this.Close();
            }
            else
            {
                AutoClosingMessageBox.Show("Comp2 nije odgovorio tocno!", "Obavijest", 1000);
                System.Threading.Thread.Sleep(1000);
            }
        }
        public int izaberiBroj()
        {
            int broj = neotvoreni[0];
            String text = provjeri_tablicu(broj);
            panel1.Controls["button" + broj.ToString()].Text = text;
            neotvoreni.Remove(broj);
            panel1.Enabled = false;
            panel1.Controls["button" + broj.ToString()].Enabled = false;
            //printaj_neotvorene();
            int oznaka = broj % 4;
            if (oznaka == 0)
                oznaka = 20;
            else
                oznaka = oznaka + 16;
            // vraca oznaku rjesenje u kojoj je izabrani broj 
            return oznaka;
        }
        public void simulirajComp1()
        {
            AutoClosingMessageBox.Show("Na potezu je Comp1", "Caption", 1000);
            int stupac, randomNumber;
            // disable-aj buttone za pogađanje
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;
            // pricekaj da comp1 otvori pojam
            System.Threading.Thread.Sleep(2000);

            if (stupciZaPogadjanje.Count != 0)
            {
                if (neotvoreni.Count != 0)   //nisu pogodjeni svi stupci niti otvorena sva polja
                {
                    stupac = izaberiBroj();
                    System.Threading.Thread.Sleep(2000);
                    Random random = new Random();
                    randomNumber = random.Next(0, 2);
                    if(randomNumber == 0)
                    {
                        Debug.WriteLine("tu je greska" + stupac);
                        Comp1Pogodio(stupac);
                        if(neotvoreni.Count != 0)
                        {
                            stupac = izaberiBroj();
                        }
                        if(stupciZaPogadjanje.Count != 0)
                            AutoClosingMessageBox.Show("Comp1 nije pogodio rješenje stupca!", "Caption", 1000);
                    }
                    
                    else
                        AutoClosingMessageBox.Show("Comp1 nije pogodio rješenje stupca!", "Caption", 1000);
                }
                // nema slobodnih polja, pogađa stupac
                else
                {
                    Random random = new Random();
                    randomNumber = random.Next(0, 2);
                    if (randomNumber == 0)
                    {   //pogađa rješenje točno jednog stupca
                        Comp1PogodioStupac(stupciZaPogadjanje[0]);
                        if (stupciZaPogadjanje.Count == 0)
                        //ako nema stupaca za pogađanje pogađa konačno
                          Comp1Konacno(); 
                    }
                       
                    else
                        AutoClosingMessageBox.Show("Comp1 nije pogodio rješenje stupca!", "Caption", 1000);
                }
    
            }
                
            
            else
            // pogođeni stupci, pogađa se konačno rješenje
            Comp1Konacno();
            
        }

        public void simulirajComp2()
        {
            int stupac,randomNumber;
            AutoClosingMessageBox.Show("Na potezu je Comp2", "Caption", 1000);
            // disable-aj buttone za pogađanje
            button17.Enabled = false;
            button18.Enabled = false;
            button19.Enabled = false;
            button20.Enabled = false;
            button21.Enabled = false;
            // pricekaj da comp2 otvori pojam
            System.Threading.Thread.Sleep(2000);

            if (stupciZaPogadjanje.Count != 0)
            {
                if (neotvoreni.Count != 0)   //nisu pogodjeni svi stupci niti otvorena sva polja
                {
                    stupac = izaberiBroj();
                    Random random = new Random();
                    randomNumber = random.Next(0, 2);
                    if(randomNumber == 0)
                    {
                        Debug.WriteLine("tu je greska" + stupac);
                        Comp2Pogodio(stupac);
                        if (neotvoreni.Count != 0)
                        {
                            stupac = izaberiBroj();
                        }
                        if(stupciZaPogadjanje.Count != 0)
                            AutoClosingMessageBox.Show("Comp2 nije pogodio rješenje stupca!", "Caption", 1000);
                    }
                    
                    else
                        AutoClosingMessageBox.Show("Comp2 nije pogodio rješenje stupca!", "Caption", 1000);
                }
                // nema slobodnih polja, pogađa stupac
                else
                {
                    Random random = new Random();
                    randomNumber = random.Next(0, 2);
                    if (randomNumber == 0)
                    {   //pogađa rješenje točno jednog stupca
                        Comp2PogodioStupac(stupciZaPogadjanje[0]);
                        if (stupciZaPogadjanje.Count == 0)
                        //ako nema stupaca za pogađanje pogađa konačno
                            Comp2Konacno();
                    }

                    else
                        AutoClosingMessageBox.Show("Comp2 nije pogodio rješenje stupca!", "Caption", 1000);
                }

            }
            else
                // pogođeni stupci, pogađa se konačno rješenje
                Comp2Konacno();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            id_buttona = 1;
            String text = provjeri_tablicu(id_buttona);
            button1.Text = text;
            button1.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            id_buttona = 5;
            String text = provjeri_tablicu(id_buttona);
            button5.Text = text;
            button5.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            id_buttona = 9;
            String text = provjeri_tablicu(id_buttona);
            button9.Text = text;
            button9.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            id_buttona = 13;
            String text = provjeri_tablicu(id_buttona);
            button13.Text = text;
            button13.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            id_buttona = 2;
            String text = provjeri_tablicu(id_buttona);
            button2.Text = text;
            button2.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            id_buttona = 6;
            String text = provjeri_tablicu(id_buttona);
            button6.Text = text;
            button6.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            id_buttona = 10;
            String text = provjeri_tablicu(id_buttona);
            button10.Text = text;
            button10.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            id_buttona = 14;
            String text = provjeri_tablicu(id_buttona);
            button14.Text = text;
            button14.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            id_buttona = 3;
            String text = provjeri_tablicu(id_buttona);
            button3.Text = text;
            button3.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            id_buttona = 7;
            String text = provjeri_tablicu(id_buttona);
            button7.Text = text;
            button7.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            id_buttona = 11;
            String text = provjeri_tablicu(id_buttona);
            button11.Text = text;
            button11.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            id_buttona = 15;
            String text = provjeri_tablicu(id_buttona);
            button15.Text = text;
            button15.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            id_buttona = 4;
            String text = provjeri_tablicu(id_buttona);
            button4.Text = text;
            button4.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            id_buttona = 8;
            String text = provjeri_tablicu(id_buttona);
            button8.Text = text;
            button8.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            id_buttona = 12;
            String text = provjeri_tablicu(id_buttona);
            button12.Text = text;
            button12.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            id_buttona = 16;
            String text = provjeri_tablicu(id_buttona);
            button16.Text = text;
            button16.Enabled = false;
            panel1.Enabled = false;
            //makni button za kviska
            button22.Visible = false;
            neotvoreni.Remove(id_buttona);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            id_buttona = 17;
            //panel1.Enabled = true;
            //makni button za kviska
            button22.Visible = false;
            String input = Interaction.InputBox("Rješenje stupca A", "Molimo unesite rješenje:", "", 300, 200);
            Debug.WriteLine("input igraca: " + input);
            if (input.ToLower() == RjesenjaStupaca[0].ToLower())
            {
                igracPogodio(0, input);
            }
            else
            {
                panel1.Enabled = false;
                AutoClosingMessageBox.Show("Netočan odgovor! :(", "Caption", 1000);
                //System.Threading.Thread.Sleep(2000);
                if (gotovo == 0) simulirajComp1();
                if (gotovo == 0) simulirajComp2();
                if(gotovo == 0) AutoClosingMessageBox.Show("Vi ste na potezu :)", "Caption", 1000);
                if(gotovo == 0)
                {
                    //vrati da su buttoni za pogađanje enabled, igrac na potezu
                    foreach (int i in stupciZaPogadjanje)
                    {
                        this.Controls["button" + i.ToString()].Enabled = true;
                    }
                    this.Controls["button21"].Enabled = true;
                    panel1.Enabled = true;
                }
               
            }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            id_buttona = 18;
            // panel1.Enabled = true;
            //makni button za kviska
            button22.Visible = false;
            String input = Interaction.InputBox("Rješenje stupca B", "Molimo unesite rješenje:", "", 300, 200);
            Debug.WriteLine("input igraca: " + input);
            if (input.ToLower() == RjesenjaStupaca[1].ToLower())
            {
                igracPogodio(1, input);
            }
            else
            {
                panel1.Enabled = false;
                AutoClosingMessageBox.Show("Netočan odgovor! :(", "Caption", 1000);
                if (gotovo == 0) simulirajComp1();
                if (gotovo == 0) simulirajComp2();
                if (gotovo == 0) AutoClosingMessageBox.Show("Vi ste na potezu :)", "Caption", 1000);
                if (gotovo == 0)
                {
                    //vrati da su buttoni za pogađanje enabled, igrac na potezu
                    foreach (int i in stupciZaPogadjanje)
                    {
                        this.Controls["button" + i.ToString()].Enabled = true;
                    }
                    this.Controls["button21"].Enabled = true;
                    panel1.Enabled = true;
                }
            }

        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            id_buttona = 19;
            //panel1.Enabled = true;
            //makni button za kviska
            button22.Visible = false;
            String input = Interaction.InputBox("Rješenje stupca C", "Molimo unesite rješenje:", "", 300, 200);
            Debug.WriteLine("input igraca: " + input);
            if (input.ToLower() == RjesenjaStupaca[2].ToLower())
            {
                igracPogodio(2, input);
            }
            else
            {
                panel1.Enabled = false;
                AutoClosingMessageBox.Show("Netočan odgovor! :(", "Caption", 1000);
                // System.Threading.Thread.Sleep(2000);
                if (gotovo == 0) simulirajComp1();
                if (gotovo == 0) simulirajComp2();
                if (gotovo == 0) AutoClosingMessageBox.Show("Vi ste na potezu :)", "Caption", 1000);
                if (gotovo == 0)
                {
                    //vrati da su buttoni za pogađanje enabled, igrac na potezu
                    foreach (int i in stupciZaPogadjanje)
                    {
                        this.Controls["button" + i.ToString()].Enabled = true;
                    }
                    this.Controls["button21"].Enabled = true;
                    panel1.Enabled = true;
                }
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            id_buttona = 20;
            // panel1.Enabled = true;
            //makni button za kviska
            button22.Visible = false;

            String input = Interaction.InputBox("Rješenje stupca D", "Molimo unesite rješenje:", "", 300, 200);
            Debug.WriteLine("input igraca: " + input);
            if (input.ToLower() == RjesenjaStupaca[3].ToLower())
            {
                igracPogodio(3, input);
            }
            else
            {
                panel1.Enabled = false;
                AutoClosingMessageBox.Show("Netočan odgovor! :(", "Caption", 1000);
                // System.Threading.Thread.Sleep(2000);
                if (gotovo == 0) simulirajComp1();
                if (gotovo == 0) simulirajComp2();
                if (gotovo == 0) AutoClosingMessageBox.Show("Vi ste na potezu :)", "Caption", 1000);
                if (gotovo == 0)
                {
                    //vrati da su buttoni za pogađanje enabled, igrac na potezu
                    foreach (int i in stupciZaPogadjanje)
                    {
                        this.Controls["button" + i.ToString()].Enabled = true;
                    }
                    this.Controls["button21"].Enabled = true;
                    panel1.Enabled = true;
                }

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            id_buttona = 21;
            //panel1.Enabled = true;
            //makni button za kviska
            button22.Visible = false;

            String input = Interaction.InputBox("Konačno rješenje", "Molimo unesite rješenje:", "", 300, 200);
            Debug.WriteLine("input igraca: " + input);
            if (input.ToLower() == RjesenjeZaIgru.ToLower())
            {
                button21.Enabled = false;
                button21.Text = RjesenjeZaIgru;
                bodoviIgrac = bodoviIgrac + 20;
                KviskoIgrac = 1;
                if (bodoviComp1 >= bodoviZaKviska)
                    KviskoComp1 = 1;
                if (bodoviComp2 >= bodoviZaKviska)
                    KviskoComp2 = 1;
                //ako je igrac iskoristio kviska udvostruči bodove
                if(koristiKviska == 1)
                {
                    bodoviIgrac = 2 * bodoviIgrac;
                }
                //zbroji ukupne bodove
                ukIgrac = ukIgrac + bodoviIgrac;
                ukComp1 = ukComp1 + bodoviComp1;
                ukComp2 = ukComp2 + bodoviComp2;
                // provjeri je li netko možda prije imao kviska pa ga nije iskoristio i to šalji u slijedeću igru
                int KI = max(KviskoIgrac, IgracPravoKviska);
                int KC1 = max(KviskoComp1, Comp1PravoKviska);
                int KC2 = max(KviskoComp2, Comp2PravoKviska);

                AutoClosingMessageBox.Show("Čestitamo, pobijedili ste! :D"  +
                    "\nBodovi igraca: " + bodoviIgrac + " Kvisko: " + KviskoIgrac +
                    "\nBodovi Comp1: " + bodoviComp1 + " Kvisko: " + KviskoComp1 +
                    "\nBodovi Comp2: " + bodoviComp2 + " Kvisko:" + KviskoComp2, "Pobjeda", 5000);
                gotovo = 1;
                Form3 igra3 = new Form3(ukIgrac,ukComp1,ukComp2,KI,KC1,KC2);
                igra3.Show();
                this.Close();
            }
            else
            {
                AutoClosingMessageBox.Show("Netočan odgovor! :(", "Caption", 1000);
                System.Threading.Thread.Sleep(2000);
                // System.Threading.Thread.Sleep(2000);
                if (gotovo == 0) simulirajComp1();
                if (gotovo == 0) simulirajComp2();
                if (gotovo == 0) AutoClosingMessageBox.Show("Pogađajte konačno rješenje! :)", "Caption", 1000);
                if (gotovo == 0)
                {
                    this.Controls["button21"].Enabled = true;
                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            koristiKviska = 1;
            IgracPravoKviska = 0;
            AutoClosingMessageBox.Show("Izabrali ste korištenje Kviska!", "Bravo", 1000);
            button22.Visible = false;
        }
    }
}
