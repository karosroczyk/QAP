using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mutacje_charty
{
    public class InputData
    {
        public List<List<int>> MatrixD { get; } //Read only
        public List<List<int>> MatrixF { get; } //Read only

        public static int size;

        public InputData()
        {
            /*MatrixD = new List<List<int>>(){
                new List<int>(){0,1,3,4,1,3},
                new List<int>(){1,0,1,5,1,1},
                new List<int>(){3,1,0,2,1,1},
                new List<int>(){4,5,2,0,1,1},
                new List<int>(){1,1,1,1,0,2},
                new List<int>(){3,2,3,1,2,0}
            };

            MatrixF = new List<List<int>>(){
                new List<int>(){0,1,2,3,4,5},
                new List<int>(){5,0,1,2,3,4},
                new List<int>(){4,5,0,1,2,3},
                new List<int>(){3,4,5,0,1,2},
                new List<int>(){2,3,4,5,0,1},
                new List<int>(){1,2,3,4,5,0}
            };*/

            MatrixF = new List<List<int>>(){
    new List<int>(){0, 1, 2, 2, 3, 4, 4, 5, 3, 5, 6, 7},
    new List<int>(){1, 0, 1, 1, 2, 3, 3, 4, 2, 4, 5, 6},
    new List<int>(){2, 1, 0, 2, 1, 2, 2, 3, 1, 3, 4, 5},
    new List<int>(){2, 1, 2, 0, 1, 2, 2, 3, 3, 3, 4, 5},
    new List<int>(){3, 2, 1, 1, 0, 1, 1, 2, 2, 2, 3, 4},
    new List<int>(){4, 3, 2, 2, 1, 0, 2, 3, 3, 1, 2, 3},
    new List<int>(){4, 3, 2, 2, 1, 2, 0, 1, 3, 1, 2, 3},
    new List<int>(){5, 4, 3, 3, 2, 3, 1, 0, 4, 2, 1, 2},
    new List<int>(){3, 2, 1, 3, 2, 3, 3, 4, 0, 4, 5, 6},
    new List<int>(){5, 4, 3, 3, 2, 1, 1, 2, 4, 0, 1, 2},
    new List<int>(){6, 5, 4, 4, 3, 2, 2, 1, 5, 1, 0, 1},
    new List<int>(){7, 6, 5, 5, 4, 3, 3, 2, 6, 2, 1, 0}
    };

            MatrixD = new List<List<int>>(){
    new List<int>(){0, 3, 4, 6, 8, 5, 6, 6, 5, 1, 4, 6},
    new List<int>(){3, 0, 6, 3, 7, 9, 9, 2, 2, 7, 4, 7},
    new List<int>(){4, 6, 0, 2, 6, 4, 4, 4, 2, 6, 3, 6},
    new List<int>(){6, 3, 2, 0, 5, 5, 3, 3, 9, 4, 3, 6},
    new List<int>(){8, 7, 6, 5, 0, 4, 3, 4, 5, 7, 6, 7},
    new List<int>(){5, 9, 4, 5, 4, 0, 8, 5, 5, 5, 7, 5},
    new List<int>(){6, 9, 4, 3, 3, 8, 0, 6, 8, 4, 6, 7},
    new List<int>(){6, 2, 4, 3, 4, 5, 6, 0, 1, 5, 5, 3},
    new List<int>(){5, 2, 2, 9, 5, 5, 8, 1, 0, 4, 5, 2},
    new List<int>(){1, 7, 6, 4, 7, 5, 4, 5, 4, 0, 7, 7},
    new List<int>(){4, 4, 3, 3, 6, 7, 6, 5, 5, 7, 0, 9},
    new List<int>(){6, 7, 6, 6, 7, 5, 7, 3, 2, 7, 9, 0}
    };

            size = MatrixD.Count;

        }

        public InputData(List<List<int>> MatrixD_, List<List<int>> MatrixF_)
        {
            MatrixD = MatrixD_;
            MatrixF = MatrixF_;
        }

        public int ObjectiveFunction(List<int> NewLayout)
        {
            int Value = 0;
            for (int i = 0; i < NewLayout.Count; i++)
            {
                for (int j = 0; j < NewLayout.Count; j++)
                {
                    if (j != i)
                    {
                        Value += MatrixD[NewLayout[i] - 1][NewLayout[j] - 1] * MatrixF[i][j];
                    }
                }
            }
            return Value;
        }

        public List<int> ObjectiveFunctionVector(List<List<int>> Population)
        {
            List<int> ResultsOfObjectiveFunction = new List<int>();
            foreach (var vec in Population)
            {
                ResultsOfObjectiveFunction.Add(ObjectiveFunction(vec));
            }
            return ResultsOfObjectiveFunction;
        }
    }
}
