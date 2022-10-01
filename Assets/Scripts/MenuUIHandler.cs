using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameManager.Instance;

        if (nameField != null)
        {
            nameField.text = gameManager.CurrentName;
        }

        if (highScoreText != null)
        {
            highScoreText.text = gameManager.GetHighScoreString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeName(string newName)
    {
        Debug.Log("Changing namer to " + newName);

        GameManager.Instance.CurrentName = newName;

        if (GameManager.Instance.HighScore == 0)
        {
            GameManager.Instance.HighScoreName = newName;
        }
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHighScores()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        GameManager.Instance.SaveGameData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
