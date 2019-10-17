using System;
using System.Collections.Generic;
using System.Text;

namespace QAP
{
    class InputData
    {
        public List<List<int>> MatrixD { get; } //Read only
        public List<List<int>> MatrixF { get; } //Read only

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
                new List<int>(){0, 1, 2 ,3, 1, 2, 3 ,4 ,2 ,3 ,4 ,5},
                new List<int>(){1, 0, 1, 2, 2, 1, 2, 3, 3, 2 ,3 ,4},
                new List<int>(){2, 1, 0, 1, 3, 2, 1, 2 ,4 ,3 ,2 ,3},
                new List<int>(){3, 2, 1, 0 ,4 ,3 ,2 ,1 ,5 ,4, 3, 2},
                new List<int>(){1, 2, 3, 4 ,0 ,1 ,2 ,3 ,1 ,2 ,3 ,4 },
                new List<int>(){2, 1, 2, 3, 1 ,0 ,1 ,2 ,2 ,1 ,2 ,3, },
                new List<int>(){3, 2 ,1 ,2 ,2 ,1 ,0 ,1 ,3 ,2, 1 ,2 },
                new List<int>(){4, 3, 2, 1, 3, 2 ,1 ,0 ,4, 3, 2, 1 },
                new List<int>(){2, 3, 4 ,5 ,1 ,2 ,3 ,4 ,0 ,1 ,2 ,3 },
                new List<int>(){3, 2 ,3 ,4 ,2 ,1 ,2 ,3 ,1 ,0, 1, 2 },
                new List<int>(){4, 3, 2, 3 ,3 ,2 ,1 ,2 ,2 ,1 ,0 ,1 },
                new List<int>(){5, 4, 3, 2, 4 ,3 ,2 ,1 ,3 ,2 ,1 ,0 }
            };

            MatrixD = new List<List<int>>(){
                new List<int>(){0 , 5 , 2 , 4,  1,  0,  0,  6,  2,  1,  1,  1},
                new List<int>(){5,  0,  3,  0,  2,  2,  2,  0,  4,  5,  0,  0},
                new List<int>(){2,  3,  0,  0,  0,  0,  0,  5,  5,  2,  2,  2},
                new List<int>(){ 4,  0,  0,  0,  5,  2,  2, 10,  0,  0,  5,  5 },
                new List<int>(){ 1,  2,  0,  5,  0, 10,  0,  0,  0,  5,  1,  1},
                new List<int>(){0,  2,  0,  2, 10,  0,  5,  1,  1,  5,  4,  0 },
                new List<int>(){0,  2,  0,  2,  0,  5,  0, 10,  5,  2,  3,  3 },
                new List<int>(){6,  0,  5, 10,  0,  1, 10,  0,  0,  0,  5,  0},
                new List<int>(){ 2,  4,  5,  0,  0,  1,  5,  0,  0,  0, 10, 10},
                new List<int>(){ 1,  5,  2,  0,  5,  5,  2,  0,  0,  0,  5,  0},
                new List<int>(){1,  0,  2,  5,  1,  4,  3,  5, 10,  5,  0,  2},
                new List<int>(){1,  0,  2,  5,  1,  0,  3,  0, 10,  0,  2,  0 }
            };
        }

        public InputData(List<List<int>> MatrixD_, List<List<int>> MatrixF_)
        {
            MatrixD = MatrixD_;
            MatrixF = MatrixF_;
        }

        public int ObjectiveFunction(List<int> NewLayout )
        {
            int Value = 0;
            for (int i = 0; i<NewLayout.Count; i++)
            {
                for(int j = 0; j<NewLayout.Count; j++)
                {
                    if (j != i)
                    {
                        Value += MatrixD[NewLayout[i] - 1][NewLayout[j] - 1] * MatrixF[i][j];
                    }
                }
            }
            return Value;
        }
    }
}
