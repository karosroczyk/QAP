using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejnaprobachartu
{
    class Mutation : Selection
    {
        public List<int> MutatedIndividual { get; set; }
        public List<int> ResultsOfObjectiveFunction { get; }
        public Random rand = new Random();
        public double PreviousValue = 0.3;
        public int NumberOfIter;
        public int actual_iter = 0;

        public Mutation(List<List<int>> FirstPopulation, int NumberOfIter_) : base(FirstPopulation)
        {
            //Population = FirstPopulation;
            InputData inputdata = new InputData();
            ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);
            NumberOfIter = NumberOfIter_;
        }
        public double SetDynamicParameter()
        {
            double StartValue = 0.3;
            double NewValue = PreviousValue + (0.5 + StartValue) / (NumberOfIter * Population.Count);

            PreviousValue = NewValue;
            return NewValue;
        }
        public double GaussianLayout()
        {
            actual_iter++;
            const double medium = 5;
            const double sigma = 2.5;
            const double PI = 3.14;
            const double diff = 0.00033;
            double x = (diff * actual_iter);

            var first = 1 / Math.Sqrt(2 * PI) * sigma;
            var y = first * Math.Exp((-(x - medium) * (x - medium)) / (2 * sigma * sigma));

            return y;
        }
        public List<int> ToMutate()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            List<int> Randoms = RankingMethond();
            double DefaultValueForMut = GaussianLayout();

            for (int j = 0; j < Population.Count; j++)
            {
                double value = Population[Randoms[0]][j] + DefaultValueForMut * (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        public List<int> ToMutateB()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            List<int> Randoms = RankingMethond();
            double scaler = rand.NextDouble();
            Console.WriteLine(scaler);
            double DefaultValueForMut = SetDynamicParameter();

            for (int j = 0; j < Population.Count; j++)
            {
                double value = (scaler * Population[Randoms[0]][j]) + DefaultValueForMut * (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<int> ToMutate2()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();

            List<int> Best = ElitistMethond(1);
            List<int> Difference = RankingMethond();

            double DefaultValueForMut = SetDynamicParameter();

            for (int j = 0; j < Population.Count; j++)
            {
                double value = Population[Best[0]][j] + DefaultValueForMut * (Population[Difference[0]][j] - Population[Difference[1]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        public List<int> ToMutate2B()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            double scaler = rand.NextDouble();
            List<int> Best = ElitistMethond(1);
            List<int> Difference = RankingMethond();

            double DefaultValueForMut = SetDynamicParameter();

            for (int j = 0; j < Population.Count; j++)
            {
                double value = (scaler * Population[Best[0]][j]) + DefaultValueForMut * (Population[Difference[0]][j] - Population[Difference[1]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<int> ToMutate3(int number_of_vectors)
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            int n = 2 * number_of_vectors + 1;

            if (n > Population.Count)
            {
                number_of_vectors = (Population.Count - 1) / 2;
            }

            List<int> Randoms = RankingMethond();
            double DefaultValueForMut = SetDynamicParameter();
            for (int j = 0; j < Population.Count; j++)
            {
                int difference = 0;
                int number = number_of_vectors;
                while (number != 0)
                {
                    difference += (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                    number--;
                }

                double value = Population[Randoms[0]][j] + DefaultValueForMut * difference;
                MutatedIndividual_tmp.Add(value);
            }
            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        public List<int> ToMutate3B(int number_of_vectors)
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            double scaler = rand.NextDouble();
            int n = 2 * number_of_vectors + 1;

            if (n > Population.Count)
            {
                number_of_vectors = (Population.Count - 1) / 2;
            }

            List<int> Randoms = RankingMethond();
            double DefaultValueForMut = SetDynamicParameter();
            for (int j = 0; j < Population.Count; j++)
            {
                int difference = 0;
                int number = number_of_vectors;
                while (number != 0)
                {
                    difference += (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                    number--;
                }

                double value = (scaler * Population[Randoms[0]][j]) + DefaultValueForMut * difference;
                MutatedIndividual_tmp.Add(value);
            }
            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<int> ToMutate4(int number_of_vectors, int iter)
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            double scaler = rand.NextDouble();
            int n = 2 * number_of_vectors + 1;

            if (n > Population.Count)
            {
                number_of_vectors = (Population.Count - 1) / 2;
            }

            List<int> Best = ElitistMethond(1);
            List<int> Randoms = RandomWitoutRestrictions();
            double DefaultValueForMut = 0.8;
            for (int j = 0; j < Population.Count; j++)
            {
                int difference = 0;
                int number = number_of_vectors;
                while (number != 0)
                {
                    difference += (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                    number--;
                }

                double value = (scaler * Population[iter][j]) + DefaultValueForMut * (Population[Best[0]][j] - Population[Randoms[0]][j]) + DefaultValueForMut * difference;
                MutatedIndividual_tmp.Add(value);
            }
            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        public List<int> ToMutate4B(int number_of_vectors, int iter)
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            int n = 2 * number_of_vectors + 1;

            if (n > Population.Count)
            {
                number_of_vectors = (Population.Count - 1) / 2;
            }

            List<int> Best = ElitistMethond(1);
            List<int> Randoms = RandomWitoutRestrictions();
            double DefaultValueForMut = 0.8;
            for (int j = 0; j < Population.Count; j++)
            {
                int difference = 0;
                int number = number_of_vectors;
                while (number != 0)
                {
                    difference += (Population[Randoms[1]][j] - Population[Randoms[2]][j]);
                    number--;
                }

                double value = Population[iter][j] + DefaultValueForMut * (Population[Best[0]][j] - Population[Randoms[0]][j]) + DefaultValueForMut * difference;
                MutatedIndividual_tmp.Add(value);
            }
            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }

        public List<int> SwitchMethod(int caseSwitch, int iter = 0, int number_of_vectors = 2)
        {
            List<int> MutatedIndividual = new List<int>(new int[size]);
            switch (caseSwitch)
            {
                case 1:
                    MutatedIndividual = ToMutate();
                    break;
                case 2:
                    MutatedIndividual = ToMutateB();
                    break;
                case 3:
                    MutatedIndividual = ToMutate2();
                    break;
                case 4:
                    MutatedIndividual = ToMutate2B();
                    break;
                case 5:
                    MutatedIndividual = ToMutate3(number_of_vectors);
                    break;
                case 6:
                    MutatedIndividual = ToMutate3B(number_of_vectors);
                    break;
                case 7:
                    MutatedIndividual = ToMutate4(number_of_vectors, iter);
                    break;
                case 8:
                    MutatedIndividual = ToMutate4B(number_of_vectors, iter);
                    break;
                default:
                    MutatedIndividual = ToMutate();
                    break;
            }
            return MutatedIndividual;
        }
    }
}
