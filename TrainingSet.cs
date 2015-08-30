using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    // stores TrainingSet data for classifier
    class TrainingSet
    {
        // stores all outcomes and their corresponding evidence true and totals 
        // <Outcome : <Evidence : (True, Total)>>
        private Dictionary<string, Dictionary<string, Tuple<int, int>>> outcomeSet = 
            new Dictionary<string, Dictionary<string, Tuple<int, int>>>();

        // stores all evidences and their true and total amounts summed from all outcomes
        // <Evidence, (True, Total)>
        private Dictionary<string, Tuple<int, int>> evidenceTotals = new Dictionary<string, Tuple<int, int>>;

        // stores number of entries per outcome
        private Dictionary<string, int> outcomeTotals = new Dictionary<string, int>();

        // stores number of total entries
        private int totalOutcomeEntries = 0;

        // adds outcome to outcomeSet and updates evidence trues and totals in evidenceTotals
        public void addOutcome(string outcome, Dictionary<string, Tuple<int, int>> evidence)
        {
            outcomeSet.Add(outcome, evidence);

            if (!outcomeTotals.ContainsKey(outcome))
            {
                outcomeTotals[outcome] = 0;
            }

            int outcomeTotal = 0;

            foreach (KeyValuePair<string, Tuple<int, int>> entry in evidence)
            {
                if (!evidenceTotals.ContainsKey(entry.Key))
                {
                    evidenceTotals[entry.Key] = Tuple.Create(entry.Value.Item1, entry.Value.Item2);
                } else
                {
                    evidenceTotals[entry.Key] = Tuple.Create(evidenceTotals[entry.Key].Item1 + entry.Value.Item1, 
                        evidenceTotals[entry.Key].Item2 + entry.Value.Item2);
                }

                outcomeTotal += entry.Value.Item2;
            }

            outcomeTotals[outcome] = outcomeTotal;
            totalOutcomeEntries += outcomeTotal;
        }

        // removes outcome to outcomeSet and updates evidence trues and totals in evidenceTotals
        public void removeOutcome(string outcome)
        {
            foreach (KeyValuePair<string, Tuple<int, int>> entry in outcomeSet[outcome])
            {
                evidenceTotals[entry.Key] = Tuple.Create(evidenceTotals[entry.Key].Item1 - entry.Value.Item1,
                    evidenceTotals[entry.Key].Item2 - entry.Value.Item2);
            }

            totalOutcomeEntries -= outcomeTotals[outcome];
            outcomeTotals.Remove(outcome);
            outcomeSet.Remove(outcome);
        }

        public Dictionary<string, Dictionary<string, Tuple<int, int>>> Outcomes
        {
            get
            {
                return outcomeSet;
            }
        }

        public Dictionary<string, Tuple<int, int>> TotalEvidences
        {
            get
            {
                return evidenceTotals;
            }
        }

        public Dictionary<string, int> OutcomeTotals
        {
            get
            {
                return outcomeTotals;
            }
        }

        public int TotalOutcomeEntries
        {
            get
            {
                return totalOutcomeEntries;
            }
        }
    }
}
