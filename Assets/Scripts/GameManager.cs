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
    public static string ControlInput;

    private static string PathSaveHighscoresFile;
    private static string PathSaveSettingsFile;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        PathSaveHighscoresFile = $"{Application.persistentDataPath}/savedhighscores.json";
        PathSaveSettingsFile = $"{Application.persistentDataPath}/savedsettings.json";
        AllScores = new List<Highscore>();
        LoadScores();
        LoadSettings();
    }

    [System.Serializable]
    class SaveHighscoreData
    {
        public List<Highscore> AllScores;
    }

    [System.Serializable]
    class SaveSettingsData
    {
        public string PlayerControlInput;
    }

    public static void UpdateScores(int points)
    {
        AllScores.Add(new Highscore(CurrentPlayer, points));
        AllScores.Sort((y, x) => x.GetScore().CompareTo(y.GetScore()));
        AllScores.RemoveAll((score) => string.IsNullOrEmpty(score.GetName()));
        SaveScores();
    }

    public static void UpdateSettings()
    {
        SaveSettingsData data = new SaveSettingsData();
        data.PlayerControlInput = ControlInput;
        string json = JsonUtility.ToJson(data);
        if (File.Exists(PathSaveSettingsFile))
        {
            File.Delete(PathSaveSettingsFile);
        }
        File.WriteAllText(PathSaveSettingsFile, json);
    }


    private static void SaveScores()
    {
        SaveHighscoreData data = new SaveHighscoreData();
        data.AllScores = AllScores;
        string json = JsonUtility.ToJson(data);
        if (File.Exists(PathSaveHighscoresFile))
        {
            File.Delete(PathSaveHighscoresFile);
        }
        File.WriteAllText(PathSaveHighscoresFile, json);
    }

    private static void LoadScores()
    {
        if (File.Exists(PathSaveHighscoresFile))
        {
            string json = File.ReadAllText(PathSaveHighscoresFile);
            SaveHighscoreData data = JsonUtility.FromJson<SaveHighscoreData>(json);
            AllScores.AddRange(data.AllScores);
        }
    }

    private static void LoadSettings()
    {
        if (File.Exists(PathSaveSettingsFile))
        {
            string json = File.ReadAllText(PathSaveSettingsFile);
            SaveSettingsData data = JsonUtility.FromJson<SaveSettingsData>(json);
            ControlInput = data.PlayerControlInput;
        }
    }

}
