﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QAP
{
    class Mutation : Kit
    {
        public int size { get; }
        public List<int> MutatedIndividual { get; set; }
        public List<List<int>> Population { get; } //new List<List<int>>();
        public List<int> ResultsOfObjectiveFunction { get; }
        InputData inputdata = new InputData();
        public Mutation(List<List<int>> FirstPopulation)
        {
            Population = FirstPopulation;
            size = FirstPopulation.Count;
            InputData inputdata = new InputData();
            ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);
        }
        public List<int> RandomWitoutRestrictions()
        {
            List<int> WhichIndividuals = new List<int>();
            int number = 3;
            var rand = new System.Random();

            while (number != 0)
            {
                WhichIndividuals.Add(GiveRandomNumber(WhichIndividuals, Population.Count - 1, rand));
                number--;
            }
            return WhichIndividuals;
        }


        public List<int> RankingMethond()
        {
            List<int> WhichIndividuals = new List<int>();
            List<int> WhichIndividualsIndex = new List<int>();
            List<int> WhichIndividualBeforeSelection = new List<int>();
            List<int> ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);

            var rand = new System.Random();
            int size_tmp = size;
            int number = 3;

            while (size_tmp != 0)
            {
                var index = ResultsOfObjectiveFunction.IndexOf(ResultsOfObjectiveFunction.Min());
                ResultsOfObjectiveFunction[index] = Int32.MaxValue;

                int numberOfIter = size_tmp;
                while (numberOfIter != 0)
                {
                    WhichIndividualBeforeSelection.Add(index);
                    numberOfIter--;
                }
                size_tmp--;
            }

            while (number != 0)
            {
                var tmp = GiveRandomNumber(WhichIndividualsIndex, WhichIndividualBeforeSelection.Count - 1, rand);
                for (int i = 0; i < WhichIndividualBeforeSelection.Count; i++) // to w celu uniknięcia powtórzeń w wektorach
                {
                    if (WhichIndividualBeforeSelection[i] == WhichIndividualBeforeSelection[tmp])
                        WhichIndividualsIndex.Add(i);
                }

                WhichIndividuals.Add(WhichIndividualBeforeSelection[tmp]);
                number--;
            }
            return WhichIndividuals;
        }

        public List<int> RouletteMethond()
        {
            List<int> WhichIndividuals = new List<int>();
            List<int> ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);
            List<Tuple<int, int>> tuple = new List<Tuple<int, int>>();
            int range_begin = 0;
            int range_end = 0;
            var rand = new System.Random();
            int number = 3;

            foreach (var elem in ResultsOfObjectiveFunction)
            {
                range_end += elem;
                tuple.Add(new Tuple<int, int>(range_begin, range_end));
                range_begin = elem + 1;
            }

            while (number != 0)
            {
                var tmp = GiveRandomNumber(WhichIndividuals, ResultsOfObjectiveFunction.Sum(), rand);
                for (int i = 0; i < tuple.Count; i++)
                {
                    if (tuple[i].Item2 > tmp)
                    {
                        WhichIndividuals.Add(i);
                        number--;
                        break;
                    }
                }
            }
            return WhichIndividuals;
        }

        public List<int> TournamentMethond()
        {
            List<int> WhichIndividuals = new List<int>();
            List<int> ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);
            List<int> ResultsOfObjectiveFunction_const = new List<int>();
            ResultsOfObjectiveFunction_const.AddRange(ResultsOfObjectiveFunction);
            List<int> TournamentTable = new List<int>();
            int number = 3;

            while (number != 0)
            {
                int sizeOfTournament_const = ResultsOfObjectiveFunction.Count;
                while (sizeOfTournament_const > 1)
                {
                    for (int i = 0; i < sizeOfTournament_const; i++)
                    {
                        TournamentTable.Add(Math.Min(ResultsOfObjectiveFunction[i], ResultsOfObjectiveFunction[i + 1]));
                        i++;

                        if ((sizeOfTournament_const % 2) == 1 && i == sizeOfTournament_const - 2)
                        {
                            TournamentTable.Add(ResultsOfObjectiveFunction.Last());
                            i++;
                        }
                    }

                    if (TournamentTable.Count == 1)
                        break;
                    sizeOfTournament_const = TournamentTable.Count;
                    ResultsOfObjectiveFunction.RemoveAll(elem => ResultsOfObjectiveFunction.Contains(elem));
                    ResultsOfObjectiveFunction.AddRange(TournamentTable);
                    TournamentTable.RemoveAll(elem => TournamentTable.Contains(elem));
                }

                var index = ResultsOfObjectiveFunction_const.FindIndex(value => value == TournamentTable[0]);
                WhichIndividuals.Add(index);
                ResultsOfObjectiveFunction_const[index] = Int32.MaxValue; // jak sie wstawi to 0 ten index nigdy juz nie wygra
                ResultsOfObjectiveFunction.RemoveAll(elem => ResultsOfObjectiveFunction.Contains(elem));
                ResultsOfObjectiveFunction.AddRange(ResultsOfObjectiveFunction_const);
                TournamentTable.RemoveAt(0);
                number--;
            }
            return WhichIndividuals;
        }

        public List<int> ElitistMethond()
        {
            List<int> WhichIndividuals = new List<int>();
            List<int> ResultsOfObjectiveFunction = inputdata.ObjectiveFunctionVector(Population);
            int number = 3;

            while (number != 0)
            {
                int index = ResultsOfObjectiveFunction.FindIndex(min => min == ResultsOfObjectiveFunction.Min());
                WhichIndividuals.Add(index);
                ResultsOfObjectiveFunction[index] = Int32.MaxValue;
                number--;
            }

            return WhichIndividuals;
        }

        public List<int> ToMutate()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            List<int> WhichIndividuals = RandomWitoutRestrictions();
            //List<int> WhichIndividuals = RankingMethond();
            //List<int> WhichIndividuals = RouletteMethond();
            //List<int> WhichIndividuals = TournamentMethond();
            //List<int> WhichIndividuals = ElitistMethond();
            double DefaultValueForMut = 0.8;

            for (int j = 0; j < Population.Count; j++)
            {
                double value = Population[WhichIndividuals[0]][j] + DefaultValueForMut * (Population[WhichIndividuals[1]][j] - Population[WhichIndividuals[2]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }
    }
}
