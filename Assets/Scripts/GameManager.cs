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

    private static string PathSaveFile;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PathSaveFile = $"{Application.persistentDataPath}/savefile.json";
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
        if (File.Exists(PathSaveFile))
        {
            File.Delete(PathSaveFile);
        }
        File.WriteAllText(PathSaveFile, json);
    }

    private static void LoadScores()
    {
        if (File.Exists(PathSaveFile))
        {
            string json = File.ReadAllText(PathSaveFile);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            AllScores.AddRange(data.AllScores);
        }
    }
}
