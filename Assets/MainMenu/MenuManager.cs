using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public TMP_InputField field;
    //public TextMeshProUGUI nameTextFromField;
    public TextMeshProUGUI bestScoreText;

    private void Start()
    {
        DataManager.dM.LoadBestScore();
        if(DataManager.dM.playerName=="" && DataManager.dM.playerScore == 0)
        {
            bestScoreText.text = "No High Score";
        }
        else
        {
            bestScoreText.text = $"{DataManager.dM.playerName} : {DataManager.dM.playerScore}";
        }
    }
    public void StartGame()
    {
        if (field.text=="" || field.text==null)
        {
            DataManager.dM.currentName = "Unknown";
        }
        else
        {
            DataManager.dM.currentName = field.text;
        }
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else 
 Application.Quit();
#endif
    }

    public void Reset()
    {
        DataManager.dM.playerName = null;
        DataManager.dM.playerScore = 0;
        DataManager.dM.SaveBestScore();
        DataManager.dM.LoadBestScore();
        if (DataManager.dM.playerName == "" && DataManager.dM.playerScore == 0)
        {
            bestScoreText.text = "No High Score";
        }
        else
        {
            bestScoreText.text = $"{DataManager.dM.playerName} : {DataManager.dM.playerScore}";
        }
    }
}
