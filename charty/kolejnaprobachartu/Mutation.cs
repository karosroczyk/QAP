using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kolejnaprobachartu
{
    class Mutation
    {
        public int size { get; }
        public int switchCase { get; }
        public List<int> MutatedIndividual { get; set; }
        public List<List<int>> Population { get; } //new List<List<int>>();
        public List<int> ResultsOfObjectiveFunction { get; }
        public List<int> WhichIndividuals { get; set; }
        InputData inputdata = new InputData();
        public Mutation(List<List<int>> FirstPopulation, int switchCase_)
        {
            Population = FirstPopulation;
            size = FirstPopulation.Count;
            switchCase = switchCase_;
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
                WhichIndividuals.Add(Kit.GiveRandomNumber(WhichIndividuals, Population.Count - 1, rand));
                number--;
            }
            return WhichIndividuals;
        }

        public List<int> RandomWitoutRestrictions2()
        {
            List<int> WhichIndividuals = new List<int>();
            int number = 3;
            var rand = new System.Random();

            while (number != 0)
            {
                WhichIndividuals.Add(Kit.GiveRandomNumber2(Population.Count - 1, rand));
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
                var tmp = Kit.GiveRandomNumber(WhichIndividualsIndex, WhichIndividualBeforeSelection.Count - 1, rand);
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

        public List<int> RankingMethond2()
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
                var tmp = Kit.GiveRandomNumber2(WhichIndividualBeforeSelection.Count - 1, rand);

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
                var tmp = Kit.GiveRandomNumber(WhichIndividuals, ResultsOfObjectiveFunction.Sum(), rand);
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

        public List<int> TournamentMethond2()
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
                Console.WriteLine(index);
                //ResultsOfObjectiveFunction_const[index] = Int32.MaxValue; // jak sie wstawi to 0 ten index nigdy juz nie wygra
                ResultsOfObjectiveFunction.RemoveAll(elem => ResultsOfObjectiveFunction.Contains(elem));
                ResultsOfObjectiveFunction.AddRange(ResultsOfObjectiveFunction_const);
                TournamentTable.RemoveAt(0);
                number--;
            }
            Console.WriteLine("-----");
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

        public void SwitchMethod(int caseSwitch)
        {
            switch (caseSwitch)
            {
                case 1:
                    WhichIndividuals = RandomWitoutRestrictions();
                    break;
                case 2:
                    WhichIndividuals = RankingMethond();
                    break;
                case 3:
                    WhichIndividuals = RouletteMethond();
                    break;
                case 4:
                    WhichIndividuals = TournamentMethond();
                    break;
                case 5:
                    WhichIndividuals = ElitistMethond();
                    break;
                case 6:
                    WhichIndividuals = RandomWitoutRestrictions2();
                    break;
                case 7:
                    WhichIndividuals = RankingMethond2();
                    break;
                case 8:
                    WhichIndividuals = TournamentMethond2();
                    break;
                default:
                    WhichIndividuals = RankingMethond();
                    break;
            }
        }

        public List<int> ToMutate()
        {
            List<int> MutatedIndividual = new List<int>(new int[Population.Count]);
            List<double> MutatedIndividual_tmp = new List<double>();
            SwitchMethod(switchCase);
            double DefaultValueForMut = 0.8;

            for (int j = 0; j < Population.Count; j++)
            {
                double value = Population[WhichIndividuals[0]][j] + DefaultValueForMut * (Population[WhichIndividuals[1]][j] - Population[WhichIndividuals[2]][j]);
                MutatedIndividual_tmp.Add(value);
            }

            Kit.Normalize(MutatedIndividual_tmp, MutatedIndividual);
            return MutatedIndividual;
        }
    }
}
