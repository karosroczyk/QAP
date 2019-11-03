using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QAP
{
    public class Kit
    {
        public static int GiveRandomNumber(List<int> list, int max_value, Random rand)
        {
            var range = Enumerable.Range(1, max_value).Where(i => !list.Contains(i));
            int index = rand.Next(0, max_value - list.Count);
            return range.ElementAt(index);
        }

        public void Normalize(List<double> MutatedIndividual_tmp, List<int> MutatedIndividual)
        {
            List<double> ListToConvert = MutatedIndividual_tmp;
            int cnt = 1;

            while (cnt != (MutatedIndividual_tmp.Count + 1))
            {
                var index = ListToConvert.FindIndex(min => min == MutatedIndividual_tmp.Min());
                MutatedIndividual[index] = cnt;
                MutatedIndividual_tmp[index] = Double.MaxValue;//.RemoveAt(MutatedIndividual_tmp.FindIndex(min => min == MutatedIndividual_tmp.Min()));

                cnt++;
            }
        }
    }
}
