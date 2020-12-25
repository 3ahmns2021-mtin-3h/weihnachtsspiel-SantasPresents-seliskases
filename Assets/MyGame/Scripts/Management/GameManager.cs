using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        string highScorePos = "highScorePos";
        string highScoreName = "highScoreName";
        

        for (int i = 1; i <= 5; i++)
        {
            if (PlayerPrefs.GetInt(highScorePos + i) < playerScore)
            {
                tempScore = PlayerPrefs.GetInt(highScorePos + i);
                PlayerPrefs.SetInt(highScorePos + i, playerScore);

                tempName = PlayerPrefs.GetString(highScoreName + i);
                PlayerPrefs.SetString(highScoreName + i, playerName);

                if (i < 5)
                {
                    int j = i + 1;

                    playerScore = PlayerPrefs.GetInt(highScorePos + j);
                    PlayerPrefs.SetInt(highScorePos + j, tempScore);

                    playerName = PlayerPrefs.GetString(highScoreName + j);
                    PlayerPrefs.SetString(highScoreName + j, tempName);
                }
            }
        }
    }

    #region Weihnachtsmann
    private void SetWeihnachtsmann(GameObject instaniatedObject)
    {
        weihnachtsmann = instaniatedObject;
    }
    public static GameObject weihnachtsmann;
    #endregion

    #region Canvas
    private void SetCanvas()
    {
        canvas = canvasObject;
    }
    public static Canvas canvas;
    #endregion

    #region Sack
    private void SetSack()
    {
        sack = sackScript;
    }
    public static Sack sack;
    #endregion
}
