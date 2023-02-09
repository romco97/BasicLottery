using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace lotto
{
    internal class Program
    {
        static List<Lotto> lData = new List<Lotto>();
        class Lotto
        {
            private string Year;
            private string Week;
            private string Date;
            private int[] Numbers;

            public Lotto(string year, string week, string date, int[] numbers)
            {
                Year = year;
                Week = week;
                Date = date;
                Numbers = numbers;
            }
            public Lotto() { }
            protected void Load()
            {
                try
                {
                    StreamReader sr = new StreamReader("otos.csv");
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(';');
                        lData.Add(new Lotto(line[0], line[1], line[2], new int[] { int.Parse(line[11]),
                        int.Parse(line[12]), int.Parse(line[13]), int.Parse(line[14]), int.Parse(line[15]) }));
                    }
                    sr.Close();
                }
                catch (FileNotFoundException e) { Console.WriteLine(e.Message); }

                /*
                foreach (Lotto i in lData)
                {
                    Console.WriteLine("{0} {1} {2}", i.Year, i.Week, i.Date);
                    foreach(int x in i.Numbers)
                    {
                        Console.Write(x+" ");
                    }
                }
                */
            }
            protected void SearchByNumber()
            {
                Console.WriteLine("Kérlek add meg a keresett számot!");
                Console.Write(">>");
                int num = int.Parse(Console.ReadLine());
                int c = 0;
                foreach (Lotto i in lData)
                    foreach (int j in i.Numbers)
                        if (num.Equals(j))
                        {
                            Console.WriteLine("\nÉv: {0}\nHét: {1}", i.Year, i.Week);
                            foreach (int x in i.Numbers)
                            {
                                if (x.Equals(num)) { Console.ForegroundColor = ConsoleColor.Green; c++; }
                                else Console.ResetColor();
                                Console.Write(x + " ");
                            }
                            Console.WriteLine("\n");
                        }
                Console.Write("Előfordulás: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(c);
                Console.ResetColor();
            }


            protected void SearchByYear()
            {
                Console.WriteLine("Kérlek add meg a keresett évszámot!");
                Console.Write(">>");
                int year = int.Parse(Console.ReadLine());

                foreach (Lotto i in lData)
                    if (year.Equals(int.Parse(i.Year)))
                    {
                        Console.WriteLine("\nHét: {0}\nSzámok: ", i.Week);
                        foreach (int x in i.Numbers)
                            Console.Write(x + " ");

                        Console.WriteLine();
                    }
            }

            protected void VirtualLottery()
            {
                int[] playerNumbers = new int[5];
                Console.WriteLine("Kérlek adj meg 5 db számot:\n");
                for (int i = 0; i < playerNumbers.Length; i++)
                {
                    Console.Write("Szám: ");
                    playerNumbers[i] = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                Console.WriteLine("Évszám\tHét\tSzámok\tTalálatok");

                int c = 0;
                foreach (Lotto i in lData)
                {
                    c = 0;
                    for (int j = 0; j < playerNumbers.Length; j++)
                    {
                        if (i.Numbers.Contains(playerNumbers[j])) c++;
                    }

                    if (c > 0)
                    {
                        Console.Write("{0} - {1} : ",i.Year,i.Week);
                        foreach (int x in i.Numbers)
                        {
                            if (playerNumbers.Contains(x))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("{0} ", x);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write("{0} ", x);
                            }
                            
                        }
                        Console.WriteLine(" :: {0}",c);
                        Console.WriteLine();
                    }
                }
            }


        }
        class Menu : Lotto
            {
                public Menu()
                {
                    Load();
                }

                public void MainMenu()
                {
                    Console.WriteLine("MENÜ\n");
                    Console.WriteLine("1 -> Szám keresés");
                    Console.WriteLine("2 -> Keresés évszám alapján");
                    Console.WriteLine("3 -> Virtuális Lottó");
                    Console.WriteLine("0 -> Kilépés");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(">>");
                    Console.ResetColor();
                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        default: Console.WriteLine("Nem létező opció!"); Thread.Sleep(2000); MainMenu(); break;
                        case 0: Environment.Exit(0); break;
                        case 1: Console.Clear(); SearchByNumber(); BackToMenu(); break;
                        case 2: Console.Clear(); SearchByYear(); BackToMenu(); break;
                        case 3: Console.Clear(); VirtualLottery(); BackToMenu(); break;
                }
                }

                private void BackToMenu()
                {
                    Console.Write("\nVissza...");
                    Console.ReadLine();
                    Console.Clear();
                    MainMenu();
                }
            }


            static void Main(string[] args)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lottó számok 1957-től 2022-ig");
                Console.ResetColor();
                Menu m = new Menu();
                m.MainMenu();


                Console.ReadKey();
            }
    }
}
