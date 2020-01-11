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
        public ClassThatHoldsStuff()
        {

        }

        public int Intvalue { get => intvalue; set => intvalue = value; }
        public string Stringvalue { get => stringvalue; set => stringvalue = value; }
        public double Doublevalue { get => doublevalue; set => doublevalue = value; }

        public override string ToString()
        {
            return "IntValue is: " + intvalue + "\nString Value is: " + stringvalue + "\nDouble value is: " + doublevalue;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool successfulselection = false;
            int selection = 0;
            bool runagain = true;
            char mrRunAgainChar = 'Y';
            while (runagain)
            {
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
                successfulselection = false;
                switch (selection)
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
                Console.WriteLine("Do you want to run this program again? (Y/N) ");
                while (!successfulselection)
                {
                    try
                    {
                        mrRunAgainChar = char.ToUpper(char.Parse(Console.ReadLine()));
                        successfulselection = true;
                    }
                    catch(FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if(mrRunAgainChar != 'Y')
                {
                    runagain = false;
                }
                successfulselection = false;
            }
        }

        static void SerializeMeCaptain()
        {
            string stringtoserialize = "";
            int choice = 0;
            bool successfulselection = false;
            string mrserializer = "";
            bool nothingtoserialize = false;
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
                    nothingtoserialize = true;
                    break;
            }
            while (!nothingtoserialize)
            {
                string filename = "";
                Console.WriteLine("Enter file name to serialize to: ");
                filename = Console.ReadLine();
                File.WriteAllText(filename, mrserializer);
                nothingtoserialize = true;
            }

        }

        static void DeSerializeMeCaptain()
        {
            string mrjsonstring = "";
            string filename = "";
            bool fileExists = true;
            bool foundFiles = false;
            Console.WriteLine("Here is the list of *.json files in the current directory: ");
            DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = directoryInfo.GetFiles("*.json");
            foreach (FileInfo file in files)
            {
                Console.WriteLine(file);
                foundFiles = true;
            }
            if(!foundFiles)
            {
                Console.WriteLine("Did not find any *.json files.");
                fileExists = false;
            }
            while (foundFiles)
            {
                Console.WriteLine("Enter a filename to deserialize from: ");
                try
                {
                    filename = Console.ReadLine();
                    mrjsonstring = File.ReadAllText(filename);
                    foundFiles = false;
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                    fileExists = false;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    fileExists = false;
                }
            }
            while (fileExists)
            {
                Console.WriteLine($"File string is outputting as: {mrjsonstring}");
                bool validselection = false;
                int choice = 0;
                while (!validselection)
                {
                    try
                    {
                        Console.WriteLine("Make a determination of value to read this into: int (1), string (2), double (3), ClassThatHoldsStuff (4)");
                        string selection = Console.ReadLine();
                        choice = Int32.Parse(selection);
                        validselection = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.WriteLine("The serialized data is as follows: ");
                switch (choice)
                {
                    case 1:
                        int intserializeddata = JsonSerializer.Deserialize<int>(mrjsonstring);
                        Console.WriteLine(intserializeddata);
                        break;
                    case 2:
                        string stringserializeddata = JsonSerializer.Deserialize<string>(mrjsonstring);
                        Console.WriteLine(stringserializeddata);
                        break;
                    case 3:
                        double doubleserializeddata = JsonSerializer.Deserialize<double>(mrjsonstring);
                        Console.WriteLine(doubleserializeddata);
                        break;
                    case 4:
                        ClassThatHoldsStuff ClassThatHoldsStuffserializeddata = JsonSerializer.Deserialize<ClassThatHoldsStuff>(mrjsonstring);
                        Console.WriteLine(ClassThatHoldsStuffserializeddata);
                        break;
                    default:
                        Console.WriteLine("I was not given a valid choice and will now terminate.");
                        break;
                }
                fileExists = false;
            }
        }
    }
}
