using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mutacje_charty
{
    class Final
    {
        public static List<int> toChart = new List<int>();

        public static List<List<int>> GenerateFirstPopulation(int size)
        {
            var rand = new System.Random();
            List<List<int>> FirstPopulation = new List<List<int>>();

            for (int i = 0; i < size; i++)
            {
                List<int> Individual = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    Individual.Add(Kit.GiveRandomNumber(Individual, size, rand));
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
                        var ind = Kit.GiveRandomNumber(CrossedIndividual, MutatedIndividual.Count, random2);
                        CrossedIndividual.Add(ind);
                    }
                    else
                        CrossedIndividual.Add(MutatedIndividual[i]);
                }
                else //Parent
                {
                    if (CrossedIndividual.Contains(ParentIndividual[i]))
                    {
                        var ind = Kit.GiveRandomNumber(CrossedIndividual, ParentIndividual.Count, random2);
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

        public static void RemoveAllElems()
        {
            toChart.Clear();
        }

        List<int> finals = new List<int>();
        public static string final;
        public void Koncowa(int switchCase, int number_of_vectors = 2)
        {
            //Console.WriteLine(FirstPopulation[0][0]);
            // Dane wejściowe
            InputData inputdata = new InputData();

            List<List<int>> FirstPopulation = GenerateFirstPopulation(inputdata.MatrixD.Count); /* new List<List<int>>(){
                new List<int>(){13,9,5,16,11,15,2,4,3,8,14,10,12,1,7,6},
                new List<int>(){5,13,11,12,10,3,6,7,1,8,4,14,2,16,9,15},
                new List<int>(){11,7,3,16,9,15,10,1,6,8,12,4,14,13,5,2},
                new List<int>(){1,15,3,16,11,5,2,10,6,14,13,7,9,12,4,8},
                new List<int>(){5,13,2,8,3,11,9,6,1,14,7,4,15,10,16,12},
                new List<int>(){10,14,6,2,12,4,7,8,9,13,15,5,1,3,11,16},
                new List<int>(){6,4,16,1,13,7,5,9,10,14,15,2,12,11,3,8},
                new List<int>(){7,12,14,13,4,2,10,1,8,15,3,9,16,11,5,6},
                new List<int>(){5,8,14,6,3,10,7,4,12,16,11,15,2,1,9,13},
                new List<int>(){10,12,3,6,8,11,7,2,13,16,9,1,4,15,14,5},
                new List<int>(){10,1,14,8,2,13,12,16,5,7,4,6,9,15,11,3},
                new List<int>(){6,9,16,2,12,14,5,11,15,8,10,3,13,1,4,7},
                new List<int>(){7,2,6,8,10,15,9,14,13,3,1,13,5,4,12,11},
                new List<int>(){3,8,13,12,14,6,15,10,7,1,9,5,16,4,2,11},
                new List<int>(){8,9,15,6,7,4,13,10,1,2,5,12,16,11,3,14},
                new List<int>(){16,1,4,8,11,6,15,2,5,12,13,7,9,3,14,10}
            };*/

            // Generacja pierwszej populacji 
            Mutation mutationdata = new Mutation(FirstPopulation);
            int num_of_iter = 20000;

            //List<int> MutatedIndividual2 = mutationdata.TournamentMethond();
            while (num_of_iter != 0)
            {
                for (int i = 0; i < InputData.size; i++)
                {
                    // Mutacja
                    List<int> MutatedIndividual2 = mutationdata.SwitchMethod(switchCase, i, number_of_vectors);

                    // Krzyżowanie
                    List<int> CrossedIndividual2 = BinomialCrossver(FirstPopulation[i], MutatedIndividual2);

                    // Replacement
                    Replacement(FirstPopulation, CrossedIndividual2, i);
                }
                var wyniki = inputdata.ObjectiveFunctionVector(FirstPopulation);
                toChart.Add(wyniki.Min());
                if (num_of_iter == 1)
                {
                    finals.Add(wyniki.Min());
                    final += wyniki.Min().ToString() + " ";
                    //MessageBox.Show(wyniki.Min().ToString());
                }
                num_of_iter--;
            }
        }
    }
}
