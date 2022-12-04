using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ConsoleApp11
{
    internal class Script
    {
        private static string Name;
        private long Time;
        private static double SpeedMin;
        private static double SpeedSec;
        public Stopwatch sw = new Stopwatch();

        public Script(string name)
        {
            Name = name;
            Thread textWriter = new Thread(new ThreadStart(Text));
            textWriter.Start();
            sw.Start();
        }

        private void Text()
        {
            int index = 0;
            int indexProection = index;
            int yCord = 0;
            string text = "Я прыгну со скалы и разобьюсь о волны. Лишь деревья и трава меня будут помнить. Прыгну со скалы и разобьюсь о волны. Навсегда оставшись маленькой частью моря..";
            Console.Write(text);
            char[] characters = text.ToCharArray();
            while (true)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Время: " + (60 - sw.ElapsedMilliseconds / 1000));
                if (sw.ElapsedMilliseconds == 60000)
                {
                    sw.Stop();
                    Time = 60000;
                    SpeedSec = index / (Time / 1000);
                    SpeedMin = SpeedSec / 60000;
                    break;
                }
                if (index % 120 == 0 && index != 0) { yCord++; indexProection = 0; }
                if (index == text.Length)
                {
                    sw.Stop();
                    Time = sw.ElapsedMilliseconds;
                    SpeedSec = index / (double)(sw.ElapsedMilliseconds / 10000);
                    SpeedMin = index * ((double)sw.ElapsedMilliseconds / 60000.0);
                    break;
                }
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.KeyChar == text[index])
                {
                    Console.SetCursorPosition(indexProection, yCord);
                    Console.ForegroundColor = ConsoleColor.Green;
                    characters[index] = key.KeyChar;
                    Console.Write(key.KeyChar);
                    Console.ResetColor();
                    index++;
                    indexProection++;
                }
                else if (key.KeyChar != text[index])
                {
                    Console.SetCursorPosition(indexProection, yCord);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(key.KeyChar);
                    Console.ResetColor();
                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    Console.SetCursorPosition(indexProection, yCord);
                    Console.Write(text[index]);
                }
            }

            Script.FileSerialize();
        }
        public static void FileSerialize()
        {
            string jsonRead = "";
            if (File.Exists("D:\\file\\json.json"))
            {
                jsonRead = File.ReadAllText("D:\\file\\json.json");
            }
            List<cringe> cringes = JsonConvert.DeserializeObject<List<cringe>>(jsonRead) ?? new List<cringe>(); //Не ну тут мы реал кринжа навалили чето полного

            cringes.Add(new cringe(Name, SpeedMin, SpeedSec));//омега кринж кринж.адд нью кринж

            string json = JsonConvert.SerializeObject(cringes);
            File.WriteAllText("D:\\file\\json.json", json);

            Console.Clear();
            Console.WriteLine("Хотите ли продолжить? (y/n)");
            string answ = Console.ReadLine();
            if (answ == "y")
            {
                Console.Clear();
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                Console.Clear();
                Script newScript = new Script(name);
            }
            else if(answ == "n")
            {
                Console.Clear();
                Console.WriteLine("Ну ладно.. :(  ");
            }

        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Clear();
            Script newScript = new Script(name);
        }
    }
}
