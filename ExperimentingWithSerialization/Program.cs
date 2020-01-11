using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ExperimentingWithSerialization
{
    class ClassThatHoldsStuff
    {
        int intvalue = 0;
        string stringvalue = "";
        double doublevalue = 0.0;

        public ClassThatHoldsStuff(int inputtedint, string inputtedstring, double inputteddouble)
        {
            intvalue = inputtedint;
            stringvalue = inputtedstring;
            doublevalue = inputteddouble;
        }

        public int Intvalue { get => intvalue; set => intvalue = value; }
        public string Stringvalue { get => stringvalue; set => stringvalue = value; }
        public double Doublevalue { get => doublevalue; set => doublevalue = value; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool successfulselection = false;
            int selection = 0;
            while (!successfulselection)
            {
                try
                {
                    Console.WriteLine("Do you want to save a variable into memory or load a variable from memory? (1 or 2): ");
                    string beforeparse = Console.ReadLine();
                    selection = Int32.Parse(beforeparse);
                    successfulselection = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            switch(selection)
            {
                case 1:
                    SerializeMeCaptain();
                    break;
                case 2:
                    DeSerializeMeCaptain();
                    break;
                default:
                    Console.WriteLine("You failed to make a valid selection. The program will now terminate.");
                    break;
            }
        }

        static void SerializeMeCaptain()
        {
            string stringtoserialize = "";
            int choice = 0;
            bool successfulselection = false;
            string mrserializer = "";
            while (!successfulselection)
            {
                try
                {
                    Console.WriteLine("What do you want to serialize? int (1), string (2), double (3), or ClassThatHoldsStuff (4): ");
                    string selectionstring = Console.ReadLine();
                    choice = Int32.Parse(selectionstring);
                    successfulselection = true;
                }
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            switch(choice)
            {
                case 1:
                    Console.WriteLine("Enter integer value to serialize: ");
                    Int32.TryParse(Console.ReadLine(), out int inttoserialize);
                    mrserializer = JsonSerializer.Serialize(inttoserialize);
                    break;
                case 2:
                    Console.WriteLine("Enter string that you want to serialize: ");
                    stringtoserialize = Console.ReadLine();
                    mrserializer = JsonSerializer.Serialize(stringtoserialize);
                    break;
                case 3:
                    Console.WriteLine("Enter double value to serialize: ");
                    Double.TryParse(Console.ReadLine(), out double doubletoserialize);
                    mrserializer = JsonSerializer.Serialize(doubletoserialize);
                    break;
                case 4:
                    Console.WriteLine("Enter an int value to serialize: ");
                    Int32.TryParse(Console.ReadLine(), out int inttoserialize2);
                    stringtoserialize = Console.ReadLine();
                    Double.TryParse(Console.ReadLine(), out double doubletoserialize2);
                    ClassThatHoldsStuff mrclass = new ClassThatHoldsStuff(inttoserialize2, stringtoserialize, doubletoserialize2);
                    mrserializer = JsonSerializer.Serialize(mrclass);
                    break;
                default:
                    Console.WriteLine("Well, if you didn't want to serialize something, then why did you enter this function?");
                    break;
            }
            string filename = "" ;
            Console.WriteLine("Enter file name to serialize to: ");
            filename = Console.ReadLine();
            File.WriteAllText(filename, mrserializer);

        }

        static void DeSerializeMeCaptain()
        {
            Console.WriteLine("You are in the Deserialize function!");
        }
    }
}
