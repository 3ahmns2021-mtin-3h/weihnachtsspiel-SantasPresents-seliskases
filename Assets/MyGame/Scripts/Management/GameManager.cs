using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Singletons")]
    public GameObject weihnachtsmannPrefab;
    public Canvas canvasObject;
    [Space]
    public Transform spawnPosition;
    public Sack sackScript;

    private void Awake()
    {
        SetWeihnachtsmann(Instantiate(weihnachtsmannPrefab, spawnPosition.position, Quaternion.identity));
        SetCanvas();
        SetSack();

        weihnachtsmann.transform.SetParent(canvas.transform);
    }

    public void EndGame()
    {
        int playerScore = sack.presents;
        int tempScore;

        string playerName = PlayerPrefs.GetString("playerName");
        string tempName;

        string highScore = "highScore";
        string highScoreName = "highScoreName";
        

        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt(highScore + i) < playerScore)
            {
                tempScore = PlayerPrefs.GetInt(highScore + i);
                PlayerPrefs.SetInt(highScore + i, playerScore);

                tempName = PlayerPrefs.GetString(highScoreName + i);
                PlayerPrefs.SetString(highScoreName + i, playerName);

                if (i < 3)
                {
                    int j = i + 1;

                    playerScore = PlayerPrefs.GetInt(highScore + j);
                    PlayerPrefs.SetInt(highScore + j, tempScore);

                    playerName = PlayerPrefs.GetString(highScoreName + j);
                    PlayerPrefs.SetString(highScoreName + j, tempName);
                }
            }
        }

        RestartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void SetWeihnachtsmann(GameObject instantiatedObject)
    {
        weihnachtsmann = instantiatedObject;
    }
    public static GameObject weihnachtsmann;

    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;

    private void SetSack()
    {
        sack = sackScript;
    }
    public static Sack sack;
}
