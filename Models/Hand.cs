using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatzeeWPF.Models
{
    class Hand
    {

        private Dictionary<int, int> dieResult;

        public Dictionary<int, int> DieResult
        {
            get { return dieResult; }
        }

        public Hand(int[] dieValues)
        {
            dieResult = dieValues.GroupBy(y => y)
            .ToDictionary(y => y.Key, y => y.Count());
        }

        public CombinationRow[] GetNumbers()
        {
            string scoreType = "";
            return dieResult.Where(x => x.Value >= 1).Select(x => new CombinationRow() { ScoreType = (x.Key + ":s"), Points = (x.Key * x.Value) }).ToArray();
        }
        public CombinationRow[] GetChance()
        {
            return new CombinationRow[] { new CombinationRow { ScoreType = "Chance", Points = dieResult.Sum(x => x.Key * x.Value) } };

        }

        public CombinationRow[] GetPairs()
        {
            return dieResult.Where(x => x.Value >= 2).Select(x => new CombinationRow() { ScoreType = "Pair", Key = x.Key.ToString(), Points = (x.Key * 2) }).ToArray();
        }

        public CombinationRow[] GetThreeKind()
        {
            return dieResult.Where(x => x.Value >= 3).Select(x => new CombinationRow() { ScoreType = "Three of a kind", Key = x.Key.ToString(), Points = (x.Key * 3) }).ToArray();
        }

        public CombinationRow[] GetFourKind()
        {
            return dieResult.Where(x => x.Value >= 4).Select(x => new CombinationRow() { ScoreType = "Four of a kind", Key = x.Key.ToString(), Points = (x.Key * 4) }).ToArray();
        }


        public CombinationRow[] GetLargeStraight()
        {
            if (dieResult.Max(x => x.Value) == 1 && dieResult.Sum(x => x.Key) == 20)
            {
                return new CombinationRow[] { new CombinationRow() { ScoreType = "Large straight", Points = 20 } };
            }
            return new CombinationRow[] { };

        }

        public CombinationRow[] GetSmallStraight()
        {

            if (dieResult.Max(x => x.Value) == 1 && dieResult.Sum(x => x.Key) == 15)
            {
                return new CombinationRow[] { new CombinationRow() { ScoreType = "Small straight", Points = 15 } };
            }
            return new CombinationRow[] { };

        }

        public CombinationRow[] GetYatzee()
        {
            return dieResult.Where(x => x.Value == 5).Select(x => new CombinationRow() { ScoreType = "Yatzee", Points = 50 }).ToArray();
        }

        public CombinationRow[] GetFullHouse()
        {
            int low = 0;
            int high = 0;
            foreach (var pair in dieResult)
            {
                if (pair.Value == 3) high = pair.Key;
                if (pair.Value == 2) low = pair.Key;
            }
            if (high == 0 || low == 0) return new CombinationRow[] { };
            return new CombinationRow[] { new CombinationRow() { ScoreType = "Full house", Key = high.ToString() + ", " + low.ToString(), Points = ((high * 3) + (low * 2)) } };

        }

        public CombinationRow[] GetTwoPairs()
        {

            int first = 0;
            int second = 0;
            foreach (var pair in dieResult)
            {
                if (pair.Value >= 2)
                {
                    if (first == 0) first = pair.Key;
                    else second = pair.Key;
                }

            }
            if (first == 0 || second == 0) return new CombinationRow[] { };
            return new CombinationRow[] { new CombinationRow() { ScoreType = "Two pairs", Key = first.ToString() + ", " + second.ToString(), Points = ((first * 2) + (second * 2)) } };
        }

        public CombinationRow[] GetCombinations()
        {
            List<CombinationRow> scoreList = new List<CombinationRow>();

            scoreList.AddRange(GetYatzee());
            scoreList.AddRange(GetPairs());
            scoreList.AddRange(GetThreeKind());
            scoreList.AddRange(GetFourKind());
            scoreList.AddRange(GetLargeStraight());
            scoreList.AddRange(GetSmallStraight());
            scoreList.AddRange(GetFullHouse());
            scoreList.AddRange(GetTwoPairs());
            scoreList.AddRange(GetNumbers());
            scoreList.AddRange(GetChance());

            return scoreList.OrderBy(s => s.Points).Reverse().ToArray();

        }

        public CombinationRow[] GetAllCombinations()
        {
            List<CombinationRow> scoreList = new List<CombinationRow>();

            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Chance", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Pair", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Three of a kind", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Four of a kind", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Large straight", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Small straight", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Full house", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Two pairs", Points = 0 } });
            scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = "Yatzee", Points = 0 } });
            for (int i = 0; i < 6; i++)
            {
                scoreList.AddRange(new CombinationRow[] { new CombinationRow { ScoreType = i + ":s", Points = 0 } });
            }

            return scoreList.ToArray();
        }








    }
}
