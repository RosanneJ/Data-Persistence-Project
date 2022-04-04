using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresManager : MonoBehaviour
{
    public Text HighScores;

    void Start()
    {
        List<Highscore> allScores = GameManager.AllScores;
        if (allScores.Count > 0)
        {
            foreach (Highscore item in allScores)
            {
                HighScores.text += $"\n {allScores.IndexOf(item) + 1}. {item.GetName()} - {item.GetScore()}";
            }
        }
    }

}
