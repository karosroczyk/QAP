using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kolejnaprobachartu
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

      public void Koncowa(int switchCase)
        {
            //Console.WriteLine(FirstPopulation[0][0]);
            // Dane wejściowe
            InputData inputdata = new InputData();

            List<List<int>> FirstPopulation = new List<List<int>>(){
                new List<int>(){6,1,9,2,7,3,10,8,4,11,5,12},
                new List<int>(){4,3,9,7,5,12,8,2,11,10,1,6},
                new List<int>(){11,7,10,6,8,9,12,5,2,1,3,4},
                new List<int>(){3,11,2,5,4,7,10,8,12,6,9,1},
                new List<int>(){8,4,10,7,5,3,12,9,6,1,2,11},
                new List<int>(){2,12,10,3,6,7,1,11,4,5,8,9},
                new List<int>(){9,4,2,7,3,12,8,11,1,5,10,6},
                new List<int>(){2,5,7,12,6,8,9,11,4,1,3,10},
                new List<int>(){6,7,1,4,11,9,8,3,2,5,12,10},
                new List<int>(){3,6,7,9,10,1,12,11,8,4,2,5},
                new List<int>(){2,11,8,6,9,4,10,5,1,12,3,7},
                new List<int>(){12,3,5,4,9,8,6,11,2,7,10,1}
            };

            // Generacja pierwszej populacji 
            Mutation mutationdata = new Mutation(FirstPopulation, switchCase);
            int num_of_iter = 50000;

            //List<int> MutatedIndividual2 = mutationdata.TournamentMethond();
            while (num_of_iter != 0)
            {
                for (int i = 0; i < InputData.size; i++)
                {
                    // Mutacja
                    List<int> MutatedIndividual2 = mutationdata.ToMutate();

                    // Krzyżowanie
                    List<int> CrossedIndividual2 = BinomialCrossver(FirstPopulation[i], MutatedIndividual2);

                    // Replacement
                    Replacement(FirstPopulation, CrossedIndividual2, i);
                }
                var wyniki = inputdata.ObjectiveFunctionVector(FirstPopulation);
                toChart.Add(wyniki.Min());
                if(num_of_iter == 1)
                {
                    MessageBox.Show(wyniki.Min().ToString());
                }

                num_of_iter--;
            }
        }
    }
}
