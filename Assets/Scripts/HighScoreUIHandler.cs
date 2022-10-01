using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScoreUIHandler : MonoBehaviour
{
    public TMP_Text highScoreTabelText;

    // Start is called before the first frame update
    void Start()
    {
        string highScoreTableString = "";

        foreach(GameManager.HighScoreEntry highScoreEntry in GameManager.Instance.TopHighScores)
        {
            highScoreTableString += highScoreEntry.HighScoreName + " : " + highScoreEntry.HighScore.ToString() + "\r\n";
        }

        highScoreTabelText.text = highScoreTableString;
    }

    public void GoBackToMenu()       
    {
        SceneManager.LoadScene(0);
    }
}
