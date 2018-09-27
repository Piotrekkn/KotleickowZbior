using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proxy
{
    interface PrzykladowyInterfejs
    {
        void PrzykladowaFunkcja();
    }
    class PrzykladowaKlasa : PrzykladowyInterfejs
    {
        public void PrzykladowaFunkcja()
        {
            Console.WriteLine("Do something");
        }
    }
        class Proxy : PrzykladowyInterfejs
    {
        private PrzykladowyInterfejs PrzykladowaKlasa;
        private string Haslo { get; set; }

        public Proxy(string Haslo)
        {
            this.Haslo = Haslo;
        }

        private void StworzPrzykladowaKlasa()
        {
            PrzykladowaKlasa = new PrzykladowaKlasa();
        }

        public void PrzykladowaFunkcja()
        {
            if (Haslo == "admin1")
            {
                StworzPrzykladowaKlasa();
                PrzykladowaKlasa.PrzykladowaFunkcja();
                
            }
            else
            {
                Console.WriteLine("zle haslo");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string Haslo;
            Console.WriteLine("Podaj haslo (admin1) Wyjscie wpisz (exit):");
            do {
                Haslo = Console.ReadLine();
                Proxy proxy = new Proxy(Haslo);
                proxy.PrzykladowaFunkcja();
            }
            while (Haslo!="exit");
            Console.ReadKey();
        }
    }

}