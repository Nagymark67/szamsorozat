namespace szamsorozat
{
    internal class Program
    {
        static List<szamok> Szamsorozat = new();
        static void Main(string[] args)
        {
            Feladat01();
            Feladat02();
            Feladat03();
            Feladat04_05();
            Feladat06();
            Feladat07();
        }

        private static void Feladat01()
        {

            using StreamReader sr = new(path: @"..\..\..\src\sorozat.txt");
            while (!sr.EndOfStream)
            {
                int szam = int.Parse(sr.ReadLine());
                szamok szamoks = new();                
                szamoks.szam = szam;
                Szamsorozat.Add(szamoks);
            }

        }

        private static void Feladat02()
        {
            Console.WriteLine("2. feladat: Elemek száma a sorozatban: {0}db", Szamsorozat.Count);
        }

        private static void Feladat03()
        {
            List<int> paratlanok = new();
            for (int i = 0; i < Szamsorozat.Count; i++)
            {
                if (Szamsorozat[i].szam % 2 != 0)
                {
                    paratlanok.Add(Szamsorozat[i].szam);
                }
            }
            int Osszeg = paratlanok.Sum();
            int Darabszam = paratlanok.Count();
            double Atlag = paratlanok.Average();
            Console.WriteLine("3. feladat: Páratlan számok:");
            Console.WriteLine("\tÖsszege: {0}", Osszeg);
            Console.WriteLine("\tDarabszáma: {0}", Darabszam);
            Console.WriteLine("\tÁtlaga: {0}", Atlag);
        }

        private static void Feladat04_05()
        {
            Console.WriteLine("5. feladat:");
            Console.Write("\tKérem az állomány nevét: ");
            string allomanynev = Console.ReadLine();
            Console.Write("\tKérem a kezdőindexet: ");
            int kezdoindex = int.Parse(Console.ReadLine());
            Console.Write("\tKérem a részsorozat hosszát: ");
            int reszhossz = int.Parse(Console.ReadLine());
            reszsorozatKiir(allomanynev, Szamsorozat, kezdoindex, reszhossz);
        }

        private static void Feladat06()
        {
            
            KeyValuePair<int, int> leghosszabb = leghosszabbKiir();
            Console.WriteLine("6. feladat: Első leghosszabb szigorúan monoton növekvő sorozat:\n\tHossza: {0}\n\tIndexe: {1}", leghosszabb.Value, leghosszabb.Key);
        }

        private static void Feladat07()
        {
            
            KeyValuePair<int, int> leghosszabb = leghosszabbKiir();
            using StreamWriter sw = new(path: @"..\..\..\bin\Debug\leghosszabb.txt");
            for (int i = leghosszabb.Key; i < leghosszabb.Key+leghosszabb.Value+1; i++)
            {
                sw.WriteLine(Szamsorozat[i].szam);
            }
        }

        private static void reszsorozatKiir(string filenev, List<szamok> adat, int kezdoIndex, int hossz)
        {
            using StreamWriter sw = new StreamWriter(path: @"..\..\..\bin\Debug\" + filenev);
            int j = kezdoIndex;
            for (int i = 0; i < hossz; i++)
            {
                sw.WriteLine(adat[j].szam);
                j++;
            }
        }

        private static KeyValuePair<int, int> leghosszabbKiir()
        {
            Dictionary<int, int> Darabok = new Dictionary<int, int>();
            int kezdo = 0;
            int db = 1;
            Darabok.Add(kezdo, db);
            for (int i = 1; i < Szamsorozat.Count; i++)
            {
                if (Szamsorozat[i - 1].szam < Szamsorozat[i].szam)
                {
                    if (!Darabok.Keys.Contains(i))
                    {
                        if (db == 0)
                        {
                            kezdo = i;
                        }
                        db++;
                    }
                }
                else
                {
                    if (!Darabok.Keys.Contains(kezdo))
                    {
                        Darabok.Add(kezdo - 1, db);
                    }
                    db = 0;
                    kezdo = i;
                }

            }
            return Darabok.MaxBy(e => e.Value);
        }
    }
}