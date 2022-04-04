using UnityEngine;
using System.Collections;

[System.Serializable]
public class Highscore
{
    private int Score;
    private string Name;

    public Highscore(string Name, int Score)
    {
        this.Name = Name;
        this.Score = Score;
    }

    public int GetScore()
    {
        return this.Score;
    }

    public string GetName()
    {
        return this.Name;
    }
}
