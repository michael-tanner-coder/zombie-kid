using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

[System.Serializable]
public struct Score 
{
    public int value;
    public string holder;
    public string message;
}

public class ScoreTracker : MonoBehaviour
{
    [Tooltip("The score the player currently has at the time of a game over")]
    [SerializeField] private IntVariable _playerScore;

    [Tooltip("The list of high scores the player or NPCs have achieved")]
    [SerializeField] private List<Score> _highScores = new List<Score>();
    
    [Tooltip("Messages to display when the player reaches a specific score")]
    [SerializeField] private IntStringDictionary _scoreMessageMap = new IntStringDictionary();

    [Tooltip("The collection of text objects that will hold the scores and messages for the gravesite")]
    [SerializeField] List<DialogSequence> _scoreEpitaphs = new List<DialogSequence>();
    
    [Tooltip("The limit to how many elements are allowed in the high score list")]
    [Range(5, 10)]
    [SerializeField] private int _maxScoreCount = 5;

    void Awake()
    {
        _highScores = ServiceLocator.Instance.Get<SaveDataManager>().GetHighScores();
        UpdateScoreList();
    }

    public void UpdateScoreList()
    {
        // get the player's score and the lowest score to compare them
        int currentScore = _playerScore.Value;

        _highScores.Sort(CompareScoreValues);

        Score lowestScore = _highScores[_highScores.Count - 1];

        // only add a new score to the list if player's score is at least higher than the lowest score
        if (lowestScore.value < currentScore)
        {
            // get the location of the lowest score (to be replaced)
            int indexOfLowestScore = _highScores.IndexOf(lowestScore);

            // create a new score object
            Score newScore = new Score();
            newScore.value = currentScore;
            newScore.holder = "Rico";
            newScore.message = GetScoreMessage(currentScore);

            // replace the lowest score with the new score
            _highScores[indexOfLowestScore] = newScore;

            // re-sort the list
            _highScores.Sort(CompareScoreValues);
        }
        
        // restrict the high score list to its maximum list
        if (_highScores.Count > _maxScoreCount)
        {
            _highScores.RemoveAt(_highScores.Count - 1);
        }

        // update the list of score graves to hold the new messages and scores
        for(int i = 0; i < _highScores.Count; i++)
        {
            if (_scoreEpitaphs[i] != null)
            {
                CreateScoreEpitaph(_scoreEpitaphs[i], _highScores[i]);
            }
        }

        ServiceLocator.Instance.Get<SaveDataManager>().SetHighScores(_highScores);
    }

    void CreateScoreEpitaph(DialogSequence dialogObject, Score scoreObject)
    {
        DialogLine nameLine = new DialogLine();
        nameLine.SetContent("Here lies " + scoreObject.holder);

        DialogLine scoreLine = new DialogLine();
        scoreLine.SetContent(scoreObject.value + " points - " + scoreObject.message);

        List<DialogLine> epitaphLines = new List<DialogLine>();
        epitaphLines.Add(nameLine);
        epitaphLines.Add(scoreLine);

        dialogObject.SetLines(epitaphLines);
    }

    string GetScoreMessage(int score)
    {
        string scoreMessage = "";

        foreach(int messageKey in _scoreMessageMap.Keys)
        {
            if (score >= messageKey)
            {
                scoreMessage = _scoreMessageMap[messageKey];
            }
        }

        return scoreMessage;
    }

    public int CompareScoreValues(Score a, Score b)
    {
        if (a.value == b.value) return 0;
        return a.value > b.value ? -1 : 1;
    }
}