using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static List<Highscore> AllScores;
    public static string CurrentPlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        AllScores = new List<Highscore>();
        LoadScores();
    }

    [System.Serializable]
    class SaveData
    {
        public List<Highscore> AllScores;
    }

    public static void UpdateScores(int points)
    {
        AllScores.Add(new Highscore(CurrentPlayer, points));
        AllScores.Sort((y, x) => x.GetScore().CompareTo(y.GetScore()));
        SaveScores();
    }

    private static void SaveScores()
    {
        SaveData data = new SaveData();
        data.AllScores = AllScores;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private static void LoadScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            AllScores.AddRange(data.AllScores);
        }
    }
}
