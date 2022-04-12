using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIMenuHandler : MonoBehaviour
{
    private string PlayerName;
    public GameObject StartButton;
    public GameObject EnterNameField;
    public GameObject PlayerControlField;

    public void Start()
    {
        StartButton.GetComponent<Button>().interactable = !string.IsNullOrEmpty(PlayerName);
        PlayerControlField.GetComponent<Dropdown>().value = GameManager.ControlInput == "Keys" ? 0 : 1;
        PlayerControlField.GetComponent<Dropdown>().onValueChanged.AddListener(
            delegate
        {
            DropdownValueChanged(PlayerControlField.GetComponent<Dropdown>());
        });
    }

    private void DropdownValueChanged(Dropdown change)
    {
        switch(change.value)
        {
            case 0:
                GameManager.ControlInput = "Keys";
                break;
            case 1:
                GameManager.ControlInput = "Mouse";
                break;
            default:
                break;
        }
        GameManager.UpdateSettings();
    }

    public void OnPlayerNameEdited()
    {
        PlayerName = EnterNameField.GetComponent<TMP_InputField>().text;
        
        if (StartButton != null)
        {
            StartButton.GetComponent<Button>().interactable = !string.IsNullOrEmpty(PlayerName);
        }
    }

    public void StartNew()
    {
        GameManager.CurrentPlayer = PlayerName;
        SceneManager.LoadScene(2);
    }

    public void OpenHighScores()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
