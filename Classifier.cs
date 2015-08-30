using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesClassifier
{
    class Classifier
    {
        TrainingSet trainingSet = new TrainingSet();

        // runs Naive Bayes classification and returns outcome with most probability
        public string classify(Dictionary<string, Tuple<int, int>> myEvidence)
        {
            Double maxProbability = 0;
            string maxOutcome = "";

            // p(O | x_1, x_2, ... x_n) = (p(x_1 | O) * p(x_2 | O) * ... p(x_n | O) * p(O)) / (p(x_1) * p(x_2) * ... p(x_n))
            foreach (KeyValuePair<string, Dictionary<string, Tuple<int, int>>> outcomeEntry in trainingSet.Outcomes)
            {
                Double entryProbability = 1;

                foreach (KeyValuePair<string, Tuple<int, int>> evidenceEntry in outcomeEntry.Value)
                {
                    if (myEvidence.ContainsKey(evidenceEntry.Key))
                    {
                        entryProbability *= (evidenceEntry.Value.Item1 / (double) evidenceEntry.Value.Item2) /
                            (trainingSet.TotalEvidences[evidenceEntry.Key].Item1 / (double) trainingSet.TotalEvidences[evidenceEntry.Key].Item2);
                    }
                }

                entryProbability *= (trainingSet.OutcomeTotals[outcomeEntry.Key] / (double) trainingSet.TotalOutcomeEntries);

                if (entryProbability > maxProbability)
                {
                    maxProbability = entryProbability;
                    maxOutcome = outcomeEntry.Key;
                }
            }

            return maxOutcome;
        }
    }
}
