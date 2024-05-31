using System.Collections.Generic;

namespace GU1.Envir;

public class LeaderBoard {

    public List<Score> Scores = new();

    public void AddScore(string name, int value) => Scores.Add(new Score { Name = name, Value = value });
    public void TrimScores(int count) => Scores.RemoveRange(count, Scores.Count - count);
    public void ClearScores() => Scores.Clear();
    public void SortScores() => Scores.Sort((a, b) => b.Value.CompareTo(a.Value));

    public void SaveScores() {

        // Save the scores to a file
    }

    public void LoadScores() {

        // Load the scores from a file
    }

    public struct Score {

        public string Name;
        public int Value;
    }
}
