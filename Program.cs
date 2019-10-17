using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Net;

namespace QAP
{
    class Program
    {
        public static int GiveRandomNumber(List<int> list, int max_value, Random rand)
        {
            var range = Enumerable.Range(1, max_value).Where(i => !list.Contains(i));
            int index = rand.Next(0, max_value - list.Count);
            return range.ElementAt(index);
        }

        public static List<List<int>> GenerateFirstPopulation(int size)
        {
            var rand = new System.Random();
            List<List<int>> FirstPopulation = new List<List<int>>();

            for (int i = 0; i < size; i++)
            {
                List<int> Individual = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    Individual.Add(GiveRandomNumber(Individual, size, rand));
                }
                FirstPopulation.Add(Individual);
            }

            return FirstPopulation;
        }

        public static void Normalize(List<double> MutatedIndividual_tmp , List<int> MutatedIndividual)
        {
            List<double> ListToConvert = MutatedIndividual_tmp;
            int cnt = 1;

            while(cnt != (MutatedIndividual_tmp.Count+1))
            {
                 var index = ListToConvert.FindIndex(min => min==MutatedIndividual_tmp.Min());
                MutatedIndividual[index] = cnt;
                MutatedIndividual_tmp[index] = Double.MaxValue;//.RemoveAt(MutatedIndividual_tmp.FindIndex(min => min == MutatedIndividual_tmp.Min()));

                cnt++;
            }

        }

        public static List<int> Mutation(List<List<int>> Population, int i)
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            List<int> WhichIndividuals = new List<int>() {i};
            var rand = new System.Random();
            int number = 3;
            double DefaultValueForMut = 0.8;

            while(number != 0)
            {
                WhichIndividuals.Add(GiveRandomNumber(WhichIndividuals, Population[i].Count-1, rand));
                number--;
            }

            for (int j = 0; j<Population[i].Count; j++)
            {
                double value = Population[WhichIndividuals[1]][j] + DefaultValueForMut * (Population[WhichIndividuals[2]][j] - Population[WhichIndividuals[3]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);

            return MutatedIndividual;
        }

        public static List<int> BinomialCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            Random random = new Random();
            double Cr = random.NextDouble();

            Random random2 = new Random();

            int size = ParentIndividual.Count;
            for (int i = 0; i< size; i++)
            {
                double Cr_tmp = random.NextDouble();

                if (Cr_tmp < Cr) // Mutated
                {
                    if (CrossedIndividual.Contains(MutatedIndividual[i]))
                    {
                        var ind = GiveRandomNumber(CrossedIndividual, MutatedIndividual.Count, random2);
                        CrossedIndividual.Add(ind);
                    }
                    else
                        CrossedIndividual.Add(MutatedIndividual[i]);
                }
                else //Parent
                {
                    if (CrossedIndividual.Contains(ParentIndividual[i]))
                    {
                        var ind = GiveRandomNumber(CrossedIndividual, ParentIndividual.Count, random2);
                        CrossedIndividual.Add(ind);
                    }
                    else
                        CrossedIndividual.Add(ParentIndividual[i]);
                }
            }
            return CrossedIndividual;
        }

        public static void Replacement(List<List<int>> FirstPopulation, List<int> CrossedIndividual, int i)
        {
            InputData inputdata = new InputData();

            if (inputdata.ObjectiveFunction(CrossedIndividual) < inputdata.ObjectiveFunction(FirstPopulation[i])){ 
                FirstPopulation.RemoveAt(i);
                FirstPopulation.Insert(i, CrossedIndividual);
            }
        }

        static void Main(string[] args)
        {
            // Dane wejściowe
            InputData inputdata = new InputData();

            // Generacja pierwszej populacji 
            List<List<int>> FirstPopulation = GenerateFirstPopulation(inputdata.MatrixD.Count);

            int num_of_iter = 10;

            while (num_of_iter != 0)
            {
                for (int i = 0; i < inputdata.MatrixD.Count; i++)
                {
                    // Mutacja
                    List<int> MutatedIndividual = Mutation(FirstPopulation, 0);

                    // Krzyżowanie
                    List<int> CrossedIndividual = BinomialCrossver(FirstPopulation[0], MutatedIndividual);

                    // Replacement
                    Replacement(FirstPopulation, CrossedIndividual, i);

                    Console.WriteLine("------------------------");
                    foreach (var vec in FirstPopulation)
                    {
                        Console.Write(inputdata.ObjectiveFunction(vec) + " ");
                        foreach (var ob in vec)
                        {
                            Console.Write(ob + " ");
                        }
                        Console.WriteLine();
                    }
                }

                Console.WriteLine("------------------------");
                num_of_iter--;
            }





            /*foreach(var i in MutatedIndividual)
            {
                Console.WriteLine(i);
            }*/

            /* foreach (var dat in FirstPopulation)
             {
                 dat.ForEach(Console.Write);
                 Console.WriteLine('\n');
             }*/
            Console.ReadKey();
        }
    }
}
