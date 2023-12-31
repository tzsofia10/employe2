using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace empleyee
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.Készíts egy osztályt, amely tartalmazza a szükséges mezőket. Nem kötelező kidolgozni a property - ket.
            //2.Írd meg a konstruktort.

            var sr = new StreamReader(
                path: @"..\..\..\munkaV.txt",
                encoding: System.Text.Encoding.UTF8
                );

            while (!sr.EndOfStream)
            {
                var sor = new MunkaValalo(sr.ReadLine());
                munkasok.Add(sor);

            }
            

            //3.Készíts egy osztályon belüli virtuális metódust az adatok kiírására.
            foreach (var munkas in munkasok)
            {
                Console.WriteLine($"{munkas.Name}, {munkas.Age}, {munkas.Gender} város: {munkas.City}, {munkas.Department}, kereset {munkas.Salary} euro");
            }


            //4.Propertyk kidolgozása(Szorgalmi feladat)
            //Dolgozd ki a property-ket is, és használd őket az adatokhoz való korrekt hozzáférésre és módosításra.
            //5.Hibakezelés(Szorgalmi feladat)
            //Implementálj hibakezelést az alkalmazásban, például az adatok beolvasásakor vagy a fájlba írás során.

            //6.Az osztály segítségével hozz létre egy listát, amely objektumpéldányokat tartalmaz a forrásfájlból beolvasott adatokkal.
            //7.A virtuális metódus segítségével írd ki az összes adatot.

            //A következő feladatokat a program osztályban elhelyezett statikus metódusokkal oldd meg. (Aki szeret kísérletezni, teheti ezeket a metódusokat egy újabb osztályba.) Egyes feladatokat meg lehet oldani LINQ-val is, de ha belefér az időbe, kódoljátok le hagyományosan is.Ha van olyan feladat, ami nem egyértelmű, pl.az, hogyan kell kiírni, ott rád van bízva a megoldás.

            //8.Függvény segítségével írd ki az életkorok átlagát.

            Console.WriteLine("8.feladat");
            int kor = 0;
            for (int i = 0; i < munkasok.Count; i++)
            {
                kor += munkasok[i].Age;
            }
            Console.WriteLine($"Életkorok átlaga {kor / munkasok.Count}");


            //9.Függvény segítségével írd ki azon személyek számát, akiknek a városa 'Budapest'.
            Console.WriteLine("9.feladat");
            string kulcsszo = "Budapest";
            List<MunkaValalo> talalt = munkatalalat(munkasok, kulcsszo);
            if (talalt.Count > 0)
            {
                Console.WriteLine($"Budapestiek száma: {talalt.Count}");
            }

            //10.Függvény segítségével keresd ki, majd a virtuális metódus segítségével írd ki a legidősebb személy adatait.
            Console.WriteLine("\n10.feladat");

            MunkaValalo legidosebb = null;                 
            int max = 0;
            foreach (var munkas in munkasok)
            {
                if (munkas.Age > max)
                {
                    max += munkas.Age;
                    legidosebb = munkas;
                }
            }

            if (legidosebb != null)
            {
                legidosebb.kiir();
            }
            else
            {
                Console.WriteLine("nincs adat");
            }
            

            //11.Függvény segítségével döntsd el, majd a főprogramban írd ki, hogy van-e 30 év fölötti személy, és emellett írd ki a nevét is. (Ez a függvény tehát két értéket kell, hogy generáljon, ezt egyetlen szövegként add vissza a főprogramnak, és a főprogram bontsa szét az adatokat, majd utána írja ki.)
            Console.WriteLine("11.feladat");
            Console.WriteLine("30 év fölötti személyek:");
            idos();

            //12.Függvénnyel válogasd ki azon személyek nevét egy új tömbbe(nem listába), akik 30 évnél fiatalabbak. Ennek a tömbnek a hasznos tartalmát írd ki a főprogramban.
            Console.WriteLine("\n12. feladat");
            Console.WriteLine("30 évnél fiatalabbak");
            fiatal();

            //13.Egyetlen függvénnyel keresd meg a legfiatalabb és a legidősebb személyt.A függvénynek legyen két olyan paramétere, amiben az eredményt vissza lehet juttatni a főprogramba, és ott ki lehet írni a nevüket és a korukat. A függvény visszatérési értéke pedig képes legyen azt jelezni, hogy van-e több ugyanolyan korú legfiatalabb személy.
            

            //14.Készíts egy függvényt, ami átszámolja az euróban megadott havi fizetést éves fizetéssé, és az eredményt még váltsd át magyar forintba is.
            Console.WriteLine("\n14.feladat");
            EuroToHuf();

            //15.Készíts egy függvényt, amelynek visszatérési értéke egy objektumokat tartalmazó lista, amelyben szerepel az 5 millió forint éves fizetés feletti munkavállalók neve és az éves fizetésük forintban. (Az átszámításhoz használd az előző feladat függvényét.)  Az elkészült listát a főprogram írja ki egy új fájlba(a virtuális metódus segítségével).

            //16.Írj egy függvényt, aminek a paramétere az eredeti adatokat tartalmazó listának megfelelő típusú.Ennek segítségével számold ki az összes alkalmazott átlagfizetését.
            Console.WriteLine("16.feladat");
            int atlagFiz = atlagFizetes(munkasok);
            Console.WriteLine($"Átlag fizetés: {atlagFiz} euro");

            //17.Készíts a főprogramban egy olyan listát, amiben csak a developer beosztásúak találhatók, minden tulajdonságukkal.Hívd meg újra a főprogramból az előző függvényt, de most ez az új lista legyen a paramétere. A főprogram írja ki a developerek átlagfizetését.

            //18.Számold ki a férfi és női alkalmazottak átlagfizetését tetszőleges módszerrel.
            int atlagFizFemale = atlagFizetesFemale(munkasok);
            Console.WriteLine($"Átlag fizetés a nőknek: {atlagFizFemale} euro");
            
            int atlagFizMale = atlagFizetesMale(munkasok);
            Console.WriteLine($"Átlag fizetés a férfiaknak: {atlagFizMale} euro");

        }

        static List<MunkaValalo> munkasok = new List<MunkaValalo>();
        static List<MunkaValalo> munkatalalat(List<MunkaValalo> munkasok, string kulcs)
        {
            kulcs = kulcs.ToLower();
            List<MunkaValalo> talalatok = munkasok.Where(munka => munka.City.ToLower().Contains(kulcs)).ToList();
            return talalatok;
        }

        static void idos()
        {
            foreach (var munkas in munkasok)
            {
                if (munkas.Age > 30)
                {
                    Console.WriteLine($"{munkas.Name}, {munkas.Age} éves");
                }
            }
        }

        static void fiatal()
        {
            foreach (var munkas in munkasok)
            {
                if (munkas.Age < 30)
                {
                    Console.WriteLine($"{munkas.Name}, {munkas.Age} éves");
                }
            }
        }    

        static void EuroToHuf()
        {
            int valto = 388;
            foreach (var munkas in munkasok)
            {
                int evesFizetesEuro = munkas.Salary * 12;
                int evesFizetesHuf = evesFizetesEuro * valto;
                Console.WriteLine($"{munkas.Name}, éves fizetés: {evesFizetesHuf} HUF");
            }
        }

        static int atlagFizetes(List<MunkaValalo> munkasok)
        {
            int fizu = 0;
            foreach (var munkas in munkasok)
            {
                fizu += munkas.Salary;
            }
            int atlag = fizu / munkasok.Count;   
            return atlag;
        }
        static int atlagFizetesFemale(List<MunkaValalo> munkasok)
        {
            int fizu = 0;
            int female = 0;

            foreach (var munkas in munkasok)
            {
                if (munkas.Gender == false)
                {
                    fizu += munkas.Salary;
                    female++;
                }
            }

            int atlag = fizu / female;
            return atlag;
        }

        static int atlagFizetesMale(List<MunkaValalo> munkasok)
        {
            int fizu = 0;
            int male = 0;

            foreach (var munkas in munkasok)
            {
                if (munkas.Gender != false )
                {
                    fizu += munkas.Salary;
                    male++;
                }
            }

            int atlag = fizu / male;
            return atlag;
        }


    }

}
