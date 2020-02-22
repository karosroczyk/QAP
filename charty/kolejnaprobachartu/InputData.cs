﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejnaprobachartu
{
    public class InputData
    {
        public  List<List<int>> MatrixD { get; } //Read only
        public  List<List<int>> MatrixF { get; } //Read only

        public static int size;

        public InputData()
        {
            /*MatrixF = new List<List<int>>(){
    new List<int>(){0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
    new List<int>(){1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
    new List<int>(){1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1},
    new List<int>(){1, 1, 1, 0, 7, 7, 7, 7, 7, 7, 7, 1, 1, 1, 0, 1},
    new List<int>(){1, 1, 1, 7, 0, 21, 21, 21, 21, 21, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 0, 21, 21, 21, 21, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 21, 0, 21, 21, 21, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 21, 21, 0, 21, 21, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 21, 21, 21, 0, 21, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 21, 21, 21, 21, 0, 21, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 7, 21, 21, 21, 21, 21, 21, 0, 2, 2, 2, 1, 1},
    new List<int>(){1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 0, 6, 6, 4, 1},
    new List<int>(){1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 6, 0, 6, 4, 1},
    new List<int>(){1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 6, 6, 0, 4, 1},
    new List<int>(){0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 4, 4, 4, 0, 0},
    new List<int>(){1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0}
    };

            MatrixD = new List<List<int>>(){
    new List<int>(){0, 0, 0, 1, 0, 1, 1, 2, 0, 1, 1, 2, 1, 2, 2, 3},
    new List<int>(){0, 0, 1, 0, 1, 0, 2, 1, 1, 0, 2, 1, 2, 1, 3, 2},
    new List<int>(){0, 1, 0, 0, 1, 2, 0, 1, 1, 2, 0, 1, 2, 3, 1, 2},
    new List<int>(){1, 0, 0, 0, 2, 1, 1, 0, 2, 1, 1, 0, 3, 2, 2, 1},
    new List<int>(){0, 1, 1, 2, 0, 0, 0, 1, 1, 2, 2, 3, 0, 1, 1, 2},
    new List<int>(){1, 0, 2, 1, 0, 0, 1, 0, 2, 1, 3, 2, 1, 0, 2, 1},
    new List<int>(){1, 2, 0, 1, 0, 1, 0, 0, 2, 3, 1, 2, 1, 2, 0, 1},
    new List<int>(){2, 1, 1, 0, 1, 0, 0, 0, 3, 2, 2, 1, 2, 1, 1, 0},
    new List<int>(){0, 1, 1, 2, 1, 2, 2, 3, 0, 0, 0, 1, 0, 1, 1, 2},
    new List<int>(){1, 0, 2, 1, 2, 1, 3, 2, 0, 0, 1, 0, 1, 0, 2, 1},
    new List<int>(){1, 2, 0, 1, 2, 3, 1, 2, 0, 1, 0, 0, 1, 2, 0, 1},
    new List<int>(){2, 1, 1, 0, 3, 2, 2, 1, 1, 0, 0, 0, 2, 1, 1, 0},
    new List<int>(){1, 2, 2, 3, 0, 1, 1, 2, 0, 1, 1, 2, 0, 0, 0, 1},
    new List<int>(){2, 1, 3, 2, 1, 0, 2, 1, 1, 0, 2, 1, 0, 0, 1, 0},
    new List<int>(){2, 3, 1, 2, 1, 2, 0, 1, 1, 2, 0, 1, 0, 1, 0, 0},
    new List<int>(){3, 2, 2, 1, 2, 1, 1, 0, 2, 1, 1, 0, 1, 0, 0, 0}

    };*/

            MatrixF = new List<List<int>>(){
    new List<int>(){0, 12, 86, 68, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){12, 0, 0, 0, 69, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){86, 0, 0, 0, 0, 34, 35, 0, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){68, 0, 0, 0, 0, 0, 0, 19, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){0, 69, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){0, 0, 34, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    new List<int>(){0, 0, 35, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0},
    new List<int>(){0, 0, 0, 19, 0, 0, 0, 0, 0, 21, 88, 0, 0, 0, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 79, 0, 0, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 21, 0, 0, 0, 0, 3, 0, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 88, 0, 0, 0, 0, 0, 40, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 0, 79, 0, 0, 0, 0, 0, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 77},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40, 0, 0, 0, 0},
    new List<int>(){0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 77, 0, 0}
    };

            MatrixD = new List<List<int>>(){
    new List<int>(){0, 84, 81, 10, 40, 39, 5, 83, 10, 78, 88, 74, 88, 16, 21},
    new List<int>(){84, 0, 39, 67, 62, 38, 3, 16, 43, 47, 70, 97, 4, 17, 34},
    new List<int>(){81, 39, 0, 53, 12, 50, 98, 94, 4, 16, 24, 17, 20, 38, 60},
    new List<int>(){10, 67, 53, 0, 43, 90, 84, 60, 50, 43, 46, 80, 53, 20, 1},
    new List<int>(){40, 62, 12, 43, 0, 86, 18, 1, 28, 14, 58, 34, 2, 3, 55},
    new List<int>(){39, 38, 50, 90, 86, 0, 58, 92, 90, 62, 99, 93, 64, 50, 83},
    new List<int>(){5, 3, 98, 84, 18, 58, 0, 53, 93, 69, 98, 27, 19, 50, 86},
    new List<int>(){83, 16, 94, 60, 1, 92, 53, 0, 30, 35, 39, 55, 25, 12, 41},
    new List<int>(){10, 43, 4, 50, 28, 90, 93, 30, 0, 16, 10, 96, 7, 11, 52},
    new List<int>(){78, 47, 16, 43, 14, 62, 69, 35, 16, 0, 28, 13, 97, 63, 91},
    new List<int>(){88, 70, 24, 46, 58, 99, 98, 39, 10, 28, 0, 94, 91, 44, 62},
    new List<int>(){74, 97, 17, 80, 34, 93, 27, 55, 96, 13, 94, 0, 48, 36, 11},
    new List<int>(){88, 4, 20, 53, 2, 64, 19, 25, 7, 97, 91, 48, 0, 69, 80},
    new List<int>(){16, 17, 38, 20, 3, 50, 50, 12, 11, 63, 44, 36, 69, 0, 41},
    new List<int>(){21, 34, 60, 1, 55, 83, 86, 41, 52, 91, 62, 11, 80, 41, 0}
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
