using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejnaprobachartu
{
    class Crossover : Kit
    {
        public double PreviousValue = 0.3;
        public int NumberOfIter;
        public int Size;
        public int actual_iter = 0; 
        public Crossover(int size, int NumberOfIter_) 
        {
            Size = size;
            NumberOfIter = NumberOfIter_;
        }
        public double SetCrDynamicParameter()
        {
            double StartValue = 0.3;
            double NewValue = PreviousValue - (StartValue - 0.7) / (NumberOfIter * Size);

            PreviousValue = NewValue;
            return NewValue;
        }
        public static int GaussianLayout(double x)
        {
            const double medium = 0.25;
            const double sigma = 0.25;
            const double PI = 3.14;

            var first = 1 / Math.Sqrt(2 * PI) * sigma;
            var y = first * Math.Exp((-(x - medium) * (x - medium)) / (2 * sigma * sigma));

            return (int)(y * 1000);
        }

        public static double GaussianProbability()
        {
            List<int> parameters = new List<int>();
            List<int> parameters_scaled = new List<int>();

            int suma = 0;
            double start = 0.05;
            while (start < 1.05)
            {
                int value = GaussianLayout(start);
                parameters.Add(value);
                suma += value;

                start += 0.05;
            }
            foreach (var item in parameters)
            {
                double tmp = 100 * (double)item / (double)suma;
                parameters_scaled.Add((int)(tmp * 10));
            }

            int cnt = 0;
            for (int i = 0; i < parameters_scaled.Count; i++)
            {
                cnt += parameters_scaled[i];
                parameters_scaled[i] = cnt;
            }
            var rand = new System.Random();
            int random = rand.Next(0, 989);
            int index = 0;
            for (int i = 0; i < parameters_scaled.Count; i++)
            {
                if (random >= parameters_scaled[i])
                    index = i;
            }
            return 0.05 * (index + 1);
        }
        public List<int> BinomialCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            Random random = new Random();
            // double Cr = random.NextDouble();
            double Cr = 0.25;
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

        public List<int> OXCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            for (int i = 0; i < ParentIndividual.Count; i++)
            {
                CrossedIndividual.Add(0);
            }

            Random rnd = new Random();
            int offset = rnd.Next(0, ParentIndividual.Count);
            int length = rnd.Next(1, ParentIndividual.Count - offset);
            int iter = 0;
            while (iter != length)
            {
                CrossedIndividual[offset + iter] = MutatedIndividual[offset + iter];
                iter++;
            }

            for (int i = 0; i < ParentIndividual.Count; i++)
            {
                if (CrossedIndividual[i] == 0)
                {
                    var i_tmp = 0;
                    while (CrossedIndividual.Contains(ParentIndividual[i_tmp]))
                    {
                        i_tmp++;
                    }
                    CrossedIndividual[i] = ParentIndividual[i_tmp];
                }
            }
            return CrossedIndividual;
        }

        public List<int> CXCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            for (int i = 0; i < ParentIndividual.Count; i++)
            {
                CrossedIndividual.Add(0);
            }

            CrossedIndividual[0] = MutatedIndividual[0];
            var index = 0;

            while (ParentIndividual[index] != MutatedIndividual[0])
            {
                index = MutatedIndividual.FindIndex(x => x == ParentIndividual[index]);
                CrossedIndividual[index] = MutatedIndividual[index];
            }

            for (int i = 0; i < ParentIndividual.Count; i++)
            {
                if (CrossedIndividual[i] == 0)
                {
                    var i_tmp = 0;
                    while (CrossedIndividual.Contains(ParentIndividual[i_tmp]))
                    {
                        i_tmp++;
                    }
                    CrossedIndividual[i] = ParentIndividual[i_tmp];
                }
            }
            return CrossedIndividual;
        }

        public List<int> PMXCrossver(List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> MutatedIndividualSubstringCopy = new List<int>();
            List<int> ParentIndividualSubstringCopy = new List<int>();
            List<int> MutatedIndividualSubstring = new List<int>();
            List<int> ParentIndividualSubstring = new List<int>();
            Random rnd = new Random();
            int offset = rnd.Next(0, ParentIndividual.Count);
            int length = rnd.Next(1, ParentIndividual.Count - offset);
            int iter = 0;

            while (iter != length)
            {
                MutatedIndividualSubstring.Add(MutatedIndividual[offset + iter]);
                ParentIndividualSubstring.Add(ParentIndividual[offset + iter]);
                iter++;
            }

            for (int i = offset; i < offset + MutatedIndividualSubstring.Count; i++)
            {
                MutatedIndividual[i] = 0;
                ParentIndividual[i] = 0;
            }

            for (int i = 0; i < MutatedIndividualSubstring.Count; i++)
            {
                MutatedIndividualSubstringCopy.Add(MutatedIndividualSubstring[i]);
                ParentIndividualSubstringCopy.Add(ParentIndividualSubstring[i]);
            }

            for (int i = 0; i < MutatedIndividualSubstring.Count; i++)
            {
                if (ParentIndividualSubstring.Contains(MutatedIndividualSubstring[i]))
                {
                    var index = ParentIndividualSubstring.FindIndex(x => x == MutatedIndividualSubstring[i]);
                    MutatedIndividualSubstring[i] = MutatedIndividualSubstring[index];
                    MutatedIndividualSubstring.RemoveAt(index);
                    ParentIndividualSubstring.RemoveAt(index);
                    i = -1;
                }
            }

            for (int i = 0; i < MutatedIndividual.Count; i++)
            {
                if (!MutatedIndividual.Contains(ParentIndividual[i]))
                {
                    int index_2 = MutatedIndividualSubstring.FindIndex(x => x == ParentIndividual[i]);
                    var value = ParentIndividualSubstring[index_2];
                    var index = MutatedIndividual.FindIndex(x => x == value);
                    MutatedIndividual[index] = ParentIndividual[i];
                    ParentIndividual[i] = value;
                }
            }

            var i_tmp = 0;
            var i_tmp2 = 0;

            for (int i = 0; i < MutatedIndividual.Count; i++)
            {
                if (MutatedIndividual[i] == 0)
                {
                    MutatedIndividual[i] = ParentIndividualSubstringCopy[i_tmp];
                    i_tmp++;
                }
                if (ParentIndividual[i] == 0)
                {
                    ParentIndividual[i] = MutatedIndividualSubstringCopy[i_tmp2];
                    i_tmp2++;
                }
            }
            return MutatedIndividual;
        }

        public List<int> SwitchMethodCrossover(int caseSwitch, List<int> ParentIndividual, List<int> MutatedIndividual)
        {
            List<int> CrossedIndividual = new List<int>();
            switch (caseSwitch)
            {
                case 1:
                    CrossedIndividual = BinomialCrossver(ParentIndividual, MutatedIndividual);
                    break;
                case 2:
                    CrossedIndividual = OXCrossver(ParentIndividual,MutatedIndividual);
                    break;
                case 3:
                    CrossedIndividual = CXCrossver(ParentIndividual, MutatedIndividual);
                    break;
                case 4:
                    CrossedIndividual = PMXCrossver(ParentIndividual, MutatedIndividual);
                    break;
                default:
                    CrossedIndividual = BinomialCrossver(ParentIndividual, MutatedIndividual);
                    break;
            }
            return CrossedIndividual;
        }
    }
}
