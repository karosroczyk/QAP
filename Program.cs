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
    class Program : Kit
    {
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

        public static List<int> BinomialCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            Random random = new Random();
            double Cr = random.NextDouble();

            Random random2 = new Random();

            int size = ParentIndividual.Count;
            for (int i = 0; i < size; i++)
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

            if (inputdata.ObjectiveFunction(CrossedIndividual) < inputdata.ObjectiveFunction(FirstPopulation[i]))
            {
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
            Mutation mutationdata = new Mutation(FirstPopulation);
            int num_of_iter = 1000;

            //List<int> MutatedIndividual2 = mutationdata.TournamentMethond();
            while (num_of_iter != 0)
            {
                for (int i = 0; i < inputdata.MatrixD.Count; i++)
                {
                    // Mutacja
                    List<int> MutatedIndividual2 = mutationdata.ToMutate();

                    // Krzyżowanie
                    List<int> CrossedIndividual2 = BinomialCrossver(FirstPopulation[i], MutatedIndividual2);

                    // Replacement
                    Replacement(FirstPopulation, CrossedIndividual2, i);
                }
                if (num_of_iter == 1)
                {
                    foreach (var vec in FirstPopulation)
                    {
                        Console.Write("2) " + inputdata.ObjectiveFunction(vec) + " ");
                        foreach (var ob in vec)
                        {
                            Console.Write(ob + " ");
                        }
                        Console.WriteLine();
                    }
                }
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
            Console.WriteLine("hej");
            Console.ReadKey();
        }
    }
}
