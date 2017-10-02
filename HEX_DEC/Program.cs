using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace HEX_DEC
{
    class Program
    {
        static void Main(string[] args)
        {
            //DEFINIZIONI VARIABILI
            char ch;
            int Tchar = 0;
            int contatore = 0;
            StreamWriter scrivi;
            StreamReader leggi;
     
            string[] contenitore = new string[8];
            string risultato = "";

            string percorsocartelle = @"C:\TLC\HEX\Files\";
            string PercorsoHEX = percorsocartelle + "HEX.txt";
            string PercorsoDEC = percorsocartelle + "DEC.txt";

            // CREAZIONE CARTELLA E FILES
            try
            {
                // VERIFICA SE LA CARTELLA ESISTE E SE SONO STATI CREATI I FILES NECESSARI
                if (Directory.Exists(percorsocartelle))
                {
                    /* CODICE DI VERIFICA VISIVA
                    
                    Console.WriteLine("La directory è già presente");
                    
                     */

                    //SE IL FILE NON ESISTE LO CREA E CHIUDE LA CONNESSIONE
                    if (!File.Exists(PercorsoHEX)) {
                        FileStream fps = File.Create(PercorsoHEX);
                        fps.Close();
                    }
                    if (!File.Exists(PercorsoDEC))
                    {
                        FileStream fps2 = File.Create(PercorsoDEC);
                        fps2.Close();
                    }

                  //  Console.ReadLine();
                }
                else
                {
                    // CREA LA CARTELLA IN QUANTO E' RISULTATO CHE NON ESISTEVA PROPRIO E QUINDI ANCHE I SUOI FILES
                    DirectoryInfo di = Directory.CreateDirectory(percorsocartelle);
               //     Console.WriteLine("Directory creata.");
               //     Console.ReadLine();
                    FileStream fps = File.Create(PercorsoHEX);
                    fps.Close();
                    FileStream fps2 = File.Create(PercorsoDEC);
                    fps2.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERRORE: {0}", e.ToString());
            }
            finally {}

            leggi = new StreamReader(@"C:\TLC\HEX\Files\HEX.txt");
            scrivi = new StreamWriter(@"C:\TLC\HEX\Files\DEC.txt");
            //CARICAMENTO DELLE DIMENSIONI DEL FILE NECESSARIO ALLA CONVERSIONE
            FileInfo f = new FileInfo(PercorsoHEX);
            //CONTROLLO  CHE IL FILE NON SIA VUOTO
            if (f.Length != 0)
            {
                //CICLO PER LA LETTURA DA INIZIO A FINE FILE
                do
                {
                    //LETTURA CARATTERE PER CARATTERE
                    ch = (char)leggi.Read();
                    //POSIZIONAMENTO DEL CARATTERE IN UN ARRAY DI STRINGHE
                    contenitore[Tchar] = ch.ToString();

                    /*---------------------------------------------------- 
                       ELEMENTI DI VERIFICA VISIVA    
                       Console.Write("PROVA:  "+contenitore[Tchar]);
                    -----------------------------------------------------*/
                    if (Tchar == 7)
                    {
                        risultato = "";
                        for (int i = 0; i < 8; i++)
                        {
                            risultato = risultato + contenitore[i];
                        }

                        //CONVERSIONE DEL NUMERO
                        long conversione = Int64.Parse(risultato, System.Globalization.NumberStyles.HexNumber);

                        /* VERIFICHE VISIVE
                         Console.WriteLine("CONTENITORE: " + risultato);
                         Console.WriteLine("RISULTATO: " + conversione.ToString());
                         */
                        //DECREMENTO NECESSARIO AD IMPEDIRE UN ERRORE DI CONTEGGIO CARATTERI
                        Tchar = -1;
                    }
                    //CONTROLLO PER MULTIPLI DI 8, SE MULTIPLO DI 8 ESCLUSO IL PRIMO, METTI LA , E VAI A CAPO

                    if (contatore % 8 == 0 && contatore > 0)
                    {
                        //INSERIMENTO DELLA VIRGOLA SOLO NEL FILE
                        scrivi.WriteLine(",");
                    }
                    //INCREMENTO CONTATORI
                    Tchar++;
                    contatore++;

                    //SCRITTURA A VIDEO PER VERIFICA                
                    //Console.Write(ch);

                    //SCRITTURA SUL FILE
                    scrivi.Write(ch);
                } while (!leggi.EndOfStream);

                //DA DECOMMENTARE SE SERVE LA VIRGOLA ALLA FINE COME ULTIMA RIGA
                //scrivi.Write(",");

                //CHIUSURA E RIMESSA DISPOSIZIONE DEI FILES
                leggi.Close();
                scrivi.Close();
                leggi.Dispose();
                scrivi.Dispose();

                Console.WriteLine(" CONVERSIONE COMPLETATA ");
                //APERTURA DEL FILE RISULTATO
                Process.Start(@"C:\TLC\HEX\Files\DEC.txt");
            }
            else
            {
                Console.WriteLine("PRIMO AVVIO DEL PROGRAMMA, File Vuoto, copiare ed incollare il condice esadecimale perchè questo sia utile");
            }
                   

       //ATTESA INPUT DA TASTIERA PER TERMINARE IL PROGRAMMA
            Console.ReadLine();
        }
    }
}
