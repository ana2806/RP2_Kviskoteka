using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kviskoteka
{
    class Datoteka
    {
        String adresa;
        public Datoteka(string adresa)
        {
            this.adresa = adresa;
        }

        //NE DIRATI NIKAKO!!!!!!!!!!!!! DOBRO JE!!!!!!
        //ipak sam dirala...(samo sam promjenila ime funkcije)
        //citanje datoteke za ABC pitalicu pretpostavlja da je redak datoteke u formatu
        //pitanje,prviOdgovor,T/N,drugiOdgovor,T/N,treciOdgovor,T/N
        //gdje T oznacava da je prethodno pitanje tocno, a N da je netocno
        public List<Tuple<String, List<Tuple<String, String>>>> readABCpitalica()
        {
            int counter = 0;
            string line;

            List<Tuple<String,List<Tuple<String,String > > > > pitanjeOdgovori=new List<Tuple<string, List<Tuple<string, string>>>>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@adresa);
            while ((line = file.ReadLine()) != null)
            {
                
                string[] splitLine = line.Split(',');
                for (int i = 0; i < splitLine.Length; i++)
                {
                    splitLine[i] = splitLine[i].Trim();
                }

                //Tuple of answer-correct/incorrect 
                var a = new Tuple<String, String>(splitLine[1], splitLine[2]);
                var b = new Tuple<String, String>(splitLine[3], splitLine[4]);
                var c = new Tuple<String, String>(splitLine[5], splitLine[6]);

                //List of answers
                List<Tuple<String, String>> odgovori = new List<Tuple<String, String>> { a, b, c};
                //Tuple of question/answers
                Tuple<String, List<Tuple<String, String>>> qa = new Tuple<string, List<Tuple<string, string>>>(splitLine[0], odgovori);
                //Insert to list
                pitanjeOdgovori.Add(qa);

                counter++;

            }
            file.Close();
            System.Console.WriteLine("success", counter);
            // Suspend the screen.  
            //System.Console.ReadLine();
            return (pitanjeOdgovori);
        }

        //uređena petorka, gdje je prvi item konacno rjesenje, a ostali lista koja sadrzi
        //pojmove iz pojedinih stupaca skupa s rjesenjem stupca
        public List<Tuple<String, List<String>, List<String>, List<String>, List<String>>> readAsocijacije()
        {
            int counter = 0;
            string line,konRj = " ";
            List<String> A = new List<string>();
            List<String> B = new List<string>();
            List<String> C = new List<string>();
            List<String> D = new List<string>();
            List<Tuple<String, List<String>, List<String>, List<String>, List<String>>> listaAsocijacije = new List<Tuple<String, List<String>, List<String>, List<String>, List<String>>>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@adresa);
            while ((line = file.ReadLine()) != null)
            {
                // ako je redak djeljiv s 5, onda je to konacan pojam
                if (counter % 5 == 0)
                {

                    konRj = line;
                    //OVO NIJE DOBRO!!! NE FUNKCIONIRA KAKO TREBA (POINTERI?)
                  /*  A.Clear();
                    B.Clear();
                    C.Clear();
                    D.Clear(); */
                }
                else if(counter % 5 == 1)
                {   
                    //create new column
                    List<String> Stupac1 = new List<string>();
                    string[] splitLine = line.Split(',');
                    for (int i = 0; i < splitLine.Length; i++)
                    {
                        splitLine[i] = splitLine[i].Trim();
                        //add terms of column to first list
                        Stupac1.Add(splitLine[i]);
                    }
                    //copy column to previously declared column
                    A = Stupac1;
                }
                else if (counter % 5 == 2)
                {
                    //create new column
                    List<String> Stupac2 = new List<string>();
                    string[] splitLine = line.Split(',');
                    for (int i = 0; i < splitLine.Length; i++)
                    {
                        splitLine[i] = splitLine[i].Trim();
                        //add terms of column to second list
                        Stupac2.Add(splitLine[i]);
                    }
                    //copy column to previously declared column
                    B = Stupac2;
                }
                else if (counter % 5 == 3)
                {
                    //create new column
                    List<String> Stupac3 = new List<string>();
                    string[] splitLine = line.Split(',');
                    for (int i = 0; i < splitLine.Length; i++)
                    {
                        splitLine[i] = splitLine[i].Trim();
                        //add terms of column to third list
                        Stupac3.Add(splitLine[i]);
                    }
                    //copy column to previously declared column
                    C = Stupac3;
                }
                else if (counter % 5 == 4)
                {
                    //create new column
                    List<String> Stupac4 = new List<string>();
                    string[] splitLine = line.Split(',');
                    for (int i = 0; i < splitLine.Length; i++)
                    {
                        splitLine[i] = splitLine[i].Trim();
                        //add terms of column to fourth list
                        Stupac4.Add(splitLine[i]);
                    }
                    //copy column to previously declared column
                    D = Stupac4;


                    //add solutions and terms to tuple
                    Tuple<String, List<String>, List<String>, List<String>, List<String>> dodaj =
                        new Tuple<String, List<String>, List<String>, List<String>, List<String>>(konRj, A, B, C, D);
                    //add solution to list
                    listaAsocijacije.Add(dodaj);

                    // after adding to main list, clear main solution
                    konRj = " ";
                    // NITI OVO NIJE DOBRO JER SE SVE POBRIŠE I U GORNJOJ LISTI ASOCIJACIJA 
                    /* 
                     A.Clear();
                     B.Clear();
                     C.Clear();
                     D.Clear();  */
                }


                counter++;

            }
            file.Close();
            System.Console.WriteLine("success", counter);
            // Suspend the screen.  
            //System.Console.ReadLine();
            return (listaAsocijacije);
        }


        //NIJE DOBRO-sutra cu,spava mi se...
        //citanje datoteke za pogadanje osoba pretpostavlja da je redak datoteke u formatu
        //pogadanaOsoba,pitanje,odgovorPraveOsobe,drugiOdgovor,treciOdgovor
        //za svaku pogadanu osobu imamo ciji element ce biti cetveromjesne Tuple-ove, kojima je 
        //na prvom mjestu pitanje, na drugom dobar odgovor, a na trecem i cetvrtom ostali odgovori
        //razlog: svakoj osobi se dodjeljuje jedan broj 1-3 i odgovori te osobe su uvijek na toj poziciji u Tupleu
        //glavna struktura je dictionary gdje su kljucevi pogadane osobe, a Tuple-ovi vrijednosti
        public Dictionary<String, List<List<String>>> readOsobe()
        {
            int counter = 0;
            string line;
            
            List<string[]> pomocnaLista =new List<string[]>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@adresa);
            while ((line = file.ReadLine()) != null)
            {

                string[] splitLine = line.Split(',');
                for (int i = 0; i < splitLine.Length; i++)
                {
                    splitLine[i] = splitLine[i].Trim();
                }
                pomocnaLista.Add(splitLine);

                counter++;

            }

            //sad imamo sve retke u listi, idemo ih spremit u Dict
            String imeOsobe=pomocnaLista[0][0];
            Dictionary<String,List<List<String>>> konacna=new Dictionary<String,List<List<String>>>();
            List<List<String>> osobaPitanja=new List<List<string>>();
            List<String> qa = new List<string>();
            foreach(var redak in  pomocnaLista)
            {
                if (redak[0]==imeOsobe){
                    qa.Clear();
                    for(int i=1;i<5;i++){
                        qa.Add(redak[i]);
                    }
                    osobaPitanja.Add(new List<String> (qa));
                }
                else{
                    konacna[imeOsobe]=new List<List<String>>(osobaPitanja);
                    osobaPitanja.Clear();
                    imeOsobe=redak[0];
                    qa.Clear();
                    for(int i=1;i<5;i++){
                        qa.Add(redak[i]);
                    }
                    osobaPitanja.Add(new List<String>(qa));
                }
            }
            konacna[imeOsobe] = new List<List<String>>(osobaPitanja);
            file.Close();
            System.Console.WriteLine("success", counter);
            // Suspend the screen.  
            //System.Console.ReadLine();
            return (konacna);
        }
    }
}
