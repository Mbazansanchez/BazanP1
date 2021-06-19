using System;
using System.IO;

namespace BazanP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            double mass;
            string sMas;
            double pressureP; //pressure
            double volume; // Voulume in cubic meters
            string sVolume;
            //const double R = 8.3145 is a constant.
            double tempC; //temp in Cesius 
            string sTempC;
            string[] Gnames = new string[85];
            double[] Mweights = new double[85];
            string choice;
            int count = 0;
            double moleweight = 0;
            string response;

            do
            {

                GetMolecularWeights(ref Gnames, ref Mweights, out count);
                DisplayGasNames(Gnames, count);
                do
                {
                    Console.Out.WriteLine("\nPlease enter in a gas displayed above: ");
                    choice = Console.ReadLine();
                    moleweight = GetMolecularWeightFromName(choice, Gnames, Mweights, count);
                } while (moleweight == 0); // GLENN: (suggestion) Be a little careful here, comparing == with doubles is hazardous.
                // GLENN: Doubles can sometimes be slightly inaccurate, especially when division is involved.
                // GLENN: A more fail-safe way to do this would be to return either -1 on failure and check if < 0,
                // GLENN: or return NaN and check for NaN on failure.
                // GLENN: In this case, the code works, but just be aware of this for the future.

                Console.Out.WriteLine("Please enter the mass of the gas in grams: ");
                sMas = Console.ReadLine();
                mass = double.Parse(sMas);
                Console.Out.WriteLine("Please enter in the volume of gas in cubic meters: ");
                sVolume = Console.ReadLine();
                volume = double.Parse(sVolume);
                Console.Out.WriteLine("Please enter in the temperature in degrees Celsius  ");
                sTempC = Console.ReadLine();
                tempC = double.Parse(sTempC);
                pressureP = Pressure(mass, volume, tempC, moleweight);
                DisplayPressure(pressureP);

                Console.Out.WriteLine("Would you like to do another yes or now?");
                response = Console.ReadLine();
            } while (response == "yes"); // GLENN: Be careful, use String.Equals(response, "yes") instead.

            Console.Out.WriteLine("Thanks for using the program, GoodBye!");


        }

        static void GetMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {

            count = 0;
            gasNames = new string[85];
            molecularWeights = new double[85];
            //GetMolecularWeights(ref Gnames, ref Mweights, out count);

            // GLENN: (suggestion) If you change settings, you can avoid putting the full path in here.
            // 
            // You can make it so you can only put the file name (not full path) if you right
            // click on the csv in solution explorer, click properties. Then, in the properties, 
            // change "Copy to Output directory" to "Copy always". This is better than putting in your
            // full path because then people can download your project from git and use it without
            // changing the source code.
            string[] readText = File.ReadAllLines(@"MWGV.csv");

            // GLENN: (suggestion) You can allocate your array based on the number of lines
            //
            // One way to make this code cleanear would be to allocate your arrays after you read the lines.
            // You can do this:
            // gasNames = new string[readText.Length - 1];
            // molecularWeights = new double[readText.Length - 1];

            // GLENN: (suggestion) The spec is not clear on this, but the expectation is count will be 84.
            //
            // count = gasNames.Length - 1;

            for (count = 1; count < readText.Length; ++count)
            {

                string[] split_Element;
                split_Element = readText[count].Split(',');
                gasNames[count - 1] = split_Element[0];

                string temp = split_Element[1];
                molecularWeights[count - 1] = double.Parse(temp);

            }

        }

        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            string hello = "\t\t\t\tWelcome to the Ideal Gas Calculator.";

            Console.Out.WriteLine(hello);

            for (int i = 0; i < gasNames.Length; i++)
            {

                if (i % 3 == 0)
                {
                    Console.WriteLine();
                }

                string list = String.Format("{0,-30}", gasNames[i]);
                Console.Write(list);
            }
        }

        // GLENN: (suggestion) make sure to keep your code well formatted
        // 
        // If you indent your code properly, it is easier to read. If you want to quickly
        // Fix the indents in your file, you can do Edit -> Advanced -> Format Document.
        static double GetMolecularWeightFromName(string gasName, string[] gasNames,
        double[] molecularWeights, int countGases)
        {
            double moleweight;
            int index = 0;


            //int index = Array.IndexOf(gasNames, gasName);            

            for (int i = 0; i < gasNames.Length; ++i)

            {
                if (gasNames[i] == gasName)
                {
                    index = i;

                }

            }
            if (gasNames[index] == gasName)
            {

                Console.Out.WriteLine("The element {0} has a molecular weight of {1}\n", gasName, molecularWeights[index]);
                moleweight = molecularWeights[index];
                return moleweight;
            }

            else
            {
                Console.Out.WriteLine("\n{0} is not found on the list!", gasName);
                moleweight = 0;
                return moleweight;

            }

        }
        static double CelciusToKelvin(double celcius)
        {
            double kelvin;

            kelvin = celcius + 273.15;
            return kelvin;
        }

        static double NumberOfMoles(double mass, double molecularWeight)
        {
            double moles;
            moles = mass / molecularWeight;

            return moles;
        }

        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            const double R = 8.3145;
            double T;
            double n;
            double P;
            n = NumberOfMoles(mass, molecularWeight);
            T = CelciusToKelvin(temp);

            P = (n * R * T) / vol;

            return P;
        }

        static double PaToPSI(double pascals)
        {
            double PSI;

            PSI = pascals / 6895;


            return PSI;
        }

        private static void DisplayPressure(double pressure)
        {
            double PSI;
            PSI = PaToPSI(pressure);
            Console.Out.WriteLine("The Pressure in Pascals is: " + pressure);
            Console.Out.WriteLine("The Pressure in PSI is: " + PSI);
        }



    }

}




